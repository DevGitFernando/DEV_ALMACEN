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
    public class clsConcepto
    {
        #region Declaracion de Variables 
        double dCantidad = 0;
        string sUnidad = "";
        string sNoIdentificacion = "";
        string sDescripcion = "";
        double dValorUnitario = 0;
        double dSubTotal = 0;
        double dImporte = 0;
        double dImporteIva = 0;
        string sNotas = "";

        double dRetencionIVA = 0;
        double dRetencionISR = 0;
        double dIva = 0;
        double dImpuesto2 = 0;

        string mClaveProdServ = "";
        string mClaveUnidad = "";

        double mBase = 0;
        string mImpuestoClave = ""; 
        string mImpuesto = "";
        string mTipoFactor = "";
        double mTasaOCuota = 0;
        double mImporteImpuesto = 0;

        double mImporteImpuesto_Retencion = 0;
        double mTasaOCuota_Retencion = 0;
        string mEsObjetoDeImpuesto = "";

        clsInformacionAduanera infAduana = new clsInformacionAduanera();
        clsCuentaPredial infPredial = new clsCuentaPredial(); 
        #endregion Declaracion de Variables 

        #region Constructores y Destructor de Clase 
        public clsConcepto()
        {
        }
        #endregion Constructores y Destructor de Clase 

        #region Propiedades
        public double Cantidad
        {
            get { return dCantidad; }
            set { dCantidad = value; }
        }

        public string Unidad
        {
            get { return sUnidad; }
            set { sUnidad = value; }
        }

        public string NoIdentificacion
        {
            get { return sNoIdentificacion; }
            set { sNoIdentificacion = value; }
        }

        public string Descripcion
        {
            get { return sDescripcion; }
            set { sDescripcion = value; }
        }

        public string Notas
        {
            get { return sNotas; }
            set { sNotas = value; }
        }

        public double ValorUnitario
        {
            get { return dValorUnitario; }
            set { dValorUnitario = value; }
        }

        public double SubTotal
        {
            get { return dSubTotal; }
            set { dSubTotal = value; }
        }

        public double Importe
        {
            get { return dImporte; }
            set { dImporte = value; }
        }

        public double RetencionIVA
        {
            get { return dRetencionIVA; }
            set { dRetencionIVA = value; }
        }

        public double RetencionISR
        {
            get { return dRetencionISR; }
            set { dRetencionISR = value; }
        }

        public double Iva
        {
            get { return dIva; }
            set { dIva = value; }
        }

        public double ImporteIva
        {
            get { return dImporteIva; }
            set { dImporteIva = value; }
        }

        public double Impuesto1
        {
            get { return dIva; }
            set { dIva = value; }
        }

        public double Impuesto2
        {
            get { return dImpuesto2; }
            set { dImpuesto2 = value; }
        }

        public clsInformacionAduanera InformacionAduanera
        {
            get { return infAduana; }
            set { infAduana = value; }
        }

        public clsCuentaPredial CuentaPredial
        {
            get { return infPredial; }
            set { infPredial = value; }
        }

        public string ClaveProdServ
        {
            get { return mClaveProdServ; }
            set { mClaveProdServ = value; }
        }

        public string ClaveUnidad
        {
            get { return mClaveUnidad; }
            set { mClaveUnidad = value; }
        }

        public double Impuesto_Base
        {
            get { return mBase; }
            set { mBase = value; }
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

        public string EsObjetoDeImpuesto
        {
            get { return mEsObjetoDeImpuesto; }
            set { mEsObjetoDeImpuesto = value; }
        }

        public double Impuesto_ImporteRetencion
        {
            get { return mImporteImpuesto_Retencion; }
            set { mImporteImpuesto_Retencion = value; }
        }

        public double Impuesto_TasaOCuotaRetencion
        {
            get { return mTasaOCuota_Retencion; }
            set { mTasaOCuota_Retencion = value; }
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
            Elemento.SetAttribute("cantidad", dCantidad.ToString());

            if (sNoIdentificacion != "")
            {
                Elemento.SetAttribute("noIdentificacion", sNoIdentificacion);
            }

            Elemento.SetAttribute("descripcion", sDescripcion);
            Elemento.SetAttribute("importe", dImporte.ToString());
            Elemento.SetAttribute("unidad", sUnidad); 
            Elemento.SetAttribute("valorUnitario", dValorUnitario.ToString(CfdGeneral.FormatoDecimal));

            NodoElemento.AppendChild(Elemento); 
            return Elemento;
        }

        private string CadenaParcial()
        {
            string sRegresa = "";

            sRegresa = "|" + dCantidad.ToString();
            sRegresa += "|" + sUnidad;

            if (sNoIdentificacion != "")
            {
                sRegresa += "|" + sNoIdentificacion;
            }

            sRegresa += "|" + sDescripcion;
            sRegresa += "|" + dValorUnitario.ToString(CfdGeneral.FormatoDecimal);
            sRegresa += "|" + dImporte.ToString();

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados
    }
    #endregion Concepto

    #region Conceptos
    public class clsConceptos
    {
        ArrayList pListaConceptos; //  = new List<clsConcepto>();
        DataSet dtsConceptos; 

        #region Constructores y Destructor de Clase 
        public clsConceptos()
        {
            pListaConceptos = new ArrayList();
            dtsConceptos = new DataSet(); 
        }

        public clsConceptos(DataSet Conceptos)
        {
            pListaConceptos = new ArrayList();
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
        public ArrayList Conceptos
        {
            get { return pListaConceptos; }
        }

        public bool Add(clsConcepto Concepto)
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

        public bool Add(double Cantidad, string Unidad, string NoIdentificacion, string Descripcion, double ValorUnitario, double Importe)
        {
            bool bRegresa = Add(Cantidad, Unidad, NoIdentificacion, Descripcion, ValorUnitario, Importe, new clsInformacionAduanera(), new clsCuentaPredial()); 
            return bRegresa;
        }

        public bool Add(double Cantidad, string Unidad, string NoIdentificacion, string Descripcion, double ValorUnitario, double Importe, clsInformacionAduanera InfoAduana)
        {
            bool bRegresa = Add(Cantidad, Unidad, NoIdentificacion, Descripcion, ValorUnitario, Importe, InfoAduana, new clsCuentaPredial());
            return bRegresa;
        }

        public bool Add(double Cantidad, string Unidad, string NoIdentificacion, string Descripcion, double ValorUnitario, double Importe, clsCuentaPredial CuentaPredial)
        {
            bool bRegresa = Add(Cantidad, Unidad, NoIdentificacion, Descripcion, ValorUnitario, Importe, new clsInformacionAduanera(), CuentaPredial);
            return bRegresa;
        }

        public bool Add(double Cantidad, string Unidad, string NoIdentificacion, string Descripcion, double ValorUnitario, double Importe, clsInformacionAduanera InfoAduana, clsCuentaPredial CuentaPredial)
        {
            bool bRegresa = true;

            try
            {
                clsConcepto Concepto = new clsConcepto();
                Concepto.Cantidad = Cantidad;
                Concepto.Unidad = Unidad;
                Concepto.NoIdentificacion = NoIdentificacion;
                Concepto.Descripcion = Descripcion;
                Concepto.ValorUnitario = ValorUnitario;
                Concepto.Importe = Importe;
                Concepto.InformacionAduanera = InfoAduana;
                Concepto.CuentaPredial = CuentaPredial; 

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
        private void CargarDatos()
        {
            clsLeer leer = new clsLeer();
            clsLeer leerConceptos = new clsLeer();

            leer.DataSetClase = dtsConceptos;
            leerConceptos.DataTableClase = leer.Tabla("Conceptos");
            while (leerConceptos.Leer())
            {
                clsConcepto concepto = new clsConcepto();

                concepto.NoIdentificacion = leerConceptos.Campo("NoIdentificacion");
                concepto.Unidad = leerConceptos.Campo("Unidad");
                concepto.Descripcion = leerConceptos.Campo("Descripcion");
                concepto.Cantidad = leerConceptos.CampoDouble("Cantidad");
                concepto.ValorUnitario = leerConceptos.CampoDouble("ValorUnitario");
                concepto.SubTotal = leerConceptos.CampoDouble("SubTotal");
                concepto.Importe = leerConceptos.CampoDouble("Importe");
                concepto.Notas = leerConceptos.Campo("Notas");
                concepto.Iva = leerConceptos.CampoDouble("Impuesto1");
                concepto.ImporteIva = leer.CampoDouble("ImporteIva");  

                concepto.Impuesto1 = leerConceptos.CampoDouble("Impuesto1");
                concepto.Impuesto2 = leerConceptos.CampoDouble("Impuesto2");
                concepto.RetencionIVA = leerConceptos.CampoDouble("RetencionIVA");
                concepto.RetencionISR = leerConceptos.CampoDouble("RetencionISR");

                concepto.ClaveProdServ = leerConceptos.Campo("ClaveProdServ");
                concepto.ClaveUnidad = leerConceptos.Campo("ClaveUnidad"); 

                concepto.Impuesto_Base = leerConceptos.CampoDouble("Impuestos_Base");
                concepto.Impuesto_ImpuestoClave = leerConceptos.Campo("Impuesto_ImpuestoClave");
                concepto.Impuesto_Impuesto = leerConceptos.Campo("Impuesto_Impuesto");
                concepto.Impuesto_TipoFactor = leerConceptos.Campo("Impuestos_TipoFactor"); 
                concepto.Impuesto_TasaOCuota = leerConceptos.CampoDouble("Impuestos_TasaOCuota");
                concepto.Impuesto_Importe = leerConceptos.CampoDouble("Impuestos_Importe");

                concepto.EsObjetoDeImpuesto = leerConceptos.Campo("ClaveObjetoDeImpuesto");

                pListaConceptos.Add(concepto); 
            }
        } 

        private XmlElement ElementoNodo(XmlDocument Documento, XmlElement NodoElemento)
        {
            // XmlDocument doc = Documento; 
            XmlElement Elemento = Documento.CreateElement("Conceptos"); //"DomicilioFiscal"); 
            NodoElemento.AppendChild(Elemento);

            foreach (clsConcepto c in pListaConceptos)
            {
                Elemento.AppendChild(c.Nodo(Documento, Elemento));
            }
            
            return Elemento;
        }

        private string CadenaParcial()
        {
            string sRegresa = "";

            foreach (clsConcepto c in pListaConceptos)
            {
                sRegresa += c.Cadena; 
            }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Privados 
    }
    #endregion Conceptos
}
