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

using Dll_IGPI;
using Dll_IGPI.Protocolos;


namespace Dll_IGPI.Interface
{
    public static class SolicitudProductos
    {
        const int TmMinuto = 60;
        const int TmSegundo = 1000;

        static string Name = "Dll_IGPI.Interface.SolicitudProductos";  
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
            ////Error = new clsGrabarError(General.DatosConexion, IGPI.DatosApp, Name); 
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

            // if (IGPI.Habilitar_A)
            {
                cnn = new clsConexionSQL(General.DatosConexion);
                leer = new clsLeer(ref cnn);
                Error = new clsGrabarError(General.DatosConexion, IGPI.DatosApp, Name); 

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
            if (IGPI.Habilitar_A)
            {
                // IGPI_SerialPort.EnviarDatos(R.Solicitud);
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
                " IdTerminal, PuertoDeSalida, IdProducto, CodigoEAN, CantidadSolicitada, CantidadSurtida, StatusIGPI " + 
                " From IGPI_SolicitudesProductos (NoLock) " +
                " Where StatusIGPI = '{0}' " + 
                " Order By {1} ", (int)IGPI_StatusRespuesta_A.Registred, sOrder);

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
                    A.Parametros.Priority = IGPI_PrioridaProceso.Nivel_3.ToString();

                    sCodigoEAN = leer.Campo("CodigoEAN");


                    if (sCodigoEAN.Length <= 8)
                    {
                            
                        iNumEAN = 8;
                    }


                    switch (sCodigoEAN.Length)
                    {
                        case 7:
                            A.Parametros.TypeCode = "01"; 
                            break;

                        case 8:
                            A.Parametros.TypeCode = "03";
                            break;

                        case 9:
                        case 12:
                            A.Parametros.TypeCode = "04";
                            break;

                        default:
                            A.Parametros.TypeCode = "02";
                            break;
                    }



                    A.Parametros.ProductCode = Fg.PonCeros(sCodigoEAN, iNumEAN);
                    A.Parametros.ProductCode = sCodigoEAN;

                    A.Parametros.Quantity = leer.CampoInt("CantidadSolicitada").ToString();
                    A.Parametros.Flag = ((int)IGPI_Flags_A.Maintain_Storage_Location).ToString();
                    A.Parametros.ID = "1";

                    sSql = string.Format(" Update IGPI_SolicitudesProductos " +
                        " Set StatusIGPI = '{2}' Where Consecutivo = '{0}' and StatusIGPI = '{1}' ", 
                        sConsecutivo, (int)IGPI_StatusRespuesta_A.Registred, (int)IGPI_StatusRespuesta_A.Sendend );

                    //// Revisar el comportamiento del Random 
                    //Error.LogError(sSql); 
                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "ObtenerSolicitud()");
                    }
                    else
                    {
                        if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.SERIAL)
                        {
                            IGPI_SerialPort.EnviarDatos(A.Solicitud);
                        }

                        if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
                        {
                            IGPI_Winsock.EnviarDatos(A.Solicitud);
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
