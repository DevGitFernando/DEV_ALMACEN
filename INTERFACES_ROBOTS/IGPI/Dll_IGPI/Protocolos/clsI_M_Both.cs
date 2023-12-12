using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IGPI.Protocolos
{
    public class clsI_M_Both
    {
        #region Declaracion de Varibles 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //clsLeer leer;
        clsGrabarError Error; 
        basGenerales Fg = new basGenerales(); 
        IGPIParametros pMach;
        // string sRespuesta = "";
        string sSolicitud = "";
        // bool bResponder = false; 


        // Varibles especiales de Dialgo 
        string Name = "Protocolos.clsI_M_Both";
        // IGPI_i_State i_Estado = IGPI_i_State.None; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_M_Both()
        {
            pMach = new IGPIParametros();
            Error = new clsGrabarError(IGPI.DatosApp, Name); 
        }

        public clsI_M_Both(string Mensaje)
        {
            pMach = new IGPIParametros();
            Error = new clsGrabarError(IGPI.DatosApp, Name);
            DecodificarMensaje(Mensaje); 
        }

        ~clsI_M_Both()
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

        public string Solicitud
        {
            get 
            {
                pMach.Dialogo = "M";
                sSolicitud = pMach.Dialogo + pMach.RequestLocationNumber + pMach.ID + pMach.Attribute.ToUpper();
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
            pMach.ID = pMach.Cortar(ref sMsj, 10);
            pMach.Attribute = pMach.Cortar(ref sMsj, 1).ToUpper();

        } 
        #endregion Funciones y Procedimientos Privados
    }
}
