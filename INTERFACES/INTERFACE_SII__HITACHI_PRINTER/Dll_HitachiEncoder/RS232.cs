using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Dll_HitachiEncoder
{
    /// <summary>
    /// comChat - native serialPort services
    /// </summary>
    /// <remarks>update 2012-06-27</remarks>
    public class RS232
    {

        private bool isconnected;
        private bool sendstatus;
        private bool comopen;
        private SerialPort serialport1 = new SerialPort();
        private int _receiveDelay = 1;
        private int _threshold;
        private byte[] buffer;
        private SynchronizationContext sc = new SynchronizationContext();

        /// <summary>
        /// set or get the tim in ms where the Datareived handle waits
        /// </summary>
        public int ReceiveDelay
        {
            get
            {
                return this._receiveDelay;
            }
            set
            {
                this._receiveDelay = value;
            }
        }

        /// <summary>
        /// get or set the value when datareceived event fires
        /// </summary>
        public int Threshold
        {
            get
            {
                return this._threshold;
            }
            set
            {
                this._threshold = value;
                this.serialport1.ReceivedBytesThreshold = value;
            }
        }


        /// <summary>
        /// Default com params
        /// </summary>
        /// <returns>...,receiveTimout,Threshold,Delay</returns>
        public static string[] DefaultParams
        {
            get
            {
                return new string[] { "COM1", "19200", "8", "None", "One", "1", "1" };
            }
        }

        /// <summary>
        /// cParms - enumeration for Com Parameter
        /// </summary>
        public enum cP
        {
            cPort,
            cBaud,
            cData,
            cParity,
            cStop,
            cThreshold,
            cDelay
        }

        /// <summary>
        /// Event data send back to calling form
        /// </summary>
        /// <param name="buf">byte array data</param>
        public delegate void DatareceivedEventHandler(byte[] buf);
        private DatareceivedEventHandler DatareceivedEvent;

        public event DatareceivedEventHandler Datareceived
        {
            add
            {
                DatareceivedEvent = (DatareceivedEventHandler)System.Delegate.Combine(DatareceivedEvent, value);
            }
            remove
            {
                DatareceivedEvent = (DatareceivedEventHandler)System.Delegate.Remove(DatareceivedEvent, value);
            }
        }


        /// <summary>
        /// connection status back to form True: ok
        /// </summary>
        /// <param name="cStatus">True/False</param>
        public delegate void connectionEventHandler(bool cStatus);
        private connectionEventHandler connectionEvent;

        public event connectionEventHandler connection
        {
            add
            {
                connectionEvent = (connectionEventHandler)System.Delegate.Combine(connectionEvent, value);
            }
            remove
            {
                connectionEvent = (connectionEventHandler)System.Delegate.Remove(connectionEvent, value);
            }
        }


        /// <summary>
        /// data send successfull (True)
        /// </summary>
        public delegate void sendOKEventHandler(bool sStatus);
        private sendOKEventHandler sendOKEvent;

        public event sendOKEventHandler sendOK
        {
            add
            {
                sendOKEvent = (sendOKEventHandler)System.Delegate.Combine(sendOKEvent, value);
            }
            remove
            {
                sendOKEvent = (sendOKEventHandler)System.Delegate.Remove(sendOKEvent, value);
            }
        }


        /// <summary>
        /// data receive successfull
        /// </summary>
        /// <param name="sReceive">True/False</param>
        public delegate void recOKEventHandler(bool sReceive);
        private recOKEventHandler recOKEvent;

        public event recOKEventHandler recOK
        {
            add
            {
                recOKEvent = (recOKEventHandler)System.Delegate.Combine(recOKEvent, value);
            }
            remove
            {
                recOKEvent = (recOKEventHandler)System.Delegate.Remove(recOKEvent, value);
            }
        }


        /// <summary>
        /// initialize a new instance
        /// </summary>
        /// <remarks></remarks>
        public RS232()
        {
            sc = SynchronizationContext.Current;
        }

        /// <summary>
        /// overloaded version opens the port immediate
        /// </summary>
        public RS232(string[] comParams)
        {

            sc = SynchronizationContext.Current;
            Connect(comParams);

        }

        /// <summary>
        /// Com Port connect
        /// </summary>
        /// <param name="comParams">{"COM1", "19200", "8", "None", "One", "1", "1"}</param>
        /// <remarks></remarks>
        public void Connect(string[] comParams)
        {

            try
            {
                //params device 
                serialport1.PortName = comParams[(int)cP.cPort];
                serialport1.BaudRate = int.Parse(comParams[(int)cP.cBaud]);
                //demo working with enumerations. get enum value from string
                serialport1.Parity = (Parity)(Enum.Parse(typeof(Parity), comParams[(int)cP.cParity])); //get enum value from string
                serialport1.DataBits = int.Parse(comParams[(int)cP.cData]);
                serialport1.StopBits = (StopBits)(Enum.Parse(typeof(StopBits), comParams[(int)cP.cStop]));
                serialport1.Handshake = Handshake.None;
                serialport1.RtsEnable = false;
                //set the set the number of bytes in readbuffer, when event will be fired
                serialport1.ReceivedBytesThreshold = int.Parse(comParams[(int)cP.cThreshold]);
                this._threshold = System.Convert.ToInt32(serialport1.ReceivedBytesThreshold);
                //i this usage 5000ms will never be reached because we read only bytes present in readbuffer
                serialport1.ReadTimeout = 5000;
                //default value. make changes here
                serialport1.ReadBufferSize = 4096;
                //default value
                serialport1.WriteBufferSize = 2048;
                //make changes here
                serialport1.Encoding = System.Text.Encoding.ASCII;
                //delay so that event handle can fetch more bytes at once
                this._receiveDelay = int.Parse(comParams[(int)cP.cDelay]);



                //open and check device if is available
                serialport1.Open();

                serialport1.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived);
                ////serialport1.ErrorReceived += new SerialErrorReceivedEventHandler(errormsg);
               
            }
            catch (IOException ex)
            {
                showmessage(ex.Message + " ComOpen IO");

            }
            catch (Exception ex)
            {
                showmessage(ex.Message + " ComOpen EX");

            }
            finally
            {
                isconnected = System.Convert.ToBoolean(serialport1.IsOpen);
                if (connectionEvent != null)
                {
                    connectionEvent(isconnected);
                }
            }

        }

        /// <summary>
        /// disConnect com port
        /// </summary>
        public void Disconnect()
        {
            try
            {
                serialport1.DiscardInBuffer();
                serialport1.Close();
            }
            catch (Exception ex)
            {
                showmessage("disConnect: " + ex.Message);
            }
            finally
            {
                isconnected = System.Convert.ToBoolean(serialport1.IsOpen);
                if (connectionEvent != null)
                {
                    connectionEvent(isconnected);
                }
            }
        }

        /// <summary>
        /// send data
        /// </summary>
        /// <param name="data">byte array data</param>
        public void SendData(byte[] data)
        {

            try
            {
                serialport1.Write(data, 0, data.Length);
                sendstatus = true;
            }
            catch (Exception ex)
            {
                showmessage("sendData: " + ex.Message);
                sendstatus = false;
            }
            finally
            {
                if (sendOKEvent != null)
                {
                    sendOKEvent(sendstatus);
                }
            }

        }

        /// <summary>
        ///  threading. read the COM Port
        /// </summary>
        private void SerialPort1_DataReceived(System.Object sender,
            System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            if (!this.isconnected)
            {
                this.serialport1.DiscardInBuffer();
                return;
            }

            try
            {
                Thread.Sleep(this._receiveDelay);
                int len = System.Convert.ToInt32(serialport1.BytesToRead);
                this.buffer = new byte[len - 1 + 1];
                serialport1.Read(this.buffer, 0, len);
                if (recOKEvent != null)
                {
                    recOKEvent(true);
                }
            }
            catch (Exception ex)
            {
                showmessage("Read " + ex.Message);
                if (recOKEvent != null)
                {
                    recOKEvent(false);
                }
                return;
            }

            // data from secondary thread
            sc.Post(new SendOrPostCallback(doUpdate), this.buffer);

        }

        /// <summary>
        /// send data to main UI thread
        /// </summary>
        private void doUpdate(object b)
        {

            //now to UI class
            if (DatareceivedEvent != null)
            {
                DatareceivedEvent((byte[])b);
            }

        }


        /// <summary>
        /// exception message to form UI
        /// </summary>
        /// <param name="msg"></param>
        /// <remarks></remarks>
        public delegate void errormsgEventHandler(string msg);
        private errormsgEventHandler errormsgEvent;

        public event errormsgEventHandler errormsg
        {
            add
            {
                errormsgEvent = (errormsgEventHandler)System.Delegate.Combine(errormsgEvent, value);
            }
            remove
            {
                errormsgEvent = (errormsgEventHandler)System.Delegate.Remove(errormsgEvent, value);
            }
        }


        /// <summary>
        /// error to UI
        /// </summary>
        private void showmessage(string msg)
        {
            if (errormsgEvent != null)
            {
                errormsgEvent("<SerialPort " + this.serialport1.PortName + "> " + msg);
            }
        }

    }
}