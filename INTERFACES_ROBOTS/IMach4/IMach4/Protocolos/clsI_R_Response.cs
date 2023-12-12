using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

using Dll_IMach4;

namespace Dll_IMach4.Protocolos
{
    public class clsI_R_Response
    {
        #region Declaracion de Variables  
        private basGenerales Fg = new basGenerales();

        private string sMensajeRecibido = ""; 
        private string sDialogo = "r";
        private string sDialogoDescripcion = "Verificación de Comunicación.";
        private string sRequestLocationNumber = "001";
        private string sProtocolVersion = "1020";  
        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase 
        public clsI_R_Response(string Mensaje)
        {
            this.sMensajeRecibido = sDialogo + Fg.Mid(Mensaje, 2) + "028\0";
            sProtocolVersion = Fg.PonCeros(0, 4); 
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades Publicas 
        int iLargoMensaje = 0; 
        public string Dialogo
        {
            get { return sDialogo; } 
        }

        public string DialogoDescripcion
        {
            get { return sDialogoDescripcion; }
        }

        public string Respuesta
        {
            get { return sMensajeRecibido; }
        }

        #endregion Propidades Publicas 

        #region Funciones y Procedimientos Publicos  
        //private byte[] Respuesta()
        //{
        //    string sMsj = sMensajeRecibido + "\0" + ""; 
        //    //byte[] buffer = new byte[sMensajeRecibido.Length - 2];
        //    byte[] test = System.Text.Encoding.UTF8.GetBytes(sMsj);

        //    test[0] = (int)Dialogos.a; 
        //    ////for (int i = 1; i <= sMensajeRecibido.Length - 1; i++)
        //    ////{ 
        //    ////    //buffer[i] = Convert.ToByte(Convert.ToDecimal(Fg.Mid(sMensajeRecibido, i,1))); 
        //    ////}

        //    iLargoMensaje = test.Length;
        //    return test; 
        //}
        #endregion Funciones y Procedimientos Publicos 
    }
}
