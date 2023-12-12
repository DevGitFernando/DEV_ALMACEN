using System;
using System.Collections.Generic;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

namespace Dll_IMach4.Protocolos
{
    public class clsI_P_Response
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
        string Name = "Protocolos.clsI_P_Response";
        string sDialogo = "P"; 

        #endregion Declaracion de Varibles

        #region Constructores y Destructor de Clase 
        public clsI_P_Response()
        {
            pMach = new Dll_IMach4Parametros();
            pMach.Dialogo = "P";
            sRespuesta = "";

            // Establecer conexion 
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(Dll_IMach4.DatosApp, Name); 
        }

        public clsI_P_Response(Dll_IMach4Parametros Parametros)
        {
            sRespuesta = ""; 
            pMach = Parametros; 
            pMach.Dialogo = "P"; 

            // Establecer conexion 
            leer = new clsLeer(ref cnn);
            Error = new clsGrabarError(Dll_IMach4.DatosApp, Name); 

            DecodificarMensaje(); 
        }

        ~clsI_P_Response()
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
            string sNombre = "";  
            string sCodigoEAN = pMach.ProductCode.Replace("\0", "");
            int iNumEAN = 13;

            if (sCodigoEAN.Length <= 8)
                iNumEAN = 8;

            sSql = string.Format("Select top 1 replace(convert(varchar(10), getdate(), 104 ), '.', '') as ERP_Date, right( (replicate('0', {1}) + CodigoEAN), {1}) as CodigoDll_IMach4, * " +
                " From vw_Productos_CodigoEAN (NoLock) " +
                " Where right( (replicate('0', {1}) + CodigoEAN), {1}) = '{0}' ", Fg.PonCeros(sCodigoEAN, iNumEAN), iNumEAN);

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
                    // Error.GrabarError(string.Format("CodigoEAN '{0}' No Encontrado", sCodigoEAN), "DecodificarMensaje");
                }
                else
                {
                    pMach.ProductName = leer.Campo("DescripcionSal");
                    pMach.ProductCode = leer.Campo("CodigoDll_IMach4");
                    pMach.Barcode = leer.Campo("CodigoDll_IMach4"); 
                }
            }

            sNombre = pMach.ProductName;
            // pMach.Barcode = pMach.ProductCode;
            //pMach.ProductCode = "";
            pMach.PackageUnit = "";
            pMach.ShortDosageForm = ""; 

            sRespuesta = pMach.Dialogo + pMach.RequestLocationNumber +
                         pMach.ProductCode + pMach.ProductName +
                         pMach.ShortDosageForm + pMach.PackageUnit + pMach.Barcode +
                         pMach.MinimumStorageTemperature + pMach.MaximumStorageTemperature +
                         pMach.MinQuantity + pMach.MaxQuantity; 


            
            //sRespuesta = sRespuesta; //.Replace("\0", "0");
        }

        #endregion Funciones y Procedimientos Privados

    }
}
