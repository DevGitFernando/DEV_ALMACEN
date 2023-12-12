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

namespace Dll_IFacturacion.CFDI.CFDFunctionsEx
{
    public class clsComprobante
    {
        #region Declaracion de Variables  
        string sVersion = "3.2";
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
        string sFormaDePago_Descripcion = ""; 
        string sCondicionesDePago = "";
        string sTipoDeCambio = "";
        string sMoneda = "";
        string sMetodoDePago = "";
        string sMetodoDePagoDescripcion = "";
        string sNumCtaPago = "";
        string sNumeroCuentaPredial = "";

        string sObservaciones_01 = "";
        string sObservaciones_02 = "";
        string sObservaciones_03 = "";
        string sReferencia = "";
        string sReferencia_02 = "";
        string sReferencia_03 = "";
        string sReferencia_04 = "";
        string sReferencia_05 = "";

        string sTipoDocumento = "";
        string sTipoInsumo = "";
        string sRubroFinanciamiento = "";
        string sFuenteFinanciamiento = "";

        private string mTipoDeDocumento = ""; 

        double dRetencionIVA = 0;
        double dRetencionISR = 0;
        double dIva = 0;
        double dImpuesto2 = 0;
        double dIEPS = 0;
        double dISH = 0;

        double dSubTotal = 0;
        double dSubTotal_0 = 0;
        double dSubTotal_Gravado = 0;
        double dDescuento = 0;
        string sMotivoDescuento = "";
        double dTotal = 0;
        string sEfectoDeComprobante = "";
        string sEfectoDeComprobante_Descripcion = "";

        string mTipoDeRelacion = "";
        string mTipoDeRelacion_Descripcion = "";

        string mSAT_ClaveDeConfirmacion = "";
        string mCFDI_Relacionado_CPago = "";
        string mSerie_Relacionada_CPago = "";
        long mFolio_Relacionado_CPago = 0;
        string mExportacion = "";

        // clsSelloDigital cSelloDigital = new clsSelloDigital(); 
        clsEmisor cEmisor = new clsEmisor();
        clsRegimenFiscales cRegimen = new clsRegimenFiscales(); 
        clsReceptor cReceptor = new clsReceptor();
        clsConceptos cConceptos = new clsConceptos();
        clsTraslados cTraslados = new clsTraslados();
        clsRetenciones cRetenciones = new clsRetenciones();
        clsRegimenFiscal cRegimenFiscal = new clsRegimenFiscal(); 

        DataSet dtsComprobante = new DataSet();
        basGenerales Fg = new basGenerales();
        string sFileXml = "";
        string sFileXmlImpresion = "";

        clsLeer leerUUIDs_Relacionados = new clsLeer();
        clsLeer leerInformacionPagos = new clsLeer();

        #endregion Declaracion de Variables

        #region Constructores y Destructor de Clase
        public clsComprobante()
        {
        }

        public clsComprobante(DataSet DatosComprobante)
        {
            dtsComprobante = DatosComprobante;
            CargarComprobante(); 
        }
        #endregion Constructores y Destructor de Clase

        #region Propiedades 
        public string Version
        {
            get { return sVersion; }
            // set { sVersion = value; }
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

        public string FormaDePago_Descripcion
        {
            get { return sFormaDePago_Descripcion; }
            set { sFormaDePago_Descripcion = value; }
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

        public string NumCtaPago
        {
            get { return sNumCtaPago; }
            set { sNumCtaPago = value; }
        }

        public string NumeroCuentaPredial
        {
            get { return sNumeroCuentaPredial; }
            set { sNumeroCuentaPredial = value;  } 
        }

        public string TipoDeDocumento
        {
            get { return sTipoDocumento; }
            set { sTipoDocumento = value; }
        }

        public string TipoDeInsumo
        {
            get { return sTipoInsumo; }
            set { sTipoInsumo = value; }
        }

        public string RubroFinanciamiento
        {
            get { return sRubroFinanciamiento; }
            set { sRubroFinanciamiento = value; }
        }

        public string FuenteFinanciamiento
        {
            get { return sFuenteFinanciamiento; }
            set { sFuenteFinanciamiento = value; }
        }

        public string Referencia
        {
            get { return sReferencia; }
            set { sReferencia = value; }
        }
        public string Referencia_02
        {
            get { return sReferencia_02; }
            set { sReferencia_02 = value; }
        }
        public string Referencia_03
        {
            get { return sReferencia_03; }
            set { sReferencia_03 = value; }
        }

        public string Referencia_04
        {
            get { return sReferencia_04; }
            set { sReferencia_04 = value; }
        }
        public string Referencia_05
        {
            get { return sReferencia_05; }
            set { sReferencia_05 = value; }
        }
        public string Observaciones_01
        {
            get { return sObservaciones_01; }
            set { sObservaciones_01 = value; }
        }

        public string Observaciones_02
        {
            get { return sObservaciones_02; }
            set { sObservaciones_02 = value; }
        }

        public string Observaciones_03
        {
            get { return sObservaciones_03; }
            set { sObservaciones_03 = value; }
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

        public double SubTotal
        {
            get { return dSubTotal; }
            set { dSubTotal = value; }
        }

        public double SubTotal_0
        {
            get { return dSubTotal_0; }
            set { dSubTotal_0 = value; }
        }

        public double SubTotal_Gravado
        {
            get { return dSubTotal_Gravado; }
            set { dSubTotal_Gravado = value; }
        }

        public double Descuento
        {
            get { return dDescuento; }
            set { dDescuento = value; }
        }

        public string MotivoDescuento
        {
            get { return sMotivoDescuento; }
            set { sMotivoDescuento = value; }
        }

        public double Iva
        {
            get { return dIva; }
            set { dIva = value; }
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

        public double IEPS
        {
            get { return dIEPS; }
            set { dIEPS = value; }
        }

        public double ISH
        {
            get { return dISH; }
            set { dISH = value; }
        }
        
        public double Total
        {
            get { return dTotal; }
            set { dTotal = value; }
        }

        public string TipoDeRelacion
        {
            get { return mTipoDeRelacion; }
            set { mTipoDeRelacion = value; }
        }

        public string TipoDeRelacion_Descripcion
        {
            get { return mTipoDeRelacion_Descripcion; }
            set { mTipoDeRelacion_Descripcion = value; }
        }

        public string EfectoDeComprobante
        {
            get { return sEfectoDeComprobante; }
            set { sEfectoDeComprobante = value; }
        }

        public string EfectoDeComprobante_Descripcion
        {
            get { return sEfectoDeComprobante_Descripcion; }
            set { sEfectoDeComprobante_Descripcion = value; }
        }

        public string SAT_ClaveDeConfirmacion
        {
            get { return mSAT_ClaveDeConfirmacion; }
            set { mSAT_ClaveDeConfirmacion = value; }
        }

        public string CFDI_Relacionado_CPago
        {
            get { return mCFDI_Relacionado_CPago; }
            set { mCFDI_Relacionado_CPago = value; }
        }

        public string Serie_Relacionada_CPago
        {
            get { return mSerie_Relacionada_CPago; }
            set { mSerie_Relacionada_CPago = value; }
        }

        public long Folio_Relacionado_CPago
        {
            get { return mFolio_Relacionado_CPago; }
            set { mFolio_Relacionado_CPago = value; }
        }

        public string Exportacion
        {
            get { return mExportacion; }
            set { mExportacion = value; }
        }

        public string NombreFile
        {
            get
            {
                string sRegresa = cEmisor.RFC + "_" + sSerie + Fg.PonCeros(sFolio, 10);  
                return sRegresa;
            }
        }

        public DataSet UUIDs_Relacionados
        {
            get { return leerUUIDs_Relacionados.DataSetClase.Copy(); }
        }

        public DataSet Informacion_Pago
        {
            get { return leerInformacionPagos.DataSetClase.Copy(); }
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

        public string Cadena
        {
            get { return CadenaParcial(); }
        }
        #endregion Propiedades 

        #region Propiedades Clases
        //public clsComprobante Comprobante
        //{
        //    get { return cComprobante; }
        //    set { cComprobante = value; }
        //}

        public clsEmisor Emisor
        {
            get { return cEmisor; }
            set { cEmisor = value; }
        }

        public clsRegimenFiscales RegimenFiscal
        {
            get { return cRegimen; }
            set { cRegimen = value; }
        }

        public clsRegimenFiscal RegimenFiscal_33
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

        public clsTraslados Traslados
        {
            get { return cTraslados; }
            set { cTraslados = value; }
        }

        public clsRetenciones Retenciones
        {
            get { return cRetenciones; }
            set { cRetenciones = value; }
        }

        //public XmlElement Nodo
        //{
        //    get { return ElementoNodo(); }
        //}
        #endregion Propiedades Clases

        #region Funciones y Procedimientos Publicos 
        //public string GenerarSello()
        //{
        //    string sRegresa = ""; 
        //    byte[] b;
        //    byte[] block;

        //    sSelloDigital = sRegresa; 
        //    return sRegresa; 
        //}
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
            leer.RenombrarTabla(9, "Retenciones");

            leer.RenombrarTabla(10, "UUIDs_Relacionados");
            leer.RenombrarTabla(11, "InformacionPagos");

            //leer.RenombrarTabla(12, "UUIDs_Relacionados_Pagos");

            leer.RenombrarTabla(12, "UUIDs_Relacionados_Pagos_Retenciones");
            leer.RenombrarTabla(13, "UUIDs_Relacionados_Pagos_Traslados");
            leer.RenombrarTabla(14, "UUIDs_Relacionados_Pagos_ImpuestosP");

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
            ////clsLeer leerEmisor = new clsLeer();
            ////clsLeer leerEmisorDomicilioFiscal = new clsLeer();
            ////clsLeer leerEmisorExpedidoEn = new clsLeer(); 

            ////clsLeer leerReceptor = new clsLeer();
            ////clsLeer leerConceptos = new clsLeer();
            ////clsLeer leerTraslados = new clsLeer();
            ////clsLeer leerRetenciones = new clsLeer();

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
                sFormaDePago_Descripcion = leerComprobante.Campo("XMLFormaPagoDescripcion");

                sCondicionesDePago = leerComprobante.Campo("XMLCondicionesPago");
                sMetodoDePago = leerComprobante.Campo("XMLMetodoPago");
                sMetodoDePagoDescripcion = leerComprobante.Campo("XMLMetodoPagoDescripcion");
                

                sNumCtaPago = leerComprobante.Campo("XMLNumeroCuentaPago");

                sReferencia = leerComprobante.Campo("Referencia");
                sReferencia_02 = leerComprobante.Campo("Referencia_02");
                sReferencia_03 = leerComprobante.Campo("Referencia_03");
                sReferencia_04 = leerComprobante.Campo("Referencia_04");
                sReferencia_05 = leerComprobante.Campo("Referencia_05");
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


                sEfectoDeComprobante = leerComprobante.Campo("EfectoComprobante");
                sEfectoDeComprobante_Descripcion = leerComprobante.Campo("EfectoComprobante_Descripcion");


                mTipoDeRelacion = leerComprobante.Campo("TipoRelacion");
                mTipoDeRelacion_Descripcion = leerComprobante.Campo("TipoRelacion_Descripcion");


                mSAT_ClaveDeConfirmacion = leerComprobante.Campo("SAT_ClaveDeConfirmacion");
                mCFDI_Relacionado_CPago = leerComprobante.Campo("CFDI_Relacionado_CPago");
                mSerie_Relacionada_CPago = leerComprobante.Campo("Serie_Relacionada_CPago");
                mFolio_Relacionado_CPago = leerComprobante.CampoInt("Folio_Relacionado_CPago");

                mExportacion = leerComprobante.Campo("ClaveExportacion");

                /* 
                        D.Documento, 		
                        D.Notas, D.FechaPago, D.Cancelado, D.FechaCancelacion, 
                        D.TipoRecibo, 
                */
            }

            cEmisor = new clsEmisor(dtsComprobante);
            cRegimen = new clsRegimenFiscales(dtsComprobante); 
            cReceptor = new clsReceptor(dtsComprobante);
            cConceptos = new clsConceptos(dtsComprobante);
            cTraslados = new clsTraslados(dtsComprobante);
            cRetenciones = new clsRetenciones(dtsComprobante);

            leerUUIDs_Relacionados.DataTableClase = leer.Tabla("UUIDs_Relacionados");
            leerInformacionPagos.DataTableClase = leer.Tabla("InformacionPagos");

            DataSet dtsPagos = new DataSet("Pagos");
            dtsPagos.Tables.Add(leer.Tabla("UUIDs_Relacionados"));
            dtsPagos.Tables.Add(leer.Tabla("InformacionPagos"));
            dtsPagos.Tables.Add(leer.Tabla("UUIDs_Relacionados_Pagos"));
            dtsPagos.Tables.Add(leer.Tabla("UUIDs_Relacionados_Pagos_Retenciones"));
            dtsPagos.Tables.Add(leer.Tabla("UUIDs_Relacionados_Pagos_Traslados"));
            dtsPagos.Tables.Add(leer.Tabla("UUIDs_Relacionados_Pagos_ImpuestosP"));

            ////leerUUIDs_Relacionados.DataSetClase = dtsPagos.Copy();
            leerInformacionPagos.DataSetClase = dtsPagos.Copy();


            foreach (clsRegimenFiscal regimen in cRegimen.RegimenesFiscales)
            {
                cRegimenFiscal = regimen;
                break; 
            }

            //leerReceptor.DataTableClase = leer.Tabla("Receptor"); 
            //leerConceptos.DataTableClase = leer.Tabla("Conceptos");
            //leerTraslados.DataTableClase = leer.Tabla("Traslados");
            //leerRetenciones.DataTableClase = leer.Tabla("Retenciones");

        }
        #endregion Funciones y Procedimientos Privados Comprobante 

        #region Funciones y Procedimientos Privados
        public bool FormatoXml()
        {
            bool bRegresa = true; 

            XmlDocument doc = new XmlDocument();
            XmlDeclaration declaracion = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement Comprobante = doc.CreateElement("Comprobante");
            doc.InsertBefore(declaracion, doc.DocumentElement);
            
            Comprobante.SetAttribute("xmlns", "http://www.sat.gob.mx/cfd/2");
            Comprobante.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            Comprobante.SetAttribute("xmlns:schemaLocation", "http://www.sat.gob.mx/cfd/2  http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd");

            ////Encabezado.SetAttribute("xmlns", "http://www.sat.gob.mx/cfd/2");
            ////Encabezado.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            ////Encabezado.SetAttribute("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd");
            ////doc.AppendChild(Encabezado); 


            Comprobante.SetAttribute("version", sVersion);
            Comprobante.SetAttribute("serie", sSerie);
            Comprobante.SetAttribute("folio", sFolio);
            Comprobante.SetAttribute("fecha", FechaHora(dFecha));
            Comprobante.SetAttribute("noAprobacion", iNoAprobacion.ToString());
            Comprobante.SetAttribute("anoAprobacion", iAñoAprobacion.ToString());
            Comprobante.SetAttribute("formaDePago", sFormaDePago);
            Comprobante.SetAttribute("subTotal", dSubTotal.ToString(CfdGeneral.FormatoDecimal));
            Comprobante.SetAttribute("descuento", dDescuento.ToString(CfdGeneral.FormatoDecimal)); 
            Comprobante.SetAttribute("total", dTotal.ToString(CfdGeneral.FormatoDecimal));
            Comprobante.SetAttribute("tipoDeComprobante", tpTipoComprobante.ToString().ToLower());
            Comprobante.SetAttribute("noCertificado", sNoCertificado);
            Comprobante.SetAttribute("certificado", sCertificado);
            Comprobante.SetAttribute("sello", sSelloDigital);
            doc.AppendChild(Comprobante);


            // XmlElement Emisor = doc.CreateElement("Emisor"); // (doc, Comprobante);
            
            cEmisor.Nodo(doc, Comprobante); 
            cReceptor.Nodo(doc, Comprobante);
            cConceptos.Nodo(doc, Comprobante);
            cTraslados.Nodo(doc, Comprobante);

            ////doc.AppendChild(cEmisor.Nodo(doc, Comprobante);
            ////doc.AppendChild(cReceptor.Nodo(doc, Comprobante));
            ////doc.AppendChild(cConceptos.Nodo(doc, Comprobante));
            ////doc.AppendChild(cTraslados.Nodo(doc, Comprobante));

            string sRuta = System.Windows.Forms.Application.StartupPath; 
            doc.Save(String.Format(@"{0}\{1}_{2}.xml", sRuta, cEmisor.RFC, Fg.PonCeros(sFolio, 20)));

            return bRegresa;
        }

        private string CadenaParcial()
        {
            string sRegresa = "";

            sRegresa = "|" + sVersion;
            sRegresa += "|" + sSerie;
            sRegresa += "|" + sFolio;
            sRegresa += "|" + FechaHora(dFecha);

            sRegresa += "|" + iNoAprobacion.ToString();
            sRegresa += "|" + iAñoAprobacion.ToString();
            sRegresa += "|" + tpTipoComprobante.ToString().ToLower();

            sRegresa += "|" + sFormaDePago;


            if (sCondicionesDePago != "")
            {
                sRegresa += "|" + sCondicionesDePago;
            }

            sRegresa += "|" + dSubTotal.ToString(CfdGeneral.FormatoDecimal);

            if (dDescuento != 0)
            {
                sRegresa += "|" + dDescuento.ToString(CfdGeneral.FormatoDecimal);
            }

            sRegresa += "|" + dTotal.ToString(CfdGeneral.FormatoDecimal);


            sRegresa += cEmisor.Cadena;
            sRegresa += cReceptor.Cadena;
            sRegresa += cConceptos.Cadena;
            sRegresa += cTraslados.Cadena;

            if ( dIva > 0 )
            {
                //sRegresa += "|" + dIva.ToString(CfdGeneral.FormatoDecimal);
            }

            return sRegresa;
        }

        #region Manejo de Cadenas 
        public string Right(string prtDato, int prtLargo)
        {
            string sRegresa = Strings.Right(prtDato, prtLargo);
            return sRegresa;
        }

        public string Left(string prtDato, int prtLargo)
        {
            string sRegresa = Strings.Left(prtDato, prtLargo);
            return sRegresa;
        }

        public string Mid(string prtDato, int prtPosIni)
        {
            string sRegresa = Strings.Mid(prtDato, prtPosIni);
            return sRegresa;
        }

        public string Mid(string prtDato, int prtPosIni, int prtLargo)
        {
            string sRegresa = "";

            try
            {
                sRegresa = Strings.Mid(prtDato, prtPosIni, prtLargo);
            }
            catch { sRegresa = ""; }
            return sRegresa;
        }

        public string PonCeros(string prtDato, int prtLargo)
        {
            string sCadena = "";

            try
            {
                sCadena = Strings.StrDup(prtLargo, "0").ToString() + prtDato.Trim();   //"000000000000000000000000000000";
            }
            catch { }
            //string sValor = sCadena + prtDato.Trim();
            string sRegresa = Right(sCadena, prtLargo);
            return sRegresa;
        }

        public string PonCeros(int prtDato, int prtLargo)
        {
            string sRegresa = PonCeros(prtDato.ToString(), prtLargo);
            return sRegresa;
        }
        #endregion Manejo de Cadenas

        #region Fechas
        public string FechaHora(DateTime Fecha)
        {
            string sRegresa = "";

            sRegresa = FechaYMD(Fecha);
            sRegresa += "T" + Hora(Fecha);

            return sRegresa;
        }

        public string Hora(DateTime Fecha)
        {
            string sRegresa = "", sHora = "", sMinuto = "", sSegundo = "";

            //sRegresa = Fecha.TimeOfDay.ToString();

            sHora = PonCeros(Fecha.Hour,2); // Strings.Left(sRegresa, 2);
            sMinuto = PonCeros(Fecha.Minute, 2); // Strings.Mid(sRegresa, 4, 2);
            sSegundo = PonCeros(Fecha.Second, 2); // Strings.Mid(sRegresa, 7, 2);


            return sHora + ":" + sMinuto + ":" + sSegundo;
        }

        public string FechaYMD(DateTime Fecha)
        {
            string sRegresa = FormatearFecha(Fecha, "-", FormatoFecha.YMD);
            return sRegresa;
        }

        //public static string FechaYMD(DateTime Fecha, string Separador)
        //{
        //    string sRegresa = FormatearFecha(Fecha, Separador, FormatoFecha.YMD);
        //    return sRegresa;
        //}

        //public static string FechaDMY(DateTime Fecha)
        //{
        //    string sRegresa = FormatearFecha(Fecha, "-", FormatoFecha.DMY);
        //    return sRegresa;
        //}

        private string FormatearFecha(DateTime Fecha, string Caracter, FormatoFecha Formato)
        {
            string sRegresa = "", sYear = "", sMes = "", sDia = "";

            sRegresa = "";  //Fecha.ToShortDateString().ToString();

            sYear = PonCeros(Fecha.Year, 4); // Strings.Right(sRegresa, 4);
            sMes = PonCeros(Fecha.Month, 2); // Strings.Mid(sRegresa, 4, 2);
            sDia = PonCeros(Fecha.Day, 2); // Strings.Left(sRegresa, 2);

            switch (Formato)
            {
                case FormatoFecha.YMD:
                    sRegresa = sYear + Caracter + sMes + Caracter + sDia;
                    break;

                case FormatoFecha.DMY:
                    sRegresa = sDia + Caracter + sMes + Caracter + sYear;
                    break;
            }

            return sRegresa;
        }
        #endregion Fechas
        #endregion Funciones y Procedimientos Privados
    }
}
