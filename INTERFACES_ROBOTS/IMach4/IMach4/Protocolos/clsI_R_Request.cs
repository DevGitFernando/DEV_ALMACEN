using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using Dll_IMach4;

namespace Dll_IMach4.Protocolos
{
    public class clsI_R_Request
    {
        #region Declaracion de Variables  
        private basGenerales Fg = new basGenerales();

        private string sMensajeRecibido = ""; 
        private string sDialogo = "r";
        private string sDialogoDescripcion = "Verificación de Comunicación.";
        private string sRequestLocationNumber = "____";
        private string sProtocolVersion = "____";  
        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase 
        public clsI_R_Request(string Mensaje)
        {
            this.sMensajeRecibido = Mensaje;
            sProtocolVersion = Fg.PonCeros(0, 4); 
            RecibirPaquete(); 
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades Publicas 
        public string Dialogo
        {
            get { return sDialogo; } 
        }

        public string DialogoDescripcion
        {
            get { return sDialogoDescripcion; }
        }
        #endregion Propidades Publicas 

        #region Funciones y Procedimientos Privados 
        private void RecibirPaquete()
        {
            string sMsj = sMensajeRecibido;
            // string sMsj = sProtocolo + orden + location + puerto + estado + linea + producto + cantidad + id;

            sRequestLocationNumber = Fg.Mid(sMsj, 2, 3);
            sProtocolVersion = Fg.Mid(sMsj, 5, 4);

            General.msjUser(sRequestLocationNumber + "     " + sProtocolVersion); 

            EnviarRepuesta(); 
        }

        private void EnviarRepuesta()
        {
            string sMsj = sDialogo + sRequestLocationNumber + sProtocolVersion + "\0";
            IMachSerialPort.EnviarMensaje(sMsj); 
        }
        #endregion Funciones y Procedimientos Privados
    }
}
