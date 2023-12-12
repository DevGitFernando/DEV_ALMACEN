using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using Dll_IMach4;
using Dll_IMach4.Protocolos;


namespace Dll_IMach4.Interface
{
    public static class SolicitudProductos
    {
        const int TmMinuto = 60;
        const int TmSegundo = 1000;

        static string Name = "Dll_IMach4.Interface.SolicitudProductos";  
        static clsConexionSQL cnn;
        static clsLeer leer;
        static clsGrabarError Error;
        static basGenerales Fg = new basGenerales(); 

        static clsI_A_Request A;
        static int iTiempo = TmSegundo * (15); 
        static Timer temporizador;

        static string sOrder = "";
        static string sOrderDesc = " Desc ";         

        #region Constructor de Clase 
        static SolicitudProductos()
        {
            ////cnn = new clsConexionSQL(General.DatosConexion);
            ////leer = new clsLeer(ref cnn);
            ////Error = new clsGrabarError(General.DatosConexion, IMach4.DatosApp, Name); 
        }
        #endregion Constructor de Clase

        #region Propiedades 
        public static double TiempoEnSegundosDecimales
        {
            get { return (iTiempo / TmSegundo); }
            set { iTiempo = (int)((double)TmSegundo * (value)); }
        }

        public static int TiempoEnSegundos
        {
            get { return (iTiempo / TmSegundo); }
            set { iTiempo = TmSegundo * (value); }
        }

        public static int TiempoEnMinutos
        {
            get { return ((iTiempo / TmSegundo)/ TmMinuto); }
            set { iTiempo = TmMinuto * TmSegundo * (value); }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public static void Iniciar()
        {
            Detener();

            // if (IMach4.Habilitar_A)
            {
                cnn = new clsConexionSQL(General.DatosConexion);
                leer = new clsLeer(ref cnn);
                Error = new clsGrabarError(General.DatosConexion, IMach4.DatosApp, Name); 

                // Inicializar el Protocolo A 
                A = new clsI_A_Request();

                temporizador = new Timer();
                temporizador.Tick += new EventHandler(Solicitar_Productos);

                temporizador.Interval = iTiempo;
                temporizador.Enabled = true;
                temporizador.Start();
            }
        }

        public static void Detener()
        {
            if (temporizador != null)
            {
                temporizador.Stop();
                temporizador.Enabled = false;
                temporizador = null;
                A = null; 
            }
        }
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados
        private static void Solicitar_Productos(object sender, EventArgs e)
        {
            if (IMach4.Habilitar_A)
            {
                // IMach4_SerialPort.EnviarDatos(R.Solicitud);
                ObtenerSolicitud(); 
            }
        }

        private static void ObtenerSolicitud()
        {
            string sSql = ""; 
            string sConsecutivo = "";
            string sCodigoEAN = ""; 
            int iNumEAN = 13; 


            sOrder = " NEWID() "; 
            sSql = string.Format(" Select Top 1 IdCliente, FolioSolicitud, cast(Consecutivo as varchar) as Consecutivo, " + 
                " IdTerminal, PuertoDeSalida, IdProducto, CodigoEAN, CantidadSolicitada, CantidadSurtida, StatusIMach " + 
                " From IMach_SolicitudesProductos (NoLock) " + 
                " Where StatusIMach = '{0}' " + 
                " Order By {1} ", (int)IMach4_StatusRespuesta_A.Registred, sOrder);

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "ObtenerSolicitud()"); 
            }
            else
            {
                if (leer.Leer())
                {
                    sConsecutivo = leer.Campo("Consecutivo");
                    
                    A.Dialogo = "A";
                    A.Parametros.OrderNumber = leer.Campo("Consecutivo");
                    A.Parametros.RequestLocationNumber = leer.Campo("IdTerminal");
                    A.Parametros.DeliveryPort = leer.Campo("PuertoDeSalida"); 
                    A.Parametros.Priority = IMach4_PrioridaProceso.Nivel_3.ToString();

                    sCodigoEAN = leer.Campo("CodigoEAN"); 
                    
                    if (sCodigoEAN.Length <= 8)
                        iNumEAN = 8;

                    A.Parametros.ProductCode = Fg.PonCeros(sCodigoEAN, iNumEAN);                    
                    A.Parametros.Quantity = leer.CampoInt("CantidadSolicitada").ToString();
                    A.Parametros.Flag = ((int)IMach4_Flags_A.Maintain_Storage_Location).ToString();
                    A.Parametros.ID = "1";

                    sSql = string.Format(" Update IMach_SolicitudesProductos " + 
                        " Set StatusIMach = '{2}' Where Consecutivo = '{0}' and StatusIMach = '{1}' ", 
                        sConsecutivo, (int)IMach4_StatusRespuesta_A.Registred, (int)IMach4_StatusRespuesta_A.Sendend );

                    //// Revisar el comportamiento del Random 
                    //Error.LogError(sSql); 
                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "ObtenerSolicitud()");
                    }
                    else
                    {
                        if (IMach4.Protocolo_Comunicacion == ProtocoloConexion_IMach.SERIAL)
                        {
                            IMach4_SerialPort.EnviarDatos(A.Solicitud);
                        }

                        if (IMach4.Protocolo_Comunicacion == ProtocoloConexion_IMach.TCP_IP)
                        {
                            IMach4_Winsock.EnviarDatos(A.Solicitud);
                        }
                    }
                }
            } 

            // Cambiar el Ordenamiento de la busqueda. 
            // RequisicionDeProducto = CantidadSolicitada > 0 ? true : false;
            sOrder = sOrder == "" ? sOrderDesc : ""; 

        }
        #endregion Funciones y Procedimientos Privados
    }
}
