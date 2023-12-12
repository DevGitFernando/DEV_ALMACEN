using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales; 

namespace Dll_MA_IFacturacion.CFDI.CFDFunctionsEx
{
    public class clsEmisor
    {
        #region Declaracion de Variables
        string sNombre = "";
        string sRFC = "";
        string sFileXml = "";
        string sFileXmlImpresion = "";
        basGenerales Fg = new basGenerales(); 

        DataSet dtsEmisor; 

        clsDomicilio ubicacionDomicilioFiscal = new clsDomicilio(TipoDeDomicilio.DomicilioFiscal); 
        clsDomicilio ubicacionExpedidoEn = new clsDomicilio(TipoDeDomicilio.ExpedidoEn);
        clsRegimenFiscal regimenFiscal = new clsRegimenFiscal(); 

        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase 
        public clsEmisor()
        {
            dtsEmisor = new DataSet(); 
        }

        public clsEmisor( DataSet DatosEmisor )
        {
            dtsEmisor = DatosEmisor;
            CargarDatos(); 
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades 
        public string Nombre
        {
            get { return sNombre; }
            set { sNombre = value; }
        }

        public string RFC
        {
            get { return sRFC; }
            set { sRFC = value; }
        }

        public clsDomicilio DomicilioFiscal
        {
            get { return ubicacionDomicilioFiscal; }
            set { ubicacionDomicilioFiscal = value; }
        }

        public clsDomicilio ExpedidoEn
        {
            get { return ubicacionExpedidoEn; }
            set { ubicacionExpedidoEn = value; }
        }

        public clsRegimenFiscal RegimenFiscal
        {
            get { return regimenFiscal; }
            set { regimenFiscal = value; }
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
        private void CargarDatos()
        {
            clsLeer leer = new clsLeer();
            clsLeer leerEmisor = new clsLeer();

            leer.DataSetClase = dtsEmisor;
            leerEmisor.DataTableClase = leer.Tabla("Emisor");
            if (leerEmisor.Leer())
            {
                sNombre = leerEmisor.Campo("Nombre");
                sRFC = leerEmisor.Campo("RFC").Replace("-", ""); 
            }

            ubicacionDomicilioFiscal = new clsDomicilio(dtsEmisor, TipoDeDomicilio.DomicilioFiscal);
            ubicacionExpedidoEn = new clsDomicilio(dtsEmisor, TipoDeDomicilio.ExpedidoEn); 

        }

        private XmlElement ElementoNodo(XmlDocument Documento, XmlElement NodoElemento)
        {
            //XmlDocument doc = Documento;
            XmlElement Elemento = Documento.CreateElement("Emisor"); //"DomicilioFiscal");         

            Elemento.SetAttribute("nombre", sNombre);
            Elemento.SetAttribute("rfc", sRFC);
            NodoElemento.AppendChild(Elemento); 
            Elemento.AppendChild(ubicacionDomicilioFiscal.Nodo(Documento, Elemento));

            if(ubicacionExpedidoEn.ExistenDatos)
            {
                Elemento.AppendChild(ubicacionExpedidoEn.Nodo(Documento, Elemento));
            }
            return Elemento;
        }

        private string CadenaParcial()
        {
            string sRegresa = "";

            sRegresa = "|" + sRFC;
            sRegresa += "|" + sNombre;
            sRegresa += ubicacionDomicilioFiscal.Cadena;

            if (ubicacionExpedidoEn.ExistenDatos)
            {
                sRegresa += ubicacionExpedidoEn.Cadena; 
            } 

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
