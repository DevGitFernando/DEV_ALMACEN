using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;

using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;
using System.ServiceProcess;

namespace UpdaterOficinaCentralRegional
{
    public class clsSvrIIS
    {
        private enum StatusServicio
        {
            Iniciar = 1, Detener = 2, Pausar = 3, Reiniciar = 4
        }
        #region Declaracion de Variables
        private Process proceso;
        private string sSvrName = "";
        private string sIIS_reset = "iisreset";
        private ServiceController mySvr;

        #endregion Declaracion de Variables

        #region Constructor y Destructor de Clase
        public clsSvrIIS()
        {
            sSvrName = General.NombreEquipo;
            mySvr = new ServiceController("IISADMIN", "."); 
        }

        public clsSvrIIS(string Servidor)
        {
            sSvrName = Servidor;
            mySvr = new ServiceController("IISADMIN", "."); 
        }
        #endregion Constructor y Destructor de Clase 

        //public void Pausar()
        //{
        //    Procesar(StatusServicio.Pausar); 
        //}

        //public void Reiniciar()
        //{
        //    Procesar(StatusServicio.Reiniciar); 
        //}

        private void Procesar(StatusServicio Tipo)
        {
            int iProcesos = 0;

            while (iProcesos < 3)
            {
                try
                {
                    switch (Tipo)
                    {
                        case StatusServicio.Iniciar:
                            //mySvr.Stop();
                            mySvr.Start();
                            break;

                        case StatusServicio.Detener:
                            mySvr.Stop();
                            break;

                        case StatusServicio.Pausar:
                            mySvr.Pause();
                            break;

                        case StatusServicio.Reiniciar:
                            mySvr.Continue();
                            break;

                        default:
                            break;
                    }
                    break;
                }
                catch (Exception ex1 )
                {
                    ex1.Source = ex1.Source; 
                }
                iProcesos++; 
            }
        }

        private void IIS_Reset(string Valor)
        {
            for (int i = 1; i <= 3; i++)
            {
                try
                {
                    proceso = new Process();
                    proceso.StartInfo.FileName = sIIS_reset;
                    proceso.StartInfo.Arguments = "/" + Valor.ToUpper();
                    proceso.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    proceso.Start();
                    proceso.WaitForExit();
                    proceso = null;
                    break;
                }
                catch (Exception ex1)
                {
                    General.msjAviso(ex1.Message);
                }
            }
        }

        /// <summary>
        /// Inicia todos los servicios de Internet. 
        /// </summary>
        public void Iniciar()
        {
            IIS_Reset("start");
        }

        /// <summary>
        /// Detiene todos los servicios de Internet. 
        /// </summary>
        public void Detener()
        {
            IIS_Reset("stop");
        }

        /// <summary>
        /// Detiene y a continuación reinicia todos los servicios de Internet. 
        /// </summary>
        public void Reiniciar()
        {
            IIS_Reset("restart");
        }

        /// <summary>
        /// Reinicia el Equipo. 
        /// </summary>
        public void ReiniciarEquipo()
        {
            IIS_Reset("reboot");
        }

        /// <summary>
        /// Reinicia el equipo si se produce un error al Iniciar, Detener ó Reiniciar
        /// los Servicios de Internet. 
        /// </summary>
        public void ReiniciarEquipoOnError()
        {
            IIS_Reset("rebootonerror");
        }

        /// <summary>
        /// No forzar la terminación de servicios de Internet si no se consigue 
        /// detenerlos según el procedimiento habitual. 
        /// </summary>
        public void NoForzar()
        {
            IIS_Reset("noforce");
        }

        /// <summary>
        /// Muestra el estado de todos los servicios de Internet.  
        /// </summary>
        public void Status()
        {
            proceso = new Process();
            proceso.StartInfo.FileName = sIIS_reset;
            proceso.StartInfo.Arguments = "/STATUS";
            proceso.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            proceso.Start();
            proceso.WaitForExit();
            proceso = null;
        }

        /// <summary>
        /// Habilita el reinicio de los servicios de Internet en el sistema local. 
        /// </summary>
        public void Enable()
        {
            IIS_Reset("enable");
        }

        /// <summary>
        /// Deshabilita el reinicio de los servicios de Internet en el sistema local. 
        /// </summary>
        public void Disable()
        {
            IIS_Reset("disable");
        }
    }
}
