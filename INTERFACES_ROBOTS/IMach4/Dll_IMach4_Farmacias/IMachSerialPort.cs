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
    public class IMach4_SerialPort
    {
        #region Declaracion de Variables 
        static readonly string sModulo = "";

        static basGenerales Fg = new basGenerales(); 

        #endregion Declaracion de Variables 

        #region Constructores y Destructor de Clase 
        IMach4_SerialPort()
        {
        }

        ~IMach4_SerialPort()
        {
        }
        #endregion Constructores y Destructor de Clase 
    
        #region Propiedades 
        public static int PuertoSerial
        {
            get { return 0; }
            set { ; }
        }

        private static SerialPort Puerto
        {
            get { return new SerialPort(); }
            set { ; }
        }

        public static bool Solicitud_K_Iniciada
        {
            set 
            {
                ;
            }
        }

        public static bool Solicitud_K_Completada
        {
            get { return false;  }
        } 
        #endregion Propiedades

        #region Eventos de Puerto
        private static SerialDataReceivedEventHandler SerialDataReceivedEventHandler1;
        private static SerialErrorReceivedEventHandler SerialErrorReceivedEventHandler1;

        public static void ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
        }

        public static void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
        }        
        #endregion Eventos de Puerto

        #region Enviar NEMONICOS
        private static void Enviar_NEMONICO(int Enviar)
        {
        }

        private static void Enviar_BCC(int Valor)
        {
        }

        private static int GenerarBCC(string Datos)
        {
            return 0;
        }
        #endregion Enviar NEMONICOS

        #region Manejo de Dialogos 
        private static bool EsDialogo(int Valor)
        {
            return false; 
        }

        private static bool EsFormatoHumano(string Mensaje)
        {
            return false;
        }

        private static void BuscaFormatoHumano()
        {
        }

        private static void DecodificarDialogo()
        {
        }
        #endregion Manejo de Dialogos

        #region Funciones y Procedimientos Publicos 
        public static bool AbrirPuerto()
        {
            return AbrirPuerto(""); 
        }

        public static bool AbrirPuerto(string NombrePuerto)
        {
            return false; 
        }

        public static bool EnviarDatos(string Informacion)
        {
            return false;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados de Conversion 
        private static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            return buffer;
        }

        private static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            return sb.ToString().ToUpper();
        }

        private static string HexToString(byte[] data)
        {
            string sRegresa = ""; 
            return sRegresa;
        }

        private static string FormatearRecepcion(string Informacion)
        {
            string sRegresa = "";
            return sRegresa;
        } 
        #endregion Funciones y Procedimientos Privados de Conversion
    }
}
