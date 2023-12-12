using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO; 
using System.IO.Ports;

using SC_SolutionsSystem; 

namespace Dll_IMach4
{
    public partial class FrmChat : FrmBase 
    {

/* 
                bitRates[ 0 ] = 300; 
                bitRates[ 1 ] = 600; 
                bitRates[ 2 ] = 1200; 
                bitRates[ 3 ] = 2400; 
                bitRates[ 4 ] = 9600; 
                bitRates[ 5 ] = 14400; 
                bitRates[ 6 ] = 19200; 
                bitRates[ 7 ] = 38400; 
                bitRates[ 8 ] = 57600; 
                bitRates[ 9 ] = 115200; 
                bitRates[ 10 ] = 128000;  
 */

        public FrmChat()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; 
        }


        private void FrmChat_Load(object sender, EventArgs e)
        {
            cboPorts.Clear();
            cboPorts.Add();

            foreach (string port in SerialPort.GetPortNames())
            {
                cboPorts.Add(port, port); 
            }

            cboPorts.SelectedIndex = 0; 
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort.IsOpen)
                    serialPort.Close();

                serialPort = new SerialPort(cboPorts.Data, 19200);
                serialPort.Open();

                if (serialPort.IsOpen)
                {
                    txtRecibir.Focus();
                    lblStatus.Text = "PUERTO " + cboPorts.Data + " ABIERTO CORRECTAMENTE"; 
                }
            }
            catch ( Exception ex )
            {
                General.msjError(ex.Message.ToString()); 
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                if ( ! serialPort.IsOpen)
                    lblStatus.Text = "PUERTO " + cboPorts.Data + " CERRADO CORRECTAMENTE"; 
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {

        }

    }
}
