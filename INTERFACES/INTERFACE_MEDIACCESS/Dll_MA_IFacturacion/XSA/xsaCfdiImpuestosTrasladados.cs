#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
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
    public class xsaCfdiImpuestoTrasladado
    {
        #region Declaración de Variables
        xsaTiposDeRegistro tipoSeccion = xsaTiposDeRegistro.ImpuestosTrasladados;
        cfdImpuestosTrasladados tpImpuesto = cfdImpuestosTrasladados.IVA;
        double dTasa = 0.00;
        double dImporte = 0.00; 
        #endregion Declaración de Variables

        #region Constructores y Destructor de Clase
        public xsaCfdiImpuestoTrasladado()
        {
        }

        ~xsaCfdiImpuestoTrasladado()
        {
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades
        public string Cadena
        {
            get { return GenerarCadena(); }
        }

        public cfdImpuestosTrasladados Impuesto
        {
            get { return tpImpuesto; }
            set { tpImpuesto = value; }
        }

        public double Tasa
        {
            get { return dTasa; }
            set { dTasa = value; }
        }

        public double Importe
        {
            get { return dImporte; }
            set { dImporte = value; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private string GenerarCadena()
        {
            string sRegresa = "";

            // Iniciar el Documento 
            sRegresa = xsaTipoDeRegistro.TipoDeRegistro(tipoSeccion) + "|";
            sRegresa += tpImpuesto.ToString() + "|";
            sRegresa += dTasa.ToString(DtIFacturacion.sFormato_02) + "|";
            sRegresa += dImporte.ToString(DtIFacturacion.sFormato_02) + "|";

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }

    public class xsaCfdiImpuestosTrasladados
    {
        #region Declaración de Variables
        //xsaTiposDeRegistro tipoSeccion = xsaTiposDeRegistro.ImpuestosTrasladados;
        List<xsaCfdiImpuestoTrasladado> pListaImpuestos; //  = new List<clsConcepto>();
        int iRenglones = 0; 
        #endregion Declaración de Variables 

        #region Constructores y Destructor de Clase 
        public xsaCfdiImpuestosTrasladados()
        {
            pListaImpuestos = new List<xsaCfdiImpuestoTrasladado>(); 
        }

        ~xsaCfdiImpuestosTrasladados()
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
        public bool Add(xsaCfdiImpuestoTrasladado Impuesto)
        {
            bool bRegresa = true;

            try
            {
                pListaImpuestos.Add(Impuesto);
            }
            catch
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        public bool Add(string TipoImpuesto, double Tasa, double Importe)
        {
            bool bRegresa = true;
            cfdImpuestosTrasladados tipo = cfdImpuestosTrasladados.Ninguna;

            switch (TipoImpuesto.ToUpper())
            {
                case "IVA":
                    tipo = cfdImpuestosTrasladados.IVA; 
                    break;

                case "IEPS":
                    tipo = cfdImpuestosTrasladados.IEPS;
                    break;
            }

            bRegresa = this.Add(tipo, Tasa, Importe); 

            return bRegresa; 
        }

        public bool Add(cfdImpuestosTrasladados TipoImpuesto, double Tasa, double Importe)
        {
            bool bRegresa = true;

            try
            {
                xsaCfdiImpuestoTrasladado Impuesto = new xsaCfdiImpuestoTrasladado();
                Impuesto.Impuesto = TipoImpuesto;
                Impuesto.Tasa = Tasa;
                Impuesto.Importe = Importe; 

                pListaImpuestos.Add(Impuesto);
            }
            catch
            {
                bRegresa = false;
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private string GenerarCadena()
        {
            string sRegresa = "";
            iRenglones = 0; 

            // Iniciar el Documento 
            //sRegresa = xsaTipoDeRegistro.TipoDeRegistro(tipoSeccion) + "|"; 
            //sRegresa = pListaImpuestos.Count == 0 ? "" : xsaTipoDeRegistro.TipoDeRegistro(tipoSeccion) + "|";

            foreach (xsaCfdiImpuestoTrasladado c in pListaImpuestos)
            {
                sRegresa += c.Cadena + "\n";
                iRenglones++; 
            }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados 
    }
}
