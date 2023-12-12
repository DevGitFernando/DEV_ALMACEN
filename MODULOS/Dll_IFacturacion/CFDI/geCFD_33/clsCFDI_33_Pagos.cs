﻿using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Text;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;

using Dll_IFacturacion.CFDI; 
using Dll_IFacturacion.CFDI.geCFD; 

namespace Dll_IFacturacion.CFDI.geCFD_33
{
    public class clsCFDI_33_Pagos
    {
        private string defPrefix = "cfdi";
        private string defPrefix_nm = "pago20";
        private string defURI = "http://www.sat.gob.mx/cfd/4";
        private string defURI_nm = "http://www.sat.gob.mx/Pagos20";

        private ArrayList schemaLocation = new ArrayList();
        private ArrayList xmlns = new ArrayList();
        private XmlDocument cfd = new XmlDocument();


        public clsEmisor Emisor = new clsEmisor();
        public clsReceptor Receptor = new clsReceptor();
        public ArrayList Conceptos = new ArrayList();
        public clsImpuestos Impuestos = new clsImpuestos();

        private XmlElement mAddenda;
        private XmlElement mComplemento;
        private string mlastError;

        private double mversion = 3.2;
        private string mcertificado = "";
        private string mcondicionesDePago = "";
        private double mdescuento = 0;
        private DateTime mfecha = DateTime.Now;
        private DateTime mFechaFolioFiscalOrig = DateTime.Now;
        private long mfolio = 0;
        private string mFolioRelacionado = "";
        private string mFolioFiscalOrig = "";
        private string mformaDePago = "";
        private string mformaDePago_Descripcion = "";
        private string mLugarExpedicion = "";
        private string mmetodoDePago = "";
        private string mmetodoDePagoDescripcion;
        private string mMoneda = "";
        private double mMontoFolioFiscalOrig = 0;
        private string mmotivoDescuento = "";
        private string mnoCertificado = "";
        private string mNumCtaPago = "";
        private string msello = "";
        private string mserie;
        private string mSerieFolioFiscalOrig = "";
        private double msubTotal = 0;
        private string mTipoCambio = "";
        private string mtipoDeComprobante = "";

        private string mExportacion = "";
        private string mTipoDeRelacion = "";
        private string mTipoDeRelacion_Descripcion = "";


        private string mSAT_ClaveDeConfirmacion = "";
        private string mCFDI_Relacionado_CPago = "";
        private string mSerie_Relacionada_CPago = "";
        private long mFolio_Relacionado_CPago = 0;


        private string mEfectoDeComprobante = "";
        private string mEfectoDeComprobante_Descripcion = "";

        private double mtotal = 0;
        private string mCadenaOriginal = "";
        private string mTipoDeDocumento = "";

        DataSet dtsImpresion;
        DataTable dtComprobante;
        DataTable dtEmisor;
        DataTable dtEmisorDomicilioFiscal;
        DataTable dtEmisorDomicilioExpedidoEn;
        DataTable dtEmisorRegimen;
        DataTable dtReceptor;
        DataTable dtReceptorDomililio;
        DataTable dtConceptos;
        DataTable dtConceptos_Pago;
        DataTable dtConceptos_InformacionAduanera;
        DataTable dtImpuestos;
        DataTable dtRetenciones;
        DataTable dtTraslados;
        DataTable dtTimbreFiscal;
        DataTable dtLogo;
        DataTable dtCBB;
        DataTable dtCFDI_Relacionados;

        private string sObservaciones_01 = "";
        private string sObservaciones_02 = "";
        private string sObservaciones_03 = "";
        private string sReferencia = "";

        private string sTipoDocumento = "";
        private string sTipoInsumo = "";
        private string sRubroFinanciamiento = "";
        private string sFuenteFinanciamiento = "";

        DataSet dtsUUIDs_Relacionados = new DataSet();
        DataSet dtsInformacion_Pago = new DataSet();

        public clsCFDI_33_Pagos()
        {
            this.InitCFD();
        }

        public bool appendChildInCFD( XmlElement element )
        {
            try
            {
                this.cfd.DocumentElement.AppendChild(element);
                return true;
            }
            catch(Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public object getObjectInArrayList( ArrayList oArrayList, int index )
        {
            try
            {
                return oArrayList[index];
            }
            catch(Exception exception)
            {
                this.lastError = exception.Message;
                return null;
            }
        }

        private void LogProceso( string Mensaje )
        {
            LogProceso("", Mensaje);
        }

        private void LogProceso( string Funcion, string Mensaje )
        {
            clsGrabarError.LogFileError(string.Format("clsCFDI_33_Pagos.{0}:    {1}", Funcion, Mensaje));
        }

        public string getXML( string filename, bool omitSeal )
        {
            this.InitCFD_DataSet(); //// Preparar el xml para la impresion 
            string sCadena = "";

            XmlElement documentElement;
            XmlElement elementEmisor;
            XmlElement elementReceptor;
            XmlElement elementConceptos;

            XmlElement complementoElement;
            XmlElement pagoElement;
            XmlElement pagoElement_Item;
            XmlElement pagoElement_Item_Totales;
            XmlElement pagoElement_UUID_Relacionado_Item;

            XmlElement elementUUIDs_Relacionados;
            XmlElement elementUUID_Relacionado;

            XmlElement element3;
            XmlElement element4;
            XmlElement element5;

            XmlElement elementConcepto_Item;
            XmlElement elementImpuestos;
            XmlElement elementTraslados;
            XmlElement elementTraslado_Item;
            XmlElement elementRetenciones;
            XmlElement elementRetencion_Item;


            XmlElement pagoElement_Impuestos;
            XmlElement pagoElement_Impuestos_Item;
            XmlElement pagoElement_Impuestos_Retenidos;
            XmlElement pagoElement_Impuestos_Traslados;


            XmlElement pagoElement_Impuestos_P;
            XmlElement pagoElement_Impuestos_Item_P;
            XmlElement pagoElement_Impuestos_Retenidos_P;
            XmlElement pagoElement_Impuestos_Traslados_P;


            Exception exception2;
            if(!this.isOK(omitSeal))
            {
                LogProceso(lastError);
                return "";
            }

            this.InitCFD();
            documentElement = this.cfd.DocumentElement;
            documentElement = this.cfd.DocumentElement;
            documentElement.SetAttribute("xmlns:pago20", defURI_nm);
            documentElement.SetAttribute("xmlns:cfdi", "http://www.sat.gob.mx/cfd/4");
            documentElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            documentElement.SetAttribute("xmlns:xs", "http://www.w3.org/2001/XMLSchema");

            foreach (string str2 in this.xmlns)
            {
                documentElement.SetAttribute(str2.Split(new char[] { ';' })[0], str2.Split(new char[] { ';' })[1]);
            }

            string str = "";
            foreach(string str3 in this.schemaLocation)
            {
                str = str + " " + str3;
            }

            documentElement.SetAttribute("schemaLocation",
                "http://www.w3.org/2001/XMLSchema-instance",
                "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd " +
                "http://www.sat.gob.mx/Pagos20 http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos20.xsd" + str);

            documentElement.SetAttribute("Version", this.version.ToString("#.0"));
            if((this.serie != "") && (this.serie != null))
            {
                documentElement.SetAttribute("Serie", this.toUTF8(this.serie));
            }

            if(this.folio > 0L)
            {
                documentElement.SetAttribute("Folio", this.toUTF8(this.folio.ToString()));
            }

            documentElement.SetAttribute("Fecha", this.fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
            documentElement.SetAttribute("SubTotal", this.subTotal.ToString("0"));
            documentElement.SetAttribute("Total", this.total.ToString("0"));
            documentElement.SetAttribute("TipoDeComprobante", this.tipoDeComprobante.ToUpper());

            if (this.mExportacion != "")
            {
                documentElement.SetAttribute("Exportacion", mExportacion);
            }

            documentElement.SetAttribute("LugarExpedicion", this.toUTF8(this.LugarExpedicion));

            if((this.Moneda != "") && (this.Moneda != null))
            {
                documentElement.SetAttribute("Moneda", this.toUTF8(this.Moneda));
            }

            if((this.sello != "") && (this.sello != null))
            {
                documentElement.SetAttribute("Sello", this.toUTF8(this.sello));
            }
            documentElement.SetAttribute("NoCertificado", this.toUTF8(this.noCertificado));
            documentElement.SetAttribute("Certificado", this.toUTF8(this.certificado));


            if(mCFDI_Relacionado_CPago != "")
            {
                elementUUIDs_Relacionados = this.newElement("CfdiRelacionados");
                elementUUIDs_Relacionados.SetAttribute("TipoRelacion", this.toUTF8(this.TipoDeRelacion));
                {
                    elementUUID_Relacionado = this.newElement("CfdiRelacionado");
                    elementUUID_Relacionado.SetAttribute("UUID", mCFDI_Relacionado_CPago);
                    elementUUIDs_Relacionados.AppendChild(elementUUID_Relacionado);
                }
                documentElement.AppendChild(elementUUIDs_Relacionados);
            }



            if(!this.Emisor.isOK())
            {
                this.lastError = "Error Emisor. " + this.Emisor.lastError;
                LogProceso(lastError);
                return "";
            }

            elementEmisor = this.newElement("Emisor");
            elementEmisor.SetAttribute("Rfc", this.toUTF8(this.Emisor.rfc));
            if((this.Emisor.nombre != "") && (this.Emisor.nombre != null))
            {
                elementEmisor.SetAttribute("Nombre", this.toUTF8(this.Emisor.nombre));
                elementEmisor.SetAttribute("RegimenFiscal", this.toUTF8(this.Emisor.RegimenFiscal_33.Regimen));
            }
            documentElement.AppendChild(elementEmisor);


            if(!this.Receptor.isOK())
            {
                this.lastError = "Error Receptor. " + this.Receptor.lastError;
                LogProceso(lastError);
                return "";
            }

            elementReceptor = this.newElement("Receptor");
            elementReceptor.SetAttribute("Rfc", this.toUTF8(this.Receptor.rfc));
            if(this.Receptor.nombre != "")
            {
                elementReceptor.SetAttribute("Nombre", this.toUTF8(this.Receptor.nombre));
                elementReceptor.SetAttribute("UsoCFDI", this.toUTF8(this.Receptor.UsoDeCFDI));

                elementReceptor.SetAttribute("DomicilioFiscalReceptor", this.toUTF8(this.Receptor.DomicilioFiscal));
                elementReceptor.SetAttribute("RegimenFiscalReceptor", this.toUTF8(this.Receptor.RegimenFiscal));
            }
            documentElement.AppendChild(elementReceptor);


            if(this.Conceptos.Count < 1)
            {
                this.lastError = "Falta elemento requerido: Conceptos";
                LogProceso(lastError);
                return "";
            }

            int iConceptos = 0;
            elementConceptos = this.newElement("Conceptos");
            foreach(clsConcepto concepto in this.Conceptos)
            {
                iConceptos++;
                object[] obj = {
                    iConceptos,
                    toUTF8(concepto.ClaveProdServ), toUTF8(concepto.noIdentificacion), toUTF8(concepto.descripcion),
                    toUTF8(concepto.ClaveUnidad), toUTF8(concepto.unidad),
                    concepto.cantidad, concepto.valorUnitario, concepto.tasaiva, concepto.importeiva,  concepto.importe, concepto.notas,
                    
                    concepto.Impuesto_Base, concepto.Impuesto_ImpuestoClave, concepto.Impuesto_Impuesto,
                    concepto.Impuesto_TipoFactor, concepto.Impuesto_TasaOCuota,
                    concepto.Impuesto_Importe, concepto.Impuesto_TasaOCuotaRetencion, concepto.Impuesto_ImporteRetencion 
                    };
                dtConceptos.Rows.Add(obj);


                //dtConceptos.Columns.Add("Orden", System.Type.GetType("System.Int32"));
                //dtConceptos.Columns.Add("ClaveProdServ", System.Type.GetType("System.String"));
                //dtConceptos.Columns.Add("NoIdentificacion", System.Type.GetType("System.String"));
                //dtConceptos.Columns.Add("Descripcion", System.Type.GetType("System.String"));
                //dtConceptos.Columns.Add("ClaveUnidad", System.Type.GetType("System.String"));
                //dtConceptos.Columns.Add("Unidad", System.Type.GetType("System.String"));
                //dtConceptos.Columns.Add("Cantidad", System.Type.GetType("System.Double"));
                //dtConceptos.Columns.Add("ValorUnitario", System.Type.GetType("System.Double"));
                //dtConceptos.Columns.Add("TasaIVA", System.Type.GetType("System.Double"));
                //dtConceptos.Columns.Add("ImporteIva", System.Type.GetType("System.Double"));
                //dtConceptos.Columns.Add("Importe", System.Type.GetType("System.Double"));
                //dtConceptos.Columns.Add("Notas", System.Type.GetType("System.String"));

                //dtConceptos.Columns.Add("Impuestos_Base", System.Type.GetType("System.Double"));
                //dtConceptos.Columns.Add("Impuesto_ImpuestoClave", System.Type.GetType("System.String"));
                //dtConceptos.Columns.Add("Impuesto_Impuesto", System.Type.GetType("System.String"));
                //dtConceptos.Columns.Add("Impuestos_TipoFactor", System.Type.GetType("System.String"));
                //dtConceptos.Columns.Add("Impuestos_TasaOCuota", System.Type.GetType("System.Double"));
                //dtConceptos.Columns.Add("Impuestos_Importe", System.Type.GetType("System.Double"));



                if (!concepto.isOK())
                {
                    this.lastError = "Error en concepto. " + concepto.lastError;
                    return "";
                }

                elementConcepto_Item = this.newElement("Concepto");
                elementConcepto_Item.SetAttribute("ClaveProdServ", this.toUTF8(concepto.ClaveProdServ));
                elementConcepto_Item.SetAttribute("Cantidad", concepto.cantidad.ToString());
                elementConcepto_Item.SetAttribute("ClaveUnidad", this.toUTF8(concepto.ClaveUnidad));
                elementConcepto_Item.SetAttribute("Descripcion", this.toUTF8(concepto.descripcion));
                elementConcepto_Item.SetAttribute("ValorUnitario", concepto.valorUnitario.ToString("0"));
                elementConcepto_Item.SetAttribute("Importe", concepto.importe.ToString("0"));
                elementConcepto_Item.SetAttribute("ObjetoImp", "01");

                elementConceptos.AppendChild(elementConcepto_Item);
            }
            documentElement.AppendChild(elementConceptos);


            complementoElement = this.newElement("Complemento");
            pagoElement = this.newElement_PG("Pagos");
            pagoElement.SetAttribute("Version", "2.0");



            clsLeer informacionPago = new clsLeer();
            clsLeer conceptoItem = new clsLeer();

            informacionPago.DataSetClase = dtsInformacion_Pago;
            dtConceptos_Pago = informacionPago.Tabla("InformacionPagos"); //  dtsInformacion_Pago.Tables["InformacionPagos"];
            conceptoItem.DataTableClase = dtConceptos_Pago;
            
            if (conceptoItem.Registros < 1)
            {
                this.lastError = "Falta elemento requerido: Conceptos";
                LogProceso(lastError);
                return "";
            }


            while (conceptoItem.Leer())
            {
                ////////// Totales  
                pagoElement_Item_Totales = this.newElement_PG("Totales");

                if (conceptoItem.CampoDouble("TotalRetencionesIVA") > 0)
                {
                    pagoElement_Item_Totales.SetAttribute("TotalRetencionesIVA", this.toUTF8(conceptoItem.CampoDouble("TotalRetencionesIVA").ToString("0.#0")));
                }

                if (conceptoItem.CampoDouble("TotalRetencionesISR") > 0)
                {
                    pagoElement_Item_Totales.SetAttribute("TotalRetencionesISR", this.toUTF8(conceptoItem.CampoDouble("TotalRetencionesISR").ToString("0.#0")));
                }

                if (conceptoItem.CampoDouble("TotalRetencionesIEPS") > 0)
                {
                    pagoElement_Item_Totales.SetAttribute("TotalRetencionesIEPS", this.toUTF8(conceptoItem.CampoDouble("TotalRetencionesIEPS").ToString("0.#0")));
                }

                if (conceptoItem.CampoDouble("TotalTrasladosBaseIVA16") > 0)
                {
                    pagoElement_Item_Totales.SetAttribute("TotalTrasladosBaseIVA16", this.toUTF8(conceptoItem.CampoDouble("TotalTrasladosBaseIVA16").ToString("0.#0")));
                }

                if (conceptoItem.CampoDouble("TotalTrasladosImpuestoIVA16") > 0)
                {
                    pagoElement_Item_Totales.SetAttribute("TotalTrasladosImpuestoIVA16", this.toUTF8(conceptoItem.CampoDouble("TotalTrasladosImpuestoIVA16").ToString("0.#0")));
                }

                if (conceptoItem.CampoDouble("TotalTrasladosBaseIVA8") > 0)
                {
                    pagoElement_Item_Totales.SetAttribute("TotalTrasladosBaseIVA8", this.toUTF8(conceptoItem.CampoDouble("TotalTrasladosBaseIVA8").ToString("0.#0")));
                }

                if (conceptoItem.CampoDouble("TotalTrasladosImpuestoIVA8") > 0)
                {
                    pagoElement_Item_Totales.SetAttribute("TotalTrasladosImpuestoIVA8", this.toUTF8(conceptoItem.CampoDouble("TotalTrasladosImpuestoIVA8").ToString("0.#0")));
                }

                if (conceptoItem.CampoDouble("TotalTrasladosBaseIVA0") > 0)
                {
                    pagoElement_Item_Totales.SetAttribute("TotalTrasladosBaseIVA0", this.toUTF8(conceptoItem.CampoDouble("TotalTrasladosBaseIVA0").ToString("0.#0")));
                    pagoElement_Item_Totales.SetAttribute("TotalTrasladosImpuestoIVA0", this.toUTF8(conceptoItem.CampoDouble("TotalTrasladosImpuestoIVA0").ToString("0.#0")));
                }

                if (conceptoItem.CampoDouble("TotalTrasladosImpuestoIVA0") > 0)
                {
                    ////pagoElement_Item_Totales.SetAttribute("TotalTrasladosImpuestoIVA0", this.toUTF8(conceptoItem.CampoDouble("TotalTrasladosImpuestoIVA0").ToString("0.#0")));
                }

                if (conceptoItem.CampoDouble("TotalTrasladosBaseIVAExento") > 0)
                {
                    pagoElement_Item_Totales.SetAttribute("TotalTrasladosBaseIVAExento", this.toUTF8(conceptoItem.CampoDouble("TotalTrasladosBaseIVAExento").ToString("0.#0")));
                }

                //////// Este campo es REQUERIDO 
                ////if (conceptoItem.CampoDouble("MontoTotalPagos") > 0) 
                {
                    pagoElement_Item_Totales.SetAttribute("MontoTotalPagos", this.toUTF8(conceptoItem.CampoDouble("MontoTotalPagos").ToString("0.#0")));
                }

                ////////// Totales  
                ///

                pagoElement_Item = this.newElement_PG("Pago");

                pagoElement_Item.SetAttribute("FechaPago", this.toUTF8(conceptoItem.CampoFecha("FechaDePago").ToString("yyyy-MM-ddTHH:mm:ss")));
                pagoElement_Item.SetAttribute("FormaDePagoP", this.toUTF8(conceptoItem.Campo("FormaDePago").ToString()));
                pagoElement_Item.SetAttribute("MonedaP", this.toUTF8(conceptoItem.Campo("Moneda").ToString()));

                if (conceptoItem.CampoDouble("TipoCambio") >= 1)
                {
                    if (conceptoItem.Campo("Moneda").ToUpper() == "MXN")
                    {
                        pagoElement_Item.SetAttribute("TipoCambioP", this.toUTF8("1"));
                    }
                    else
                    {
                        pagoElement_Item.SetAttribute("TipoCambioP", this.toUTF8(conceptoItem.CampoDouble("TipoCambio").ToString("0.#0")));
                    }
                    ////pagoElement_Item.SetAttribute("TipoCambioP", this.toUTF8(1.ToString("0.#0")));
                }

                pagoElement_Item.SetAttribute("Monto", this.toUTF8(conceptoItem.CampoDouble("Monto").ToString("0.#0")));

                if (conceptoItem.Campo("NumeroDeOperacion") != "")
                {
                    pagoElement_Item.SetAttribute("NumOperacion", this.toUTF8(conceptoItem.Campo("NumeroDeOperacion").ToString()));
                }



                if (conceptoItem.Campo("RfcEmisorCtaOrd") != "")
                {
                    pagoElement_Item.SetAttribute("RfcEmisorCtaOrd", this.toUTF8(conceptoItem.Campo("RfcEmisorCtaOrd").ToString()));
                }

                if (conceptoItem.Campo("NomBancoOrdExt") != "")
                {
                    pagoElement_Item.SetAttribute("NomBancoOrdExt", this.toUTF8(conceptoItem.Campo("NomBancoOrdExt").ToString()));
                }

                if (conceptoItem.Campo("CtaOrdenante") != "")
                {
                    pagoElement_Item.SetAttribute("CtaOrdenante", this.toUTF8(conceptoItem.Campo("CtaOrdenante").ToString()));
                }

                if (conceptoItem.Campo("RfcEmisorCtaBen") != "")
                {
                    pagoElement_Item.SetAttribute("RfcEmisorCtaBen", this.toUTF8(conceptoItem.Campo("RfcEmisorCtaBen").ToString()));
                }

                if (conceptoItem.Campo("CtaBeneficiario") != "")
                {
                    pagoElement_Item.SetAttribute("CtaBeneficiario", this.toUTF8(conceptoItem.Campo("CtaBeneficiario").ToString()));
                }

                if (conceptoItem.Campo("TipoCadPago") != "")
                {
                    pagoElement_Item.SetAttribute("TipoCadPago", this.toUTF8(conceptoItem.Campo("TipoCadPago").ToString()));
                }

                if (conceptoItem.Campo("CertificadoPago") != "")
                {
                    pagoElement_Item.SetAttribute("CertPago", this.toUTF8(conceptoItem.Campo("CertificadoPago").ToString()));
                }

                if (conceptoItem.Campo("CadenaPago") != "")
                {
                    pagoElement_Item.SetAttribute("CadPago", this.toUTF8(conceptoItem.Campo("CadenaPago").ToString()));
                }

                if (conceptoItem.Campo("SelloPago") != "")
                {
                    pagoElement_Item.SetAttribute("SelloPago", this.toUTF8(conceptoItem.Campo("SelloPago").ToString()));
                }


                clsLeer datos_Retenciones = new clsLeer();
                clsLeer datos_Traslados = new clsLeer();

                clsLeer datos_UUIDS = new clsLeer();
                datos_UUIDS.DataSetClase = informacionPago.DataSetClase;

                if (datos_UUIDS.Registros > 0)
                {
                    while (datos_UUIDS.Leer())
                    {
                        pagoElement_UUID_Relacionado_Item = this.newElement_PG("DoctoRelacionado");

                        pagoElement_UUID_Relacionado_Item.SetAttribute("IdDocumento", this.toUTF8(datos_UUIDS.Campo("UUID_Relacionado").ToString()));
                        pagoElement_UUID_Relacionado_Item.SetAttribute("Serie", this.toUTF8(datos_UUIDS.Campo("Serie_Relacionada").ToString()));
                        pagoElement_UUID_Relacionado_Item.SetAttribute("Folio", this.toUTF8(datos_UUIDS.Campo("Folio_Relacionado").ToString()));
                        pagoElement_UUID_Relacionado_Item.SetAttribute("MonedaDR", this.toUTF8(datos_UUIDS.Campo("Moneda").ToString()));

                        if (datos_UUIDS.CampoDouble("TipoCambio") >= 1)
                        {
                            if (datos_UUIDS.Campo("Moneda").ToUpper() == "MXN")
                            {
                                pagoElement_UUID_Relacionado_Item.SetAttribute("EquivalenciaDR", this.toUTF8("1"));
                            }
                            else
                            {
                                pagoElement_UUID_Relacionado_Item.SetAttribute("EquivalenciaDR", this.toUTF8(datos_UUIDS.CampoDouble("TipoCambio").ToString("0.###0")));
                            }
                            ////pagoElement_Item.SetAttribute("TipoCambioP", this.toUTF8(1.ToString("0.#0")));
                        }

                        //pagoElement_UUID_Relacionado_Item.SetAttribute("MetodoDePagoDR", this.toUTF8(datos_UUIDS.Campo("MetodoDePagoDR").ToString()));
                        pagoElement_UUID_Relacionado_Item.SetAttribute("NumParcialidad", this.toUTF8(datos_UUIDS.Campo("NumParcialidad").ToString()));
                        pagoElement_UUID_Relacionado_Item.SetAttribute("ImpSaldoAnt", this.toUTF8(datos_UUIDS.CampoDouble("Importe_SaldoAnterior").ToString("0.#0")));
                        pagoElement_UUID_Relacionado_Item.SetAttribute("ImpPagado", this.toUTF8(datos_UUIDS.CampoDouble("Importe_Pagado").ToString("0.#0")));
                        pagoElement_UUID_Relacionado_Item.SetAttribute("ImpSaldoInsoluto", this.toUTF8(datos_UUIDS.CampoDouble("Importe_SaldoInsoluto").ToString("0.#0")));

                        pagoElement_UUID_Relacionado_Item.SetAttribute("ObjetoImpDR", this.toUTF8(datos_UUIDS.Campo("ObjetoImpDR")));


                        if (datos_UUIDS.CampoBool("TieneRetenciones") || datos_UUIDS.CampoBool("TieneTraslados"))
                        {
                            /////////////// Impuestos 
                            pagoElement_Impuestos = this.newElement_PG("ImpuestosDR");

                            string sFiltro = "";
                            datos_Retenciones = new clsLeer();
                            sFiltro = string.Format(" UUID_Relacionado = '{0}' and TieneRetenciones = 1 ", datos_UUIDS.Campo("UUID_Relacionado"));
                            sFiltro = string.Format(" UUID_Relacionado = '{0}' ", datos_UUIDS.Campo("UUID_Relacionado"));
                            datos_Retenciones.DataRowsClase = datos_UUIDS.Tabla("UUIDs_Relacionados_Pagos_Retenciones").Select(sFiltro);

                            if (datos_Retenciones.Registros > 0)
                            {
                                pagoElement_Impuestos_Retenidos = this.newElement_PG("RetencionesDR");

                                while (datos_Retenciones.Leer())
                                {
                                    pagoElement_Impuestos_Item = this.newElement_PG("RetencionDR");

                                    pagoElement_Impuestos_Item.SetAttribute("BaseDR", datos_Retenciones.CampoDouble("BaseDR").ToString("0.#0"));
                                    pagoElement_Impuestos_Item.SetAttribute("ImpuestoDR", datos_Retenciones.Campo("ImpuestoDR"));
                                    pagoElement_Impuestos_Item.SetAttribute("TipoFactorDR", datos_Retenciones.Campo("TipoFactorDR"));
                                    pagoElement_Impuestos_Item.SetAttribute("TasaOCuotaDR", datos_Retenciones.CampoDouble("TasaOCuotaDR").ToString("0.#####0"));
                                    pagoElement_Impuestos_Item.SetAttribute("ImporteDR", datos_Retenciones.CampoDouble("ImporteDR").ToString("0.#0"));

                                    pagoElement_Impuestos_Retenidos.AppendChild(pagoElement_Impuestos_Item);
                                }
                                pagoElement_Impuestos.AppendChild(pagoElement_Impuestos_Retenidos);
                            }



                            datos_Traslados = new clsLeer();
                            sFiltro = string.Format(" UUID_Relacionado = '{0}' and TieneTraslados = 1 ", datos_UUIDS.Campo("UUID_Relacionado"));
                            sFiltro = string.Format(" UUID_Relacionado = '{0}' ", datos_UUIDS.Campo("UUID_Relacionado"));
                            datos_Traslados.DataRowsClase = datos_UUIDS.Tabla("UUIDs_Relacionados_Pagos_Traslados").Select(sFiltro);
                            if (datos_Traslados.Registros > 0)
                            {
                                pagoElement_Impuestos_Traslados = this.newElement_PG("TrasladosDR");
                                while (datos_Traslados.Leer())
                                {
                                    pagoElement_Impuestos_Item = this.newElement_PG("TrasladoDR");

                                    pagoElement_Impuestos_Item.SetAttribute("BaseDR", datos_Traslados.CampoDouble("BaseDR").ToString("0.#0"));
                                    pagoElement_Impuestos_Item.SetAttribute("ImpuestoDR", datos_Traslados.Campo("ImpuestoDR"));
                                    pagoElement_Impuestos_Item.SetAttribute("TipoFactorDR", datos_Traslados.Campo("TipoFactorDR"));
                                    pagoElement_Impuestos_Item.SetAttribute("TasaOCuotaDR", datos_Traslados.CampoDouble("TasaOCuotaDR").ToString("0.#####0"));
                                    pagoElement_Impuestos_Item.SetAttribute("ImporteDR", datos_Traslados.CampoDouble("ImporteDR").ToString("0.#0"));

                                    pagoElement_Impuestos_Traslados.AppendChild(pagoElement_Impuestos_Item);
                                }
                                pagoElement_Impuestos.AppendChild(pagoElement_Impuestos_Traslados);
                            }


                            /////////////// Impuestos 

                            pagoElement_UUID_Relacionado_Item.AppendChild(pagoElement_Impuestos);
                        }


                        pagoElement_Item.AppendChild(pagoElement_UUID_Relacionado_Item);
                    }

                    /////////////// ImpuestosP 
                    pagoElement_Impuestos_P = this.newElement_PG("ImpuestosP");

                    datos_Retenciones = new clsLeer();
                    datos_Retenciones.DataRowsClase = datos_UUIDS.Tabla("UUIDs_Relacionados_Pagos_ImpuestosP").Select(" EsRetencion = 1 ");

                    if (datos_Retenciones.Registros > 0)
                    {
                        pagoElement_Impuestos_Retenidos_P = this.newElement_PG("RetencionesP");

                        while (datos_Retenciones.Leer())
                        {
                            pagoElement_Impuestos_Item_P = this.newElement_PG("RetencionP");

                            pagoElement_Impuestos_Item_P.SetAttribute("ImpuestoP", datos_Retenciones.Campo("ImpuestoP"));
                            pagoElement_Impuestos_Item_P.SetAttribute("ImporteP", datos_Retenciones.CampoDouble("ImporteP").ToString("0.#0"));

                            pagoElement_Impuestos_Retenidos_P.AppendChild(pagoElement_Impuestos_Item_P);
                        }
                        pagoElement_Impuestos_P.AppendChild(pagoElement_Impuestos_Retenidos_P);
                    }


                    datos_Traslados = new clsLeer();
                    datos_Traslados.DataRowsClase = datos_UUIDS.Tabla("UUIDs_Relacionados_Pagos_ImpuestosP").Select(" EsTraslado = 1 ");
                    if (datos_Traslados.Registros > 0)
                    {
                        pagoElement_Impuestos_Traslados_P = this.newElement_PG("TrasladosP");
                        while (datos_Traslados.Leer())
                        {
                            pagoElement_Impuestos_Item_P = this.newElement_PG("TrasladoP");

                            pagoElement_Impuestos_Item_P.SetAttribute("BaseP", datos_Traslados.CampoDouble("BaseP").ToString("0.#0"));
                            pagoElement_Impuestos_Item_P.SetAttribute("ImpuestoP", datos_Traslados.Campo("ImpuestoP"));
                            pagoElement_Impuestos_Item_P.SetAttribute("TipoFactorP", datos_Traslados.Campo("TipoFactorP"));
                            pagoElement_Impuestos_Item_P.SetAttribute("TasaOCuotaP", datos_Traslados.CampoDouble("TasaOCuotaP").ToString("0.#####0"));
                            pagoElement_Impuestos_Item_P.SetAttribute("ImporteP", datos_Traslados.CampoDouble("ImporteP").ToString("0.#0"));



                            pagoElement_Impuestos_Traslados_P.AppendChild(pagoElement_Impuestos_Item_P);
                        }
                        pagoElement_Impuestos_P.AppendChild(pagoElement_Impuestos_Traslados_P);
                    }

                    pagoElement_Item.AppendChild(pagoElement_Impuestos_P);
                    /////////////// ImpuestosP 
                }

                pagoElement.AppendChild(pagoElement_Item_Totales);
                pagoElement.AppendChild(pagoElement_Item);
            }




            //// Agregar el nodo COMPLEMENTO PAGO 
            complementoElement.AppendChild(pagoElement);
            documentElement.AppendChild(complementoElement);


            if(this.Complemento != null)
            {
                try
                {
                    documentElement.AppendChild(this.Complemento);
                }
                catch(Exception exception3)
                {
                    exception2 = exception3;
                    this.lastError = "Error al agregar datos de complemento. " + exception2.Message;
                    LogProceso(lastError);
                    return "";
                }
            }

            if(this.Addenda != null)
            {
                try
                {
                    documentElement.AppendChild(this.Addenda);
                }
                catch(Exception exception4)
                {
                    exception2 = exception4;
                    this.lastError = "Error al agregar datos de addenda. " + exception2.Message;
                    LogProceso(lastError);
                    return "";
                }
            }

            if((filename != "") && (filename != null))
            {
                this.cfd.Save(filename);
            }

            return this.cfd.InnerXml;
        }

        private void InitCFD()
        {
            this.cfd.RemoveAll();
            XmlNode newChild = this.cfd.CreateXmlDeclaration("1.0", "UTF-8", "");
            this.cfd.AppendChild(newChild);
            this.cfd.AppendChild(this.newElement("Comprobante"));

            InitCFD_DataSet();
        }

        private void InitCFD_DataSet()
        {
            dtsImpresion = new DataSet("ImpresionCFDI");

            Preparar_Dts_01_Comprobante();
            Preparar_Dts_02_Emisor();
            Preparar_Dts_03_Emisor_DomicilioFiscal();
            Preparar_Dts_04_Emisor_ExpedidoEn();
            Preparar_Dts_05_Emisor_Regimen();
            Preparar_Dts_06_Receptor();
            Preparar_Dts_07_Receptor_Domicilio();
            Preparar_Dts_08_Conceptos();
            Preparar_Dts_09_Traslados();
            Preparar_Dts_10_TimbreFiscal();
            Preparar_Dts_11_Logo();
        }

        public DataSet Impresion
        {
            get
            {
                dtsImpresion = new DataSet("ImpresionCFDI");
                dtsImpresion.Tables.Add(dtComprobante.Copy());
                dtsImpresion.Tables.Add(dtEmisor.Copy());
                dtsImpresion.Tables.Add(dtEmisorDomicilioFiscal.Copy());
                dtsImpresion.Tables.Add(dtEmisorDomicilioExpedidoEn.Copy());
                dtsImpresion.Tables.Add(dtEmisorRegimen.Copy());
                dtsImpresion.Tables.Add(dtReceptor.Copy());
                dtsImpresion.Tables.Add(dtReceptorDomililio.Copy());
                dtsImpresion.Tables.Add(dtConceptos.Copy());
                dtsImpresion.Tables.Add(dtConceptos_Pago.Copy()); 
                dtsImpresion.Tables.Add(dtCFDI_Relacionados.Copy());
                dtsImpresion.Tables.Add(dtConceptos_InformacionAduanera.Copy());
                dtsImpresion.Tables.Add(dtImpuestos.Copy());
                dtsImpresion.Tables.Add(dtRetenciones.Copy());
                dtsImpresion.Tables.Add(dtTraslados.Copy());
                dtsImpresion.Tables.Add(dtTimbreFiscal.Copy());
                dtsImpresion.Tables.Add(dtLogo.Copy());
                dtsImpresion.Tables.Add(dtCBB.Copy());



                return dtsImpresion;
            }
            set { dtsImpresion = value; }
        }

        private void Preparar_Dts_01_Comprobante()
        {
            dtComprobante = new DataTable("Comprobante");

            dtComprobante.Columns.Add("Version", System.Type.GetType("System.Double"));
            dtComprobante.Columns.Add("TipoDeComprobante", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("Fecha", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("FechaFolioFiscalOrig", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("Serie", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("SerieFolioFiscalOrig", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("Folio", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("FolioFiscalOrig", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("NoCertificado", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("Certificado", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("Sello", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("FormaDePago", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("FormaDePago_Descripcion", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("MetodoDePago", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("MetodoDePagoDescripcion", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("Moneda", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("MontoFolioFiscalOrig", System.Type.GetType("System.Double"));
            dtComprobante.Columns.Add("MotivoDescuento", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("NumCtaPago", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("CondicionesDePago", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("LugarExpedicion", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("TipoCambio", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("Descuento", System.Type.GetType("System.Double"));
            dtComprobante.Columns.Add("SubTotal", System.Type.GetType("System.Double"));
            dtComprobante.Columns.Add("Total", System.Type.GetType("System.Double"));
            dtComprobante.Columns.Add("TipoDeDocumento", System.Type.GetType("System.String"));

            dtComprobante.Columns.Add("EfectoComprobante", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("EfectoComprobante_Descripcion", System.Type.GetType("System.String"));

            dtComprobante.Columns.Add("TipoDeRelacion", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("TipoDeRelacion_Descripcion", System.Type.GetType("System.String"));


            dtComprobante.Columns.Add("SAT_ClaveDeConfirmacion", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("CFDI_Relacionado_CPago", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("Serie_Relacionada_CPago", System.Type.GetType("System.String"));
            dtComprobante.Columns.Add("Folio_Relacionado_CPago", System.Type.GetType("System.String"));



            object[] obj = {
                version.ToString(), toUTF8(tipoDeComprobante),
                fecha.ToString("yyyy-MM-ddTHH:mm:ss"), FechaFolioFiscalOrig.ToString("yyyy-MM-ddTHH:mm:ss"),
                toUTF8(serie), toUTF8(SerieFolioFiscalOrig),
                toUTF8(folio.ToString()), toUTF8(FolioFiscalOrig),
                toUTF8(noCertificado), toUTF8(certificado),
                toUTF8(sello),
                toUTF8(formaDePago), toUTF8(formaDePago_Descripcion),
                toUTF8(metodoDePago), toUTF8(metodoDePagoDescripcion),
                mMoneda, MontoFolioFiscalOrig,
                toUTF8(motivoDescuento), toUTF8(NumCtaPago), toUTF8(condicionesDePago),
                toUTF8(LugarExpedicion), toUTF8(TipoCambio), descuento, subTotal, total, mTipoDeDocumento,
                toUTF8(mEfectoDeComprobante), toUTF8(mEfectoDeComprobante_Descripcion),
                toUTF8(mTipoDeRelacion), toUTF8(mTipoDeRelacion_Descripcion),

                toUTF8(mSAT_ClaveDeConfirmacion), toUTF8(mCFDI_Relacionado_CPago), toUTF8(mSerie_Relacionada_CPago), toUTF8(mFolio_Relacionado_CPago.ToString())
                           };
            dtComprobante.Rows.Add(obj);

        dtImpuestos = new DataTable("Impuestos");
            dtImpuestos.Columns.Add("TotalImpuestosRetenidos", System.Type.GetType("System.Double"));
            dtImpuestos.Columns.Add("TotalImpuestosTrasladados", System.Type.GetType("System.Double"));

            object[] obj_Imp =
            {
                this.toUTF8(this.Impuestos.totalImpuestosRetenidos.ToString()),
                this.toUTF8(this.Impuestos.totalImpuestosTrasladados.ToString())
            };
            dtImpuestos.Rows.Add(obj_Imp);
        }

        private void Preparar_Dts_02_Emisor()
        {
            dtEmisor = new DataTable("Emisor");

            dtEmisor.Columns.Add("RFC", System.Type.GetType("System.String"));
            dtEmisor.Columns.Add("Nombre", System.Type.GetType("System.String"));

            object[] obj = { toUTF8(Emisor.rfc), toUTF8(Emisor.nombre) };
            dtEmisor.Rows.Add(obj);
        }

        private void Preparar_Dts_03_Emisor_DomicilioFiscal()
        {
            dtEmisorDomicilioFiscal = new DataTable("DomicilioFiscal");

            dtEmisorDomicilioFiscal.Columns.Add("Pais", System.Type.GetType("System.String"));
            dtEmisorDomicilioFiscal.Columns.Add("Estado", System.Type.GetType("System.String"));
            dtEmisorDomicilioFiscal.Columns.Add("Localidad", System.Type.GetType("System.String"));
            dtEmisorDomicilioFiscal.Columns.Add("Municipio", System.Type.GetType("System.String"));

            dtEmisorDomicilioFiscal.Columns.Add("Colonia", System.Type.GetType("System.String"));
            dtEmisorDomicilioFiscal.Columns.Add("Calle", System.Type.GetType("System.String"));
            dtEmisorDomicilioFiscal.Columns.Add("NoExterior", System.Type.GetType("System.String"));
            dtEmisorDomicilioFiscal.Columns.Add("NoInterior", System.Type.GetType("System.String"));

            dtEmisorDomicilioFiscal.Columns.Add("CodigoPostal", System.Type.GetType("System.String"));
            dtEmisorDomicilioFiscal.Columns.Add("Referencia", System.Type.GetType("System.String"));

            object[] obj = {
                toUTF8(Emisor.DomicilioFiscal.pais), toUTF8(Emisor.DomicilioFiscal.estado),
                toUTF8(Emisor.DomicilioFiscal.localidad), toUTF8(Emisor.DomicilioFiscal.municipio),
                toUTF8(Emisor.DomicilioFiscal.colonia), toUTF8(Emisor.DomicilioFiscal.calle),
                 toUTF8(Emisor.DomicilioFiscal.noExterior), toUTF8(Emisor.DomicilioFiscal.noInterior),
                toUTF8(Emisor.DomicilioFiscal.codigoPostal), toUTF8(Emisor.DomicilioFiscal.referencia)
                           };
            dtEmisorDomicilioFiscal.Rows.Add(obj);

        }

        private void Preparar_Dts_04_Emisor_ExpedidoEn()
        {
            dtEmisorDomicilioExpedidoEn = new DataTable("ExpedidoEn");

            dtEmisorDomicilioExpedidoEn.Columns.Add("Pais", System.Type.GetType("System.String"));
            dtEmisorDomicilioExpedidoEn.Columns.Add("Estado", System.Type.GetType("System.String"));
            dtEmisorDomicilioExpedidoEn.Columns.Add("Localidad", System.Type.GetType("System.String"));
            dtEmisorDomicilioExpedidoEn.Columns.Add("Municipio", System.Type.GetType("System.String"));
            dtEmisorDomicilioExpedidoEn.Columns.Add("Colonia", System.Type.GetType("System.String"));
            dtEmisorDomicilioExpedidoEn.Columns.Add("Calle", System.Type.GetType("System.String"));
            dtEmisorDomicilioExpedidoEn.Columns.Add("NoExterior", System.Type.GetType("System.String"));
            dtEmisorDomicilioExpedidoEn.Columns.Add("NoInterior", System.Type.GetType("System.String"));
            dtEmisorDomicilioExpedidoEn.Columns.Add("CodigoPostal", System.Type.GetType("System.String"));
            dtEmisorDomicilioExpedidoEn.Columns.Add("Referencia", System.Type.GetType("System.String"));

            object[] obj = {
                toUTF8(Emisor.ExpedidoEn.pais), toUTF8(Emisor.ExpedidoEn.estado),
                toUTF8(Emisor.ExpedidoEn.localidad), toUTF8(Emisor.ExpedidoEn.municipio),
                toUTF8(Emisor.ExpedidoEn.colonia), toUTF8(Emisor.ExpedidoEn.calle),
                 toUTF8(Emisor.ExpedidoEn.noExterior), toUTF8(Emisor.ExpedidoEn.noInterior),
                toUTF8(Emisor.ExpedidoEn.codigoPostal), toUTF8(Emisor.ExpedidoEn.referencia)
                           };
            dtEmisorDomicilioExpedidoEn.Rows.Add(obj);
        }

        private void Preparar_Dts_05_Emisor_Regimen()
        {
            dtEmisorRegimen = new DataTable("Regimen");
            dtEmisorRegimen.Columns.Add("Regimen", System.Type.GetType("System.String"));
            dtEmisorRegimen.Columns.Add("Regimen_Descripcion", System.Type.GetType("System.String"));

            object[] obj = { toUTF8(this.Emisor.RegimenFiscal_33.Regimen), toUTF8(this.Emisor.RegimenFiscal_33.Descripcion) };
            dtEmisorRegimen.Rows.Add(obj);
        }

        private void Preparar_Dts_06_Receptor()
        {
            dtReceptor = new DataTable("Receptor");

            dtReceptor.Columns.Add("RFC", System.Type.GetType("System.String"));
            dtReceptor.Columns.Add("Nombre", System.Type.GetType("System.String"));

            dtReceptor.Columns.Add("UsoDeCFDI", System.Type.GetType("System.String"));
            dtReceptor.Columns.Add("UsoDeCFDI_Descripcion", System.Type.GetType("System.String"));

            dtReceptor.Columns.Add("RegimenFiscal", System.Type.GetType("System.String"));
            dtReceptor.Columns.Add("RegimenFiscal_Descripcion", System.Type.GetType("System.String"));


            //object[] obj = { toUTF8(Receptor.rfc), toUTF8(Receptor.nombre) }; 
            object[] obj =
            {
                toUTF8(Receptor.rfc), toUTF8(Receptor.nombre),
                toUTF8(Receptor.UsoDeCFDI), toUTF8(Receptor.UsoDeCFDI_Descripcion),
                toUTF8(Receptor.RegimenFiscal), toUTF8(Receptor.RegimenFiscal_Descripcion)
            };
            dtReceptor.Rows.Add(obj);
        }

        private void Preparar_Dts_07_Receptor_Domicilio()
        {
            dtReceptorDomililio = new DataTable("Domicilio");

            dtReceptorDomililio.Columns.Add("Pais", System.Type.GetType("System.String"));
            dtReceptorDomililio.Columns.Add("Estado", System.Type.GetType("System.String"));
            dtReceptorDomililio.Columns.Add("Localidad", System.Type.GetType("System.String"));
            dtReceptorDomililio.Columns.Add("Municipio", System.Type.GetType("System.String"));
            dtReceptorDomililio.Columns.Add("Colonia", System.Type.GetType("System.String"));
            dtReceptorDomililio.Columns.Add("Calle", System.Type.GetType("System.String"));
            dtReceptorDomililio.Columns.Add("NoExterior", System.Type.GetType("System.String"));
            dtReceptorDomililio.Columns.Add("NoInterior", System.Type.GetType("System.String"));
            dtReceptorDomililio.Columns.Add("CodigoPostal", System.Type.GetType("System.String"));
            dtReceptorDomililio.Columns.Add("Referencia", System.Type.GetType("System.String"));

            object[] obj = {
                toUTF8(Receptor.Domicilio.pais), toUTF8(Receptor.Domicilio.estado),
                toUTF8(Receptor.Domicilio.localidad), toUTF8(Receptor.Domicilio.municipio),
                toUTF8(Receptor.Domicilio.colonia), toUTF8(Receptor.Domicilio.calle),
                toUTF8(Receptor.Domicilio.noExterior), toUTF8(Receptor.Domicilio.noInterior),
                toUTF8(Receptor.Domicilio.codigoPostal), toUTF8(Receptor.Domicilio.referencia)
                           };
            dtReceptorDomililio.Rows.Add(obj);
        }

        private void Preparar_Dts_08_Conceptos()
        {
            ////////////////////////////////////////////////////////////////
            dtConceptos = new DataTable("Conceptos");
            dtConceptos.Columns.Add("Orden", System.Type.GetType("System.Int32"));
            dtConceptos.Columns.Add("ClaveProdServ", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("NoIdentificacion", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("Descripcion", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("ClaveUnidad", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("Unidad", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("Cantidad", System.Type.GetType("System.Double"));
            dtConceptos.Columns.Add("ValorUnitario", System.Type.GetType("System.Double"));
            dtConceptos.Columns.Add("TasaIVA", System.Type.GetType("System.Double"));
            dtConceptos.Columns.Add("ImporteIva", System.Type.GetType("System.Double"));
            dtConceptos.Columns.Add("Importe", System.Type.GetType("System.Double"));
            dtConceptos.Columns.Add("Notas", System.Type.GetType("System.String"));

            dtConceptos.Columns.Add("Impuestos_Base", System.Type.GetType("System.Double"));
            dtConceptos.Columns.Add("Impuesto_ImpuestoClave", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("Impuesto_Impuesto", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("Impuestos_TipoFactor", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("Impuestos_TasaOCuota", System.Type.GetType("System.Double"));
            dtConceptos.Columns.Add("Impuestos_Importe", System.Type.GetType("System.Double"));

            dtConceptos.Columns.Add("Impuestos_TasaOCuota_Retencion", System.Type.GetType("System.Double"));
            dtConceptos.Columns.Add("Impuestos_Importe_Retencion", System.Type.GetType("System.Double"));
            dtConceptos.Columns.Add("ObjetoDeImpuesto", System.Type.GetType("System.String"));


            ////////////////////////////////////////////////////////////////
            dtConceptos_Pago = new DataTable("Conceptos_Pagos");
            dtConceptos_Pago.Columns.Add("FechaDePago", System.Type.GetType("System.Int32"));
            dtConceptos_Pago.Columns.Add("FormaDePago", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("FormaDePago_Descripcion", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("Moneda", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("Moneda_Descripcion", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("Monto", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("NumeroDeOperacion", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("RfcEmisorCtaOrd", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("NomBancoOrdExt", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("CtaOrdenante", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("RfcEmisorCtaBen", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("CtaBeneficiario", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("TipoCadPago", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("CertificadoPago", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("CadenaPago", System.Type.GetType("System.String"));
            dtConceptos_Pago.Columns.Add("SelloPago", System.Type.GetType("System.String"));



            //x FechaDePago, FormaDePago, Moneda, TipoCambio, 
            //Monto, NumeroDeOperacion, RfcEmisorCtaOrd, NomBancoOrdExt, CtaOrdenante, 
            //RfcEmisorCtaBen, CtaBeneficiario, TipoCadPago, CertificadoPago, CadenaPago, SelloPago


            ////////////////////////////////////////////////////////////////
            string[] sListaColumnas = 
            { 
                "Serie", "Folio", "Serie_Relacionada", "Folio_Relacionado", "UUID_Relacionado", "Total_Base", "SubTotal", "IVA", "Total",
                "Moneda", "Moneda_Descripcion", "TipoDeCambio", "MetodoDePago", "MetodoDePago_Descripcion", 
                "NumParcialidad", "Importe_SaldoAnterior", "Importe_Pagado", "Importe_SaldoInsoluto", 
                "MetodoDePagoDR", "MetodoDePagoDR_Descripcion"
            };

            dtCFDI_Relacionados = new DataTable("CFDI_Relacionados");
            dtCFDI_Relacionados.Columns.Add("Serie", System.Type.GetType("System.String"));
            dtCFDI_Relacionados.Columns.Add("Folio", System.Type.GetType("System.Int32"));
            dtCFDI_Relacionados.Columns.Add("Serie_Relacionada", System.Type.GetType("System.String"));
            dtCFDI_Relacionados.Columns.Add("Folio_Relacionado", System.Type.GetType("System.Int32"));
            dtCFDI_Relacionados.Columns.Add("UUID_Relacionado", System.Type.GetType("System.String"));
            dtCFDI_Relacionados.Columns.Add("Total_Base", System.Type.GetType("System.Double"));
            dtCFDI_Relacionados.Columns.Add("SubTotal", System.Type.GetType("System.Double"));
            dtCFDI_Relacionados.Columns.Add("IVA", System.Type.GetType("System.Double"));
            dtCFDI_Relacionados.Columns.Add("Total", System.Type.GetType("System.Double"));


            dtCFDI_Relacionados.Columns.Add("Moneda", System.Type.GetType("System.String"));
            dtCFDI_Relacionados.Columns.Add("Moneda_Descripcion", System.Type.GetType("System.String"));
            dtCFDI_Relacionados.Columns.Add("TipoDeCambio", System.Type.GetType("System.Double"));
            dtCFDI_Relacionados.Columns.Add("MetodoDePago", System.Type.GetType("System.String"));
            dtCFDI_Relacionados.Columns.Add("MetodoDePago_Descripcion", System.Type.GetType("System.String"));
            dtCFDI_Relacionados.Columns.Add("Parcialidad", System.Type.GetType("System.Int32")); 
            dtCFDI_Relacionados.Columns.Add("Importe_SaldoAnterior", System.Type.GetType("System.Double"));
            dtCFDI_Relacionados.Columns.Add("Importe_Pagado", System.Type.GetType("System.Double"));
            dtCFDI_Relacionados.Columns.Add("Importe_SaldoInsoluto", System.Type.GetType("System.Double"));



            ////////////////////////////////////////////////////////////////
            dtConceptos_InformacionAduanera = new DataTable("ConceptosInformacionAduanera");
            dtConceptos_InformacionAduanera.Columns.Add("Orden", System.Type.GetType("System.Int32"));
            dtConceptos_InformacionAduanera.Columns.Add("Id", System.Type.GetType("System.Int32"));
            dtConceptos_InformacionAduanera.Columns.Add("Numero", System.Type.GetType("System.String"));
            dtConceptos_InformacionAduanera.Columns.Add("Fecha", System.Type.GetType("System.String"));
            dtConceptos_InformacionAduanera.Columns.Add("Aduana", System.Type.GetType("System.String"));



            clsLeer uuids = new clsLeer();
            uuids.DataSetClase = dtsUUIDs_Relacionados;
            uuids.FiltrarColumnas(1, sListaColumnas);
            uuids.RenombrarTabla(1, "CFDI_Relacionados");
            dtCFDI_Relacionados = uuids.DataTableClase;


            while(uuids.Leer() && 1 == 0)
            {
                object[] obj = {
                    uuids.Campo("Serie"), uuids.CampoInt("Folio"), uuids.Campo("Serie_Relacionada"), uuids.Campo("Folio_Relacionado"),
                    uuids.Campo("UUID_Relacionado"),
                    uuids.CampoDouble("Total_Base"), uuids.CampoDouble("SubTotal"), uuids.CampoDouble("IVA"), uuids.CampoDouble("Total")
                               };
                dtCFDI_Relacionados.Rows.Add(obj);
            }


            //////// Complemento de Pago 

            //////// Complemento de Pago 
        }

        private void Preparar_Dts_09_Traslados()
        {
            dtTraslados = new DataTable("Traslados");
            dtTraslados.Columns.Add("Impuesto", System.Type.GetType("System.String"));
            dtTraslados.Columns.Add("TipoFactor", System.Type.GetType("System.String"));
            dtTraslados.Columns.Add("Tasa", System.Type.GetType("System.Int32"));
            dtTraslados.Columns.Add("Importe", System.Type.GetType("System.Double"));
            dtTraslados.Columns.Add("ClaveImpuesto", System.Type.GetType("System.String"));
            dtTraslados.Columns.Add("TasaOCuota", System.Type.GetType("System.Double"));


            ////dtTraslados.Columns.Add("Impuestos_Base", System.Type.GetType("System.Double"));
            ////dtTraslados.Columns.Add("Impuesto_Impuesto", System.Type.GetType("System.String"));
            ////dtTraslados.Columns.Add("Impuestos_TipoFactor", System.Type.GetType("System.String"));
            ////dtTraslados.Columns.Add("Impuestos_Importe", System.Type.GetType("System.Double"));




            dtRetenciones = new DataTable("Retenciones");
            dtRetenciones.Columns.Add("Impuesto", System.Type.GetType("System.String"));
            dtRetenciones.Columns.Add("Importe", System.Type.GetType("System.Double"));

        }

        private void Preparar_Dts_10_TimbreFiscal()
        {
            dtTimbreFiscal = new DataTable("TimbreFiscal");

            dtTimbreFiscal.Columns.Add("FechaTimbrado", System.Type.GetType("System.String"));
            dtTimbreFiscal.Columns.Add("UUID", System.Type.GetType("System.String"));
            dtTimbreFiscal.Columns.Add("NoCertificadoSAT", System.Type.GetType("System.String"));
            dtTimbreFiscal.Columns.Add("SelloCFD", System.Type.GetType("System.String"));
            dtTimbreFiscal.Columns.Add("SelloSAT", System.Type.GetType("System.String"));
            dtTimbreFiscal.Columns.Add("Version", System.Type.GetType("System.String"));
            dtTimbreFiscal.Columns.Add("RfcProveedorCertificacion", System.Type.GetType("System.String"));

            ////object[] obj = { "", "", "", "", "", "" };
            ////dtTimbreFiscal.Rows.Add(obj);
        }

        private void Preparar_Dts_11_Logo()
        {
            dtLogo = new DataTable("Logo");
            dtLogo.Columns.Add("Logo", System.Type.GetType("System.Byte[]"));
            //dtLogo = DtIFacturacion.Logo.Copy();

            dtCBB = new DataTable("CBB");
            dtCBB.Columns.Add("CBB", System.Type.GetType("System.Byte[]"));

        }

        private bool isOK()
        {
            return this.isOK(false);
        }

        private bool isOK( bool omitSeal )
        {
            try
            {
                LogProceso("Validando versión");
                if(this.version != 4.0)
                {
                    this.lastError = "Valor incorrecto en version: debe ser 4.0";
                    return false;
                }

                LogProceso("Validando serie");
                if(this.serie == null)
                {
                    this.serie = "";
                }

                LogProceso("Validando longitud de serie");
                if(this.serie.Length > 0x19)
                {
                    this.lastError = "Valor incorrecto en atributo: serie";
                    return false;
                }

                LogProceso("Validando folio");
                if(this.folio <= 0L)
                {
                    this.lastError = "Valor incorrecto en atributo: folio, el valor no puede ser menor o igual que 0";
                    return false;
                }

                LogProceso("Validando longitud de folio");
                if(this.folio.ToString().Length > 20)
                {
                    this.lastError = "Valor incorrecto en atributo: folio";
                    return false;
                }

                LogProceso("Validando sello");
                if(!omitSeal && ((this.sello == "") || (this.sello == null)))
                {
                    this.lastError = "Falta atributo requerido: sello";
                    return false;
                }

                LogProceso("Validando forma de pago");
                if((this.formaDePago == "") || (this.formaDePago == null))
                {
                    this.lastError = "Falta atributo requerido: formaDePago";
                    return false;
                }

                LogProceso("Validando número de certificado");
                if(this.noCertificado == null)
                {
                    this.lastError = "Valor incorrecto en atributo: noCertificado";
                    return false;
                }

                LogProceso("Validando longitud de número de certificado");
                if((this.noCertificado == "") || (this.noCertificado.Length != 20))
                {
                    this.lastError = "Valor incorrecto en atributo: noCertificado";
                    return false;
                }

                LogProceso("Validando certificado");
                if((this.certificado == null) || (this.certificado == ""))
                {
                    this.lastError = "Valor incorrecto en atributo: certificado";
                    return false;
                }

                LogProceso("Validando subtotal");
                if(this.subTotal < 0.0)
                {
                    this.lastError = "Valor incorrecto en atributo: subTotal";
                    return false;
                }

                LogProceso("Validando total");
                if(this.total < 0.0)
                {
                    this.lastError = "Valor incorrecto en atributo: total";
                    return false;
                }

                LogProceso("Validando tipo de comprobante");
                if((this.tipoDeComprobante == "") || (this.tipoDeComprobante == null))
                {
                    this.lastError = "Falta atributo requerido: tipoDeComprobante";
                    return false;
                }

                //LogProceso("Validando método de pago");
                //if((this.metodoDePago == "") || (this.metodoDePago == null))
                //{
                //    this.lastError = "Falta atributo requerido: metodoDePago";
                //    return false;
                //}

                LogProceso("Validando lugar de expedición");
                if((this.LugarExpedicion == "") || (this.LugarExpedicion == null))
                {
                    this.lastError = "Falta atributo requerido: LugarExpedicion";
                    return false;
                }

                LogProceso("Validando folio fiscal");
                if(((this.FolioFiscalOrig != "") && (this.FolioFiscalOrig != null)) && (this.MontoFolioFiscalOrig < 0.0))
                {
                    this.lastError = "Valor incorreto en atributo: MontoFolioFiscalOrig";
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                LogProceso("isOK", ex.Message);
                return false;
            }
        }

        public bool loadByXMLFile( string filename )
        {
            try
            {
                string xml = File.ReadAllText(filename);
                return this.loadByXMLString(xml);
            }
            catch(Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public bool loadByXMLString( string xml )
        {
            try
            {
                XmlDocument document = new XmlDocument();
                clsFromXML mxml = new clsFromXML();
                bool flag = true;
                document.LoadXml(xml);
                XmlNode documentElement = document.DocumentElement;
                this.version = mxml.doubleAttValue(documentElement, "version");
                this.serie = mxml.stringAttValue(documentElement, "serie");
                this.folio = mxml.longAttValue(documentElement, "folio");
                this.fecha = mxml.dateTimeAttValue(documentElement, "fecha");
                this.sello = mxml.stringAttValue(documentElement, "sello");
                this.formaDePago = mxml.stringAttValue(documentElement, "formaDePago");
                this.noCertificado = mxml.stringAttValue(documentElement, "noCertificado");
                this.condicionesDePago = mxml.stringAttValue(documentElement, "condicionesDePago");
                this.subTotal = mxml.doubleAttValue(documentElement, "subTotal");
                this.descuento = mxml.doubleAttValue(documentElement, "descuento");
                this.motivoDescuento = mxml.stringAttValue(documentElement, "motivoDescuento");
                this.TipoCambio = mxml.stringAttValue(documentElement, "TipoCambio");
                this.Moneda = mxml.stringAttValue(documentElement, "Moneda");
                this.total = mxml.doubleAttValue(documentElement, "total");
                this.tipoDeComprobante = mxml.stringAttValue(documentElement, "tipoDeComprobante");
                this.metodoDePago = mxml.stringAttValue(documentElement, "metodoDePago");
                this.NumCtaPago = mxml.stringAttValue(documentElement, "NumCtaPago");
                XmlNode mainNode = mxml.getNode("Emisor", documentElement.ChildNodes);
                if(mainNode != null)
                {
                    if(!this.Emisor.loadFromXML(mainNode))
                    {
                        flag = false;
                    }
                    mainNode = null;
                }
                else
                {
                    flag = false;
                }
                XmlNode node3 = mxml.getNode("Receptor", documentElement.ChildNodes);
                if(node3 != null)
                {
                    if(!this.Receptor.loadFromXML(node3))
                    {
                        flag = false;
                    }
                    node3 = null;
                }
                else
                {
                    flag = false;
                }
                XmlNode node4 = mxml.getNode("Conceptos", documentElement.ChildNodes);
                if(node4 != null)
                {
                    foreach(XmlNode node5 in node4.ChildNodes)
                    {
                        clsConcepto concepto = new clsConcepto();
                        if(!concepto.loadFromXML(node5))
                        {
                            flag = false;
                        }
                        this.Conceptos.Add(concepto);
                        concepto = null;
                    }
                    node4 = null;
                }
                else
                {
                    flag = false;
                }
                XmlNode node6 = mxml.getNode("Impuestos", documentElement.ChildNodes);
                if(node6 != null)
                {
                    if(!this.Impuestos.loadFromXML(node6))
                    {
                        flag = false;
                    }
                    node6 = null;
                }
                else
                {
                    flag = false;
                }
                this.Complemento = (XmlElement)mxml.getNode("Complemento", documentElement.ChildNodes);
                this.Addenda = (XmlElement)mxml.getNode("Addenda", documentElement.ChildNodes);
                document = null;
                mxml = null;
                documentElement = null;
                return flag;
            }
            catch(Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public void new_schemaLocation( string slocation )
        {
            this.schemaLocation.Add(slocation);
        }

        public void new_xmlns( string name, string value )
        {
            this.xmlns.Add(name + ";" + value);
        }

        public XmlElement newElement( string sName )
        {
            return this.cfd.CreateElement(this.defPrefix, sName, this.defURI);
        }

        public XmlElement newElement_PG( string sName )
        {
            //return this.cfd.CreateElement(this.defPrefix_nm, sName, this.defURI_nm);

            return this.cfd.CreateElement(this.defPrefix_nm, sName, this.defURI_nm);
            //return this.cfd.CreateElement(this.defPrefix_nm + ":" + sName);
        }

        public void SetAttibute( XmlElement oXmlElement, string name, string value )
        {
            oXmlElement.SetAttribute(name, value);
        }

        private double t_importe( double v )
        {
            v = Math.Round(v, 6);
            return v;
        }

        public string toUTF8( string stext )
        {
            UTF8Encoding encoding = new UTF8Encoding();

            if(stext == null)
            {
                stext = "";
            }

            byte[] bytes = encoding.GetBytes(stext);
            return encoding.GetString(bytes);
        }

        public string toImage( byte[] bytes )
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetString(bytes);
        }

        public DataSet UUIDs_Relacionados
        {
            get { return dtsUUIDs_Relacionados; }
            set { dtsUUIDs_Relacionados = value; }
        }

        public DataSet Informacion_Pago
        { 
            get { return dtsInformacion_Pago; }
            set { dtsInformacion_Pago = value; }
        }

        public XmlElement Addenda
        {
            get
            {
                return this.mAddenda;
            }
            set
            {
                this.mAddenda = value;
            }
        }

        public string certificado
        {
            get
            {
                return this.mcertificado;
            }
            set
            {
                this.mcertificado = value;
            }
        }

        public XmlDocument cfdXmlDocument
        {
            get
            {
                return this.cfd;
            }
        }

        public XmlElement Complemento
        {
            get
            {
                return this.mComplemento;
            }
            set
            {
                this.mComplemento = value;
            }
        }

        public string condicionesDePago
        {
            get
            {
                return this.mcondicionesDePago;
            }
            set
            {
                if(value == null)
                {
                    value = "";
                }
                this.mcondicionesDePago = value.Trim();
            }
        }

        public double descuento
        {
            get
            {
                return this.mdescuento;
            }
            set
            {
                this.mdescuento = this.t_importe(value);
            }
        }

        public DateTime fecha
        {
            get
            {
                return this.mfecha;
            }
            set
            {
                this.mfecha = value;
            }
        }

        public DateTime FechaFolioFiscalOrig
        {
            get
            {
                return this.mFechaFolioFiscalOrig;
            }
            set
            {
                this.mFechaFolioFiscalOrig = value;
            }
        }

        public long folio
        {
            get
            {
                return this.mfolio;
            }
            set
            {
                this.mfolio = value;
            }
        }

        public string FolioRelacionado
        {
            get { return mFolioRelacionado; }
            set { mFolioRelacionado = value; }
        }

        public string FolioFiscalOrig
        {
            get
            {
                return this.mFolioFiscalOrig;
            }
            set
            {
                this.mFolioFiscalOrig = value;
            }
        }

        public string formaDePago
        {
            get
            {
                return this.mformaDePago;
            }
            set
            {
                if(value == null)
                {
                    value = "";
                }
                this.mformaDePago = value.Trim();
            }
        }

        public string formaDePago_Descripcion
        {
            get
            {
                return this.mformaDePago_Descripcion;
            }
            set
            {
                if(value == null)
                {
                    value = "";
                }
                this.mformaDePago_Descripcion = value.Trim();
            }
        }

        public string lastError
        {
            get
            {
                return this.mlastError;
            }
            set
            {
                this.mlastError = value.Trim();
            }
        }

        public string LugarExpedicion
        {
            get
            {
                return this.mLugarExpedicion;
            }
            set
            {
                this.mLugarExpedicion = value;
            }
        }

        public string metodoDePago
        {
            get
            {
                return this.mmetodoDePago;
            }
            set
            {
                if(value == null)
                {
                    value = "";
                }
                this.mmetodoDePago = value.Trim();
            }
        }

        public string metodoDePagoDescripcion
        {
            get
            {
                return this.mmetodoDePagoDescripcion;
            }
            set
            {
                if(value == null)
                {
                    value = "";
                }
                this.mmetodoDePagoDescripcion = value.Trim();
            }
        }

        public string Moneda
        {
            get
            {
                return this.mMoneda;
            }
            set
            {
                this.mMoneda = value;
            }
        }

        public double MontoFolioFiscalOrig
        {
            get
            {
                return this.mMontoFolioFiscalOrig;
            }
            set
            {
                this.mMontoFolioFiscalOrig = this.t_importe(value);
            }
        }

        public string motivoDescuento
        {
            get
            {
                return this.mmotivoDescuento;
            }
            set
            {
                if(value == null)
                {
                    value = "";
                }
                this.mmotivoDescuento = value.Trim();
            }
        }

        public string noCertificado
        {
            get
            {
                return this.mnoCertificado;
            }
            set
            {
                this.mnoCertificado = value.Trim();
            }
        }

        public string NumCtaPago
        {
            get
            {
                return this.mNumCtaPago;
            }
            set
            {
                if(value == null)
                {
                    value = "";
                }
                this.mNumCtaPago = value;
            }
        }

        public string sello
        {
            get
            {
                return this.msello;
            }
            set
            {
                this.msello = value;
            }
        }

        public string serie
        {
            get
            {
                return this.mserie;
            }
            set
            {
                if(value == null)
                {
                    value = "";
                }
                this.mserie = value.Trim();
                this.mserie = this.mserie.ToUpper();
            }
        }

        public string SerieFolioFiscalOrig
        {
            get
            {
                return this.mSerieFolioFiscalOrig;
            }
            set
            {
                this.mSerieFolioFiscalOrig = value;
            }
        }

        public double subTotal
        {
            get
            {
                return this.msubTotal;
            }
            set
            {
                this.msubTotal = this.t_importe(value);
            }
        }

        public string TipoCambio
        {
            get
            {
                return this.mTipoCambio;
            }
            set
            {
                this.mTipoCambio = value;
            }
        }

        public string Exportacion
        {
            get { return mExportacion; }
            set { mExportacion = value; }
        }

        public string tipoDeComprobante
        {
            get
            {
                return this.mtipoDeComprobante;
            }
            set
            {
                if(value == null)
                {
                    value = "";
                }

                if(((value.Trim().ToLower() != "i") && (value.Trim().ToLower() != "e")) && (value.Trim().ToLower() != "t") && (value.Trim().ToLower() != "p"))
                {
                    value = "";
                }
                this.mtipoDeComprobante = value.Trim().ToLower();
            }
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
            get { return mEfectoDeComprobante; }
            set { mEfectoDeComprobante = value; }
        }

        public string EfectoDeComprobante_Descripcion
        {
            get { return mEfectoDeComprobante_Descripcion; }
            set { mEfectoDeComprobante_Descripcion = value; }
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

        public double total
        {
            get
            {
                return this.mtotal;
            }
            set
            {
                this.mtotal = this.t_importe(value);
            }
        }

        public double version
        {
            get
            {
                return this.mversion;
            }
            set
            {
                if(value < 0.0)
                {
                    value = 3.0;
                }
                this.mversion = value;
            }
        }

        public string cadenaOriginal
        {
            get { return mCadenaOriginal; }
            set { mCadenaOriginal = value; }
        }

        public string TipoDeDocumentoElectronico
        {
            get { return mTipoDeDocumento; }
            set { mTipoDeDocumento = value; }
        }

        public string Referencia
        {
            get { return sReferencia; }
            set { sReferencia = value; }
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

    }
}

