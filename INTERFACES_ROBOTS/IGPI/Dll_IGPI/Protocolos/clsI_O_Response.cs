using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IGPI.Protocolos
{
    public class clsI_O_Response
    {
        #region Declaracion de Varibles 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //clsLeer leer;
        clsGrabarError Error; 
        basGenerales Fg = new basGenerales(); 
        IGPIParametros pMach;
        // string sRespuesta = "";
        // string sSolicitud = ""; 
        // bool bResponder = false;
        string sMensajeMEDIMAT = ""; 

        // Varibles especiales de Dialgo 
        string Name = "Protocolos.clsI_O_Response";
        // IGPI_i_State i_Estado = IGPI_i_State.None; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_O_Response(string Mensaje)
        {
            pMach = new IGPIParametros();
            Error = new clsGrabarError(IGPI.DatosApp, Name);

            DecodificarMensaje(Mensaje); 
        }

        ~clsI_O_Response()
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
        private void DecodificarMensaje(string Mensaje)
        {
            string sMsj = Mensaje;

            pMach.Dialogo = pMach.Cortar(ref sMsj, 1);
            pMach.RequestLocationNumber = pMach.Cortar(ref sMsj, 3);           
            pMach.OrderNumber = pMach.Cortar(ref sMsj, 8);
            pMach.State = pMach.Cortar(ref sMsj, 2);

            // Final de la cadena 
            sMensajeMEDIMAT = pMach.Cortar(ref sMsj, 3);
            sMensajeMEDIMAT = sMsj; 

        } 
        #endregion Funciones y Procedimientos Privados
    }
}
