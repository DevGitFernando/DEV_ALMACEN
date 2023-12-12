#region USING
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
#endregion USING

namespace Dll_IFacturacion.XSA
{
    public class xsaCfdiEmbarque
    {
        #region Declaración de Variables
        xsaTiposDeRegistro tipoSeccion = xsaTiposDeRegistro.DireccionEmbarque;
        string sIdReceptor = "";
        string sRFC = "";
        string sNombre = "";
        string sPais = "";
        string sCalle = "";
        string sNumExterior = "";
        string sNumInterior = "";
        string sColonia = "";
        string sLocalidad = "";
        string sReferencia = "";
        string sMunicipio = "";
        string sEstado = "";
        string sCodigoPostal = "";
        int iRenglones = 0; 
        #endregion Declaración de Variables 

        #region Constructores y Destructor de Clase 
        public xsaCfdiEmbarque()
        { 
        }

        ~xsaCfdiEmbarque()
        { 
        }
        #endregion Constructores y Destructor de Clase 

        #region Propiedades 
        public int Renglones
        {
            get { return iRenglones; }
        }

        public string Cadena
        {
            get { return GenerarCadena(); }
        }

        public string IdReceptor
        {
            get { return sIdReceptor; }
            set { sIdReceptor = value; }
        }

        public string RFC
        {
            get { return sRFC; }
            set { sRFC = value; }
        }

        public string Nombre
        {
            get { return sNombre; }
            set { sNombre = value; }
        }

        public string Pais
        {
            get { return sPais; }
            set { sPais = value; }
        }

        public string Calle
        {
            get { return sCalle; }
            set { sCalle = value; }
        }

        public string NumExterior
        {
            get { return sNumExterior; }
            set { sNumExterior = value; }
        }

        public string NumInterior
        {
            get { return sNumInterior; }
            set { sNumInterior = value; }
        }

        public string Colonia
        {
            get { return sColonia; }
            set { sColonia = value; }
        }

        public string Localidad
        {
            get { return sLocalidad; }
            set { sLocalidad = value; }
        }

        public string Referencia
        {
            get { return sReferencia; }
            set { sReferencia = value; }
        }

        public string Municipio
        {
            get { return sMunicipio; }
            set { sMunicipio = value; }
        }

        public string Estado
        {
            get { return sEstado; }
            set { sEstado = value; }
        }

        public string CodigoPostal
        {
            get { return sCodigoPostal; }
            set { sCodigoPostal = value; }
        } 
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos 
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private string GenerarCadena()
        {
            string sRegresa = "";

            if (sIdReceptor != "")
            {
                iRenglones = 1; 
                // Iniciar el Documento 
                sRegresa = xsaTipoDeRegistro.TipoDeRegistro(tipoSeccion) + "|";

                sRegresa += sIdReceptor + "|";
                sRegresa += sRFC + "|";
                sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sNombre) + "|";
                sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sPais) + "|";
                sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sCalle) + "|";
                sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sNumExterior) + "|";
                sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sNumInterior) + "|";
                sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sLocalidad) + "|";
                sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sReferencia) + "|";
                sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sMunicipio) + "|";
                sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sEstado) + "|";
                sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sCodigoPostal) + "|";
            }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
