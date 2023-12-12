using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IGPI.Protocolos
{
    public class clsI_B_Response
    {
        #region Declaracion de Varibles 
        clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
        clsLeer leer;
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
        public clsI_B_Response(string Mensaje)
        {
            pMach = new IGPIParametros();
            Error = new clsGrabarError(IGPI.DatosApp, Name);

            DecodificarMensaje(Mensaje); 
        }

        ~clsI_B_Response()
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

            pMach.CountryCode = pMach.Cortar(ref sMsj, 3);
            pMach.TypeCode = pMach.Cortar(ref sMsj, 2); 
            pMach.ProductCode = pMach.Cortar(ref sMsj, 20);
            
            pMach.Quantity = pMach.Cortar(ref sMsj, 5);
            pMach.LineNumber = pMach.Cortar(ref sMsj, 2);
            
            pMach.StorageLocation = pMach.Cortar(ref sMsj, 10); /// REV 
            pMach.Capacity = pMach.Cortar(ref sMsj, 5);
            pMach.QuantityPartial = pMach.Cortar(ref sMsj, 5);

            pMach.StorageDate = pMach.Cortar(ref sMsj, 6);

            
            string sCodigoEAN = pMach.ProductCode.Replace("\0", "").Trim();
            int iNumEAN = 13;

            if (sCodigoEAN.Length <= 8)
            {
                iNumEAN = 8; 
            }

            ////string sSql	= string.Format(" update IGPI_StockProductos set ExistenciaIGPI = '{2}', Actualizado = 0 " + 
            ////    " Where right( (replicate('0', {1}) + CodigoEAN), {1}) = '{0}' ", 
            ////    Fg.PonCeros(sCodigoEAN, iNumEAN), iNumEAN, pMach.Quantity); 

            string sSql = string.Format("Exec spp_Mtto_IGPI_StockProductos @CodigoEAN = '{0}', @Cantidad = '{1}', @Status = 'A', @Tipo = '1', @Proceso = '{2}' ",
                Fg.PonCeros(sCodigoEAN, iNumEAN), pMach.Quantity, Dialogos.b);

            leer = new clsLeer(ref cnn);
            if (!leer.Exec(sSql))
            {
                Error.GrabarError(leer, "DecodificarMensaje()");
                Error.LogError(leer.MensajeError);
            }

        } 
        #endregion Funciones y Procedimientos Privados
    }
}
