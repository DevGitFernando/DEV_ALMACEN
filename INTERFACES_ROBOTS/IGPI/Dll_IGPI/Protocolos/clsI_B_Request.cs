using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IGPI.Protocolos
{
    public class clsI_B_Request
    {
        #region Declaracion de Varibles 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //clsLeer leer;
        clsGrabarError Error; 
        basGenerales Fg = new basGenerales(); 
        IGPIParametros pMach;
        //string sRespuesta = "";
        string sSolicitud = "";  
        bool bResponder = true; 


        // Varibles especiales de Dialgo 
        string Name = "Protocolos.clsI_B_Request";
        // IGPI_i_State i_Estado = IGPI_i_State.None; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_B_Request():this("") 
        {
        }

        public clsI_B_Request(string Mensaje)
        {
            pMach = new IGPIParametros();
            pMach.Dialogo = "B"; 
            Error = new clsGrabarError(IGPI.DatosApp, Name);
        }

        public clsI_B_Request(IGPIParametros Parametros)
        {
            pMach = Parametros;
            pMach.Dialogo = "B"; 
            Error = new clsGrabarError(IGPI.DatosApp, Name);
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

        public IGPIParametros Parametros
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
                sSolicitud = pMach.Dialogo + pMach.RequestLocationNumber + pMach.CountryCode + pMach.TypeCode + pMach.ProductCode + "";

                sSolicitud = pMach.Dialogo + pMach.RequestLocationNumber + "039" + pMach.TypeCode + pMach.ProductCode + "";
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
