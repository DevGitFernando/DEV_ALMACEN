using System;
using System.Collections; 
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
using System.Threading; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Comunicaciones;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using Dll_IMach4.Protocolos;

namespace Dll_IMach4
{
    public class IMach4_Winsock 
    {
        #region Declaracion de Variables 
        // static readonly string sModulo = "IMach4_SerialPort";

        static basGenerales Fg = new basGenerales();  
        static Winsock spPuertoWS; 
        static int iBloqueDeDatos = 464; // El bloque de datos es de 464 + 2 adicionales 
        static BloqueDeDatos_TCP_IP tpBloqueDeDatos = BloqueDeDatos_TCP_IP.Normal; 
        // static bool bPuertoAbierto = false; 

        static string sServidor = "";
        static int iPuerto_Remoto = 5510;
        static int iPuerto_Local = 5610; 
        static PictureBox pcIndicador_Comunicacion = null;
        static RichTextBox rtxtLog_Comunicacion = null; 
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

        static Thread conectar; 
        static bool bConexionEstablecida = false; 
        static bool bEstableciendoConexion = false;
        static bool bTerminarConexion = false;
        static bool bIniciandoLog = true;

        static LongitudLog iMB_File = LongitudLog.MB_01;
        static int iTamFile = (1024 * 1024) * (int)LongitudLog.MB_01;

        #endregion Declaracion de Variables 

        #region Constructores y Destructor de Clase 
        IMach4_Winsock()
        {
        }

        ~IMach4_Winsock()
        {
            try
            {
                if (spPuertoWS != null)
                {
                    spPuertoWS.Close();
                }
            }
            catch { }
            finally 
            { 
                spPuertoWS = null;
                GC.Collect(); 
            } 
        }
        #endregion Constructores y Destructor de Clase 
    
        #region Propiedades 
        public static int Puerto_Remoto
        {
            get { return iPuerto_Remoto; }
            set { iPuerto_Remoto = value; }
        }

        public static int Puerto_Local
        {
            get { return iPuerto_Local; }
            set { iPuerto_Local = value; }
        }

        public static int LongitudMensaje
        {
            get { return iBloqueDeDatos - 1; } 
        }

        public static PictureBox IndicadorDeComunicacion
        {
            get { return pcIndicador_Comunicacion; }
            set { pcIndicador_Comunicacion = value; }
        }

        public static RichTextBox Log_Comunicacion
        {
            get { return rtxtLog_Comunicacion; }
            set 
            { 
                rtxtLog_Comunicacion = value; 
                ////if ( rtxtLog_Comunicacion != null ) 
                ////{
                ////    setRuler(rtxtLog_Comunicacion, false); 
                ////}
            }
        }

        public static BloqueDeDatos_TCP_IP Bloque__TCP_IP
        {
            get { return tpBloqueDeDatos; }
            set 
            {
                tpBloqueDeDatos = value;
                iBloqueDeDatos = (int)TamañoBloqueDeDatos_TCP_IP.Normal;  
                
                if (tpBloqueDeDatos == BloqueDeDatos_TCP_IP.Completo)
                {
                    iBloqueDeDatos = (int)TamañoBloqueDeDatos_TCP_IP.Full;  
                }

                iBloqueDeDatos++; 
            }
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

        static string sFile = Application.StartupPath + @"\LogIMach.txt";
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

        private static void SetMessage(object Mensaje)
        {
            string sMensaje = (string)Mensaje;

            try
            {
                int iLines = rtxtLog_Comunicacion.Lines.Length;
                string[] sLines = rtxtLog_Comunicacion.Lines;
                ArrayList sLn = new ArrayList();



                rtxtLog_Comunicacion.Text += string.Format("{0}\n", sMensaje);
                ////rtxtLog_Comunicacion.AppendText(sMensaje); 
            }
            catch { }
        }

        private static void RegistrarEvento(EventoLog Tipo, string Mensaje)
        {
            RegistrarEvento_Ext((int)Tipo, Mensaje); 
        }

        private static void RegistrarEvento_Ext(int Tipo, string Mensaje)
        {
            DateTime dt = DateTime.Now;
            string sTexto = "";
            string sContenido = ""; 
            string sMarcaTiempo = string.Format("{0}{1}{2} {3}{4}{5}{6}",
                Fg.PonCeros(dt.Year, 4), Fg.PonCeros(dt.Month, 2), Fg.PonCeros(dt.Day, 2),
                Fg.PonCeros(dt.Hour, 2), Fg.PonCeros(dt.Minute, 2), Fg.PonCeros(dt.Second, 2),
                Fg.PonCeros(dt.Millisecond, 4));

            sTexto = string.Format("{0}     Tipo : {1:000}      {2} ", sMarcaTiempo, Tipo, Mensaje);

            if (rtxtLog_Comunicacion != null)
            {
                sTexto = sTexto.Replace("\0", ""); 
                ////sTexto = FormatearRecepcion(sTexto);

                try
                {
                    rtxtLog_Comunicacion.Text += string.Format("{0} \n", sTexto); 

                    ////if (!bIniciandoLog)
                    ////{
                    rtxtLog_Comunicacion.SelectionStart = rtxtLog_Comunicacion.Text.Length;
                    ////}

                    ////if (bIniciandoLog)
                    ////{
                    ////    bIniciandoLog = false;
                    ////}

                    rtxtLog_Comunicacion.ScrollToCaret();
                }
                catch { }
            }

            if (IMach4.HabilitarLogDeTexto)
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

        /// <summary>
        /// set ruler in box
        /// </summary>
        /// <returns>length of ruler</returns>
        private static int setRuler(RichTextBox rb, bool isHex)
        {

            int rbWidth = rb.Width;
            string s = string.Empty;
            int anzMarks = 0;
            int xScale = 830; 

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

        ////public static void ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        ////{
        ////    SerialError SerialErrorReceived1 = 0;

        ////    SerialErrorReceived1 = e.EventType;

        ////    switch (SerialErrorReceived1)
        ////    {
        ////        case SerialError.Frame:
        ////            General.msjError("Framing error.");

        ////            break;
        ////        case SerialError.Overrun:
        ////            General.msjError ("Character buffer overrun.");

        ////            break;
        ////        case SerialError.RXOver:
        ////            General.msjError("Input buffer overflow.");

        ////            break;
        ////        case SerialError.RXParity:
        ////            General.msjError("Parity error."); 
        ////            break;

        ////        case SerialError.TXFull:
        ////            General.msjError("Output buffer full.");
        ////            break;
        ////    }
        ////}

        public static void ErrorReceived(Winsock sender, string Descripcion, string Metodo, string myEx)
        {
            RegistrarEvento(EventoLog.ErrorRecepcion, Descripcion + " " + Metodo + " " + myEx); 
        }

        public static void Desconexion(Winsock sender)
        {
            RegistrarEvento(EventoLog.Desconexion, sender.GetState.ToString()); 

            UpdateIndicadorComunicacion(false);
            bConexionEstablecida = false; 
            bool bRegresa = false; // AbrirPuerto();

            if (!bTerminarConexion)
            {
                while (!bConexionEstablecida)
                {
                    bRegresa = false;
                    bRegresa = AbrirPuerto();
                }
            }
        }

        public static void DataReceived(Winsock sender, int BytesTotal)
        {
            // if (!bEnviandoInformacion && !bRecibiendo)
            {
                //////////// Temporizador para la Entrada de Datos 
                ////System.Threading.Thread.Sleep(50);

                bRecibiendo = true;
                bB_0 = false;
                bSTX = false;
                bDLE = false;
                bETX = false;
                bBCC = false;
                // bEsDialogo = false; 

                string sCadena = "";
                int iValor = 0;
                string sDatosRecibidos_Buffer = "";
                bool bEsDialogo_Entrada = false;

                // Obtain the number of bytes waiting in the port's buffer
                int bytes = 0;

                // Create a byte array buffer to hold the incoming data
                byte[] buffer = new byte[bytes];

                ////// Read the data from the port and store it in our buffer 
                ////if (!bEnviandoInformacion)
                {
                    ////spPuerto.Read(buffer, 0, bytes);
                    spPuertoWS.GetData(ref buffer);
                    sDatosRecibidos = HexToString(buffer);
                    RegistrarEvento(EventoLog.RecepcionDeDatosPuros, sDatosRecibidos); 
                    bytes = buffer.Length;
                }

                // Leer Entrada de Buffer 
                // sDatosRecibidos += HexToString(buffer); 
                // foreach (byte by in buffer)
                if (bytes >= 1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        byte by = buffer[i];
                        iValor = Convert.ToInt32(by);
                        sDatosRecibidos += Convert.ToString(by);
                        sCadena += string.Format("{0:X}", iValor);

                        // Determinar el tipo de Dialogo 
                        if (EsDialogo(iValor))
                        {
                            bEsDialogo_Entrada = true;
                        }
                    }
                }

                ////// Confirmar la recepcion 
                //if (bBCC)
                {
                    sDatosRecibidos_Buffer = HexToString(buffer);
                    sDatosRecibidos_Buffer = Fg.Mid(sDatosRecibidos_Buffer, 2);

                    RegistrarEvento(EventoLog.RecepcionDeDatos, sDatosRecibidos_Buffer.Length.ToString());
                    RegistrarEvento(EventoLog.RecepcionDeDatos, sDatosRecibidos_Buffer);

                    //// sDatosRecibidos_Buffer = FormatearRecepcion(sDatosRecibidos_Buffer);
                    foreach (string sMsjEntrada in MensajesRecibidos(sDatosRecibidos_Buffer))
                    {
                        RegistrarEvento(EventoLog.DecodificacionDeDatos, sDatosRecibidos_Buffer);
                        ///iBCC = GenerarBCC(sDatosRecibidos_Buffer);
                        ///if (bEsDialogo_Entrada)
                        {
                            sDatosRecibidos_Buffer = FormatearRecepcion(sMsjEntrada);
                            DecodificarDialogo(sDatosRecibidos_Buffer);
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
            string sBuffer = ((int)Enviar).ToString(); 

            if (spPuertoWS.IsOpen)
            {
                spPuertoWS.Send(buffer_x); 
            }
        }

        private static void Enviar_BCC(int BCC)
        {
            byte[] buffer_x = { Convert.ToByte(BCC) };
            string sBuffer = BCC.ToString(); 

            if (spPuertoWS.IsOpen)
            {
                spPuertoWS.Send(buffer_x); 
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
            return EsFormatoHumano(Mensaje, true); 
        }

        private static bool EsFormatoHumano(string Mensaje, bool ValidarDialogo)
        {
            bool bRegresa = false;
            int iValor = Fg.Asc(Fg.Mid(Mensaje, 1, 1));

            // Revisar si es un Dialogo Valido
            if (ValidarDialogo)
            {
                EsDialogo(iValor);
            }

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

        private static void DecodificarDialogo(string MensajeRecibido)
        {
            bool bRespuesta = false;
            byte[] buffer_envio = new byte[1000];
            string sMensajeRecibido = "";
            string sSql = "";

            sMensajeRecibido = MensajeRecibido.Replace("'", "*");
            sMensajeRecibido = sMensajeRecibido.Replace("028\0", "    ");
            sMensajeRecibido = sMensajeRecibido.Replace("\0", " ").Trim();

            sSql = string.Format(" Insert Into IMach_Log ( Mensaje ) values ( '{0}' ) ", sMensajeRecibido);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "DecodificarDialogo()");
            }

            switch (dialogo)
            {
                case Dialogos.a:
                    clsI_A_Response a = new clsI_A_Response(MensajeRecibido);
                    if (a.GenerarRespuesta)
                    {
                        EnviarDatos(a.Respuesta);  // Solicitar existencia despues de atender solicitud 
                    }
                    break;

                case Dialogos.A:
                    break;

                case Dialogos.b:
                    clsI_B_Response b = new clsI_B_Response(MensajeRecibido); 
                    break;

                case Dialogos.B:
                    break;

                case Dialogos.f: 
                case Dialogos.F:
                    clsI_F_Both Ff = new clsI_F_Both(MensajeRecibido); 
                    break;

                case Dialogos.g:
                    break;

                case Dialogos.G:
                    clsI_G_Response G = new clsI_G_Response(MensajeRecibido); 
                    break;

                case Dialogos.i:
                    clsI_I_Request I = new clsI_I_Request(MensajeRecibido);
                    if (I.GenerarRespuesta)
                    {
                        EnviarDatos(I.Respuesta);
                    }

                    ////if (I.Status == IMach4_i_State.ConfirmationPackageStored)
                    ////{
                    ////    clsI_B_Request B = new clsI_B_Request();
                    ////    B.Parametros = I.Parametros;
                    ////    EnviarDatos(B.Solicitud); 
                    ////}

                    break;

                case Dialogos.I:
                    break;

                case Dialogos.k:
                    clsI_K_Response k = new clsI_K_Response(MensajeRecibido);
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
                    clsI_M_Both M = new clsI_M_Both(MensajeRecibido); 
                    break;

                case Dialogos.o:
                    break;

                case Dialogos.O:
                    break;

                case Dialogos.p:
                    clsI_P_Request P = new clsI_P_Request(MensajeRecibido);
                    if (P.GenerarRespuesta)
                    {
                        EnviarDatos(P.Respuesta);
                    }
                    break;

                case Dialogos.P:
                    break;

                case Dialogos.r: 
                    IMach4.Habilitar_A = true; 
                    break;

                case Dialogos.R:
                    clsI_R_Response r = new clsI_R_Response(MensajeRecibido);
                    if (r.GenerarRespuesta)
                    {
                        bRespuesta = EnviarDatos(r.Respuesta);
                        IMach4.Habilitar_A = true; 
                    }
                    break;

                case Dialogos.s:
                    clsI_S_Response s = new clsI_S_Response(MensajeRecibido); 
                    break;

                case Dialogos.S:
                    break;

                default:
                    break;
            }
        }
        #endregion Manejo de Dialogos

        #region Funciones y Procedimientos Publicos 
        public static void UpdateIndicadorComunicacion()
        {
            UpdateIndicadorComunicacion(bConexionEstablecida); 
        }

        private static void UpdateIndicadorComunicacion(bool Status)
        {
            if (pcIndicador_Comunicacion != null)
            {
                pcIndicador_Comunicacion.BackColor = Status ? Color.Green : Color.Red; 
            }
        }

        public static bool AbrirPuerto()
        {
            return AbrirPuerto(sServidor, iPuerto_Remoto, iPuerto_Local); 
        }

        public static bool AbrirPuerto(string Servidor, int PuertoRemoto, int PuertoLocal)
        {
            bool bRegresa = false;

            try
            {
                sServidor = Servidor;
                iPuerto_Remoto = PuertoRemoto;
                iPuerto_Local = PuertoLocal;


                bEstableciendoConexion = true;
                conectar = new Thread(AbrirConexion);
                conectar.Name = "EstableciendoConexion";
                conectar.Start();

                while (bEstableciendoConexion)
                {
                    System.Threading.Thread.Sleep(1000);
                }

                //AbrirConexion(); 
                bRegresa = bConexionEstablecida;

                conectar = null;
            }
            catch
            {
            }
            finally
            {
                conectar = null;
            }

            return bRegresa;  
        } 

        private static void AbrirConexion()
        {
            bool bRegresa = false;
            int iIntentosConexion = 5;
            int iRevisiones = 0; 
            int i = 0;
            int j = 0;

            bConexionEstablecida = false; 
            bEstableciendoConexion = true; 
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(cnn.DatosConexion, IMach4.DatosApp, "IMach4_SerialPort");
            Error.NombreLogErorres = "IMach_Errores"; 

            if (spPuertoWS == null)
            {
                spPuertoWS = new Winsock();
            }

            if (!spPuertoWS.IsOpen)
            {
                try
                {
                    while (j <= iRevisiones)
                    {
                        i = 0;

                        while (i <= iIntentosConexion)
                        {
                            spPuertoWS = new Winsock();
                            spPuertoWS.RemoteIP = sServidor;
                            spPuertoWS.LocalPort = iPuerto_Local;
                            spPuertoWS.RemotePort = iPuerto_Remoto;
                            spPuertoWS.BufferLength = iBloqueDeDatos; // 464; 
                            spPuertoWS.Connect();
                            System.Threading.Thread.Sleep(100);

                            System.Threading.Thread.Sleep(50);
                            if (!spPuertoWS.IsOpen)
                            {
                                spPuertoWS = null;
                                //spPuertoWS.Dispose(); 
                            }
                            else
                            {
                                spPuertoWS.DataArrival += new Winsock.DataArrivalEventHandler(DataReceived);
                                spPuertoWS.Disconnected += new Winsock.DisconnectedHandler(Desconexion);
                                spPuertoWS.HandleError += new Winsock.HandleErrorHandler(ErrorReceived);
                                UpdateIndicadorComunicacion(true);

                                bRegresa = true;

                                i = iIntentosConexion + 1;
                                j = iRevisiones + 1;


                                System.Threading.Thread.Sleep(100);

                                break;
                            }
                            i++;
                        }

                        j++;
                        if (!bRegresa)
                        {
                            spPuertoWS = null;
                        }
                    }

                }
                catch ( Exception ex )
                {
                    ////General.msjError(ex.Message); 
                    RegistrarEvento(EventoLog.Error, ex.Message);
                    spPuertoWS = null; 
                    bRegresa = false;
                }
            }


            bEstableciendoConexion = false;
            bConexionEstablecida = bRegresa; 
        }

        private static bool CerrarPuerto()
        {
            bool bRegresa = false;
            int iIntentos = 0;

            while (iIntentos <= 10)
            {
                try
                {
                    if (conectar != null)
                    {
                        if (conectar.ThreadState == System.Threading.ThreadState.Running)
                        {
                            conectar.Abort();
                        }
                    }
                    conectar = null;


                    if (spPuertoWS.GetState != WinsockStates.Closed)
                    {
                        spPuertoWS.Close();
                        spPuertoWS = null;
                    }
                    bRegresa = true;
                    break;
                }
                catch (Exception ex)
                {
                    ex.Source = ex.Source;
                }

                iIntentos++;
            }

            return bRegresa; 
        }

        public static bool Terminar()
        {
            bool bRegresa = false;
            bTerminarConexion = false; 

            if (General.msjConfirmar("Si cierra Interface IMedimat se deshabilitara la entrega de Productos al mostrador.\n\n¿ Desea continuar ?") == DialogResult.Yes)
            {
                bTerminarConexion = true; 
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

            sSql = string.Format(" Insert Into IMach_Log ( Mensaje ) values ( '{0}' ) ", sMensajeRecibido);

            ////if (leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "RegistrarSolicitud");
            ////}
        }

        public static bool EnviarDatos(string Informacion)
        {
            bool bRegresa = false;
            int iBuffer = 0;

            RegistrarEvento(EventoLog.EnvioDeDatos, Informacion); 

            iBuffer = iBloqueDeDatos; 
            byte[] buffer = GenerarBuffer(iBuffer);
            byte[] buffer_Aux = GenerarBuffer(iBuffer);
            byte[] buffer_Datos = System.Text.Encoding.UTF8.GetBytes(Informacion);
            int i = 0;

            buffer[i] = Convert.ToByte((int)NEMONICOS.STX);
            i++;

            foreach (byte by in buffer_Datos)
            {
                buffer[i] = by;
                i++;
            }

            i++; 
            buffer[i] = Convert.ToByte((int)NEMONICOS.ETX);

            //////////////////////////////////////////// 
            ////// Registrar los datos enviados a Medimat 
            RegistrarSolicitud(Informacion); 

            //////////////////////////////////////////// 
            ////// Enviar datos por el socket 
            bRegresa = EnviarDatos(buffer, 3); 

            return bRegresa; 
        }

        private static byte[] GenerarBuffer(int Longitud)
        {
            byte[] buffer = new byte[Longitud];
            int iValor = 0;

            for (int i = 0; i <= Longitud - 1; i++)
            {
                buffer[i] = Convert.ToByte(0); 
            }

            return buffer; 
        }

        private static bool EnviarDatos(byte[] Informacion, int Intentos)
        {
            bool bExito = false;
            bEnviandoInformacion = true;

            int iRevisiones = 0;
            bool bContinua = false;
            // string sRegresa = "";

            int iReservado = 0;
            string sCadena = "";
            int iValor = 0;
            int iLen = Informacion.Length + iReservado;
            byte[] buffer_DATOS = new byte[iLen]; 


            if ( bConexionEstablecida )
            {
                spPuertoWS.Send(Informacion);
            }


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
            {
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            }
            return buffer;
        }

        private static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            }
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

        private static string Cortar(ref string Mensaje, int Caracteres)
        {
            return Cortar(ref Mensaje, 1, Caracteres);
        }

        private static string Cortar(ref string Mensaje, int Inicio, int Caracteres)
        {
            string sRegresa = "";
            int iLargo = Mensaje.Length;
            try
            {
                sRegresa = Fg.Mid(Mensaje, Inicio, Caracteres);
                Mensaje = Fg.Right(Mensaje, iLargo - Caracteres);
            }
            catch { }

            return sRegresa;
        }

        private static string QuitarNemonicos(string Informacion)
        {
            Informacion = Informacion.Trim();
            bool bFormatoHumano_2 = false;
            string sRegresa = "";
            string sMsj = Informacion;
            string sDato = "";
            int iValor = 0;
            int iCorte = 0;
            NEMONICOS dato = NEMONICOS.NULO;

            for (int i = 1; i <= sMsj.Length; i++)
            {
                sDato = Fg.Mid(sMsj, i, 1);
                try
                {
                    iValor = Fg.Asc(sDato);
                    dato = (NEMONICOS)iValor;

                    switch (dato)
                    {
                        case NEMONICOS.DLE:
                        case NEMONICOS.EOT:
                        case NEMONICOS.ETX:
                        case NEMONICOS.NAK:
                        case NEMONICOS.SOH:
                        case NEMONICOS.STX:
                            sDato = "";
                            break;
                        default:
                            break;
                    }

                    sRegresa += sDato;
                }
                catch
                {
                    dato = NEMONICOS.NULO;
                }
            }

            return sRegresa;
        }

        private static ArrayList MensajesRecibidos(string Informacion)
        {
            ArrayList sLista = new ArrayList();
            Informacion = Informacion.Trim();
            bool bFormatoHumano_2 = false;
            string sRegresa = "";
            string sMsj = Informacion;
            string sDato = "";
            int iValor = 0;
            int iCorte = 0;
            NEMONICOS dato = NEMONICOS.NULO;

            if (Informacion.Length > 500)
            {
                dato = NEMONICOS.NULO;
            }

            //////if (EsFormatoHumano(Informacion))
            //////{
            //////    bFormatoHumano_2 = true;
            //////    sMsj = Informacion.Substring(0, Informacion.IndexOf("\0"));
            //////}

            while (sMsj.Length > 0)
            {
                for (int i = 1; i <= sMsj.Length; i++)
                {
                    sDato = Fg.Mid(sMsj, i, 1);
                    try
                    {
                        iCorte++;
                        iValor = Fg.Asc(sDato);
                        if (iValor == (int)NEMONICOS.ETX)
                        {
                            ////sMsj = sMsj.Replace("\0", " ");
                            ////sMsj = sMsj.Trim();
                            sRegresa = Cortar(ref sMsj, iCorte--);
                            sRegresa = sRegresa.Replace("\0", " ");
                            sRegresa = sRegresa.Trim();
                            sRegresa = QuitarNemonicos(sRegresa);

                            ////RegistrarEvento(5, sRegresa); 
                            sLista.Add(sRegresa);
                            sRegresa = "";
                            sDato = "";
                            iCorte = 0;
                            break;
                        }
                        else
                        {
                            sRegresa += sDato;
                        }

                        if (iCorte > i)
                        {
                            sMsj = "";
                            break;
                        }
                    }
                    catch
                    {
                        dato = NEMONICOS.NULO;
                    }
                }
            }

            return sLista;
        }

        private static string FormatearRecepcion(string Informacion)
        {
            Informacion = Informacion.Trim(); 
            bool bFormatoHumano_2 = false;
            string sRegresa = "";
            string sMsj = Informacion;
            string sDato = "";
            int iValor = 0;
            NEMONICOS dato = NEMONICOS.NULO;

            
            if (EsFormatoHumano(Informacion))
            {
                bFormatoHumano_2 = true;
                try
                {
                    if (Informacion.Contains("\0"))
                    {
                        sMsj = Informacion.Substring(0, Informacion.IndexOf("\0"));
                    }
                }
                catch (Exception ex)
                { 

                }
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
