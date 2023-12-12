using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4.Protocolos
{
    class clsI_I_Request
    {
        #region Declaracion de Varibles 
        clsI_I_Response Response; 
        IMach4Parametros pMach;
        basGenerales Fg = new basGenerales(); 
        string sSolicitudRecibida = "";
        string sRespuestaA_Solicitud = "";
        // bool bResponder = false; 

        IMach4_i_State i_Estado = IMach4_i_State.None;

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_I_Request(string Mensaje)
        {
            pMach = new IMach4Parametros();
            pMach.Dialogo = "i";

            this.sSolicitudRecibida = Mensaje;
            DecodificarMensaje(sSolicitudRecibida);

            // Preparar la respuesta 
            Response = new clsI_I_Response(pMach);
            i_Estado = Response.Status; 
            sRespuestaA_Solicitud = Response.Respuesta; 
        }

        ~clsI_I_Request()
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

        public IMach4Parametros Parametros
        {
            get { return pMach; }
            set { pMach = value; }
        }

        public IMach4_i_State Status
        {
            get { return i_Estado; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private void DecodificarMensaje(string Mensaje)
        { 
            string sMsj = Mensaje;

            pMach.Dialogo = pMach.Cortar(ref sMsj, 1);
            pMach.OrderNumber = pMach.Cortar(ref sMsj, 8);
            pMach.RequestLocationNumber = pMach.Cortar(ref sMsj, 3);


            pMach.DeliveryNote = pMach.Cortar(ref sMsj, 20);
            pMach.ProductCode = pMach.Cortar(ref sMsj, 20);
            pMach.Barcode = pMach.Cortar(ref sMsj, 20);
            pMach.BatchNumber = pMach.Cortar(ref sMsj, 20);
            pMach.Quantity = pMach.Cortar(ref sMsj, 5);
            pMach.UseByDate = pMach.Cortar(ref sMsj, 8);
            pMach.StorageDate = pMach.Cortar(ref sMsj, 8);
            pMach.State = pMach.Cortar(ref sMsj, 2); 
        } 
        #endregion Funciones y Procedimientos Privados
    }
}
