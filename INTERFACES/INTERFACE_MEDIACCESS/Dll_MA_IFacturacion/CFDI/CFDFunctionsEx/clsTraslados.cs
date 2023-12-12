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
    #region Traslado
    public class clsTraslado
    {
        #region Declaracion de Variables
        TrasladoImpuestos tipoImpuesto = TrasladoImpuestos.Ninguna;

        double dImporte = 0;
        double dTasaImpuesto = 0;
        string mImpuesto = "";
        string mImpuestoClave = "";
        string mTipoFactor = "";
        double mTasaOCuota = 0;
        double mImporteImpuesto = 0;

        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase
        public clsTraslado()
        {
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades
        public double Importe
        {
            get { return dImporte; }
            set { dImporte = value; }
        }

        public double Tasa
        {
            get { return dTasaImpuesto; }
            set { dTasaImpuesto = value; }
        }

        public TrasladoImpuestos Impuesto
        {
            get { return tipoImpuesto; }
            set { tipoImpuesto = value; }
        }

        public string Impuesto_ImpuestoClave
        {
            get { return mImpuestoClave; }
            set { mImpuestoClave = value; }
        }

        public string Impuesto_Impuesto
        {
            get { return mImpuesto; }
            set { mImpuesto = value; }
        }

        public string Impuesto_TipoFactor
        {
            get { return mTipoFactor; }
            set { mTipoFactor = value; }
        }

        public double Impuesto_TasaOCuota
        {
            get { return mTasaOCuota; }
            set { mTasaOCuota = value; }
        }

        public double Impuesto_Importe
        {
            get { return mImporteImpuesto; }
            set { mImporteImpuesto = value; }
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
            // XmlDocument doc = Documento; 
            XmlElement Elemento = Documento.CreateElement("Traslado"); //"DomicilioFiscal");
            Elemento.SetAttribute("impuesto", tipoImpuesto.ToString().ToUpper());
            Elemento.SetAttribute("tasa", dTasaImpuesto.ToString(CfdGeneral.FormatoDecimal));
            Elemento.SetAttribute("importe", dImporte.ToString(CfdGeneral.FormatoDecimal));
            NodoElemento.AppendChild(Elemento);
            return Elemento;
        }

        private string CadenaParcial()
        {
            string sRegresa = "";

            sRegresa = "|" + tipoImpuesto.ToString();
            sRegresa += "|" + dTasaImpuesto.ToString(CfdGeneral.FormatoDecimal);
            sRegresa += "|" + dImporte.ToString(CfdGeneral.FormatoDecimal);

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
    #endregion Traslado

    #region Traslados
    public class clsTraslados
    {
        List<clsTraslado> pListaTraslados; //  = new List<clsConcepto>(); 
        DataSet dtsTraslados;

        double dImporteImpuestosTrasladados = 0;

        #region Constructores y Destructor de Clase
        public clsTraslados()
        {
            pListaTraslados = new List<clsTraslado>();
        }

        public clsTraslados(DataSet Traslados)
        {
            pListaTraslados = new List<clsTraslado>();
            dtsTraslados = Traslados;
            CargarDatos();
        }

        #endregion Constructores y Destructor de Clase

        #region Propiedades
        public List<clsTraslado> ListaTraslados
        {
            get { return pListaTraslados; }
        }

        public string Cadena
        {
            get { return CadenaParcial(); }
        }

        public double ImporteImpuestosTrasladados
        {
            get { return dImporteImpuestosTrasladados; }
        }

        public XmlElement Nodo(XmlDocument Documento, XmlElement NodoElemento)
        {
            return ElementoNodo(Documento, NodoElemento);
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Publicos
        public List<clsTraslado> Traslados
        {
            get { return pListaTraslados; }
        }

        public bool Add(clsTraslado Traslado)
        {
            bool bRegresa = true;

            try
            {
                pListaTraslados.Add(Traslado);
            }
            catch
            {
                bRegresa = false;
            }

            return bRegresa;
        }

        public bool Add(double Importe, double TasaIva, TrasladoImpuestos Impuesto)
        {
            bool bRegresa = true;

            try
            {
                clsTraslado Traslado = new clsTraslado();
                Traslado.Importe = Importe;
                Traslado.Tasa = TasaIva;
                Traslado.Impuesto = Impuesto;

                pListaTraslados.Add(Traslado);
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
            ////clsLeer leer = new clsLeer();
            ////clsLeer leerTraslados = new clsLeer();

            ////leer.DataSetClase = dtsTraslados;
            ////leerTraslados.DataTableClase = leer.Tabla("Traslados");
            ////while (leerTraslados.Leer())
            ////{
            ////    clsTraslado traslado = new clsTraslado();

            ////    traslado.Importe = leerTraslados.CampoDouble("Importe");
            ////    traslado.Tasa = leerTraslados.CampoDouble("TasaIva");
            ////    traslado.Impuesto = TrasladoImpuestos.IVA;

            ////    pListaTraslados.Add(traslado);
            ////}

            clsLeer leer = new clsLeer();
            clsLeer leerTraslados = new clsLeer();

            leer.DataSetClase = dtsTraslados;
            leerTraslados.DataTableClase = leer.Tabla("Traslados");
            while (leerTraslados.Leer())
            {
                clsTraslado traslado = new clsTraslado();

                traslado.Importe = leerTraslados.CampoDouble("Importe");
                traslado.Tasa = leerTraslados.CampoDouble("TasaIva");
                traslado.Impuesto = TrasladoImpuestos.IVA;

                traslado.Impuesto_Importe = leerTraslados.CampoDouble("Importe");
                traslado.Impuesto_ImpuestoClave = leerTraslados.Campo("Impuesto_ImpuestoClave");
                traslado.Impuesto_Impuesto = leerTraslados.Campo("Impuesto_Impuesto");
                traslado.Impuesto_TasaOCuota = leerTraslados.CampoDouble("Impuestos_TasaOCuota");
                traslado.Impuesto_TipoFactor = leerTraslados.Campo("Impuestos_TipoFactor");

                pListaTraslados.Add(traslado);
            }
        }

        private XmlElement ElementoNodo(XmlDocument Documento, XmlElement NodoElemento)
        {
            // XmlDocument doc = Documento;
            XmlElement impuestos = Documento.CreateElement("Impuestos");
            NodoElemento.AppendChild(impuestos);
            XmlElement Elemento = Documento.CreateElement("Traslados"); //"DomicilioFiscal"); 
            impuestos.AppendChild(Elemento);

            foreach (clsTraslado c in pListaTraslados)
            {
                Elemento.AppendChild(c.Nodo(Documento, Elemento));
            }

            return Elemento;
        }

        private string CadenaParcial()
        {
            string sRegresa = "";

            foreach (clsTraslado t in pListaTraslados)
            {
                sRegresa += t.Cadena;
            }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
    #endregion Traslados
}
