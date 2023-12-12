using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4.Protocolos
{
    public class clsI_F_Both
    {
        #region Declaracion de Varibles 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        //clsLeer leer;
        clsGrabarError Error; 
        basGenerales Fg = new basGenerales(); 
        IMach4Parametros pMach;
        // string sRespuesta = "";
        // string sSolicitud = ""; 
        // bool bResponder = false; 


        // Varibles especiales de Dialgo 
        string Name = "Protocolos.clsI_M_Both";
        // IMach4_i_State i_Estado = IMach4_i_State.None; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_F_Both()
        {
            pMach = new IMach4Parametros();
            Error = new clsGrabarError(IMach4.DatosApp, Name); 
        }

        public clsI_F_Both(string Mensaje)
        {
            pMach = new IMach4Parametros();
            Error = new clsGrabarError(IMach4.DatosApp, Name);
            DecodificarMensaje(Mensaje); 
        }

        ~clsI_F_Both()
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
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private void DecodificarMensaje(string Mensaje)
        {
            string sMsj = Mensaje;

            pMach.Dialogo = pMach.Cortar(ref sMsj, 1);
            pMach.RequestLocationNumber = pMach.Cortar(ref sMsj, 3);
            pMach.ProductCode = pMach.Cortar(ref sMsj, 20);

            pMach.Barcode = pMach.Cortar(ref sMsj, 20);
            pMach.Supplier = pMach.Cortar(ref sMsj, 20);
            pMach.DeliveryNote = pMach.Cortar(ref sMsj, 20);


            pMach.UseByDateAgo = pMach.Cortar(ref sMsj, 8);
            pMach.UseByDateAfter = pMach.Cortar(ref sMsj, 8); 
            pMach.StorageDateE_Rung_Ago = pMach.Cortar(ref sMsj, 8);
            pMach.StorageDateE_Rung_After = pMach.Cortar(ref sMsj, 8);

            pMach.BatchNumber = pMach.Cortar(ref sMsj, 20);
            pMach.Attribute = pMach.Cortar(ref sMsj, 1);
            pMach.System = pMach.Cortar(ref sMsj, 3);
            pMach.Magazine = pMach.Cortar(ref sMsj, 3);
            pMach.Shelf = pMach.Cortar(ref sMsj, 3);

        } 
        #endregion Funciones y Procedimientos Privados
    }
}
