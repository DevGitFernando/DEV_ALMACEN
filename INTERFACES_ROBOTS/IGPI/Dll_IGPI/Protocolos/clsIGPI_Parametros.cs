﻿using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IGPI.Protocolos
{
    public class IGPIParametros
    {
        #region Declaracion de Varibles
        basGenerales Fg = new basGenerales();

        string sDialogo = "";
        string sRequestLocationNumber = "";
        string sOrderNumber = "";
        string sDeliveryPort = "";
        string sProductCode = "";
        string sProductName = "";
        string sShortDosageForm = "";
        string sPackageUnit = "";
        string sMinimumStorageTemperature = "";
        string sMaximumStorageTemperature = "";
        string sSize = "";
        string sBarcode = "";
        string sBatchNumber = "";
        string sWidth = "";
        string sHeight = "";
        string sDepth = "";
        string sState = "";
        string sPriority = "";
        string sLineNumber = "";
        string sQuantity = "";
        string sQuantityPartial = "";
        string sFlag = "";
        string sStorageLocation = "";
        string sCapacity = "";
        string sHumanReadableFormat = "028\0";
        string sProtocolVersion = "";
        string sDeliveryNote = "";
        string sSupplier = "";
        string sUseByDate = "";
        string sUseByDateAgo = "";
        string sUseByDateAfter = "";
        string sStorageDate = "";
        string sStorageDateE_Rung_Ago = "";
        string sStorageDateE_Rung_After = "";
        string sERP_Date = "";
        string sSystem = "";
        string sMagazine = "";
        string sShelf = "";
        string sAttribute = "";
        string sID = "";


        string sCountryCode = "";
        string sTypeCode = "";
        string sExpiry_Date = "";

        // string sFormatoHumano = "028\0"; 

        bool bDialog = false;
        bool bRequesLocationNumber = false;

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase
        public IGPIParametros()
        {
            if (IGPI.Protocolo_Comunicacion == ProtocoloConexion_IGPI.TCP_IP)
            {
                sHumanReadableFormat = "028";
            }

            sCountryCode = IGPI.CountryCode;
            sTypeCode = IGPI.TypeCode; 

        }

        ~IGPIParametros()
        {
        }
        #endregion Constructores y Destructor de Clase

        #region Funciones y Procedimientos Publicos
        public string Cortar(ref string Mensaje, int Caracteres)
        {
            return Cortar(ref Mensaje, 1, Caracteres);
        }

        public string Cortar(ref string Mensaje, int Inicio, int Caracteres)
        {
            string sRegresa = "";
            int iLargo = Mensaje.Length;
            try
            {
                sRegresa = Fg.Mid(Mensaje, Inicio, Caracteres);
                Mensaje = Fg.Right(Mensaje, iLargo - Caracteres);
            }
            catch { }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        enum OrientacionFormato
        {
            Izquierda, Derecha 
        }

        private string Formatear(string Cadena, int Caracteres)
        {
            return Formatear(OrientacionFormato.Izquierda, Cadena, Caracteres); 
        }

        private string Formatear(OrientacionFormato Formato, string Cadena, int Caracteres)
        {
            string sCadena = Cadena.Replace("\0", "");
            string sCadenaFormato = ""; 
            int iCaracteres = Caracteres - sCadena.Length;

            for (int i = 1; i <= iCaracteres; i++)
            {
                sCadenaFormato += "\0";
                //sCadenaFormato += " ";
            }

            ////switch (Formato)
            ////{
            ////    case OrientacionFormato.Izquierda:
            ////        sCadena = sCadena + sCadenaFormato;
            ////        sCadena = Fg.Left(sCadena, Caracteres);
            ////        break;

            ////    case OrientacionFormato.Derecha:
            ////        sCadena = sCadenaFormato + sCadena;
            ////        sCadena = Fg.Right(sCadena, Caracteres);
            ////        break;
            ////}

            sCadena = sCadena + sCadenaFormato; 
            sCadena = Fg.Mid(sCadena, 1, Caracteres);
            return sCadena; 
        }

        private string Normalizar(string Valor)
        {
            string sRegresa = Valor;
            string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

            for (int i = 0; i <= consignos.Length - 1; i++)
            {
                sRegresa = sRegresa.Replace(consignos[i], sinsignos[i]);
            }

            return sRegresa; 
        }
        #endregion Funciones y Procedimientos Privados

        #region Propiedades Publicas
        private bool Get()
        {
            bRequesLocationNumber = bDialog;
            return bRequesLocationNumber;
        }

        public string Dialogo
        {
            get { return sDialogo; }
            set
            {
                //if (!bDialog)
                {
                    bDialog = true;
                    // Asegurar que solo lea un Caracter 
                    sDialogo = value.Substring(0, 1);
                }
            }
        }

        public string RequestLocationNumber
        {
            get { return sRequestLocationNumber; }
            set
            {
                //if (!bRequesLocationNumber)
                {
                    bRequesLocationNumber = true;
                    sRequestLocationNumber = Fg.PonCeros(value, 3);
                }
            }
        }

        public string OrderNumber
        {
            get { return Fg.PonCeros(sOrderNumber, 8); }
            set { sOrderNumber = Fg.PonCeros(value, 8); }
        }

        public string DeliveryPort
        {
            get { return Fg.PonCeros(sDeliveryPort, 3); }
            set { sDeliveryPort = Fg.PonCeros(value, 3); }
        }

        public string ProductCode
        {
            get { return Formatear(OrientacionFormato.Izquierda, sProductCode, 20); }
            //get { return sProductCode; }
            set { sProductCode = Fg.Left(value, 20); }
        }

        public string ProductName
        {
            get { return Formatear(sProductName, 40); }
            set { sProductName = Normalizar(Fg.Left(value, 40)); }
        }

        public string ShortDosageForm
        {
            get { return Formatear(sShortDosageForm, 3); }
            set { sShortDosageForm = value; }
        }

        //xx
        public string PackageUnit
        {
            get { return Formatear(sPackageUnit, 10); }
            set { sPackageUnit = value; }
        }

        public string MinimumStorageTemperature
        {
            get { return Formatear(sMinimumStorageTemperature, 3); }
            set { sMinimumStorageTemperature = Fg.PonCeros(value, 3); }
        }

        public string MaximumStorageTemperature
        {
            get { return Formatear(sMaximumStorageTemperature, 3); }
            set { sMaximumStorageTemperature = Fg.PonCeros(value, 3); }
        }

        public string Size
        {
            get { return Fg.PonCeros(sSize, 3); }
            set { sSize = Fg.PonCeros(value, 3); }
        }

        public string Barcode
        {
            get { return Formatear(sBarcode, 20); }
            set { sBarcode = value; }
        }

        public string BatchNumber
        {
            get { return sBatchNumber; }
            set { sBatchNumber = Fg.PonCeros(value, 20); }
        }

        public string Width
        {
            get { return Fg.PonCeros(sWidth, 3); }
            set { sWidth = Fg.PonCeros(value, 3); }
        }

        public string Height
        {
            get { return Formatear(sHeight, 3); }
            set { sHeight = Fg.PonCeros(value, 3); }
        }

        public string Depth
        {
            get { return Fg.PonCeros(sDepth, 3); }
            set { sDepth = Fg.PonCeros(value, 3); }
        }

        public string State
        {
            get { return Fg.PonCeros(sState, 2); }
            set { sState = Fg.PonCeros(value, 2); }
        }

        public string Priority
        {
            get { return Fg.PonCeros(sPriority, 1); }
            set { sPriority = Fg.PonCeros(value, 1); }
        }

        public string LineNumber
        {
            get { return Fg.PonCeros(sLineNumber, 2); }
            set { sLineNumber = Fg.PonCeros(value, 2); }
        }

        public string Quantity
        {
            get { return Fg.PonCeros(sQuantity, 5); }
            set { sQuantity = Fg.PonCeros(value, 5); }
        }

        public string QuantityPartial
        {
            get { return Fg.PonCeros(sQuantityPartial, 5); }
            set { sQuantityPartial = Fg.PonCeros(value, 5); }
        }

        public string Flag
        {
            get { return Fg.PonCeros(sFlag, 1); }
            set { sFlag = Fg.PonCeros(value, 1); }
        }

        public string StorageLocation
        {
            get { return sStorageLocation; }
            set { sStorageLocation = Fg.PonCeros(value, 3); }
        }

        public string Capacity
        {
            get { return Fg.PonCeros(sCapacity, 3); }
            set { sCapacity = Fg.PonCeros(value, 3); }
        }

        public string HumanReadableFormat
        {
            get
            {
                return sHumanReadableFormat;
            }
            // set { sHumanReadableFormat = Fg.PonCeros(value, 3); }
        }

        public string ProtocolVersion
        {
            get { return sProtocolVersion; }
            set { sProtocolVersion = Fg.PonCeros(value, 4); }
        }

        public string DeliveryNote
        {
            get { return sDeliveryNote; }
            set { sDeliveryNote = Fg.PonCeros(value, 20); }
        }

        public string Supplier
        {
            get { return sSupplier; }
            set { sSupplier = Fg.PonCeros(value, 3); }
        }

        public string UseByDate
        {
            get { return sUseByDate; }
            set { sUseByDate = Fg.PonCeros(value, 8); }
        }

        public string UseByDateAgo
        {
            get { return sUseByDateAgo; }
            set { sUseByDateAgo = Fg.PonCeros(value, 8); }
        }

        public string UseByDateAfter
        {
            get { return sUseByDateAfter; }
            set { sUseByDateAfter = Fg.PonCeros(value, 8); }
        }

        public string StorageDate
        {
            get { return sStorageDate; }
            set { sStorageDate = Fg.PonCeros(value, 8); }
        }

        public string StorageDateE_Rung_Ago
        {
            get { return sStorageDateE_Rung_Ago; }
            set { sStorageDateE_Rung_Ago = Fg.PonCeros(value, 8); }
        }

        public string StorageDateE_Rung_After
        {
            get { return sStorageDateE_Rung_After; }
            set { sStorageDateE_Rung_After = Fg.PonCeros(value, 8); }
        }

        public string ERP_Date
        {
            get { return Fg.PonCeros(sERP_Date, 6); }
            set { sERP_Date = Fg.PonCeros(value, 8); }
        }

        public string Expiry_Date
        {
            get { return Fg.PonCeros(sExpiry_Date, 6); }
            set { sExpiry_Date = Fg.PonCeros(value, 6); }
        }

        public string System
        {
            get { return sSystem; }
            set { sSystem = Fg.PonCeros(value, 3); }
        }

        public string Magazine
        {
            get { return sMagazine; }
            set { sMagazine = Fg.PonCeros(value, 3); }
        }

        public string Shelf
        {
            get { return sShelf; }
            set { sShelf = Fg.PonCeros(value, 3); }
        }

        public string Attribute
        {
            get { return sAttribute; }
            set { sAttribute = Fg.PonCeros(value, 3); }
        }

        public string ID
        {
            get { return sID; }
            set { sID = Fg.PonCeros(value, 10); }
        }

        public string CountryCode
        {
            get { return sCountryCode; }
            set { sCountryCode = Fg.PonCeros(value, 3); }
        }

        public string TypeCode
        {
            get { return sTypeCode; }
            set { sTypeCode = Fg.PonCeros(value, 2); }
        }
        #endregion Propiedades Publicas

        #region Propiedades Publicas Ext
        string sOrdersState = "";
        string sMinQuantity = "";
        string sMaxQuantity = "";

        public string OrdersState
        {
            get { return Fg.PonCeros(sOrdersState, 2); }
            set { sOrdersState = Fg.PonCeros(value, 2); }
        }

        public string MinQuantity
        {
            get { return Fg.PonCeros(sMinQuantity, 5); }
            set { sMinQuantity = Fg.PonCeros(value, 5); }
        }

        public string MaxQuantity
        {
            get { return Fg.PonCeros(sMaxQuantity, 5); }
            set { sMaxQuantity = Fg.PonCeros(value, 5); }
        }

        #endregion Propiedades Publicas Ext

    }
}
