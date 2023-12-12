using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

using Dll_IGPI;
using Dll_IGPI.Protocolos;


namespace Dll_IGPI.Interface
{
    public static class clsProbarConexion
    {
        const int TmMinuto = 60;
        const int TmSegundo = 1000;

        static clsI_R_Request R;
        static int iTiempo = TmSegundo * (45); 
        static Timer temporizador;

        #region Constructor de Clase 
        static clsProbarConexion()
        { 
        }
        #endregion Constructor de Clase

        #region Propiedades 
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

            ////if (IGPI.Habilitar_R)
            ////{
            ////    R = new clsI_R_Request();

            ////    temporizador = new Timer();
            ////    temporizador.Tick += new EventHandler(Probar_R);

            ////    temporizador.Interval = iTiempo;
            ////    temporizador.Enabled = true;
            ////    temporizador.Start();
            ////}
        }

        public static void Detener()
        {
            if (temporizador != null)
            {
                temporizador.Stop();
                temporizador.Enabled = false;
                temporizador = null; 
            }
        }
        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados
        private static void Probar_R(object sender, EventArgs e)
        {
            if (IGPI.Habilitar_R)
            {
                Random xR = new Random(5);
                int iMinutos = xR.Next(1, 8);


                // Reajustar el tiempo a 45 Segundos tras cada envio 
                iTiempo = (TmSegundo * (60)) * iMinutos;
                temporizador.Stop(); 

                // IGPI.Habilitar_R = false; 
                if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.SERIAL)
                {
                    //IGPI_SerialPort.EnviarDatos(R.Solicitud);
                }

                if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
                {
                    ////////IGPI_Winsock.EnviarDatos(R.Solicitud);
                }


                // Reajustar el tiempo a 45 Segundos tras cada envio 
                temporizador.Interval = iTiempo;
                temporizador.Start(); 
            }
        }
        #endregion Funciones y Procedimientos Privados
    }
}
