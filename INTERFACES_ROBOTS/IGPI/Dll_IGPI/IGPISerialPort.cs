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

using Dll_IGPI.Protocolos; 

namespace Dll_IGPI
{
    public class IGPI_SerialPort
    {
        #region Declaracion de Variables 
        // static readonly string sModulo = "IGPI_SerialPort";

        static basGenerales Fg = new basGenerales(); 
        static SerialPort spPuerto;
        // static bool bPuertoAbierto = false; 

        static string sNombrePuerto = "COM0";
        // static string sDatosRecibidos = null;


        // Test 
        static clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        static clsLeer leer;
        static clsGrabarError Error; 

        static string sDatosRecibidos = "";
        static Dialogos dialogo = Dialogos.None;
        static bool bSTX = false;
        static bool bDLE = false;
        static bool bETX = false;
        static bool bBCC = false;
        static int iBCC = 0;
        static bool bEsDialogo = false;
        static bool bEnviandoInformacion = false;
        static bool bRecibiendo = false;
        static bool bB_0 = false;
        static bool bManejaFormatoHumano = false;

        static bool bSolicitud_K_Iniciada = false;
        static bool bSolicitud_K_Terminada = false;

        static LongitudLog iMB_File = LongitudLog.MB_01;
        static int iTamFile = (1024 * 1024) * (int)LongitudLog.MB_01;

        #endregion Declaracion de Variables 

        #region Constructores y Destructor de Clase 
        IGPI_SerialPort()
        {
        }

        ~IGPI_SerialPort()
        {
            try
            {
                if (spPuerto != null)
                {
                    spPuerto.Close();
                }
            }
            catch { }
            finally 
            { 
                spPuerto = null;
                GC.Collect(); 
            } 
        }
        #endregion Constructores y Destructor de Clase 
    
        #region Propiedades 
        private static bool Get()
        {
            bManejaFormatoHumano = bSTX; 
            return bManejaFormatoHumano; 
        }

        public static int PuertoSerial
        {
            get { return Convert.ToInt32(sNombrePuerto.Replace("COM", "")); }
            set { sNombrePuerto = "COM" + value.ToString(); }
        }

        private static SerialPort Puerto
        {
            get { return spPuerto; }
            set { spPuerto = value; }
        }

        public static bool Solicitud_K_Iniciada
        {
            set 
            {
                bSolicitud_K_Iniciada = true;
                bSolicitud_K_Terminada = false; 
            }
        }

        public static bool Solicitud_K_Completada
        {
            get { return (!bSolicitud_K_Iniciada) && (bSolicitud_K_Terminada);  }
        } 
        #endregion Propiedades

        #region Eventos de Puerto
        // private static SerialDataReceivedEventHandler SerialDataReceivedEventHandler1;
        // private static SerialErrorReceivedEventHandler SerialErrorReceivedEventHandler1;

        static string sFile = Application.StartupPath + @"\LogIGPI.txt";
        static StreamWriter fileLog;
        static int iLineas = 0;

        public static void ResetLog()
        {
            try
            {
                if (fileLog != null)
                {
                    fileLog.Close();
                    fileLog = null;
                }

                if (File.Exists(sFile))
                {
                    File.Delete(sFile);
                }
            }
            catch
            {
            }
        }

        private static void RegistrarEvento(int Tipo, string Mensaje)
        {
            if (IGPI.HabilitarLogDeTexto)
            {
                try
                {
                    if (!File.Exists(sFile))
                    {
                        if (fileLog == null)
                        {
                            fileLog = new StreamWriter(sFile, true, System.Text.Encoding.UTF8);
                            iLineas = 0;
                        }
                    }
                    else
                    {
                        FileInfo fl = new FileInfo(sFile);
                        if (fl.Length >= iTamFile)
                        {
                            try
                            {
                                //// Eliminar el archivo original. 
                                File.Delete(sFile);
                            }
                            catch { }
                        }

                        fileLog = new StreamWriter(sFile, true, System.Text.Encoding.UTF8);
                        iLineas = 0;
                    }

                    DateTime dt = DateTime.Now;
                    string sMarcaTiempo = string.Format("{0}{1}{2} {3}{4}{5}{6}",
                        Fg.PonCeros(dt.Year, 4), Fg.PonCeros(dt.Month, 2), Fg.PonCeros(dt.Day, 2),
                        Fg.PonCeros(dt.Hour, 2), Fg.PonCeros(dt.Minute, 2), Fg.PonCeros(dt.Second, 2),
                        Fg.PonCeros(dt.Millisecond, 4));

                    fileLog.WriteLine(string.Format("Registro : {0}   Tipo : {1}     Mensaje : {2} ", sMarcaTiempo, Tipo, Mensaje)); 
                    iLineas++;  

                    if (iLineas >= 1)
                    {
                        fileLog.Close();
                        fileLog = null; 
                        ////fileLog = new StreamWriter(sFile, true, System.Text.Encoding.UTF8);
                        iLineas = 0;
                    }
                }
                catch { }
            }
        }

        public static void ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            SerialError SerialErrorReceived1 = 0;

            SerialErrorReceived1 = e.EventType;
            string sError = ""; 

            switch (SerialErrorReceived1)
            {
                case SerialError.Frame:
                    ////General.msjError("Framing error.");
                    sError = "Framing error."; 
                    break;

                case SerialError.Overrun:
                    ////General.msjError ("Character buffer overrun.");
                    sError = "Character buffer overrun"; 
                    break;

                case SerialError.RXOver:
                    ////General.msjError("Input buffer overflow.");
                    sError = "Input buffer overflow."; 
                    break;

                case SerialError.RXParity:
                    ////General.msjError("Parity error.");
                    sError = "Parity error."; 
                    break;

                case SerialError.TXFull:
                    ////General.msjError("Output buffer full.");
                    sError = "Output buffer full."; 
                    break;

            }

            RegistrarEvento(4, sError); 
        }

        public static void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!bEnviandoInformacion && !bRecibiendo)
            {
                // Temporizador para la Entrada de Datos 
                System.Threading.Thread.Sleep(200);

                bRecibiendo = true;
                bB_0 = false;
                bSTX = false;
                bDLE = false;
                bETX = false;
                bBCC = false;
                // bEsDialogo = false; 

                string sCadena = "";
                int iValor = 0;

                // Obtain the number of bytes waiting in the port's buffer
                int bytes = spPuerto.BytesToRead;

                // Create a byte array buffer to hold the incoming data
                byte[] buffer = new byte[bytes];

                // Read the data from the port and store it in our buffer 
                if (!bEnviandoInformacion)
                {
                    spPuerto.Read(buffer, 0, bytes);
                }

                // Leer Entrada de Buffer 
                // sDatosRecibidos += HexToString(buffer); 
                foreach (byte by in buffer)
                {
                    iValor = Convert.ToInt32(by);
                    sDatosRecibidos += Convert.ToString(by);
                    sCadena += string.Format("{0:X}", iValor);

                    // Determinar el tipo de Dialogo 
                    if (!bEsDialogo && EsDialogo(iValor))
                    {
                        bEsDialogo = true; 
                    }

                    if (bBCC)
                    {
                        iBCC = iValor;
                        break;
                    }

                    if (iValor == (int)NEMONICOS.STX)
                    {
                        bSTX = true;
                        Enviar_NEMONICO(NEMONICOS.DLE); 
                    }

                    if (iValor == (int)NEMONICOS.DLE)
                    {
                        bDLE = true;
                        if (bB_0)
                        {
                        }
                    }

                    if (iValor == (int)NEMONICOS.ETX)
                    {
                        bETX = true; 
                    }

                    if (iValor == (int)NEMONICOS.NULO)
                    {
                        bB_0 = true;
                    }

                    // Se espera recibir el BCC 
                    if (bDLE && bETX)
                    {
                        bBCC = true; 
                    }
                }

                sDatosRecibidos = HexToString(buffer);
                RegistrarEvento(1, sDatosRecibidos); 

                // Confirmar la recepcion 
                if (bBCC)
                {
                    sDatosRecibidos = HexToString(buffer);
                    sDatosRecibidos = FormatearRecepcion(sDatosRecibidos); 

                    iBCC = GenerarBCC(sDatosRecibidos);
                    if (iValor != iBCC)
                    {
                        sDatosRecibidos = "";
                        bBCC = false;
                        Enviar_NEMONICO(NEMONICOS.NAK);
                    }
                    else
                    {
                        Enviar_NEMONICO(NEMONICOS.DLE);
                        if (bEsDialogo)
                        {
                            DecodificarDialogo();
                        }
                    }
                }

                // Permitir la entrada de informacion 
                bRecibiendo = false;
                ////txtRecepcion.Text += "\n" + sCadena + "\n";
            }
        }        
        #endregion Eventos de Puerto

        #region Enviar NEMONICOS
        private static void Enviar_NEMONICO(NEMONICOS Enviar)
        {
            byte[] buffer_x = { Convert.ToByte((int)Enviar) };

            if (spPuerto.IsOpen)
            {
                spPuerto.Write(buffer_x, 0, buffer_x.Length);
            }
        }

        private static void Enviar_BCC(int BCC)
        {
            byte[] buffer_x = { Convert.ToByte(BCC) };

            if (spPuerto.IsOpen)
            {
                spPuerto.Write(buffer_x, 0, buffer_x.Length);
            }
        }

        private static int GenerarBCC(string Mensaje)
        {
            int iRegresa = 0;
            int iValor = 0; ////, iDLE = 16, iETX = 3;
            // int iValor = 0, iDLE = (int)NEMONICOS.DLE, iETX = (int)NEMONICOS.ETX;

            string sCadena = Mensaje;
            // string sChar = "";

            if (Mensaje != null)
            {
                if (EsFormatoHumano(sCadena) && !bEnviandoInformacion)
                {
                    sCadena = sCadena.Replace("\0", " ");
                }

                iRegresa = 0; 
                for (int i = 1; i <= sCadena.Length; i++)
                {
                    iValor = Fg.Asc(Fg.Mid(sCadena, i, 1));
                    iRegresa = iRegresa ^ (int)Convert.ToDecimal(iValor);
                }
            }

            iRegresa = iRegresa ^ (int)NEMONICOS.DLE;
            iRegresa = iRegresa ^ (int)NEMONICOS.ETX;
            return iRegresa;
        }
        #endregion Enviar NEMONICOS

        #region Manejo de Dialogos 
        private static bool EsDialogo(int Valor)
        {
            bool bRegresa = false;
            dialogo = (Dialogos)Valor;

            switch (dialogo)
            {
                case Dialogos.a:
                case Dialogos.A:
                case Dialogos.b:
                case Dialogos.B:
                case Dialogos.f:
                case Dialogos.F:
                case Dialogos.g:
                case Dialogos.G:
                case Dialogos.i:
                case Dialogos.I:
                case Dialogos.k:
                case Dialogos.K:
                case Dialogos.m:
                case Dialogos.M:
                case Dialogos.o:
                case Dialogos.O:
                case Dialogos.p:
                case Dialogos.P:
                case Dialogos.r:
                case Dialogos.R:
                case Dialogos.s:
                case Dialogos.S:
                    bRegresa = true;
                    break;
                default:
                    break;
            }

            return bRegresa;
        }

        private static bool EsFormatoHumano(string Mensaje)
        {
            bool bRegresa = false;
            int iValor = Fg.Asc(Fg.Mid(Mensaje, 1, 1));

            // Revisar si es un Dialogo Valido
            EsDialogo(iValor);

            try
            {
                switch (iValor)
                {
                    case (int)Dialogos.s:
                    case (int)Dialogos.o:
                    case (int)Dialogos.I:
                    case (int)Dialogos.r:
                    case (int)Dialogos.R:
                        bRegresa = true;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        private static void BuscaFormatoHumano()
        {
            bManejaFormatoHumano = false;

            switch (dialogo)
            {
                case Dialogos.s:
                case Dialogos.o:
                case Dialogos.I:
                case Dialogos.r:
                case Dialogos.R:
                    bManejaFormatoHumano = true;
                    break;
                default:
                    bManejaFormatoHumano = false;
                    break;
            }
        }

        private static void DecodificarDialogo()
        {
            bool bRespuesta = false;
            byte[] buffer_envio = new byte[1000];
            string sMensajeRecibido = "";
            string sSql = "";

            sMensajeRecibido = sDatosRecibidos.Replace("'", "*");
            sMensajeRecibido = sMensajeRecibido.Replace("028\0", "    ");
            sMensajeRecibido = sMensajeRecibido.Replace("\0", " ").Trim();

            sSql = string.Format(" Insert Into IGPI_Log ( Mensaje ) values ( '{0}' ) ", sMensajeRecibido);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "DecodificarDialogo()"); 
            }

            switch (dialogo)
            {
                case Dialogos.a: 
                    clsI_A_Response a = new clsI_A_Response(sDatosRecibidos);
                    if (a.GenerarRespuesta)
                    {
                        EnviarDatos(a.Respuesta);  // Solicitar existencia despues de atender solicitud 
                    }
                    break;

                case Dialogos.A:
                    break;

                case Dialogos.b:
                    clsI_B_Response b = new clsI_B_Response(sDatosRecibidos); 
                    break;

                case Dialogos.B:
                    break;

                case Dialogos.f: 
                case Dialogos.F:
                    clsI_F_Both Ff = new clsI_F_Both(sDatosRecibidos); 
                    break;

                case Dialogos.g:
                    break;

                case Dialogos.G:
                    clsI_G_Response G = new clsI_G_Response(sDatosRecibidos); 
                    break;

                case Dialogos.i:
                    clsI_I_Request I = new clsI_I_Request(sDatosRecibidos);
                    if (I.GenerarRespuesta)
                    {
                        EnviarDatos(I.Respuesta);
                    }
                    break;

                case Dialogos.I:
                    break;

                case Dialogos.k:
                    clsI_K_Response k = new clsI_K_Response(sDatosRecibidos);
                    if (k.GenerarRespuesta)
                    {
                        EnviarDatos(k.Respuesta);
                    }
                    else
                    {
                        bSolicitud_K_Iniciada = false; 
                        bSolicitud_K_Terminada = true; 
                        break; 
                    }
                    break;

                case Dialogos.K:
                    break;

                case Dialogos.m:
                    break;

                case Dialogos.M:
                    clsI_M_Both M = new clsI_M_Both(sDatosRecibidos); 
                    break;

                case Dialogos.o:
                    break;

                case Dialogos.O:
                    break;

                case Dialogos.p:
                    clsI_P_Request P = new clsI_P_Request(sDatosRecibidos);
                    if (P.GenerarRespuesta)
                    {
                        EnviarDatos(P.Respuesta);
                    }
                    break;

                case Dialogos.P:
                    break;

                case Dialogos.r:
                    // General.msjUser(sDatosRecibidos); 
                    IGPI.Habilitar_A = true; 
                    break;

                case Dialogos.R:
                    clsI_R_Response r = new clsI_R_Response(sDatosRecibidos);
                    if (r.GenerarRespuesta)
                    {
                        bRespuesta = EnviarDatos(r.Respuesta);
                        IGPI.Habilitar_A = true; 
                    }
                    //buffer_envio = r.Respuesta();
                    //spPuerto.Write(buffer_envio, 0, buffer_envio.Length); 
                    //do
                    // {
                    // bRespuesta = EnviarDatos(r.Respuesta);
                    // } while (!bRespuesta); 
                    break;

                case Dialogos.s:
                    clsI_S_Response s = new clsI_S_Response(sDatosRecibidos); 
                    break;

                case Dialogos.S:
                    break;

                default:
                    break;
            }
        }
        #endregion Manejo de Dialogos

        #region Funciones y Procedimientos Publicos 
        public static bool AbrirPuerto()
        {
            return AbrirPuerto(sNombrePuerto); 
        }

        public static bool AbrirPuerto(string NombrePuerto)
        {
            bool bRegresa = false;

            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(cnn.DatosConexion, IGPI.DatosApp, "IGPI_SerialPort");
            Error.NombreLogErorres = "IGPI_Errores"; 

            if (spPuerto == null)
            {
                spPuerto = new SerialPort();
            }

            if (!spPuerto.IsOpen)
            {
                spPuerto = new SerialPort();
                spPuerto.PortName = NombrePuerto;
                spPuerto.BaudRate = 19200;
                spPuerto.Parity = Parity.Even;
                spPuerto.DataBits = 8;
                spPuerto.StopBits = StopBits.One;
                spPuerto.Handshake = Handshake.None; 

                try
                {
                    spPuerto.Open();

                    if (spPuerto.IsOpen)
                    {
                        ////////SerialDataReceivedEventHandler1 = new SerialDataReceivedEventHandler(DataReceived);
                        ////////SerialErrorReceivedEventHandler1 = new SerialErrorReceivedEventHandler(ErrorReceived);

                        ////////spPuerto.DataReceived += SerialDataReceivedEventHandler1;
                        ////////spPuerto.ErrorReceived += SerialErrorReceivedEventHandler1;

                        spPuerto.DataReceived += new SerialDataReceivedEventHandler(DataReceived) ;
                        spPuerto.ErrorReceived += new SerialErrorReceivedEventHandler(ErrorReceived);

                        bRegresa = true;

                        // General.msjUser("Puerto abierto satisfactoriamente.");
                    }
                }
                catch ( Exception ex )
                {
                    spPuerto = null; 
                    bRegresa = false;
                }
            }
            return bRegresa; 
        }

        private static bool CerrarPuerto()
        {
            bool bRegresa = true;
            int iIntentos = 0;

            while (iIntentos <= 2)
            {
                try
                {
                    spPuerto.Close();
                    break; 
                }
                catch { }

                iIntentos++; 
            }

            return bRegresa; 
        }

        public static bool Terminar()
        {
            bool bRegresa = false;

            if (General.msjConfirmar("Si cierra Interface Mach4 se deshabilitara la entregra de Productos al mostrador, ¿ Desea continuar ?") == DialogResult.Yes)
            {
                bRegresa = CerrarPuerto(); 
            }

            return bRegresa; 
        }

        private static void RegistrarSolicitud(string Informacion)
        {
            string sSql = "";
            string sMensajeRecibido = Informacion;

            sMensajeRecibido = sMensajeRecibido.Replace("'", "*");
            sMensajeRecibido = sMensajeRecibido.Replace("028\0", "    ");
            sMensajeRecibido = sMensajeRecibido.Replace("\0", " ").Trim();

            sSql = string.Format(" Insert Into IGPI_Log ( Mensaje ) values ( '{0}' ) ", sMensajeRecibido);

            ////if (leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "RegistrarSolicitud");
            ////}
        }

        public static bool EnviarDatos(string Informacion)
        {
            bool bRegresa = false; 
            ////while (!bRegresa)
            ////{ 
            ////} 

            RegistrarEvento(2, Informacion); 
            bRegresa = EnviarDatos(Informacion, 3); 

            return bRegresa; 
        }

        private static bool EnviarDatos(string Informacion, int Intentos)
        {
            bool bExito = false;
            // Apartar el buffer de Entrada 
            bEnviandoInformacion = true;

            // int iRevisiones = 0; 
            bool bContinua = false;
            // string sRegresa = "";

            int iReservado = 0;
            string sInf = Informacion;
            string sCadena = ""; 
            int iValor = 0; 
            int iLen = sInf.Length + iReservado;
            byte[] buffer_DATOS = new byte[iLen];
            NEMONICOS recepcion = NEMONICOS.NULO;

            //////////////////////////////////////////// 
            ////// Registrar los datos enviados a Medimat 
            RegistrarSolicitud(Informacion); 


            //// Revisar la Cadena a Enviar 
            //if (Informacion.IndexOf("\0") != -1)
            //    sInf = Fg.Mid(Informacion, 1, Informacion.IndexOf("\0")); 

            // Enviar solicitud de Conexion 
            Enviar_NEMONICO(NEMONICOS.STX); 


            // Esperar respuesta 
            while (!bContinua)
            {
                int bytes = spPuerto.BytesToRead; 
                byte[] buffer = new byte[bytes]; 
                spPuerto.Read(buffer, 0, bytes); 

                // Leer Entrada de Buffer 
                foreach (byte by in buffer) 
                { 
                    iValor = Convert.ToInt32(by); 
                    sCadena += string.Format("{0:X}", iValor); 

                    if (iValor == (int)NEMONICOS.DLE || iValor == (int)NEMONICOS.NAK) 
                    {
                        bContinua = true;
                        recepcion = (NEMONICOS)iValor;
                        break;
                    }
                }

                ////iRevisiones++;
                ////if (iRevisiones >= 5)
                ////{
                ////    break; 
                ////}
            }

            ////// Respuesta recibida 
            //// if (recepcion == NEMONICOS.DLE)
            //if (recepcion == NEMONICOS.NULO)
            //{
            //    Enviar_NEMONICO(NEMONICOS.NAK); 
            //}
            //else 
            if (recepcion == NEMONICOS.DLE)
            {
                for (int i = 0; i <= sInf.Length; i++)
                {
                    try
                    {
                        buffer_DATOS[i] = Convert.ToByte(Fg.Asc(Fg.Mid(sInf, i + 1, 1)));
                    }
                    catch { }
                }

                // Envias Byte por Byte toda la cadena de respuesta 
                foreach (byte by in buffer_DATOS)
                {
                    byte[] buffer_SENDER = { by };
                    spPuerto.Write(buffer_SENDER, 0, buffer_SENDER.Length);
                }

                ////// Test 
                Enviar_NEMONICO(NEMONICOS.NULO);
                Enviar_NEMONICO(NEMONICOS.DLE);
                Enviar_NEMONICO(NEMONICOS.ETX);

                Enviar_BCC(GenerarBCC(sInf)); 
            }

            // Liberar el buffer de Entrada 
            bEnviandoInformacion = false;

            return bExito;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados de Conversion 
        private static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        private static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }

        private static string HexToString(byte[] data)
        {
            string sRegresa = "";
            // int i = 0;

            foreach (byte b in data)
            {
                sRegresa += Convert.ToChar(b).ToString();
            }

            return sRegresa;
        }

        private static string FormatearRecepcion(string Informacion)
        {
            bool bFormatoHumano_2 = false;
            string sRegresa = "";
            string sMsj = Informacion;
            string sDato = "";
            int iValor = 0;
            NEMONICOS dato = NEMONICOS.NULO;


            if (EsFormatoHumano(Informacion))
            {
                bFormatoHumano_2 = true;
                sMsj = Informacion.Substring(0, Informacion.IndexOf("\0"));
            }

            for (int i = 1; i <= sMsj.Length; i++)
            {
                sDato = Fg.Mid(sMsj, i, 1);
                try
                {
                    iValor = Fg.Asc(sDato);
                    if (iValor == (int)NEMONICOS.DLE)
                    {
                        break;
                    }
                    else if (iValor == (int)NEMONICOS.EOT)
                    {
                        break;
                    }
                    else if (iValor == (int)NEMONICOS.ETX)
                    {
                        break;
                    }
                    else if (iValor == (int)NEMONICOS.NAK)
                    {
                        break;
                    }
                    else if (iValor == (int)NEMONICOS.SOH)
                    {
                        break;
                    }
                    else if (iValor == (int)NEMONICOS.STX)
                    {
                        break;
                    }
                    else
                    {
                        sRegresa += sDato;
                    }
                }
                catch
                {
                    dato = NEMONICOS.NULO;
                }
            }

            if (bFormatoHumano_2)
            {
                if (sMsj.Contains("\0"))
                {
                    sRegresa = sMsj.Substring(0, sMsj.IndexOf("\0"));
                }
            }

            sMsj = dato.ToString(); 
            return sRegresa;
        } 
        #endregion Funciones y Procedimientos Privados de Conversion
    }
}
