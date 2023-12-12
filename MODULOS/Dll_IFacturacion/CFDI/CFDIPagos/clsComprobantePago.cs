using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales; 

using Microsoft.VisualBasic;

using Dll_IFacturacion.CFDI; 
using Dll_IFacturacion.CFDI.CFDFunctions; 
using Dll_IFacturacion.CFDI.CFDFunctionsEx;

namespace Dll_IFacturacion.CFDI.CFDIPagos
{
    public class clsComprobantePago
    {
        #region Declaracion de Variables  
        string sVersion = "3.3";
        string sSerie = "";
        string sFolio = "";

        string sCertificado = "";
        string sNoCertificado = "";
        string sSelloDigital = ""; 

        DateTime dFecha = DateTime.Now;
        int iNoAprobacion = 0;
        int iAñoAprobacion = 0;
        TipoDeComprobanteSAT tpTipoComprobante = TipoDeComprobanteSAT.Ninguno;
        string sFormaDePago = "";
        string sCondicionesDePago = "";
        string sTipoDeCambio = "";
        string sMoneda = "";
        string sMetodoDePago = "";
        string sMetodoDePagoDescripcion = "";

        string sObservaciones_01 = "";
        string mTipoDeDocumento = ""; 
        double dIva = 0;

        double dSubTotal = 0;
        double dSubTotal_0 = 0;
        double dSubTotal_Gravado = 0;
        double dDescuento = 0;
        string sMotivoDescuento = "";
        double dTotal = 0;

        // clsSelloDigital cSelloDigital = new clsSelloDigital(); 
        clsEmisor cEmisor = new clsEmisor();
        clsReceptor cReceptor = new clsReceptor();
        clsConceptos cConceptos = new clsConceptos();
        clsRegimenFiscal cRegimenFiscal = new clsRegimenFiscal(); 

        DataSet dtsComprobante = new DataSet();
        basGenerales Fg = new basGenerales();
        string sFileXml = "";
        string sFileXmlImpresion = "";

        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase
        public clsComprobantePago()
        {
        }

        public clsComprobantePago(DataSet DatosComprobante)
        {
            dtsComprobante = DatosComprobante;
            CargarComprobante(); 
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades 
        public string Version
        {
            get { return sVersion; }
            set { sVersion = value; }
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

        public string NoCertificado
        {
            get { return sNoCertificado; }
            set { sNoCertificado = value; }
        }

        public string Certificado
        {
            get { return sCertificado; }
            set { sCertificado = value; }
        }

        public string Sello
        {
            get { return sSelloDigital; }
            set { sSelloDigital = value; }
        } 

        public DateTime Fecha
        {
            get { return dFecha; }
            set { dFecha = value; }
        }

        public int NoAprobacion 
        {
            get { return iNoAprobacion; }
            set { iNoAprobacion = value; }
        }

        public int AñoAprobacion
        {
            get { return iAñoAprobacion; }
            set { iAñoAprobacion = value; }
        }

        public TipoDeComprobanteSAT TipoDeComprobante
        {
            get { return tpTipoComprobante; }
            set { tpTipoComprobante = value; }
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

        public string MetodoDePagoDescripcion
        {
            get { return sMetodoDePagoDescripcion; }
            set { sMetodoDePagoDescripcion = value; }
        }

        //public string TipoDeDocumento
        //{
        //    get { return sTipoDocumento; }
        //    set { sTipoDocumento = value; }
        //}

        public string Observaciones 
        {
            get { return sObservaciones_01; }
            set { sObservaciones_01 = value; }
        }

        public string TipoDeDocumentoElectronico
        {
            get { return mTipoDeDocumento; }
            set { mTipoDeDocumento = value; }
        }

        public string TipoDeCambio
        {
            get { return sTipoDeCambio; }
            set { sTipoDeCambio = value; }
        }

        public string Moneda
        {
            get { return sMoneda; }
            set { sMoneda = value;  }
        }

        public double SubTotal
        {
            get { return dSubTotal; }
            set { dSubTotal = value; }
        }

        public double Iva
        {
            get { return dIva; }
            set { dIva = value; }
        }

        public double Total
        {
            get { return dTotal; }
            set { dTotal = value; }
        }

        public string NombreFile
        {
            get
            {
                string sRegresa = cEmisor.RFC + "_" + sSerie + Fg.PonCeros(sFolio, 10);  
                return sRegresa;
            }
        }

        public string NombreXml
        {
            get
            {
                sFileXml = NombreFile + ".xml"; 
                return sFileXml;
            }
        }

        public string NombreXmlImpresion
        {
            get 
            {
                sFileXmlImpresion = NombreFile + "_Impresion.xml"; 
                return sFileXmlImpresion; 
            }
        }
        #endregion Propiedades 

        #region Propiedades Clases
        public clsEmisor Emisor
        {
            get { return cEmisor; }
            set { cEmisor = value; }
        }

        public clsRegimenFiscal RegimenFiscal 
        {
            get { return cRegimenFiscal; }
            set { cRegimenFiscal = value; }
        }

        public clsReceptor Receptor
        {
            get { return cReceptor; }
            set { cReceptor = value; }
        }

        public clsConceptos Conceptos
        {
            get { return cConceptos; }
            set { cConceptos = value; }
        }
        #endregion Propiedades Clases

        #region Funciones y Procedimientos Publicos 
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados Comprobante 
        public DataSet DatosComprobante
        {
            get { return dtsComprobante; }
            set 
            {
                dtsComprobante = value;
                RenombrarTablas(); 
            }
        }

        private void RenombrarTablas()
        {
            clsLeer leer = new clsLeer();
            leer.DataSetClase = dtsComprobante;

            leer.RenombrarTabla(1, "Comprobante");
            leer.RenombrarTabla(2, "Emisor");
            leer.RenombrarTabla(3, "EmisorRegimenFiscal");
            leer.RenombrarTabla(4, "EmisorDomicilioFiscal");
            leer.RenombrarTabla(5, "EmisorExpedidoEn");

            leer.RenombrarTabla(6, "Receptor");

            leer.RenombrarTabla(7, "Conceptos");
            leer.RenombrarTabla(8, "Traslados");

            // Cargar los datos Renombrados 
            dtsComprobante = leer.DataSetClase; 
        }

        public void CargarComprobante()
        {
            CargarComprobante(dtsComprobante);  
        }

        public void CargarComprobante(DataSet DatosComprobante)
        {
            clsLeer leer = new clsLeer();
            clsLeer leerComprobante = new clsLeer(); 

            // Obtener los datos 
            dtsComprobante = DatosComprobante; 
            leer.DataSetClase = DatosComprobante;

            leerComprobante.DataTableClase = leer.Tabla("Comprobante");
            if (leerComprobante.Leer())
            {
                sSerie = leerComprobante.Campo("Serie");
                sFolio = leerComprobante.Campo("Folio");

                sNoCertificado = leerComprobante.Campo("NoCertificado");
                sCertificado = leerComprobante.Campo("Certificado"); 
                // sSelloDigital = leerComprobante.Campo("");

                dFecha = leerComprobante.CampoFecha("FechaRegistro");
                iNoAprobacion = leerComprobante.CampoInt("NoAprobacion");
                iAñoAprobacion = leerComprobante.CampoInt("AñoAprobacion");
                tpTipoComprobante = (TipoDeComprobanteSAT)leerComprobante.CampoInt("TipoDeComprobante");
                sTipoDeCambio = leerComprobante.Campo("TipoCambio"); 
                sMoneda = leerComprobante.Campo("Moneda");
                mTipoDeDocumento = leerComprobante.Campo("TipoDeDocumentoElectronico"); 

                sFormaDePago = leerComprobante.Campo("XMLFormaPago");
                sFormaDePago = sFormaDePago == "" ? leerComprobante.Campo("FormaDePago") : sFormaDePago;

                sCondicionesDePago = leerComprobante.Campo("XMLCondicionesPago");
                sMetodoDePago = leerComprobante.Campo("XMLMetodoPago");
                sMetodoDePagoDescripcion = leerComprobante.Campo("XMLMetodoPagoDescripcion");
                

                sNumCtaPago = leerComprobante.Campo("XMLNumeroCuentaPago");

                sReferencia = leerComprobante.Campo("Referencia");
                sObservaciones_01 = leerComprobante.Campo("Observaciones_01");
                sObservaciones_02 = leerComprobante.Campo("Observaciones_02");
                sObservaciones_03 = leerComprobante.Campo("Observaciones_03");

                sTipoDocumento = leerComprobante.Campo("TipoDocumentoDescripcion");
                sTipoInsumo = leerComprobante.Campo("TipoInsumoDescripcion");
                sRubroFinanciamiento = leerComprobante.Campo("RubroFinanciamento");
                sFuenteFinanciamiento = leerComprobante.Campo("Financiamiento"); 

                dSubTotal = leerComprobante.CampoDouble("SubTotal");
                dSubTotal_0 = leerComprobante.CampoDouble("");
                dSubTotal_Gravado = leerComprobante.CampoDouble("");
                dDescuento = leerComprobante.CampoDouble("Descuento");
                sMotivoDescuento = leerComprobante.Campo("MotivoDescuento");
                sNumeroCuentaPredial = leerComprobante.Campo("NumeroCuentaPredial"); 

                dIva = leerComprobante.CampoDouble("Impuesto1");
                dImpuesto2 = leerComprobante.CampoDouble("Impuesto2");
                dIEPS = leerComprobante.CampoDouble("IEPS");
                dISH = leerComprobante.CampoDouble("ISH");
                dRetencionIVA = leerComprobante.CampoDouble("RetencionISR");
                dRetencionISR = leerComprobante.CampoDouble("RetencionIVA");

                dTotal = leerComprobante.CampoDouble("Total"); 

            }

            cEmisor = new clsEmisor(dtsComprobante);
            cRegimen = new clsRegimenFiscales(dtsComprobante); 
            cReceptor = new clsReceptor(dtsComprobante);
            cConceptos = new clsConceptos(dtsComprobante);
            cTraslados = new clsTraslados(dtsComprobante);
            cRetenciones = new clsRetenciones(dtsComprobante);


            foreach (clsRegimenFiscal regimen in cRegimen.RegimenesFiscales)
            {
                cRegimenFiscal = regimen;
                break; 
            }

        }
        #endregion Funciones y Procedimientos Privados Comprobante 

    }
}
