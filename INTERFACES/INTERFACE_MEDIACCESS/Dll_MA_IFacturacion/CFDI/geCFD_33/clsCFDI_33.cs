using System;
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

using Dll_MA_IFacturacion.CFDI; 
using Dll_MA_IFacturacion.CFDI.geCFD; 

namespace Dll_MA_IFacturacion.CFDI.geCFD_33
{
    public class clsCFDI_33_Factura
    {
        private string defPrefix = "cfdi";
        private string defURI = "http://www.sat.gob.mx/cfd/3";
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

        private string mTipoDeRelacion = "";
        private string mTipoDeRelacion_Descripcion = "";
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
        DataTable dtConceptos_InformacionAduanera;
        DataTable dtImpuestos;
        DataTable dtRetenciones;
        DataTable dtTraslados;
        DataTable dtTimbreFiscal;
        DataTable dtLogo;
        DataTable dtCBB;

        DataTable dtCFDI_Relacionados;

        DataSet dtsUUIDs_Relacionados = new DataSet();

        private string sObservaciones_01 = "";
        private string sObservaciones_02= "";
        private string sObservaciones_03 = "";
        private string sReferencia = "";

        private string sTipoDocumento = ""; 
        private string sTipoInsumo = "";  
        private string sRubroFinanciamiento = ""; 
        private string sFuenteFinanciamiento = "";


        public clsCFDI_33_Factura()
        {
            this.InitCFD();
        }

        public bool appendChildInCFD(XmlElement element)
        {
            try
            {
                this.cfd.DocumentElement.AppendChild(element);
                return true;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public object getObjectInArrayList(ArrayList oArrayList, int index)
        {
            try
            {
                return oArrayList[index];
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return null;
            }
        }

        private void LogProceso(string Mensaje)
        {
            LogProceso("", Mensaje);
        }

        private void LogProceso(string Funcion, string Mensaje)
        {
            clsGrabarError.LogFileError(string.Format("clsCFDI_33_Factura.{0}:    {1}", Funcion, Mensaje));
        }

        public string getXML(string filename, bool omitSeal)
        {
            this.InitCFD_DataSet(); //// Preparar el xml para la impresion 
            string sCadena = "";

            XmlElement documentElement;
            XmlElement elementEmisor;
            XmlElement elementReceptor;
            XmlElement elementConceptos;

            XmlElement elementUUIDs_Relacionados;
            XmlElement elementUUID_Relacionado;

            XmlElement element3;
            XmlElement element4;
            XmlElement element5;

            XmlElement elementImpuestos;
            XmlElement elementTraslados;
            XmlElement elementTraslado_Item;

            Exception exception2;
            if (!this.isOK(omitSeal))
            {
                LogProceso(lastError); 
                return "";
            }

            this.InitCFD();
            documentElement = this.cfd.DocumentElement;
            documentElement.SetAttribute("xmlns:cfdi", "http://www.sat.gob.mx/cfd/3");
            documentElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            documentElement.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
            
            foreach (string str2 in this.xmlns)
            {
                documentElement.SetAttribute(str2.Split(new char[] { ';' })[0], str2.Split(new char[] { ';' })[1]);
            }

            string str = "";
            foreach (string str3 in this.schemaLocation)
            {
                str = str + " " + str3;
            }

            documentElement.SetAttribute("schemaLocation", 
                "http://www.w3.org/2001/XMLSchema-instance", 
                "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd" + str);
            
            documentElement.SetAttribute("Version", this.version.ToString("#.0"));
            if ((this.serie != "") && (this.serie != null))
            {
                documentElement.SetAttribute("Serie", this.toUTF8(this.serie));
            }

            if (this.folio > 0L)
            {
                documentElement.SetAttribute("Folio", this.toUTF8(this.folio.ToString()));
            }

            documentElement.SetAttribute("Fecha", this.fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
            documentElement.SetAttribute("FormaPago", this.toUTF8(this.formaDePago));

            if ((this.condicionesDePago != "") && (this.condicionesDePago != null))
            {
                documentElement.SetAttribute("CondicionesDePago", this.toUTF8(this.condicionesDePago));
            }

            documentElement.SetAttribute("SubTotal", this.subTotal.ToString("0.#0"));
            if (this.descuento > 0.0)
            {
                documentElement.SetAttribute("Descuento", this.descuento.ToString("0.#0"));
            }

            if ((this.motivoDescuento != "") && (this.motivoDescuento != null))
            {
                documentElement.SetAttribute("MotivoDescuento", this.toUTF8(this.motivoDescuento));
            }

            documentElement.SetAttribute("Total", this.total.ToString("0.#0"));
            documentElement.SetAttribute("TipoDeComprobante", this.tipoDeComprobante.ToUpper());
            documentElement.SetAttribute("MetodoPago", this.toUTF8(this.metodoDePago));
            documentElement.SetAttribute("LugarExpedicion", this.toUTF8(this.LugarExpedicion));

            if ((this.TipoCambio != "") && (this.TipoCambio != null))
            {
                documentElement.SetAttribute("TipoCambio", this.toUTF8(this.TipoCambio));
            }

            if ((this.Moneda != "") && (this.Moneda != null))
            {
                documentElement.SetAttribute("Moneda", this.toUTF8(this.Moneda));
            }

            //if ((this.NumCtaPago != "") && (this.NumCtaPago != null))
            //{
            //    documentElement.SetAttribute("NumCtaPago", this.toUTF8(this.NumCtaPago));
            //}

            //if ((this.FolioFiscalOrig != "") && (this.FolioFiscalOrig != null))
            //{
            //    documentElement.SetAttribute("FolioFiscalOrig", this.toUTF8(this.FolioFiscalOrig));
            //    if ((this.SerieFolioFiscalOrig != "") && (this.SerieFolioFiscalOrig != null))
            //    {
            //        documentElement.SetAttribute("SerieFolioFiscalOrig", this.toUTF8(this.SerieFolioFiscalOrig));
            //    }

            //    DateTime fechaFolioFiscalOrig = this.FechaFolioFiscalOrig;
            //    documentElement.SetAttribute("FechaFolioFiscalOrig", this.FechaFolioFiscalOrig.ToString("yyyy-MM-ddTHH:mm:ss"));
            //    documentElement.SetAttribute("MontoFolioFiscalOrig", this.MontoFolioFiscalOrig.ToString());
            //}


            if ((this.sello != "") && (this.sello != null))
            {
                documentElement.SetAttribute("Sello", this.toUTF8(this.sello));
            }
            documentElement.SetAttribute("NoCertificado", this.toUTF8(this.noCertificado));
            documentElement.SetAttribute("Certificado", this.toUTF8(this.certificado));


            ///////////////////////////////////////////////////////////// 
            clsLeer datos_UUIDS = new clsLeer();
            datos_UUIDS.DataSetClase = dtsUUIDs_Relacionados;

            if (datos_UUIDS.Registros > 0)
            {
                elementUUIDs_Relacionados = this.newElement("CfdiRelacionados");
                elementUUIDs_Relacionados.SetAttribute("TipoRelacion", this.toUTF8(this.TipoDeRelacion));
                while (datos_UUIDS.Leer())
                {
                    elementUUID_Relacionado = this.newElement("CfdiRelacionado");
                    elementUUID_Relacionado.SetAttribute("UUID", datos_UUIDS.Campo("UUID_Relacionado"));
                    elementUUIDs_Relacionados.AppendChild(elementUUID_Relacionado);
                }
                documentElement.AppendChild(elementUUIDs_Relacionados);
            }


            if (!this.Emisor.isOK())
            {
                this.lastError = "Error Emisor. " + this.Emisor.lastError;
                LogProceso(lastError); 
                return "";
            }

            elementEmisor = this.newElement("Emisor");
            elementEmisor.SetAttribute("Rfc", this.toUTF8(this.Emisor.rfc));
            if ((this.Emisor.nombre != "") && (this.Emisor.nombre != null))
            {
                elementEmisor.SetAttribute("Nombre", this.toUTF8(this.Emisor.nombre));
                elementEmisor.SetAttribute("RegimenFiscal", this.toUTF8(this.Emisor.RegimenFiscal_33.Regimen));

                //object[] obj = { toUTF8(this.Emisor.RegimenFiscal_33.Regimen), toUTF8(this.Emisor.RegimenFiscal_33.Descripcion) };
                //dtEmisorRegimen.Rows.Add(obj); 

            }
            documentElement.AppendChild(elementEmisor);
            
            
            if (!this.Receptor.isOK())
            {
                this.lastError = "Error Receptor. " + this.Receptor.lastError;
                LogProceso(lastError); 
                return "";
            }

            elementReceptor = this.newElement("Receptor");
            elementReceptor.SetAttribute("Rfc", this.toUTF8(this.Receptor.rfc));
            if (this.Receptor.nombre != "")
            {
                elementReceptor.SetAttribute("Nombre", this.toUTF8(this.Receptor.nombre));
                elementReceptor.SetAttribute("UsoCFDI", this.toUTF8(this.Receptor.UsoDeCFDI));                
            }
            documentElement.AppendChild(elementReceptor);
                       
            if (this.Conceptos.Count < 1)
            {
                this.lastError = "Falta elemento requerido: Conceptos";
                LogProceso(lastError); 
                return "";
            }

            int iConceptos = 0;
            elementConceptos = this.newElement("Conceptos");
            foreach (clsConcepto concepto in this.Conceptos)
            {
                iConceptos++;
                object[] obj = { 
                    iConceptos, 
                    toUTF8(concepto.ClaveProdServ), toUTF8(concepto.ClaveProdServ_Descripcion), 
                    toUTF8(concepto.noIdentificacion), toUTF8(concepto.descripcion), 
                    toUTF8(concepto.ClaveUnidad), toUTF8(concepto.ClaveUnidad_Descripcion), 
                    toUTF8(concepto.unidad), 
                    concepto.cantidad, concepto.valorUnitario, 
                    
                    concepto.ImporteDescuento,
                    
                    concepto.tasaiva, concepto.importeiva,  concepto.importe, concepto.notas, 
                    concepto.Impuesto_Base, concepto.Impuesto_ImpuestoClave, concepto.Impuesto_Impuesto, 
                    concepto.Impuesto_TipoFactor, concepto.Impuesto_TasaOCuota,
                    concepto.Impuesto_Importe, concepto.Impuesto_TasaOCuotaRetencion, concepto.Impuesto_ImporteRetencion 
                    };
                dtConceptos.Rows.Add(obj);


                if (!concepto.isOK())
                {
                    this.lastError = "Error en concepto. " + concepto.lastError;
                    return "";
                }

                element3 = this.newElement("Concepto");
                element3.SetAttribute("ClaveProdServ", this.toUTF8(concepto.ClaveProdServ));
                if ((concepto.noIdentificacion != "") && (concepto.noIdentificacion != null))
                {
                    element3.SetAttribute("NoIdentificacion", this.toUTF8(concepto.noIdentificacion));
                } 

                element3.SetAttribute("Cantidad", concepto.cantidad.ToString());
                element3.SetAttribute("ClaveUnidad", this.toUTF8(concepto.ClaveUnidad));
                element3.SetAttribute("Unidad", this.toUTF8(concepto.unidad));
                element3.SetAttribute("Descripcion", this.toUTF8(concepto.descripcion));
                element3.SetAttribute("ValorUnitario", concepto.valorUnitario.ToString("0.#0"));
                element3.SetAttribute("Importe", concepto.importe.ToString("0.#0"));

                if (this.descuento > 0.0)
                {
                    element3.SetAttribute("Descuento", concepto.ImporteDescuento.ToString("0.#0"));
                }


                elementImpuestos = this.newElement("Impuestos");
                elementTraslados = this.newElement("Traslados");
                elementTraslado_Item = this.newElement("Traslado");

                elementTraslado_Item.SetAttribute("Base", concepto.Impuesto_Base.ToString("0.#0"));
                elementTraslado_Item.SetAttribute("Impuesto", toUTF8(concepto.Impuesto_ImpuestoClave));
                elementTraslado_Item.SetAttribute("TipoFactor", toUTF8(concepto.Impuesto_TipoFactor));
                elementTraslado_Item.SetAttribute("TasaOCuota", concepto.Impuesto_TasaOCuota.ToString("0.#####0"));
                elementTraslado_Item.SetAttribute("Importe", concepto.Impuesto_Importe.ToString("0.#0"));

                elementTraslados.AppendChild(elementTraslado_Item); 
                elementImpuestos.AppendChild(elementTraslados);
                element3.AppendChild(elementImpuestos);

                //Base="133331.03" Impuesto="002" TipoFactor="Tasa" TasaOCuota="0.160000" Importe="21332.96" 

                if (concepto.InformacionAduanera.Count > 0)
                {
                    int iAduana = 0; 
                    foreach (clsInformacionAduanera aduanera in concepto.InformacionAduanera)
                    {
                        iAduana++;
                        object[] obj_Aduana = 
                            { 
                                iConceptos, iAduana, 
                                toUTF8(aduanera.numero), toUTF8(aduanera.fecha.ToString("yyyy-MM-dd")), toUTF8(aduanera.aduana)  
                            };
                        dtConceptos_InformacionAduanera.Rows.Add(obj_Aduana);
                        

                        if (!aduanera.isOK())
                        {
                            this.lastError = "Error en informacionAduanera de Concepto. " + aduanera.lastError;
                            return "";
                        }

                        element4 = this.newElement("InformacionAduanera");
                        element4.SetAttribute("numero", this.toUTF8(aduanera.numero));
                        element4.SetAttribute("fecha", this.toUTF8(aduanera.fecha.ToString("yyyy-MM-dd")));
                        if ((aduanera.aduana != "") && (aduanera.aduana != null))
                        {
                            element4.SetAttribute("aduana", this.toUTF8(aduanera.aduana));
                        }

                        element3.AppendChild(element4);
                        element4 = null;
                    }
                }

                if (concepto.CuentaPredial.isOK())
                {
                    element4 = this.newElement("CuentaPredial");
                    element4.SetAttribute("numero", this.toUTF8(concepto.CuentaPredial.numero));
                    element3.AppendChild(element4);
                    element4 = null;
                }

                if (concepto.ComplementoConcepto.isOK())
                {
                    try
                    {
                        element3.AppendChild(concepto.ComplementoConcepto.complemento);
                    }
                    catch (Exception exception)
                    {
                        this.lastError = "Error en complementoConcepto. " + exception.Message;
                        return "";
                    }
                }

                if (concepto.Parte.Count > 0)
                {
                    foreach (clsParte parte in concepto.Parte)
                    {
                        if (!parte.isOK())
                        {
                            this.lastError = "Error en Parte de Concepto. " + parte.lastError;
                            return "";
                        }

                        element4 = this.newElement("Parte");
                        element4.SetAttribute("cantidad", parte.cantidad.ToString());
                        if ((parte.unidad != "") && (parte.unidad != null))
                        {
                            element4.SetAttribute("unidad", this.toUTF8(parte.unidad));
                        }

                        if ((parte.noIdentificacion != "") && (parte.noIdentificacion != null))
                        {
                            element4.SetAttribute("noIdentificacion", this.toUTF8(parte.noIdentificacion));
                        }

                        element4.SetAttribute("descripcion", this.toUTF8(parte.descripcion));
                        if (parte.valorUnitario >= 0.0)
                        {
                            element4.SetAttribute("valorUnitario", parte.valorUnitario.ToString());
                        }

                        if (parte.importe >= 0.0)
                        {
                            element4.SetAttribute("importe", parte.importe.ToString());
                        }

                        if (parte.InformacionAduanera.Count > 0)
                        {
                            foreach (clsInformacionAduanera aduanera in parte.InformacionAduanera)
                            {
                                if (!aduanera.isOK())
                                {
                                    this.lastError = "Error en informacionAduanera de Parte. " + aduanera.lastError;
                                    return "";
                                }

                                element5 = this.newElement("InformacionAduanera");
                                element5.SetAttribute("numero", this.toUTF8(aduanera.numero));
                                element5.SetAttribute("fecha", this.toUTF8(aduanera.fecha.ToString("yyyy-MM-dd")));
                                if ((aduanera.aduana != "") && (aduanera.aduana != null))
                                {
                                    element5.SetAttribute("aduana", this.toUTF8(aduanera.aduana));
                                }

                                element4.AppendChild(element5);
                                element5 = null;
                            }
                        }
                        element3.AppendChild(element4);
                    }
                }
                elementConceptos.AppendChild(element3);
            }
            documentElement.AppendChild(elementConceptos);

            if (!this.Impuestos.isOK())
            {
                this.lastError = "Error Impuestos. " + this.Impuestos.lastError;
                LogProceso(lastError); 
                return "";
            }

            elementImpuestos = this.newElement("Impuestos");
            if (this.Impuestos.totalImpuestosRetenidos > 0.0)
            {
                elementImpuestos.SetAttribute("TotalImpuestosRetenidos", this.toUTF8(this.Impuestos.totalImpuestosRetenidos.ToString("0.#0")));
            }

            if (this.Impuestos.totalImpuestosTrasladados >= 0.0)
            {
                elementImpuestos.SetAttribute("TotalImpuestosTrasladados", this.toUTF8(this.Impuestos.totalImpuestosTrasladados.ToString("0.#0")));
            }

            if (this.Impuestos.Retenciones.Count > 0)
            {
                element4 = this.newElement("Retenciones");
                foreach (clsRetencion retencion in this.Impuestos.Retenciones)
                {
                    object[] obj = { toUTF8(retencion.impuesto), retencion.importe.ToString() };
                    dtRetenciones.Rows.Add(obj); 

                    if (!retencion.isOK())
                    {
                        this.lastError = "Error en Retencion de Impuestos." + retencion.lastError;
                        LogProceso(lastError); 
                        return "";
                    }
                    element5 = this.newElement("Retencion");
                    element5.SetAttribute("impuesto", this.toUTF8(retencion.impuesto));
                    element5.SetAttribute("importe", retencion.importe.ToString("0.#0"));
                    element4.AppendChild(element5);
                    element5 = null;
                }
                elementImpuestos.AppendChild(element4);
                element4 = null;
            }

            if (this.Impuestos.Traslados.Count > 0)
            {
                element4 = this.newElement("Traslados");
                foreach (clsTraslado traslado in this.Impuestos.Traslados)
                {
                    object[] obj = { 
                        toUTF8(traslado.impuesto), traslado.Impuesto_TipoFactor, traslado.tasa, traslado.importe, 
                        traslado.Impuesto_Impuesto, traslado.Impuesto_TasaOCuota 
                                   };
                    dtTraslados.Rows.Add(obj); 

                    if (!traslado.isOK())
                    {
                        this.lastError = "Error en Traslado de Impuestos." + traslado.lastError;
                        LogProceso(lastError); 
                        return "";
                    }
                    element5 = this.newElement("Traslado");
                    element5.SetAttribute("Impuesto", this.toUTF8(traslado.Impuesto_ImpuestoClave));
                    element5.SetAttribute("TipoFactor", this.toUTF8(traslado.Impuesto_TipoFactor));
                    element5.SetAttribute("TasaOCuota", traslado.Impuesto_TasaOCuota.ToString("0.#####0"));
                    element5.SetAttribute("Importe", traslado.Impuesto_Importe.ToString("0.#0"));
                    element4.AppendChild(element5);
                    element5 = null;
                }
                elementImpuestos.AppendChild(element4);
                element4 = null;
            }
            documentElement.AppendChild(elementImpuestos);


            if (this.Complemento != null)
            {
                try
                {
                    documentElement.AppendChild(this.Complemento);
                }
                catch (Exception exception3)
                {
                    exception2 = exception3;
                    this.lastError = "Error al agregar datos de complemento. " + exception2.Message;
                    LogProceso(lastError); 
                    return "";
                }
            }

            if (this.Addenda != null)
            {
                try
                {
                    documentElement.AppendChild(this.Addenda);
                }
                catch (Exception exception4)
                {
                    exception2 = exception4;
                    this.lastError = "Error al agregar datos de addenda. " + exception2.Message;
                    LogProceso(lastError); 
                    return "";
                }
            }

            if ((filename != "") && (filename != null))
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
                toUTF8(EfectoDeComprobante), toUTF8(EfectoDeComprobante_Descripcion)
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

            object[] obj = { toUTF8(Receptor.rfc), toUTF8(Receptor.nombre), toUTF8(Receptor.UsoDeCFDI), toUTF8(Receptor.UsoDeCFDI_Descripcion) };
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
            dtConceptos.Columns.Add("ClaveProdServ_Descripcion", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("NoIdentificacion", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("Descripcion", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("ClaveUnidad", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("ClaveUnidad_Descripcion", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("Unidad", System.Type.GetType("System.String"));
            dtConceptos.Columns.Add("Cantidad", System.Type.GetType("System.Double"));
            dtConceptos.Columns.Add("ValorUnitario", System.Type.GetType("System.Double"));
            dtConceptos.Columns.Add("Descuento", System.Type.GetType("System.Double"));

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


            ////////////////////////////////////////////////////////////////
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


            dtConceptos_InformacionAduanera = new DataTable("ConceptosInformacionAduanera");
            dtConceptos_InformacionAduanera.Columns.Add("Orden", System.Type.GetType("System.Int32"));
            dtConceptos_InformacionAduanera.Columns.Add("Id", System.Type.GetType("System.Int32"));
            dtConceptos_InformacionAduanera.Columns.Add("Numero", System.Type.GetType("System.String"));
            dtConceptos_InformacionAduanera.Columns.Add("Fecha", System.Type.GetType("System.String"));
            dtConceptos_InformacionAduanera.Columns.Add("Aduana", System.Type.GetType("System.String")); 


            clsLeer uuids = new clsLeer();
            uuids.DataSetClase = dtsUUIDs_Relacionados;

            while (uuids.Leer())
            {
                object[] obj = { 
                    uuids.Campo("Serie"), uuids.CampoInt("Folio"), uuids.Campo("Serie_Relacionada"), uuids.Campo("Folio_Relacionado"), 
                    uuids.Campo("UUID_Relacionado"), 
                    uuids.CampoDouble("Total_Base"), uuids.CampoDouble("SubTotal"), uuids.CampoDouble("IVA"), uuids.CampoDouble("Total")  
                               };
                dtCFDI_Relacionados.Rows.Add(obj);
            }
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

        private bool isOK(bool omitSeal)
        {
            try
            {
                LogProceso("Validando versión");
                if (this.version != 3.3)
                {
                    this.lastError = "Valor incorrecto en version: debe ser 3.3";
                    return false;
                }
                
                LogProceso("Validando serie");
                if (this.serie == null)
                {
                    this.serie = "";
                }

                LogProceso("Validando longitud de serie");
                if (this.serie.Length > 0x19)
                {
                    this.lastError = "Valor incorrecto en atributo: serie";
                    return false;
                }

                LogProceso("Validando folio");
                if (this.folio <= 0L)
                {
                    this.lastError = "Valor incorrecto en atributo: folio, el valor no puede ser menor o igual que 0";
                    return false;
                }

                LogProceso("Validando longitud de folio");
                if (this.folio.ToString().Length > 20)
                {
                    this.lastError = "Valor incorrecto en atributo: folio";
                    return false;
                }

                LogProceso("Validando sello");
                if (!omitSeal && ((this.sello == "") || (this.sello == null)))
                {
                    this.lastError = "Falta atributo requerido: sello";
                    return false;
                }

                LogProceso("Validando forma de pago");
                if ((this.formaDePago == "") || (this.formaDePago == null))
                {
                    this.lastError = "Falta atributo requerido: formaDePago";
                    return false;
                }

                LogProceso("Validando número de certificado");
                if (this.noCertificado == null)
                {
                    this.lastError = "Valor incorrecto en atributo: noCertificado";
                    return false;
                }

                LogProceso("Validando longitud de número de certificado");
                if ((this.noCertificado == "") || (this.noCertificado.Length != 20))
                {
                    this.lastError = "Valor incorrecto en atributo: noCertificado";
                    return false;
                }

                LogProceso("Validando certificado");
                if ((this.certificado == null) || (this.certificado == ""))
                {
                    this.lastError = "Valor incorrecto en atributo: certificado";
                    return false;
                }

                LogProceso("Validando subtotal");
                if (this.subTotal < 0.0)
                {
                    this.lastError = "Valor incorrecto en atributo: subTotal";
                    return false;
                }

                LogProceso("Validando total");
                if (this.total < 0.0)
                {
                    this.lastError = "Valor incorrecto en atributo: total";
                    return false;
                }

                LogProceso("Validando tipo de comprobante");
                if ((this.tipoDeComprobante == "") || (this.tipoDeComprobante == null))
                {
                    this.lastError = "Falta atributo requerido: tipoDeComprobante";
                    return false;
                }

                LogProceso("Validando método de pago");
                if ((this.metodoDePago == "") || (this.metodoDePago == null))
                {
                    this.lastError = "Falta atributo requerido: metodoDePago";
                    return false;
                }

                LogProceso("Validando lugar de expedición");
                if ((this.LugarExpedicion == "") || (this.LugarExpedicion == null))
                {
                    this.lastError = "Falta atributo requerido: LugarExpedicion";
                    return false;
                }

                LogProceso("Validando folio fiscal");
                if (((this.FolioFiscalOrig != "") && (this.FolioFiscalOrig != null)) && (this.MontoFolioFiscalOrig < 0.0))
                {
                    this.lastError = "Valor incorreto en atributo: MontoFolioFiscalOrig";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogProceso("isOK", ex.Message); 
                return false; 
            }
        }

        public bool loadByXMLFile(string filename)
        {
            try
            {
                string xml = File.ReadAllText(filename);
                return this.loadByXMLString(xml);
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public bool loadByXMLString(string xml)
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
                if (mainNode != null)
                {
                    if (!this.Emisor.loadFromXML(mainNode))
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
                if (node3 != null)
                {
                    if (!this.Receptor.loadFromXML(node3))
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
                if (node4 != null)
                {
                    foreach (XmlNode node5 in node4.ChildNodes)
                    {
                        clsConcepto concepto = new clsConcepto();
                        if (!concepto.loadFromXML(node5))
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
                if (node6 != null)
                {
                    if (!this.Impuestos.loadFromXML(node6))
                    {
                        flag = false;
                    }
                    node6 = null;
                }
                else
                {
                    flag = false;
                }
                this.Complemento = (XmlElement) mxml.getNode("Complemento", documentElement.ChildNodes);
                this.Addenda = (XmlElement) mxml.getNode("Addenda", documentElement.ChildNodes);
                document = null;
                mxml = null;
                documentElement = null;
                return flag;
            }
            catch (Exception exception)
            {
                this.lastError = exception.Message;
                return false;
            }
        }

        public void new_schemaLocation(string slocation)
        {
            this.schemaLocation.Add(slocation);
        }

        public void new_xmlns(string name, string value)
        {
            this.xmlns.Add(name + ";" + value);
        }

        public XmlElement newElement(string sName)
        {
            return this.cfd.CreateElement(this.defPrefix, sName, this.defURI);
        }

        public void SetAttibute(XmlElement oXmlElement, string name, string value)
        {
            oXmlElement.SetAttribute(name, value);
        }

        private double t_importe(double v)
        {
            v = Math.Round(v, 6);
            return v;
        }

        public string toUTF8(string stext)
        {
            UTF8Encoding encoding = new UTF8Encoding();

            if (stext == null)
            {
                stext = ""; 
            }

            byte[] bytes = encoding.GetBytes(stext);
            return encoding.GetString(bytes);
        }

        public DataSet UUIDs_Relacionados
        {
            get { return dtsUUIDs_Relacionados; }
            set { dtsUUIDs_Relacionados = value; }
        }

        public string toImage(byte[] bytes)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetString(bytes);
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
                if (value == null)
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
                if (value == null)
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
                if (value == null)
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
                if (value == null)
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
                if (value == null)
                {
                    value = "";
                }
                this.mmetodoDePagoDescripcion  = value.Trim();
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
                if (value == null)
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
                if (value == null)
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
                if (value == null)
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

        public string tipoDeComprobante
        {
            get
            {
                return this.mtipoDeComprobante;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }

                if (((value.Trim().ToLower() != "i") && (value.Trim().ToLower() != "e")) && (value.Trim().ToLower() != "t") && (value.Trim().ToLower() != "p"))
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
                if (value < 0.0)
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
            set { sReferencia = value;  }
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

