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

namespace Dll_MA_IFacturacion.XSA
{
    public class xsaCfdiEnvioDocumentos
    {
        #region Declaración de Variables
        xsaTiposDeRegistro tipoSeccion = xsaTiposDeRegistro.EnvioAutomatico;
        string sIdReceptor = "";
        string sCorreoElectronico = "";
        string sAsunto = "";
        string sMensaje = "";
        cfdFormatoDocumento tpAdjunto = cfdFormatoDocumento.AMBOS;
        int iRenglones = 0; 
        #endregion Declaración de Variables 

        #region Constructores y Destructor de Clase 
        public xsaCfdiEnvioDocumentos()
        { 
        }

        ~xsaCfdiEnvioDocumentos()
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
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos 
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private string GenerarCadena()
        {
            string sRegresa = "";

            if (sCorreoElectronico != "")
            {
                iRenglones = 1; 
                // Iniciar el Documento 
                sRegresa = xsaTipoDeRegistro.TipoDeRegistro(tipoSeccion) + "|";
                sRegresa += sIdReceptor + "|";
                sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sCorreoElectronico) + "|";
                sRegresa += sAsunto + "|";
                sRegresa += sMensaje + "|";
                sRegresa += tpAdjunto.ToString() + "|"; 
            } 

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
