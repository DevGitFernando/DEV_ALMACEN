using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4.Protocolos
{
    public class clsI_I_Response
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
        string Name = "Protocolos.clsI_I_Response";
        string sDialogo = "I"; 
        Dll_IMach4_i_State i_Estado = Dll_IMach4_i_State.None;
        Dll_IMach4_I_OrdersState I_Estado_Respuesta = Dll_IMach4_I_OrdersState.None; 


        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_I_Response()
        {
            pMach = new Dll_IMach4Parametros();
            pMach.Dialogo = "I";
            sRespuesta = "";

            // Establecer conexion 
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(Dll_IMach4.DatosApp, Name); 
        }

        public clsI_I_Response(Dll_IMach4Parametros Parametros)
        {
            sRespuesta = ""; 
            pMach = Parametros; 
            pMach.Dialogo = "I"; 

            // Establecer conexion 
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(Dll_IMach4.DatosApp, Name); 

            DecodificarMensaje(); 
        }

        ~clsI_I_Response()
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

        public bool GenerarRespuesta
        {
            get { return bResponder; }
        }

        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos 
        public string Repuesta 
        {
            get { return sRespuesta;  }
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private void DecodificarMensaje()
        {
            string sSql = ""; 
            string sCodigoEAN = pMach.ProductCode.Replace("\0", "");
            int iNumEAN = 13;

            if (sCodigoEAN.Length <= 8)
                iNumEAN = 8;

            sSql = string.Format("Select top 1 replace(convert(varchar(10), getdate(), 104 ), '.', '') as ERP_Date, right( (replicate('0', {1}) + CodigoEAN), {1}) as CodigoDll_IMach4, * " +
                " From vw_Productos_CodigoEAN (NoLock) " +
                " Where right( (replicate('0', {1}) + CodigoEAN), {1}) = '{0}' ", Fg.PonCeros(sCodigoEAN, iNumEAN), iNumEAN);


            i_Estado = (Dll_IMach4_i_State)(Convert.ToInt32(pMach.State));
            I_Estado_Respuesta = Dll_IMach4_I_OrdersState.NotAllowToStore;

            if ((i_Estado == Dll_IMach4_i_State.ConfirmationPackageStored || i_Estado == Dll_IMach4_i_State.ConfirmationPackageNotStored))
            {
                bResponder = false; 
            }
            else 
            {
                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, "DecodificarMensaje()");
                    Error.LogError(leer.MensajeError);
                }
                else
                {
                    bResponder = true; 
                    if (!leer.Leer())
                    {
                        I_Estado_Respuesta = Dll_IMach4_I_OrdersState.NotAllowToStore;
                        //pMach.ERP_Date = ""; 
                    }
                    else
                    {
                        I_Estado_Respuesta = Dll_IMach4_I_OrdersState.AllowToStore;
                        //pMach.ERP_Date = "";
                        pMach.OrdersState = Fg.PonCeros((int)I_Estado_Respuesta, 2); 
                    }
                }
            }

            pMach.ERP_Date = pMach.UseByDate;
            pMach.OrdersState = Fg.PonCeros((int)I_Estado_Respuesta,2); 
            sRespuesta = pMach.Dialogo + pMach.OrderNumber + pMach.RequestLocationNumber + 
                pMach.ProductCode + pMach.Quantity + pMach.ERP_Date + pMach.OrdersState  + "028Cargando";
            
            sRespuesta += "\0";
        }

        #endregion Funciones y Procedimientos Privados

    }
}
