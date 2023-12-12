using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4.Protocolos
{
    public class clsI_G_Response
    {
        #region Declaracion de Varibles 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrabarError Error; 
        basGenerales Fg = new basGenerales(); 
        IMach4Parametros pMach;
        //string sRespuesta = "";
        string sSolicitud = "";
        // bool bResponder = false; 


        // Varibles especiales de Dialgo 
        string Name = "Protocolos.clsI_G_Response";
        // IMach4_i_State i_Estado = IMach4_i_State.None; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_G_Response(string Mensaje)
        {
            pMach = new IMach4Parametros();
            Error = new clsGrabarError(IMach4.DatosApp, Name);

            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn); 

            DecodificarMensaje(Mensaje);  
        }

        ~clsI_G_Response()
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

        public IMach4Parametros Parametros
        {
            get { return pMach; }
            set { pMach = value; }
        }

        public string Solicitud
        {
            get 
            {
                pMach.Dialogo = "g";
                sSolicitud = pMach.Dialogo + pMach.RequestLocationNumber + pMach.ProductCode;

                return sSolicitud; 
            }
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

            pMach.Size = pMach.Cortar(ref sMsj, 5);
            pMach.Width = pMach.Cortar(ref sMsj, 5);
            pMach.Height = pMach.Cortar(ref sMsj, 5);
            pMach.Depth = pMach.Cortar(ref sMsj, 5);

            Guardar(); 
        }

        private void Guardar()
        {
            int iIntentos = 0;
            string sSql = string.Format("Exec spp_Mtto_IMach_Volumetria '{0}', '{1}', '{2}', '{3}', '{4}' ",
                pMach.ProductCode, 0, pMach.Width, pMach.Height, pMach.Depth); 

            while (iIntentos < 3)
            {
                iIntentos++;
                if (!leer.Exec(sSql))
                {
                }
                else
                {
                    iIntentos = 5; 
                }
            }
        }
        #endregion Funciones y Procedimientos Privados
    }
}
