using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IGPI.Protocolos
{
    public class clsI_A_Response
    {
        #region Declaracion de Varibles 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrabarError Error; 
        basGenerales Fg = new basGenerales(); 
        IGPIParametros pMach;
        string sRespuesta = "";
        string sSolicitudRecibida = "";
        string sRespuestaA_Solicitud = ""; 
        bool bResponder = IGPI.Habilitar_B__Al_Dispensar; //false;
        clsI_B_Request Response; 

        // Varibles especiales de Dialgo 
        string Name = "Protocolos.clsI_A_Response";

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_A_Response(string Mensaje)
        {
            pMach = new IGPIParametros(); 
            leer = new clsLeer(ref cnn); 
            Error = new clsGrabarError(General.DatosConexion, IGPI.DatosApp, Name);

            this.sSolicitudRecibida = Mensaje;
            DecodificarMensaje(Mensaje);

            Response = new clsI_B_Request(pMach);
            sRespuestaA_Solicitud = Response.Solicitud;
            bResponder = Response.GenerarRespuesta;
            bResponder = IGPI.Habilitar_B__Al_Dispensar; 
        }

        ~clsI_A_Response()
        {
            Error = null;
            leer = null;
            cnn = null; 
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

        public IGPIParametros Parametros
        {
            get { return pMach; }
            set { pMach = value; }
        }

        public bool GenerarRespuesta
        {
            get { return bResponder;  }
        }

        public string Respuesta
        {
            get { return sRespuestaA_Solicitud; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private void DecodificarMensaje(string Mensaje)
        {
            string sMsj = Mensaje.Replace("\0", " ");

            pMach.Dialogo = pMach.Cortar(ref sMsj, 1).ToUpper();
            pMach.OrderNumber = pMach.Cortar(ref sMsj, 8);
            pMach.RequestLocationNumber = pMach.Cortar(ref sMsj, 3);
            pMach.DeliveryPort = pMach.Cortar(ref sMsj, 3);
            pMach.State = pMach.Cortar(ref sMsj, 2);
            pMach.LineNumber = pMach.Cortar(ref sMsj, 2);
            pMach.CountryCode = pMach.Cortar(ref sMsj, 3);
            pMach.TypeCode = pMach.Cortar(ref sMsj, 2);
            pMach.ProductCode = pMach.Cortar(ref sMsj, 20);
            pMach.Quantity = pMach.Cortar(ref sMsj, 5);
            pMach.ID = pMach.Cortar(ref sMsj, 10);

            RegistrarRespuesta(); 
        }

        private void RegistrarRespuesta()
        {
            string sSql = string.Format(" Exec spp_Mtto_IGPI_SolicitudesProductosRespuesta '{0}', '{1}', '{2}', '{3}' ",
                pMach.OrderNumber.Trim(), pMach.ProductCode.Trim(), pMach.State.Trim(), pMach.Quantity.Trim() );

            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "RegistrarRespuesta()"); 
            }
        }

        #endregion Funciones y Procedimientos Privados
    }
}
