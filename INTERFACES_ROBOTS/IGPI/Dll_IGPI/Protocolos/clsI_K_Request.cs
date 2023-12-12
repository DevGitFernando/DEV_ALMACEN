using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IGPI.Protocolos
{
    public class clsI_K_Request
    {
        #region Declaracion de Varibles 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //clsLeer leer;
        clsGrabarError Error; 
        basGenerales Fg = new basGenerales(); 
        IGPIParametros pMach;
        string sRespuesta = "";
        //bool bResponder = true; 

        // Varibles especiales de Dialgo 
        string Name = "Protocolos.clsI_K_Request";
        // IGPI_i_State i_Estado = IGPI_i_State.None; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_K_Request()
        {
            pMach = new IGPIParametros();
            Error = new clsGrabarError(IGPI.DatosApp, Name);
            IGPI.ArchivoSalida_K = this.FileInventario_K(); 
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

        public IGPIParametros Parametros
        {
            get { return pMach; }
            set { pMach = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos 
        public string Respuesta()
        {
            sRespuesta = ""; 

            pMach.Dialogo = "K"; 
            pMach.ProductCode = "0"; 
            pMach.LineNumber = Fg.PonCeros(IGPI.Registros_K, 2);

            pMach.CountryCode = IGPI.CountryCode;
            pMach.TypeCode = IGPI.TypeCode;

            sRespuesta = pMach.Dialogo + pMach.RequestLocationNumber + pMach.CountryCode + pMach.TypeCode + pMach.ProductCode + "10";
            sRespuesta = pMach.Dialogo + pMach.RequestLocationNumber + pMach.CountryCode + pMach.TypeCode + "                    " + pMach.LineNumber;  

            return sRespuesta; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private string FileInventario_K()
        {
            DateTime dt = DateTime.Now;
            string sMarcaDeTiempo = General.FechaSinDelimitadores(dt) + "_" + General.Hora(dt, "") ; 
            string sRegresa = Path.Combine(Application.StartupPath, "Inventario_GPI__" + sMarcaDeTiempo + ".txt");

            return sRegresa; 
        }
        #endregion Funciones y Procedimientos Privados
    }
}
