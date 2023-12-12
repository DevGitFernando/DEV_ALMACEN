using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using Dll_IMach4.Protocolos; 

namespace Dll_IMach4
{
    public enum IMach_Protocolos
    {
        Request_A = 1, 
        Response_A = 2
    }

    public class IMachSerialPort
    {
        #region Declaracion de Variables 
        static readonly string sModulo = "IMachSerialPort";

        private static basGenerales Fg = new basGenerales(); 
        private static SerialPort spPuerto;
        private static bool bPuertoAbierto = false; 

        private static string sNombrePuerto = "COM12";
        private static string sDatosRecibidos = null;

        #endregion Declaracion de Variables 


        #region Constructores y Destructor de Clase 
        IMachSerialPort()
        {
        }
        #endregion Constructores y Destructor de Clase 
    
        public static SerialPort Puerto
        {
            get { return spPuerto; }
            set { spPuerto = value; }
        }

        #region Eventos de Puerto 
        private static SerialDataReceivedEventHandler SerialDataReceivedEventHandler1;
        private static SerialErrorReceivedEventHandler SerialErrorReceivedEventHandler1;

        public static void ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            SerialError SerialErrorReceived1 = 0;

            SerialErrorReceived1 = e.EventType;

            switch (SerialErrorReceived1)
            {
                case SerialError.Frame:
                    General.msjError("Framing error.");

                    break;
                case SerialError.Overrun:
                    General.msjError ("Character buffer overrun.");

                    break;
                case SerialError.RXOver:
                    General.msjError("Input buffer overflow.");

                    break;
                case SerialError.RXParity:
                    General.msjError("Parity error.");

                    break;
                case SerialError.TXFull:
                    General.msjError("Output buffer full.");
                    break;
            }
        }

        public static void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string sProtocolo = ""; 
            sDatosRecibidos = ""; 

            try
            {
                //  Leer los datos del Puerto COM.
                sDatosRecibidos = spPuerto.ReadExisting();
                sProtocolo = Fg.Mid(sDatosRecibidos, 1, 1);

                // Obtain the number of bytes waiting in the port's buffer
                int bytes = spPuerto.BytesToRead;

                // Create a byte array buffer to hold the incoming data
                byte[] buffer = new byte[bytes];
                char[] myChar = "16".ToCharArray();
                string DLE = "16";

                // Read the data from the port and store it in our buffer
                spPuerto.Read(buffer, 0, bytes); 

                sDatosRecibidos = ByteArrayToHexString(buffer);
                sProtocolo = Fg.Mid(sDatosRecibidos, 1, 2);

                // spPuerto.Write(myChar, 0, 1); 

                if (buffer.Length == 0)
                {
                    //byte[] buffer_x = HexStringToByteArray(DLE); 
                    //sProtocolo = "";
                    //spPuerto.Write(buffer_x, 0, buffer_x.Length);
                }

                // Determinar que Protocolo se recibio 
                switch (sProtocolo)
                {
                    case "02":
                        byte[] buffer_xy = HexStringToByteArray(DLE); 
                        sProtocolo = "";
                        UInt32 sOut = 0x10; 
                        // spPuerto.Write(buffer_xy, 0, buffer_xy.Length);
                        spPuerto.Write(sOut.ToString());
                        break;

                    case "a":
                        //clsI_A_Response a_reponse = new clsI_A_Response(sDatosRecibidos); 
                        break; 

                    case "R":
                        clsI_R_Request R_response = new clsI_R_Request(sDatosRecibidos); 
                        break; 

                    default:
                        break; 
                }

            }
            catch (Exception ex)
            {
                //DisplayException(ModuleName, ex); 
                General.msjError(sModulo +  "  ", ex.Message);
            }
        } 
       
        
        #endregion Eventos de Puerto

        #region Funciones y Procedimientos Publicos 
        public static void EnviarMensaje(string Mensaje)
        {
            AbrirPuerto(); 

            if (spPuerto.IsOpen)
            {
                // spPuerto.Write(Mensaje);
                spPuerto.Write(Mensaje); 
            }
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private static byte[] StringToHex(string Cadena)
        {
            return System.Text.Encoding.UTF8.GetBytes(Cadena); 
        }

        private static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data"> The array of bytes to be translated into a string of hex digits. </param>
        /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
        private static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }

        public static void AbrirPuerto()
        {
            if (spPuerto == null) 
                spPuerto = new SerialPort(); 

            if (!spPuerto.IsOpen)
            {
                spPuerto = new SerialPort();
                spPuerto.PortName = sNombrePuerto;
                spPuerto.BaudRate = 19200;
                spPuerto.Parity = Parity.Even;
                spPuerto.DataBits = 8;
                spPuerto.StopBits = StopBits.One;
                spPuerto.Handshake = Handshake.None;  

                spPuerto.Open();

                if (spPuerto.IsOpen)
                {
                    //  The port is open. Set additional parameters.
                    //  Timeouts are in milliseconds.
                    spPuerto.ReadTimeout = 10000;
                    spPuerto.WriteTimeout = 10000;


                    ////SerialDataReceivedEventHandler1 = new SerialDataReceivedEventHandler(DataReceived);
                    ////SerialErrorReceivedEventHandler1 = new SerialErrorReceivedEventHandler(ErrorReceived); 

                    //spPuerto.DataReceived += SerialDataReceivedEventHandler1;
                    //spPuerto.ErrorReceived += SerialErrorReceivedEventHandler1;

                    General.msjUser("Puerto abierto satisfactoriamente."); 
                }
            }
        }
        #endregion Funciones y Procedimientos Privados

    }
}
