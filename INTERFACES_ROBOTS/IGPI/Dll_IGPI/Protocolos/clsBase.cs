using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IGPI.Protocolos
{
    public class clsBase
    {
        #region Declaracion de Varibles
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        // clsLeer leer;
        clsGrabarError Error;
        basGenerales Fg = new basGenerales();
        IGPIParametros pMach;
        // string sRespuesta = "";
        // string sSolicitud = ""; 
        // bool bResponder = false; 


        // Varibles especiales de Dialgo 
        string Name = "Protocolos.xxx";
        // IGPI_i_State i_Estado = IGPI_i_State.None; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase
        public clsBase()
        {
            pMach = new IGPIParametros();
            Error = new clsGrabarError(IGPI.DatosApp, Name);
        }

        ~clsBase()
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
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        #endregion Funciones y Procedimientos Privados
    }
}
