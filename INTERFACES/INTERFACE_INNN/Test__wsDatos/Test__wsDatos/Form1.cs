using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Test__wsDatos.wsIN3Sigma; 

namespace Test__wsDatos
{
    public partial class Form1 : Form
    {
        //clsListView lst;
        wsIN3Sigma.wsSigma datosSigma;
        string sDatosRecibidos = "";

        public Form1()
        {
            InitializeComponent();

            radioButton1.Checked = true;

            lblCompilado.Text = string.Format("Compilación :  {0}", Application.ProductVersion);

            ////lst = new clsListView(listViewDatos);
            ////lst.Limpiar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ////lst.Limpiar();
            txtDatosRecibidos.Text = "";
        }

        private void wsSigma_obtenerMedicamentos_porPacienteCompleted(object sender, obtenerMedicamentos_porPacienteCompletedEventArgs e)
        {
            //sDatosRecibidos = e.Result;

            //txtDatosRecibidos.Text = sDatosRecibidos;

            //MessageBox.Show("Proceso terminado con correctamente.");
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            bool bRegresa = false;
            DateTime dtFechaInicial = new DateTime(dtpFechaInicial.Value.Year, dtpFechaInicial.Value.Month, dtpFechaInicial.Value.Day, 0, 0, 0);
            DateTime dtFechaFinal = new DateTime(dtpFechaFinal.Value.Year, dtpFechaFinal.Value.Month, dtpFechaFinal.Value.Day, 0, 0, 0); 

            sDatosRecibidos = "";
            txtDatosRecibidos.Text = "";

            ////string sFechaInicial = string.Format("{0:00}/{1:00}/{2:0000}", dtpFechaInicial.Value.Month, dtpFechaInicial.Value.Day, dtpFechaInicial.Value.Year);
            ////string sFechaFinal = string.Format("{0:00}/{1:00}/{2:0000}", dtpFechaFinal.Value.Month, dtpFechaFinal.Value.Day, dtpFechaFinal.Value.Year);

            ////if (radioButton1.Checked)
            ////{
            ////    sFechaInicial = string.Format("{2:0000}/{0:00}/{1:00}", dtpFechaInicial.Value.Month, dtpFechaInicial.Value.Day, dtpFechaInicial.Value.Year);
            ////    sFechaFinal = string.Format("{2:0000}/{0:00}/{1:00}", dtpFechaFinal.Value.Month, dtpFechaFinal.Value.Day, dtpFechaFinal.Value.Year);

            ////}


            try
            {
                wsSigma.Url = txtUrl.Text.Trim();

                //sDatosRecibidos = wsSigma.obtenerMedicamentos_porPaciente(dtpFechaInicial.Value, dtpFechaFinal.Value);

                //MessageBox.Show(wsSigma.obtenerMedicamentos_porPaciente(dtFechaInicial, dtFechaFinal), "Respuesta");

                sDatosRecibidos = wsSigma.obtenerMedicamentos_porPaciente(dtpFechaInicial.Value, dtpFechaInicial.Value);
                txtDatosRecibidos.Text = sDatosRecibidos;
                MessageBox.Show(sDatosRecibidos);

                //sDatosRecibidos = 
                    //wsSigma.obtenerMedicamentos_porPacienteAsync(dtpFechaInicial.Value, dtpFechaFinal.Value, bRegresa);
                //MessageBox.Show("Proceso terminado con correctamente.");
            }
            catch (Exception ex)
            {
                sDatosRecibidos = ex.Message;
                txtDatosRecibidos.Text = sDatosRecibidos;
                MessageBox.Show("Proceso terminado con errores.");
            }

            //txtDatosRecibidos.Text = sDatosRecibidos; 

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaInicial.CustomFormat = "yyyy/MM/dd";
            dtpFechaFinal.CustomFormat = dtpFechaInicial.CustomFormat;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaInicial.CustomFormat = "MM/dd/yyyy";
            dtpFechaFinal.CustomFormat = dtpFechaInicial.CustomFormat;
        }
    }
}
