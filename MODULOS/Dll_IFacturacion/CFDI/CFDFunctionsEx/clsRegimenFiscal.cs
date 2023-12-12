using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales; 

namespace Dll_IFacturacion.CFDI.CFDFunctionsEx
{
    #region Concepto 
    public class clsRegimenFiscal
    {
        #region Declaracion de Variables 
        string sIdRegimen = ""; 
        string sRegimen = "";
        string mRegimen_Descripcion;

        #endregion Declaracion de Variables 

        #region Constructores y Destructor de Clase 
        public clsRegimenFiscal()
        {
        }
        #endregion Constructores y Destructor de Clase 

        #region Propiedades

        public string IdRegimen
        {
            get { return sIdRegimen; }
            set { sIdRegimen = value; }
        }

        public string Regimen
        {
            get { return sRegimen; }
            set { sRegimen = value; }
        }

        public string Cadena
        {
            get { return CadenaParcial(); }
        }

        public XmlElement Nodo(XmlDocument Documento, XmlElement NodoElemento)
        {
            return ElementoNodo(Documento, NodoElemento);
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Privados 
        private XmlElement ElementoNodo(XmlDocument Documento, XmlElement NodoElemento)
        {
            XmlDocument doc = Documento; 
            XmlElement Elemento = doc.CreateElement("Concepto"); //"DomicilioFiscal");
            ////////Elemento.SetAttribute("cantidad", dCantidad.ToString());

            ////////if (sNoIdentificacion != "")
            ////////{
            ////////    Elemento.SetAttribute("noIdentificacion", sNoIdentificacion);
            ////////}

            ////////Elemento.SetAttribute("descripcion", sDescripcion);
            ////////Elemento.SetAttribute("importe", dImporte.ToString());
            ////////Elemento.SetAttribute("unidad", sUnidad); 
            ////////Elemento.SetAttribute("valorUnitario", dValorUnitario.ToString(CfdGeneral.FormatoDecimal));

            ////////NodoElemento.AppendChild(Elemento); 
            return Elemento;
        }

        private string CadenaParcial()
        {
            string sRegresa = "";

            //////sRegresa = "|" + dCantidad.ToString();
            //////sRegresa += "|" + sUnidad;

            //////if (sNoIdentificacion != "")
            //////{
            //////    sRegresa += "|" + sNoIdentificacion;
            //////}

            //////sRegresa += "|" + sDescripcion;
            //////sRegresa += "|" + dValorUnitario.ToString(CfdGeneral.FormatoDecimal);
            //////sRegresa += "|" + dImporte.ToString();

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
    #endregion Concepto

    #region Conceptos
    public class clsRegimenFiscales
    {
        ArrayList pListaRegimenes; //  = new List<clsConcepto>();
        DataSet dtsConceptos; 

        #region Constructores y Destructor de Clase 
        public clsRegimenFiscales()
        {
            pListaRegimenes = new ArrayList();
            dtsConceptos = new DataSet(); 
        }

        public clsRegimenFiscales(DataSet Conceptos)
        {
            pListaRegimenes = new ArrayList();
            dtsConceptos = Conceptos;
            CargarDatos(); 
        }
        #endregion Constructores y Destructor de Clase 

        #region Propiedades 
        public string Cadena
        {
            get { return CadenaParcial(); }
        }

        public XmlElement Nodo(XmlDocument Documento, XmlElement NodoElemento)
        {
            return ElementoNodo(Documento, NodoElemento);
        }
        #endregion Propiedades
        
        #region Funciones y Procedimientos Publicos
        public ArrayList RegimenesFiscales
        {
            get { return pListaRegimenes; }
        }

        public bool Add(clsRegimenFiscal Concepto)
        {
            bool bRegresa = true;

            try
            {
                pListaRegimenes.Add(Concepto); 
            }
            catch 
            {
                bRegresa = false; 
            }

            return bRegresa;
        }

        public bool Add(string IdRegimen, string Regimen)
        {
            bool bRegresa = true;

            try
            {
                clsRegimenFiscal Concepto = new clsRegimenFiscal();
                Concepto.IdRegimen = IdRegimen;
                Concepto.Regimen = Regimen;

                pListaRegimenes.Add(Concepto);
            }
            catch
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        private void CargarDatos()
        {
            clsLeer leer = new clsLeer();
            clsLeer leerConceptos = new clsLeer();

            leer.DataSetClase = dtsConceptos;
            leerConceptos.DataTableClase = leer.Tabla("EmisorRegimenFiscal");
            while (leerConceptos.Leer())
            {
                clsRegimenFiscal concepto = new clsRegimenFiscal();

                concepto.IdRegimen = leerConceptos.Campo("IdRegimen");
                concepto.Regimen = leerConceptos.Campo("Regimen");

                pListaRegimenes.Add(concepto); 
            }
        } 

        private XmlElement ElementoNodo(XmlDocument Documento, XmlElement NodoElemento)
        {
            // XmlDocument doc = Documento; 
            XmlElement Elemento = Documento.CreateElement("PENDIENTE"); //"DomicilioFiscal"); 
            NodoElemento.AppendChild(Elemento);

            foreach (clsRegimenFiscal c in pListaRegimenes)
            {
                Elemento.AppendChild(c.Nodo(Documento, Elemento));
            }
            
            return Elemento;
        }

        private string CadenaParcial()
        {
            string sRegresa = "";

            foreach (clsRegimenFiscal c in pListaRegimenes)
            {
                sRegresa += c.Cadena; 
            }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados 
    }
    #endregion Conceptos
}
