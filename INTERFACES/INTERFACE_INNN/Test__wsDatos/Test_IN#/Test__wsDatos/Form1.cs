using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; 

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
            sDatosRecibidos = "";
            txtDatosRecibidos.Text = ""; 

            try
            {
                wsSigma.Url = txtUrl.Text.Trim();

                //sDatosRecibidos = wsSigma.obtenerMedicamentos_porPaciente(dtpFechaInicial.Value, dtpFechaFinal.Value);

                sDatosRecibidos = wsSigma.obtenerMedicamentos_porPaciente(dtpFechaInicial.Value, dtpFechaFinal.Value); 
                //MessageBox.Show(wsSigma.obtenerMedicamentos_porPaciente(dtpFechaInicial.Value, dtpFechaFinal.Value), "Respuesta");
                txtDatosRecibidos.Text = sDatosRecibidos; 
                MessageBox.Show("Proceso terminado correctamente", "Respuesta"); 
                //sDatosRecibidos = 
                    //wsSigma.obtenerMedicamentos_porPacienteAsync(dtpFechaInicial.Value, dtpFechaFinal.Value, bRegresa);
                //MessageBox.Show("Proceso terminado con correctamente.");
            }
            catch (Exception ex)
            {
                sDatosRecibidos = ex.Message;
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

        private void btnDescargarXML_Click(object sender, EventArgs e)
        {
            //if (txtDatosRecibidos.Text.Trim() == "")
            //{
            //    MessageBox.Show("No existe información para descargar", "Guardar resultado");
            //}
            //else
            {
                try
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Title = "Descargar XML";
                    save.Filter = "*.xml|*.xml";

                    if (save.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                    {
                        using (StreamWriter w = new StreamWriter(save.FileName))
                        {
                            w.Write(sDatosRecibidos);
                            w.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Descargar XML", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
        }
    }
}
