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
        IMach4Parametros pMach;
        string sRespuesta = "";
        bool bResponder = true; 

        // Varibles especiales de Dialgo 
        string Name = "Protocolos.clsI_I_Response";
        // string sDialogo = "I"; 
        IMach4_i_State i_Estado = IMach4_i_State.None;
        IMach4_I_OrdersState I_Estado_Respuesta = IMach4_I_OrdersState.None; 


        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_I_Response()
        {
            pMach = new IMach4Parametros();
            pMach.Dialogo = "I";
            sRespuesta = "";

            // Establecer conexion 
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(IMach4.DatosApp, Name); 
        }

        public clsI_I_Response(IMach4Parametros Parametros)
        {
            sRespuesta = ""; 
            pMach = Parametros; 
            pMach.Dialogo = "I"; 

            // Establecer conexion 
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(IMach4.DatosApp, Name); 

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

        public IMach4Parametros Parametros
        {
            get { return pMach; }
            set { pMach = value; }
        }

        public bool GenerarRespuesta
        {
            get { return bResponder; }
        }

        public IMach4_i_State Status
        {
            get { return i_Estado; }
        }

        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos 
        public string Respuesta 
        {
            get { return sRespuesta;  }
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private void DecodificarMensaje()
        {
            string sSql = ""; 
            string sCodigoEAN = pMach.ProductCode.Replace("\0", "").Trim();
            int iNumEAN = 13;
            string sRespuestaPeticion = "";


            if (sCodigoEAN.Length <= 8)
            {
                iNumEAN = 8;
            }

            ////sSql = string.Format("Select top 1 replace(convert(varchar(10), getdate(), 104 ), '.', '') as ERP_Date, " + 
            ////    " right( (replicate('0', {1}) + CodigoEAN), {1}) as CodigoDll_IMach4, * " +
            ////    " From vw_Productos_CodigoEAN (NoLock) " + 
            ////    " Where right( (replicate('0', {1}) + CodigoEAN), {1}) = '{0}' ", Fg.PonCeros(sCodigoEAN, iNumEAN), iNumEAN);


            sSql = string.Format("Select top 1 " +
                " right( (replicate('0', {1}) + CodigoEAN), {1}) as Codigo, " +
                " ERP_Date, IdProducto, CodigoEAN, Descripcion, ClaveSSA, DescripcionClave, Status, StatusAux, StatusIMach, StatusIMachAux " + 
                " From vw_IMach_CFGC_Productos (NoLock) " +
                " Where right( (replicate('0', {1}) + CodigoEAN), {1}) = '{0}' ", Fg.PonCeros(sCodigoEAN, iNumEAN), iNumEAN);


            i_Estado = (IMach4_i_State)(Convert.ToInt32(pMach.State.Trim()));
            I_Estado_Respuesta = IMach4_I_OrdersState.NotAllowToStore;

            if ((i_Estado == IMach4_i_State.ConfirmationPackageStored || i_Estado == IMach4_i_State.ConfirmationPackageNotStored))
            {
                bResponder = false;
                if (i_Estado == IMach4_i_State.ConfirmationPackageStored)
                {
                    sSql = string.Format("Exec spp_Mtto_IMach_StockProductos @CodigoEAN = '{0}', @Cantidad = '{1}', @Status = 'A', @Tipo = '1', @Proceso = '{2}' ",
                        pMach.ProductCode.Trim(), pMach.Quantity.Trim(), Dialogos.i );
                    if (!leer.Exec(sSql))
                    {
                        Error.GrabarError(leer, "DecodificarMensaje()");
                        Error.LogError(leer.MensajeError);
                    }
                }
            }
            else if (
                   i_Estado == IMach4_i_State.EntryOfGoods
                || i_Estado == IMach4_i_State.InternalRearrangementsOfGoods
                || i_Estado == IMach4_i_State.StartNewDelivery
                || i_Estado == IMach4_i_State.EndOfDelivery
                || i_Estado == IMach4_i_State.EntryOfGoodsWithMaintenanceOfTheBachCoding
                || i_Estado == IMach4_i_State.InternalRearrangementOfGoodsWithMaintenanceOfBachCoding)
            {

                I_Estado_Respuesta = IMach4_I_OrdersState.AllowToStore;
                pMach.OrdersState = Fg.PonCeros((int)I_Estado_Respuesta, 2);

                if (sCodigoEAN == "")
                {
                    sRespuestaPeticion = Respuesta_A_Peticion(i_Estado);
                }
                else
                {
                    // Es solicitud de informacion 
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
                            I_Estado_Respuesta = IMach4_I_OrdersState.NotAllowToStore;
                            pMach.OrdersState = Fg.PonCeros((int)I_Estado_Respuesta, 2);
                            //pMach.ERP_Date = ""; 
                        }
                        else
                        {
                            ////I_Estado_Respuesta = IMach4_I_OrdersState.AllowToStore;
                            ////pMach.OrdersState = Fg.PonCeros((int)I_Estado_Respuesta, 2);

                            // Final 
                            I_Estado_Respuesta = (IMach4_I_OrdersState)(Convert.ToInt32(leer.Campo("StatusIMach")));
                            pMach.OrdersState = leer.Campo("StatusIMach");
                        }
                    }
                    sRespuestaPeticion = Respuesta_A_Peticion(I_Estado_Respuesta);
                }
            }

            pMach.ERP_Date = pMach.UseByDate;
            //pMach.OrdersState = Fg.PonCeros((int)I_Estado_Respuesta,2); 

            sRespuesta = pMach.Dialogo + pMach.OrderNumber + pMach.RequestLocationNumber +
                pMach.ProductCode + pMach.Quantity + pMach.ERP_Date + pMach.OrdersState +
                pMach.HumanReadableFormat + sRespuestaPeticion; // "Cargando Producto";

            if (IMach4.Protocolo_Comunicacion == ProtocoloConexion_IMach.SERIAL)
            {
                sRespuesta += "\0";
            }
        }

        private string Respuesta_A_Peticion(IMach4_I_OrdersState TipoRespuesta)
        {
            string sRegresa = "";

            switch (TipoRespuesta)
            {
                case IMach4_I_OrdersState.AllowToStore:
                    sRegresa = "Ingreso permitido.";
                    break;

                case IMach4_I_OrdersState.AllowToStoreWithUseByDate:
                    sRegresa = "Ingreso permitido con caducidades";
                    break;

                case IMach4_I_OrdersState.None:
                    sRegresa = "Ninguna.";
                    break;

                case IMach4_I_OrdersState.NoSettingOfMaintenanceOfBachCode:
                    sRegresa = "Sin instrucciones de mantenimiento de codigos";
                    break;

                case IMach4_I_OrdersState.NotAllowToStore:
                    sRegresa = "Ingreso no permitido.";
                    break;

                case IMach4_I_OrdersState.StoreProductoInFridge:
                    sRegresa = "Almacenar producto en refrigerador";
                    break;
            }

            return sRegresa;
        }

        private string Respuesta_A_Peticion(IMach4_i_State TipoPeticion)
        {
            string sRegresa = "";

            switch (TipoPeticion)
            {
                case IMach4_i_State.ConfirmationPackageNotStored:
                    sRegresa = "Paquete no almacenado.";
                    break;

                case IMach4_i_State.ConfirmationPackageStored:
                    sRegresa = "Paquete almacenado.";
                    break;

                case IMach4_i_State.EndOfDelivery:
                    sRegresa = "Entrega finalizada.";
                    break;

                case IMach4_i_State.EntryOfGoods:
                    sRegresa = "Ingreso permitido.";
                    break;

                case IMach4_i_State.EntryOfGoodsWithMaintenanceOfTheBachCoding:
                    sRegresa = "Ingreso permitido con mantenimiento de codigos.";
                    break;

                case IMach4_i_State.InternalRearrangementOfGoodsWithMaintenanceOfBachCoding:
                    sRegresa = "Reingreso de mercancia permitida con mantenimiento de codigos.";
                    break;

                case IMach4_i_State.InternalRearrangementsOfGoods:
                    sRegresa = "Movimiento interno de stock.";
                    break;

                case IMach4_i_State.None:
                    sRegresa = "Ninguna.";
                    break;

                case IMach4_i_State.StartNewDelivery:
                    sRegresa = "Iniciando nueva entrega.";
                    break;
            }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados

    }
}
