using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4.Protocolos
{
    public class clsI_K_Request
    {
        #region Declaracion de Varibles 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
        clsGrabarError Error; 
        basGenerales Fg = new basGenerales(); 
        Dll_IMach4Parametros pMach;
        string sRespuesta = "";
        bool bResponder = true; 

        // Varibles especiales de Dialgo 
        string Name = "Protocolos.clsI_K_Request";
        Dll_IMach4_i_State i_Estado = Dll_IMach4_i_State.None; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_K_Request()
        {
            pMach = new Dll_IMach4Parametros();
            Error = new clsGrabarError(Dll_IMach4.DatosApp, Name); 
        }

        ~clsI_K_Request()
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

        public Dll_IMach4Parametros Parametros
        {
            get { return pMach; }
            set { pMach = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos 
        public string Repuesta()
        {
            sRespuesta = "";

            pMach.Dialogo = "K";
            //pMach.LineNumber = "10";
            sRespuesta = pMach.Dialogo + pMach.RequestLocationNumber + pMach.ProductCode + pMach.LineNumber;

            return sRespuesta; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        #endregion Funciones y Procedimientos Privados
    }
}
