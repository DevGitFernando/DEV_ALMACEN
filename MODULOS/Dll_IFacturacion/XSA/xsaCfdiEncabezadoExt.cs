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
    public class xsaCfdiEncabezadoExt
    {
        #region Declaración de Variables
        xsaTiposDeRegistro tipoSeccion = xsaTiposDeRegistro.EncabezadoExt;

        string sNumCtaPago = ""; 
        string sSerie = "";
        string sFolio = "";
        string sFechaHora = "";
        DateTime dtFechaHora = DateTime.Now;
        double dMontoFiscal = 0.00;
        string sLugarExpedicion = "";
        int iRenglones = 0; 
        #endregion Declaración de Variables 

        #region Constructores y Destructor de Clase 
        public xsaCfdiEncabezadoExt()
        { 
        }

        ~xsaCfdiEncabezadoExt()
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
        
        public string NumeroCtaPago
        {
            get { return sNumCtaPago; }
            set { sNumCtaPago = value; }
        }

        public string Serie
        {
            get { return sSerie; }
            set { sSerie = value; } 
        }

        public string Folio
        {
            get { return sFolio; }
            set { sFolio = value; }
        }

        public string Fecha
        {
            get { return sFechaHora; }
            set { sFechaHora = value; }
        }

        public DateTime FechaHora
        {
            get { return dtFechaHora; }
            set
            {
                dtFechaHora = value;
                sFechaHora = General.FechaYMD(dtFechaHora) + "T" + General.Hora(dtFechaHora);
            }
        }

        public double Total
        {
            get { return dMontoFiscal; }
            set { dMontoFiscal = value; }
        }

        public string ExpedidoEn
        {
            get { return sLugarExpedicion; }
            set { sLugarExpedicion = value; }
        }
        #endregion Propiedades 

        #region Funciones y Procedimientos Publicos 
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private string GenerarCadena()
        {
            string sRegresa = "";
            iRenglones = 1; 

            // Iniciar el Seccion  
            sRegresa = xsaTipoDeRegistro.TipoDeRegistro(tipoSeccion) + "|";

            sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sNumCtaPago) + "|";
            sRegresa += sFolio + "|";
            sRegresa += sSerie + "|";
            sRegresa += sFechaHora + "|";
            sRegresa += dMontoFiscal.ToString(DtIFacturacion.sFormato_02) + "|"; 
            sRegresa += sLugarExpedicion + "|"; 

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
