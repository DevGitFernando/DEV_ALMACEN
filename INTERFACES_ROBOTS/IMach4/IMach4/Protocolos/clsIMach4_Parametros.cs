using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4.Protocolos
{
    public class Dll_IMach4Parametros
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
        string sFlag = ""; 
        string sStorageLocation = ""; 
        string sCapacity = ""; 
        string sHumanReadableFormat = ""; 
        string sProtocolVersion = ""; 
        string sDeliveryNote = ""; 
        string sSupplier = ""; 
        string sUseByDate = ""; 
        string sStorageDate = ""; 
        string sERP_Date = ""; 
        string sSystem = ""; 
        string sMagazine = "";
        string sShelf = "";
        string sAttribute = "";
        string sID = ""; 

        bool bDialog = false;
        bool bRequesLocationNumber = false; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase
        public Dll_IMach4Parametros()
        {
        }

        ~Dll_IMach4Parametros()
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
        private string Formatear(string Cadena, int Caracteres)
        {
            string sCadena = Cadena.Replace("\0", "");
            int iCaracteres = Caracteres - sCadena.Length;

            for (int i = 1; i <= iCaracteres; i++)
            {
                sCadena += "\0"; 
            }

            return sCadena; 
        }
        #endregion Funciones y Procedimientos Privados

        #region Propiedades Publicas
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
            get { return Fg.PonCeros(sOrderNumber,8); }
            set { sOrderNumber = Fg.PonCeros(value, 8); }
        }

        public string DeliveryPort
        {
            get { return Fg.PonCeros(sDeliveryPort,3); }
            set { sDeliveryPort = Fg.PonCeros(value, 3); }
        }

        public string ProductCode
        {
            get { return Formatear(sProductCode, 20); }
            set { sProductCode = Fg.Left(value, 20); }
        }

        public string ProductName
        {
            get { return Formatear(sProductName, 50); }
            set { sProductName = Fg.Left(value, 50); }
        }

        public string ShortDosageForm
        {
            get { return Formatear(sShortDosageForm,3); }
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
            set { sMaximumStorageTemperature= Fg.PonCeros(value, 3); }
        }

        public string Size
        {
            get { return Fg.PonCeros(sSize,3); }
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
            get { return Fg.PonCeros(sWidth,3); }
            set { sWidth = Fg.PonCeros(value, 3); }
        }

        public string Height
        {
            get { return Formatear(sHeight,3); }
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
            get { return Fg.PonCeros(sLineNumber,2); }
            set { sLineNumber = Fg.PonCeros(value, 2); }
        }

        public string Quantity
        {
            get { return Fg.PonCeros(sQuantity,5); }
            set { sQuantity = Fg.PonCeros(value, 5); }
        }

        public string Flag
        {
            get { return Fg.PonCeros(sFlag,1); }
            set { sFlag = Fg.PonCeros(value, 1); }
        }

        public string StorageLocation
        {
            get { return sStorageLocation; }
            set { sStorageLocation = Fg.PonCeros(value, 3); }
        }

        public string Capacity
        {
            get { return Fg.PonCeros(sCapacity,3); }
            set { sCapacity = Fg.PonCeros(value, 3); }
        }

        public string HumanReadableFormat
        {
            get { return sHumanReadableFormat; }
            set { sHumanReadableFormat = Fg.PonCeros(value, 3); }
        }

        public string ProtocolVersion
        {
            get { return sProtocolVersion; }
            set { sProtocolVersion = Fg.PonCeros(value, 3); }
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
            set { sUseByDate  = Fg.PonCeros(value, 8); }
        }

        public string StorageDate
        {
            get { return sStorageDate; }
            set { sStorageDate = Fg.PonCeros(value, 8); }
        }

        public string ERP_Date
        {
            get { return sERP_Date; }
            set { sERP_Date = Fg.PonCeros(value, 8); }
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
            set { sAttribute= Fg.PonCeros(value, 3); }
        }

        public string ID
        {
            get { return sID; }
            set { sID = Fg.PonCeros(value, 10); }
        }


        #endregion Propiedades Publicas

        #region Propiedades Publicas Ext 
        string sOrdersState = "";
        string sMinQuantity = "";
        string sMaxQuantity = "";  

        public string OrdersState 
        {
            get { return Fg.PonCeros(sOrdersState,2); }
            set { sOrdersState = Fg.PonCeros(value, 2); } 
        }

        public string MinQuantity
        {
            get { return Fg.PonCeros(sMinQuantity, 5); }
            set { sMinQuantity = Fg.PonCeros(value, 5); }
        }

        public string MaxQuantity
        {
            get { return Fg.PonCeros(sMaxQuantity,5); }
            set { sMaxQuantity = Fg.PonCeros(value, 5); }
        }

        #endregion Propiedades Publicas Ext 

    }
}
