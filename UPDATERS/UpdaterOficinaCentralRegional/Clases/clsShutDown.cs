using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

namespace UpdaterOficinaCentralRegional
{
    class clsShutDown
    {
        // string argumento = null;
        // DateTime tmp;

        /*
            Usage: shutdown [/i | /l | /s | /r | /g | /a | /h | /e] [/m computer][/t xxx][/d [p|u:]xx:yy ]
            /i Muestra la interface de usuario (GUI).
            /l Log off.
            /s Apaga la computadora.
            /r Apaga y reinicia la computadora.
            /g Apaga y reinicia la computadora. Usada para registrar aplicaciones.
            /a Aborta un apagado de sistema.
            /h Hibernar la computadora.
            /e Documenta la razon por la cual se apagara la computadora.
            /m computer Especifica el target de la computadora a apagar.
            /t xxx El periodo en que se apagar la computadora el cual se encuentre entre 0-600. Tiene por defecto asignado 30 segundos.
            /d [p|u:]xx:yy Provee la razón por la que se reinicia ó apaga el PC.
            (p indica que es planeado. u indica que es planeado por el usuario.)
         */

        public clsShutDown()
        {
            //this.argumento = argumento;
            //this.tmp = tmp;
        }

        public void Apagar()
        {
            EjecutarShut_Down("s"); 
        }

        public void Apagar_y_Reiniciar()
        {
            EjecutarShut_Down("r /f /t 0");
        }

        public void Abortar_Apagado()
        {
            EjecutarShut_Down("a");
        }

        public void Hibernar()
        {
            EjecutarShut_Down("h");
        }

        private void EjecutarShut_Down(string Argumento)
        {
            try
            {
                while (true)
                {                    
                    // if(tmp.ToLongTimeString() == DateTime.Now.ToLongTimeString())
                    {
                        Process proceso = new Process();
                        proceso.StartInfo.UseShellExecute = false;
                        proceso.StartInfo.RedirectStandardOutput = true;
                        proceso.StartInfo.FileName = "shutdown.exe";
                        proceso.StartInfo.Arguments = "/" + Argumento;
                        proceso.Start();
                        break;
                    }
                }
            }
            catch
            {
                // throw;
            }
    }

    }
}
