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
    public class xsaCfdiPago
    {
        #region Declaración de Variables
        xsaTiposDeRegistro tipoSeccion = xsaTiposDeRegistro.Pago;
        string sFormaDePago = "";
        string sCondicionesDePago = "";
        string sMetodoDePago = "";
        string sFechaVencimiento = "";
        string sObservaciones = "";
        int iRenglones = 0; 
        #endregion Declaración de Variables 

        #region Constructores y Destructor de Clase 
        public xsaCfdiPago()
        { 
        }

        ~xsaCfdiPago()
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

        public string FormaDePago
        {
            get { return sFormaDePago; }
            set { sFormaDePago = value; } 
        }

        public string CondicionesDePago
        {
            get { return sCondicionesDePago; }
            set { sCondicionesDePago = value; }
        }

        public string MetodoDePago
        {
            get { return sMetodoDePago; }
            set { sMetodoDePago = value; }
        }

        public string FechaDeVencimiento 
        {
            get { return sFechaVencimiento; }
            set { sFechaVencimiento = value; }
        }

        public string Observaciones
        {
            get { return sObservaciones; }
            set { sObservaciones = value; }
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
            sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sFormaDePago) + "|";
            sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sCondicionesDePago) + "|";
            sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sMetodoDePago) + "|";
            sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sFechaVencimiento) + "|";
            sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sObservaciones) + "|"; 

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
