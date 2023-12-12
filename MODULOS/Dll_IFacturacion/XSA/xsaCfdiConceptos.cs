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

namespace Dll_IFacturacion.XSA
{
    public class xsaCfdiConcepto
    {
        #region Declaración de Variables
        xsaTiposDeRegistro tipoSeccion = xsaTiposDeRegistro.Conceptos; 
        string sId_Interno = ""; 
        string sId_Externo = ""; 
        double dCantidad = 0.0000; 
        string sDescripcion = ""; 
        double dValorUnitario = 0.00; 
        double dImporte = 0.00; 
        string sUnidadDeMedida = ""; 
        string sCategoria = ""; 
        #endregion Declaración de Variables 

        #region Constructores y Destructor de Clase 
        public xsaCfdiConcepto()
        { 
        }

        ~xsaCfdiConcepto()
        { 
        }
        #endregion Constructores y Destructor de Clase 

        #region Propiedades 
        public string Cadena
        {
            get { return GenerarCadena(); }
        }

        public string ID_Interno
        {
            get { return sId_Interno; }
            set { sId_Interno = value; }
        }

        public string ID_Externo
        {
            get { return sId_Externo; }
            set { sId_Externo = value; }
        }

        public double Cantidad
        {
            get { return dCantidad; }
            set { dCantidad = value; }
        }

        public string Descripcion
        {
            get { return sDescripcion; }
            set { sDescripcion = value; }
        }

        public double ValorUnitario
        {
            get { return dValorUnitario; }
            set { dValorUnitario = value; }
        }

        public double Importe
        {
            get { return dImporte; }
            set { dImporte = value; }
        }

        public string UnidadDeMedida
        {
            get { return sUnidadDeMedida; }
            set { sUnidadDeMedida = value; }
        }

        public string Categoria
        {
            get { return sCategoria; }
            set { sCategoria = value; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos 
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados 
        private string GenerarCadena()
        {
            string sRegresa = "";

            // Iniciar el Documento 
            sRegresa = xsaTipoDeRegistro.TipoDeRegistro(tipoSeccion)+ "|";

            sRegresa += sId_Interno + "|"; 
            sRegresa += sId_Externo + "|"; 
            //sRegresa += dCantidad.ToString(DtIFacturacion.sFormato_04) + "|";
            sRegresa += Convert.ToInt32(0 + dCantidad) + "|"; 
            sRegresa += DtIFacturacion.QuitarSaltoDeLinea(sDescripcion) + "|"; 
            sRegresa += dValorUnitario.ToString(DtIFacturacion.sFormato_02) + "|"; 
            sRegresa += dImporte.ToString(DtIFacturacion.sFormato_02) + "|"; 
            sRegresa += sUnidadDeMedida + "|"; 
            sRegresa += sCategoria + "|"; 

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }

    public class xsaCfdiConceptos
    {
        #region Declaración de Variables
        //xsaTiposDeRegistro tipoSeccion = xsaTiposDeRegistro.Conceptos; 
        List<xsaCfdiConcepto> pListaConceptos; //  = new List<clsConcepto>(); 
        int iRenglones = 0; 
        #endregion Declaración de Variables 

        #region Constructores y Destructor de Clase 
        public xsaCfdiConceptos()
        {
            pListaConceptos = new List<xsaCfdiConcepto>(); 
        }

        ~xsaCfdiConceptos()
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
        public bool Add(xsaCfdiConcepto Concepto)
        {
            bool bRegresa = true;

            try
            {
                pListaConceptos.Add(Concepto);
            }
            catch
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        public bool Add(string Id_Interno, string Id_Externo, double Cantidad, 
            string Descripcion, double ValorUnitario, double Importe, string UnidadDeMedida, string Categoria)
        {
            bool bRegresa = true;

            try
            {
                xsaCfdiConcepto Concepto = new xsaCfdiConcepto();
                Concepto.ID_Interno = Id_Interno;
                Concepto.ID_Externo = Id_Externo;
                Concepto.Cantidad = Cantidad;
                Concepto.Descripcion = Descripcion;
                Concepto.ValorUnitario = ValorUnitario;
                Concepto.Importe = Importe;
                Concepto.UnidadDeMedida = UnidadDeMedida;
                Concepto.Categoria = Categoria; 

                pListaConceptos.Add(Concepto);
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

            foreach (xsaCfdiConcepto c in pListaConceptos)
            {
                sRegresa += c.Cadena + "\n";
                iRenglones++; 
            } 

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados 
    }
}
