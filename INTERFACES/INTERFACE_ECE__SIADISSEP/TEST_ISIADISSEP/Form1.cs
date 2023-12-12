using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading; 

using TEST_ISIADISSEP.wsInterface__ISIASSEP; 

namespace TEST_ISIADISSEP
{
    public partial class Form1 : Form
    {
        ws_Cnn_ISIADISSEP web; 

        public Form1()
        {
            InitializeComponent();

            web = new ws_Cnn_ISIADISSEP();

            lblURL.Text = "";
            txtURL_Envio.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnEnviarReceta_Click(object sender, EventArgs e)
        {
            txtRespuesta.Text = "";
            Application.DoEvents();
            Thread.Sleep(500);

            web.Url = txtURL_Envio.Text;

            try
            {
                txtRespuesta.Text = web.RecepcionDeRecetaElectronica(txtXML.Text.Trim());

            }
            catch (Exception ex)
            {
                txtRespuesta.Text = ex.Message;
            }
        }

        private void txtXML_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblURL_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtRespuesta.Text = "";
            Application.DoEvents();
            Thread.Sleep(500);

            web.Url = txtURL_Envio.Text; 

            try 
            {
                txtRespuesta.Text = web.AcuseSurtidoDeRecetaElectronica(txtXML.Text.Trim(), "");
            }
            catch (Exception ex)
            {
                txtRespuesta.Text = ex.Message;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtRespuesta.Text = "";
            Application.DoEvents();
            Thread.Sleep(500);

            web.Url = txtURL_Envio.Text; 
            try 
            {
                txtRespuesta.Text = web.CancelacionDeRecetaElectronica(txtXML.Text.Trim());
            }
            catch (Exception ex)
            {
                txtRespuesta.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtRespuesta.Text = "";
            Application.DoEvents();
            Thread.Sleep(500);

            web.Url = txtURL_Envio.Text; 

            try 
            {
                txtRespuesta.Text = web.AcuseDeCancelacionDeRecetaElectronica(txtXML.Text.Trim(), "");
            }
            catch (Exception ex)
            {
                txtRespuesta.Text = ex.Message;
            }

        }
    }
}
