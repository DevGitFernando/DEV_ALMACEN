using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Globalization;
using System.Threading;

using Microsoft.VisualBasic;

namespace Dll_HitachiEncoder
{
    public partial class FrmTest_Conexion : Form
    {
        private RS232 _rs232 ; // = new RS232();
        private int xScale = 830; // pixel / Char * 100
        private int charsInline = 80; // def n start
        private bool isConnected;
        private int RXcount;
        private int TXcount;
        private string[] comparams; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private byte[] buffer;

        public FrmTest_Conexion()
        {
            InitializeComponent();

            var context = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(context);
            CheckForIllegalCrossThreadCalls = false; 

            _rs232 = new RS232();
            comparams = RS232.DefaultParams;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //////this.Text += string.Format(" Version {0}.{1:0000}", (new Microsoft.VisualBasic.ApplicationServices.ConsoleApplicationBase()).Info.Version.Major, 
            //////    (new Microsoft.VisualBasic.ApplicationServices.ConsoleApplicationBase()).Info.Version.Minor);

            this.status0.Text = "Terminal connect: none";
            setRuler(rtbRX, false);
            setRuler(rtbTX, false);

            // read available ports on system
            string[] Portnames = SerialPort.GetPortNames();

            if (Portnames.Length > 0)
            {
                this.cboComPort.Text = Portnames[0];
            }
            else
            {
                this.status0.Text = "no ports detected";
                return;
            }

            this.cboComPort.Items.Clear();
            this.cboComPort.Items.AddRange(Portnames);

            _rs232.connection += new RS232.connectionEventHandler(connection);
            _rs232.Datareceived += new RS232.DatareceivedEventHandler(doUpdate);
            _rs232.sendOK += new RS232.sendOKEventHandler(sendata);
            _rs232.recOK += new RS232.recOKEventHandler(rdata);
            _rs232.errormsg += new RS232.errormsgEventHandler(getmessage);
        }

        #region form events
        //
        // copy, cut paste txbox
        private void CopyTx_Click(System.Object sender, System.EventArgs e)
        {
            this.rtbTX.Copy();
        }
        private void PasteTx_Click(System.Object sender, System.EventArgs e)
        {
            this.rtbTX.Paste();
        }
        private void CutTx_Click(System.Object sender, System.EventArgs e)
        {
            this.rtbTX.Cut();
        }

        /// <summary>
        /// send selected text
        /// </summary>
        private void SendTx_Click(System.Object sender, System.EventArgs e)
        {
            this.cboEnterMessage.Text = this.rtbTX.SelectedText;
            sendToCom();
        }

        /// <summary>
        /// send position of caret line from tx box
        /// </summary>
        private void SendLine_Click(System.Object sender, System.EventArgs e)
        {
            int loc = this.rtbTX.GetFirstCharIndexOfCurrentLine();
            int ln = this.rtbTX.GetLineFromCharIndex(loc);
            if (ln > 0)
            {
                this.cboEnterMessage.Text = this.rtbTX.Lines[ln];
                sendToCom();
            }
        }

        /// <summary>
        /// send only selection in tx box to com
        /// </summary>
        private void SendSelect_Click(System.Object sender, System.EventArgs e)
        {
            if (this.rtbTX.SelectionLength > 0)
            {
                this.cboEnterMessage.Text = this.rtbTX.SelectedText;
                sendToCom();
            }
        }

        /// <summary>
        /// fetch on line from rtbTX to cboEnterMessage box
        /// </summary>
        private void rtbTX_MouseClick(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            RichTextBox rb = (RichTextBox)sender;
            int loc = rb.GetFirstCharIndexOfCurrentLine();
            int ln = rb.GetLineFromCharIndex(loc);
            this.cboEnterMessage.Text = rb.Lines[ln];
        }

        /// <summary>
        /// clear rx box
        /// </summary>
        private void btnClearReceiveBoxr_Click(System.Object sender, System.EventArgs e)
        {
            this.rtbRX.Clear();
            this.rtbRX.Text = "1";
            this.charsInline = setRuler(rtbRX, System.Convert.ToBoolean(this.chkRxShowHex.Checked));
            this.RXcount = 0;
            this.lblRxCnt.Text = string.Format("count: {0:D6}", this.RXcount);
            this.statusRX.Image = this.isConnected ? Properties.Resources.ledCornerOrange : Properties.Resources.ledCornerGray;
        }

        /// <summary>
        /// clear tx box
        /// </summary>
        private void btnClearTxBox_Click(System.Object sender, System.EventArgs e)
        {
            this.rtbTX.Clear();
            this.rtbTX.Text = "1";
            this.charsInline = setRuler(rtbTX, System.Convert.ToBoolean(chkTxShowHex.Checked));
            this.TXcount = 0;
            this.lblTxCnt.Text = string.Format("count: {0:D3}", this.TXcount);
            this.statusTX.Image = this.isConnected ? Properties.Resources.ledCornerOrange : Properties.Resources.ledCornerGray;
        }

        /// <summary>
        /// button connect
        /// </summary>
        private void btnConnect_Click(System.Object sender, System.EventArgs e)
        {
            if (((ToolStripButton)sender).Text == "*connect*")
            {

                this.comparams[(int)RS232.cP.cPort] = System.Convert.ToString(cboComPort.Text);
                this.comparams[(int)RS232.cP.cBaud] = System.Convert.ToString(cboBaudrate.Text);
                this.comparams[(int)RS232.cP.cData] = System.Convert.ToString(cboDataBits.Text);
                this.comparams[(int)RS232.cP.cParity] = System.Convert.ToString(cboParity.Text);
                this.comparams[(int)RS232.cP.cStop] = System.Convert.ToString(cboStopbits.Text);
                this.comparams[(int)RS232.cP.cDelay] = System.Convert.ToString(cboDelay.Text);
                this.comparams[(int)RS232.cP.cThreshold] = System.Convert.ToString(cboThreshold.Text);
                _rs232.Connect(comparams);
            }
            else
            {
                _rs232.Disconnect();
            }
        }

        /// <summary>
        /// used in cboEnterMessage.TextUpdate
        /// </summary>
        private int count;

        /// <summary>
        /// build hex string in textbox
        /// </summary>
        private void cboEnterMessage_TextUpdate(System.Object sender,
            System.EventArgs e)
        {

            if (!this.chkTxEnterHex.Checked)
            {
                return;
            }
            ToolStripComboBox cb = (ToolStripComboBox)sender;
            string s = cb.Text;
            count++;
            if (count == 2)
            {
                cb.Text += " ";
                count = 0;
            }
            cb.SelectionStart = cb.Text.Length;

        }

        /// <summary>
        /// enter only allowed keys in hex mode
        /// </summary>
        private void cboEnterMessage_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            string allowedChars = "0123456789ABCDEFabcdef";
            bool found = default(bool);

            if (e.KeyChar.ToString() != Constants.vbCr)
            {
                if (chkTxEnterHex.Checked)
                {
                    for (int i = 1; i <= allowedChars.Length; i++)
                    {
                        if (e.KeyChar == Strings.GetChar(allowedChars, i))
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        e.Handled = true;
                    }
                }
            }
            else
            {
                count = 0;
                sendToCom();
            }

        }

        /// <summary>
        /// send data to com port
        /// </summary>
        private void sendToCom() //Handles cbxsendToCom.KeyDown
        {

            byte[] data = null;

            ToolStripComboBox with_1 = this.cboEnterMessage;
            if (with_1.Text.Length > 0)
            {

                if (!this.chkTxEnterHex.Checked)
                {
                    if (this.chkAddCR.Checked)
                    {
                        with_1.Text += ControlChars.Cr;
                    }
                    if (this.chkAddLF.Checked)
                    {
                        with_1.Text += ControlChars.Lf;
                    }
                    data = Encoding.ASCII.GetBytes(with_1.Text);
                }
                else
                {
                    data = reconvert(System.Convert.ToString(with_1.Text));
                }

                int iBuffer = 0;
                iBuffer = 464;
                byte[] buffer = GenerarBuffer(iBuffer);
                byte[] buffer_Aux = GenerarBuffer(iBuffer);
                byte[] buffer_Datos = System.Text.Encoding.UTF8.GetBytes(with_1.Text);
                int i = 0;

                buffer[i] = Convert.ToByte((int)NEMONICOS.STX);
                ////i++; 
                foreach (byte by in buffer_Datos)
                {
                    buffer[i] = by;
                    i++;
                }
                ////i++;
                buffer[i] = Convert.ToByte((int)NEMONICOS.ETX);

                //// Pasar a buffer limpio 
                Array.Copy(buffer, 0, data, 0, i); 


                /////////send data:
                ////_rs232.SendData(data);
                _rs232.SendData(data); 

                //tx counter:
                this.TXcount += data.Length;
                this.lblTxCnt.Text = string.Format("{0:D6}", TXcount);
                this.statusTX.Image = Properties.Resources.ledCornerGray;

                // display in box:
                if (chkTxShowHex.Checked && !chkTxShowAscii.Checked)
                {
                    appendBytes(this.rtbTX, data, this.charsInline, false);
                }
                else if (chkTxShowHex.Checked && chkTxShowAscii.Checked)
                {
                    appendBytes(this.rtbTX, data, this.charsInline, true);
                }
                else if (!chkTxShowHex.Checked && chkTxShowAscii.Checked)
                {
                    this.rtbTX.ScrollToCaret();
                    this.rtbTX.AppendText(with_1.Text + Constants.vbCr);
                }

                //remember data in cbx
                this.cboEnterMessage.Items.Add(with_1.Text);
                with_1.Text = string.Empty;

            }

        }

        /// <summary>
        /// view hex in rx box
        /// </summary>
        private void RxShowHex_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            this.charsInline = setRuler(this.rtbRX, System.Convert.ToBoolean(this.chkRxShowHex.Checked));
        }

        /// <summary>
        /// view hex in tx box
        /// </summary>
        private void chkTxShowHex_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            this.charsInline = setRuler(this.rtbTX, System.Convert.ToBoolean(this.chkTxShowHex.Checked));
        }

        /// <summary>
        /// save rx box to file
        /// </summary>
        private void btnSaveFileFromRxBox_Click(System.Object sender, System.EventArgs e)
        {

            SaveFileDialog1.DefaultExt = "*.TXT";
            SaveFileDialog1.Filter = "txt Files | *.TXT";
            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string fullpath = System.Convert.ToString(SaveFileDialog1.FileName);
                this.rtbRX.SaveFile(fullpath, RichTextBoxStreamType.PlainText);
                MessageBox.Show(Path.GetFileName(fullpath) + " written");

            }
            else
            {
                MessageBox.Show("no data choosen");
            }
        }

        /// <summary>
        /// load file into tx box
        /// </summary>
        private void TxbtnFile_Click(System.Object sender, System.EventArgs e)
        {

            OpenFileDialog1.DefaultExt = "*.TXT";
            OpenFileDialog1.Filter = "txt Files | *.TXT";
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string fullpath = System.Convert.ToString(OpenFileDialog1.FileName);
                this.rtbTX.Clear();
                this.rtbTX.LoadFile(fullpath, RichTextBoxStreamType.PlainText);

            }
            else
            {
                MessageBox.Show("no data choosen");
            }
        }

        /// <summary>
        /// load config
        /// </summary>
        private void LoadC(System.Object sender, System.EventArgs e)
        {
            string fname = "comterm.ini";
            try
            {
                StreamReader sr = new StreamReader(fname);
                this.cboComPort.Text = sr.ReadLine();
                this.cboBaudrate.Text = sr.ReadLine();
                this.cboDataBits.Text = sr.ReadLine();
                this.cboParity.Text = sr.ReadLine();
                this.cboStopbits.Text = sr.ReadLine();
                this.cboThreshold.Text = sr.ReadLine();
                this.cboDelay.Text = sr.ReadLine();
                sr.Close();
                if (sender != null)
                {
                    MessageBox.Show(fname + " read");
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(fname + " error: " + ex.Message);
            }

        }

        /// <summary>
        /// save comparms
        /// </summary>
        private void SaveConfig_Click(System.Object sender, System.EventArgs e)
        {
            string fname = "comterm.ini";
            StreamWriter sw = new StreamWriter(fname);
            sw.WriteLine(this.cboComPort.Text);
            sw.WriteLine(this.cboBaudrate.Text);
            sw.WriteLine(this.cboDataBits.Text);
            sw.WriteLine(this.cboParity.Text);
            sw.WriteLine(this.cboStopbits.Text);
            sw.WriteLine(this.cboThreshold.Text);
            sw.WriteLine(this.cboDelay.Text);
            sw.Close();
            MessageBox.Show(fname + " written");
        }

        /// <summary>
        ///  exit
        /// </summary>
        private void ExitTool_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        ///  form resize
        /// </summary>
        private void frmMain_ResizeEnd(System.Object sender, System.EventArgs e)
        {
            this.charsInline = setRuler(rtbRX, System.Convert.ToBoolean(chkRxShowHex.Checked));
            this.charsInline = setRuler(rtbTX, System.Convert.ToBoolean(chkTxShowHex.Checked));
        }

        #endregion
        #region Com Port class events

        /// <summary>
        ///  update boxes
        /// </summary>
        /// <param name="buffer">received bytes from class RS232</param>
        /// <remarks></remarks>
        private void doUpdate(byte[] buffer)
        {

            this.RXcount += buffer.Length;
            this.lblRxCnt.Text = string.Format("count: {0:D3}", RXcount);

            if (this.chkRxShowHex.Checked && !this.chkRxShowAscii.Checked)
            {
                appendBytes(this.rtbRX, buffer, this.charsInline, false);

            }
            else if (this.chkRxShowHex.Checked && this.chkRxShowAscii.Checked)
            {
                appendBytes(this.rtbRX, buffer, this.charsInline, true);

            }
            else if (!this.chkRxShowHex.Checked && this.chkRxShowAscii.Checked)
            {
                string s = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
                this.rtbRX.ScrollToCaret();
                //TODO
                this.rtbRX.AppendText(s); // & vbCr)

            }
        }

        /// <summary>
        /// senda data OK NOK
        /// </summary>
        private void sendata(bool sendStatus)
        {
            if (sendStatus)
            {
                this.statusTX.Image = Properties.Resources.ledCornerGreen;
            }
            else
            {
                this.statusTX.Image = Properties.Resources.ledCornerRed;
            }
        }

        /// <summary>
        /// receive successfull
        /// </summary>
        private void rdata(bool receiveStatus)
        {
            if (receiveStatus)
            {
                this.statusRX.Image = Properties.Resources.ledCornerGreen;
            }
            else
            {
                this.statusRX.Image = Properties.Resources.ledCornerRed;
            }
        }

        /// <summary>
        ///  connection status
        /// </summary>
        private void connection(bool status)
        {
            if (status)
            {
                this.cboParity.Enabled = false;
                this.cboStopbits.Enabled = false;
                this.cboComPort.Enabled = false;
                this.cboBaudrate.Enabled = false;
                this.cboDataBits.Enabled = false;
                this.cboDelay.Enabled = false;
                this.cboThreshold.Enabled = false;
                this.btnConnect.Text = "disconnect";
                this.statusC.Image = Properties.Resources.ledCornerGreen;
                this.statusRX.Image = Properties.Resources.ledCornerOrange;
                this.statusTX.Image = Properties.Resources.ledCornerOrange;
                this.sLabel(comparams);
                this.isConnected = true;
            }
            else
            {
                this.cboParity.Enabled = true;
                this.cboStopbits.Enabled = true;
                this.cboComPort.Enabled = true;
                this.cboBaudrate.Enabled = true;
                this.cboDataBits.Enabled = true;
                this.cboDelay.Enabled = true;
                this.cboThreshold.Enabled = true;
                this.btnConnect.Text = "*connect*";
                this.statusC.Image = Properties.Resources.ledCornerRed;
                this.statusRX.Image = Properties.Resources.ledCornerGray;
                this.statusTX.Image = Properties.Resources.ledCornerGray;
                this.status0.Text = " Terminal connect: none";
                this.isConnected = false;
            }
        }

        /// <summary>
        /// exception message
        /// </summary>
        private void getmessage(string msg)
        {
            //Me.status0.Text = msg
            MessageBox.Show(msg);
        }
        #endregion

        #region utilities

        /// <summary>
        /// set ruler in box
        /// </summary>
        /// <returns>length of ruler</returns>
        private int setRuler(RichTextBox rb, bool isHex)
        {

            int rbWidth = rb.Width;
            string s = string.Empty;
            int anzMarks = 0;

            if (!isHex)
            {
                anzMarks = (int)((rbWidth * 100 / xScale) / 5);
                for (int i = 1; i <= anzMarks; i++)
                {
                    if (i < 2)
                    {
                        s += string.Format("    {0:0}", i * 5);
                    }
                    else if (i < 20)
                    {
                        s += string.Format("   {0:00}", i * 5);
                    }
                    else
                    {
                        s += string.Format("  {0:000}", i * 5);
                    }
                }
            }
            else
            {
                anzMarks = (int)((rbWidth * 100 / xScale) / 3);
                for (int i = 1; i <= anzMarks; i++)
                {
                    s += string.Format(" {0:00}", i);
                }
            }


            // coloring ruler
            Color cl = rb.BackColor;
            rb.Select(0, System.Convert.ToInt32(rb.Lines[0].Length));
            rb.SelectionBackColor = Color.LightGray;
            rb.SelectedText = s;
            if (rb.Lines.Length == 1)
            {
                rb.AppendText(Constants.vbCr);
            }
            rb.SelectionBackColor = cl;
            rb.SelectionLength = 0;
            return s.Length;

        }

        /// <summary>
        /// select a font
        /// </summary>
        private void fontMenuItem_Click(System.Object sender, System.EventArgs e)
        {

            string s = System.Convert.ToString(((ToolStripMenuItem)sender).Text);
            Font fnt = null;

            if (s == "Large")
            {
                fnt = new Font("Lucida Console", 14.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
            }
            else if (s == "Medium")
            {
                fnt = new Font("Lucida Console", 12.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
            }
            else if (s == "Small")
            {
                fnt = new Font("Lucida Console", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0));
            }

            Graphics g = this.rtbTX.CreateGraphics();
            //measure with teststring
            SizeF szF = g.MeasureString("0123456789", fnt);
            this.xScale = (System.Convert.ToInt32(szF.Width)) * 10;
            g.Dispose();

            this.rtbRX.Font = fnt;
            this.rtbTX.Font = fnt;

        }

        /// <summary>
        /// append frame in one Richtextbox
        /// </summary>
        /// <param name="rb">tx or rx box here</param>
        /// <param name="data">data frame</param>
        /// <param name="currentLenght">possible chars in box</param>
        /// <param name="showHexAndAscii">determines whether also displaying Hex True</param>
        /// <remarks></remarks>
        private void appendBytes(RichTextBox rb, byte[] data, int currentLenght, bool showHexAndAscii)
        {

            string HexString = string.Empty;
            string CharString = string.Empty;
            int count = 0;

            for (int i = 0; i <= data.Length - 1; i++)
            {
                HexString += string.Format(" {0:X2}", data[i]);
                if (data[i] > 31)
                {
                    CharString += string.Format("  {0}", Strings.Chr(data[i]));
                }
                else
                {
                    CharString += "  .";
                }
                count += 3;

                //start a new line
                if (count >= currentLenght)
                {
                    rb.ScrollToCaret();
                    rb.AppendText(HexString + Constants.vbCr);
                    if (showHexAndAscii)
                    {
                        rb.ScrollToCaret();
                        rb.AppendText(CharString + Constants.vbCr);
                    }
                    HexString = string.Empty;
                    CharString = string.Empty;
                    count = 0;
                }

            }

            rb.ScrollToCaret();
            rb.AppendText(HexString + Constants.vbCr);
            if (showHexAndAscii)
            {
                rb.ScrollToCaret();
                rb.AppendText(CharString + Constants.vbCr);
            }
        }

        /// <summary>
        /// convert HEX string to its representing binary values
        /// </summary>
        private byte[] reconvert(string str)
        {
            byte[] data = null;
            string[] s = str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            data = new byte[s.Length - 1 + 1];
            for (int i = 0; i <= data.Length - 1; i++)
            {
                if (!byte.TryParse(s[i], NumberStyles.HexNumber, CultureInfo.CurrentCulture, out data[i]))
                {
                    data[i] = (byte)255;
                    Interaction.MsgBox("conversion failed!", MsgBoxStyle.Information, null);
                }
            }
            return data;
        }

        /// <summary>
        /// show status of connection
        /// </summary>
        private void sLabel(string[] comparams)
        {

            this.status0.Text = " Terminal connect: ";
            foreach (string s in comparams)
            {
                this.status0.Text += " -" + s;
            }

        } 
        #endregion


        #region Preparar Envio de Datos 
        private byte[] GenerarBuffer(int Longitud)
        {
            byte[] buffer = new byte[Longitud];
            int iValor = 0;

            for (int i = 0; i <= Longitud - 1; i++)
            {
                buffer[i] = Convert.ToByte(0);
            }

            return buffer;
        }
        #endregion Preparar Envio de Datos
    }
}
