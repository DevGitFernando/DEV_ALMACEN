using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using Test.wsInformacion; 


namespace Test
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            dgResultados.DataSource = null;
            dgResultados.Rows.Clear(); 
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar(); 
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {

            DataSet dts = new DataSet();
            DataTable dtTable = new DataTable();

            int iIdAcceso = Convert.ToInt32(txtIdAcceso.Text);
            string sFechaInicial = "";
            string sFechaFinal = "";

            //// Dirección web donde se aloja el servicio web.  
            string sUrl = "http://intermedpuebla.dyndns-ip.com/wsInformacion/wsInformacion.asmx"; 


            wsInformacion.wsInformacion inf = new Test.wsInformacion.wsInformacion();
            inf.Url = sUrl; 


            try
            {
                Limpiar(); 
                if (rdoClaves.Checked)
                {
                    // Ejecutar consulta  
                    dts = inf.ListaDeClavesLicitadas(iIdAcceso);
                    dtTable = dts.Tables[0];
                }
                else
                {
                    sFechaInicial = dtpFechaInicial.Value.Year.ToString("###0");
                    sFechaInicial += "-";
                    sFechaInicial += dtpFechaInicial.Value.Month.ToString("0#");
                    sFechaInicial += "-";
                    sFechaInicial += dtpFechaInicial.Value.Day.ToString("0#");


                    sFechaFinal = dtpFechaFinal.Value.Year.ToString("####");
                    sFechaFinal += "-";
                    sFechaFinal += dtpFechaFinal.Value.Month.ToString("0#");
                    sFechaFinal += "-";
                    sFechaFinal += dtpFechaFinal.Value.Day.ToString("0#");


                    // Ejecutar consulta  
                    dts = inf.Consumos(iIdAcceso, sFechaInicial, sFechaFinal);
                    dtTable = dts.Tables[0];
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error");
            }

            try
            {
                dgResultados.DataSource = dtTable; 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al mostrar los datos");
            }

        }
    }
}
