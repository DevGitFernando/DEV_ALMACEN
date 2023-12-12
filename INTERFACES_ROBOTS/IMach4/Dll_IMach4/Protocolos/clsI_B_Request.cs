using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4.Protocolos
{
    public class clsI_B_Request
    {
        #region Declaracion de Varibles 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //clsLeer leer;
        clsGrabarError Error; 
        basGenerales Fg = new basGenerales(); 
        IMach4Parametros pMach;
        //string sRespuesta = "";
        string sSolicitud = "";  
        bool bResponder = true; 


        // Varibles especiales de Dialgo 
        string Name = "Protocolos.clsI_B_Request";
        // IMach4_i_State i_Estado = IMach4_i_State.None; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_B_Request():this("") 
        {
        }

        public clsI_B_Request(string Mensaje)
        {
            pMach = new IMach4Parametros();
            pMach.Dialogo = "B"; 
            Error = new clsGrabarError(IMach4.DatosApp, Name);
        }

        public clsI_B_Request(IMach4Parametros Parametros)
        {
            pMach = Parametros;
            pMach.Dialogo = "B"; 
            Error = new clsGrabarError(IMach4.DatosApp, Name);
        }

        ~clsI_B_Request()
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

        public bool GenerarRespuesta
        {
            get { return bResponder; }
        }

        public string Solicitud
        {
            get 
            {
                pMach.Dialogo = "B";
                sSolicitud = pMach.Dialogo + pMach.RequestLocationNumber + pMach.ProductCode;
                return sSolicitud; 
            }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        #endregion Funciones y Procedimientos Privados
    }
}
