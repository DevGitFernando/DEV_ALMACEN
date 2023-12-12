using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4.Protocolos
{
    public class clsI_K_Response_Registros
    {
        basGenerales Fg = new basGenerales(); 
        public string CodigoEAN = "";
        public string Cantidad = "";

        public clsI_K_Response_Registros(string Registro)
        {
            Registro = Registro.Replace("\0", " "); 
            CodigoEAN = Fg.Mid(Registro, 1, 20).Trim();
            Cantidad = Fg.Mid(Registro, 21, 5).Trim(); 
        } 
    }

    public class clsI_K_Response
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
        public clsI_K_Response(string Mensaje)
        {
            pMach = new Dll_IMach4Parametros();
            pMach.Dialogo = "k";

            this.sSolicitudRecibida = Mensaje;
            DecodificarMensaje(sSolicitudRecibida);

            ////////// Preparar la respuesta 
            ////Response = new clsI_P_Response(pMach);
            ////sRespuestaA_Solicitud = Response.Repuesta;
        }

        ~clsI_K_Response()
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
            get { return bResponder; }
        }

        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private void DecodificarMensaje(string Mensaje)
        {
            bResponder = false; 
            string sMsj = Mensaje.Replace("\0", " ");
            int iDatos = 26; 
            int iVueltas = ((sMsj.Length - iDatos) / 25);
            clsI_K_Response_Registros[] k = new clsI_K_Response_Registros[iVueltas];

            // Dll_IMach4.Registros_K = 0; 
            pMach.Dialogo = pMach.Cortar(ref sMsj, 1).ToUpper();
            pMach.RequestLocationNumber = pMach.Cortar(ref sMsj, 3);
            pMach.LineNumber = pMach.Cortar(ref sMsj, 2);
            pMach.ProductCode = pMach.Cortar(ref sMsj, 20);

            for (int i = 0; i <= iVueltas - 1; i++)
            {
                bResponder = true; 
                k[i] = new clsI_K_Response_Registros(pMach.Cortar(ref sMsj, 25));
                pMach.ProductCode = k[i].CodigoEAN;
                //Dll_IMach4.Registros_K++; 
            }

            ////pMach.Dialogo = pMach.Cortar(ref sMsj, 1);
            ////pMach.RequestLocationNumber = pMach.Cortar(ref sMsj, 3); 
            ////pMach.ProductCode = pMach.Cortar(ref sMsj, 20);
            ////pMach.Barcode = pMach.Cortar(ref sMsj, 20); 

            if (iVueltas < Dll_IMach4.Registros_K)
                bResponder = false; 

            sRespuestaA_Solicitud = pMach.Dialogo + pMach.RequestLocationNumber + pMach.ProductCode + Fg.PonCeros(Dll_IMach4.Registros_K,2); 
        }
        #endregion Funciones y Procedimientos Privados
    }
}
