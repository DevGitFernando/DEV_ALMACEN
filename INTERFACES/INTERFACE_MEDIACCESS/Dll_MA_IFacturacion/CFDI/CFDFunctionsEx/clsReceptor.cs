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
    public class clsReceptor
    {
        #region Declaracion de Variables
        string sNombre = "";
        string sRFC = "";
        string sCURP = "";
        string sNSS = "";
        string sTelefono = "";
        string sUsoDeCFDI = "";
        string sUsoDeCFDI_Descripcion = "";

        DataSet dtsReceptor;

        clsDomicilio ubicacionDomicilio = new clsDomicilio(TipoDeDomicilio.Domicilio);

        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase
        public clsReceptor()
        {
        }

        public clsReceptor(DataSet DatosReceptor)
        {
            dtsReceptor = DatosReceptor;
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

        public string CURP
        {
            get { return sCURP; }
            set { sCURP = value; }
        }

        public string NSS
        {
            get { return sNSS; }
            set { sNSS = value; }
        }

        public string Telefono
        {
            get { return sTelefono; }
            set { sTelefono = value; }
        }

        public string UsoDeCFDI
        {
            get { return sUsoDeCFDI; }
            set { sUsoDeCFDI = value; }
        }

        public string UsoDeCFDI_Descripcion
        {
            get { return sUsoDeCFDI_Descripcion; }
            set { sUsoDeCFDI_Descripcion = value; }
        }

        public clsDomicilio Domicilio
        {
            get { return ubicacionDomicilio; }
            set { ubicacionDomicilio = value; }
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
            clsLeer leerReceptor = new clsLeer();

            leer.DataSetClase = dtsReceptor;
            leerReceptor.DataTableClase = leer.Tabla("Receptor");
            if (leerReceptor.Leer())
            {
                sNombre = leerReceptor.Campo("Nombre");
                sRFC = leerReceptor.Campo("RFC").Replace("-", "");
                sCURP = leerReceptor.Campo("CURP");
                sNSS = leerReceptor.Campo("NSS");
                sTelefono = leerReceptor.Campo("Telefonos");

                sUsoDeCFDI = leerReceptor.Campo("UsoDeCFDI");
                sUsoDeCFDI_Descripcion = leerReceptor.Campo("UsoDeCFDI_Descripcion");
            }

            ubicacionDomicilio = new clsDomicilio(dtsReceptor, TipoDeDomicilio.Domicilio);

        }

        private XmlElement ElementoNodo(XmlDocument Documento, XmlElement NodoElemento)
        {
            //XmlDocument doc = Documento;
            XmlElement Elemento = Documento.CreateElement("Receptor"); //"DomicilioFiscal");

            Elemento.SetAttribute("nombre", sNombre);
            Elemento.SetAttribute("rfc", sRFC);
            NodoElemento.AppendChild(Elemento);
            Elemento.AppendChild(ubicacionDomicilio.Nodo(Documento, Elemento));

            return Elemento;
        }

        private string CadenaParcial()
        {
            string sRegresa = "";

            sRegresa = "|" + sRFC;
            sRegresa += "|" + sNombre;
            sRegresa += ubicacionDomicilio.Cadena;

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
}
