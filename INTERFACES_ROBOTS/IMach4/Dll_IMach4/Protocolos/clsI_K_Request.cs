using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO; 

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
        //clsLeer leer;
        clsGrabarError Error; 
        basGenerales Fg = new basGenerales(); 
        IMach4Parametros pMach;
        string sRespuesta = "";
        //bool bResponder = true; 

        // Varibles especiales de Dialgo 
        string Name = "Protocolos.clsI_K_Request";
        // IMach4_i_State i_Estado = IMach4_i_State.None; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_K_Request()
        {
            pMach = new IMach4Parametros();
            Error = new clsGrabarError(IMach4.DatosApp, Name);
            IMach4.ArchivoSalida_K = this.FileInventario_K(); 
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

        public IMach4Parametros Parametros
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
            pMach.LineNumber = Fg.PonCeros(IMach4.Registros_K, 2); 
            sRespuesta = pMach.Dialogo + pMach.RequestLocationNumber + pMach.ProductCode + pMach.LineNumber; 
            return sRespuesta; 
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private string FileInventario_K()
        {
            DateTime dt = DateTime.Now;
            string sMarcaDeTiempo = General.FechaSinDelimitadores(dt) + "_" + General.Hora(dt, "") ; 
            string sRegresa = Path.Combine(IMach4.Directorio_Inventarios, "Inventario_MACH4__" + sMarcaDeTiempo + ".txt");

            return sRegresa; 
        }
        #endregion Funciones y Procedimientos Privados
    }
}
