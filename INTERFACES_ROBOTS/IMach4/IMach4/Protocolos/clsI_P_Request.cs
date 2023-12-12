using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4.Protocolos
{
    class clsI_P_Request
    {
        #region Declaracion de Varibles 
        clsI_P_Response Response; 
        Dll_IMach4Parametros pMach;
        basGenerales Fg = new basGenerales(); 
        string sSolicitudRecibida = "";
        string sRespuestaA_Solicitud = "";
        bool bResponder = false; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_P_Request(string Mensaje)
        {
            pMach = new Dll_IMach4Parametros();
            pMach.Dialogo = "p";

            this.sSolicitudRecibida = Mensaje;
            DecodificarMensaje(sSolicitudRecibida);

            ////// Preparar la respuesta 
            Response = new clsI_P_Response(pMach);
            sRespuestaA_Solicitud = Response.Repuesta;
        }

        ~clsI_P_Request()
        { 
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades Publicas 
        public string Dialogo
        {
            get { return pMach.Dialogo; }
            set { pMach.Dialogo = value; }
        }

        public string RequestLocationNumber
        {
            get { return pMach.RequestLocationNumber; }
            set { pMach.RequestLocationNumber = value; }
        }

        public string Respuesta
        {
            get { return sRespuestaA_Solicitud; }
        }

        public bool GenerarRespuesta
        {
            get { return Response.GenerarRespuesta;  }
        }

        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private void DecodificarMensaje(string Mensaje)
        { 
            string sMsj = Mensaje;

            pMach.Dialogo = pMach.Cortar(ref sMsj, 1);
            pMach.RequestLocationNumber = pMach.Cortar(ref sMsj, 3); 
            pMach.ProductCode = pMach.Cortar(ref sMsj, 20);
            pMach.Barcode = pMach.Cortar(ref sMsj, 20); 
        } 
        #endregion Funciones y Procedimientos Privados
    }
}
