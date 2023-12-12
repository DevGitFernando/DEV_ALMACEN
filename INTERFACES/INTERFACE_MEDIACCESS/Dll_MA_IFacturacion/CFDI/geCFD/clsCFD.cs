using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;

namespace Dll_MA_IFacturacion.CFDI.geCFD
{
    public class cCFD
    {
        private XmlDocument cfd = new XmlDocument();
        public ArrayList Conceptos = new ArrayList();
        public clsEmisor Emisor = new clsEmisor();
        public clsImpuestos Impuestos = new clsImpuestos();
        private XmlElement mAddenda;
        private int manoAprobacion;
        private string mcertificado;
        private XmlElement mComplemento;
        private string mcondicionesDePago;
        private double mdescuento;
        private DateTime mfecha;
        private DateTime mFechaFolioFiscalOrig;
        private long mfolio;
        private string mFolioFiscalOrig;
        private string mformaDePago;
        private string mlastError;
        private string mLugarExpedicion;
        private string mmetodoDePago;
        private string mMoneda;
        private double mMontoFolioFiscalOrig;
        private string mmotivoDescuento;
        private long mnoAprobacion;
        private string mnoCertificado;
        private string mNumCtaPago;
        private string msello;
        private string mserie;
        private string mSerieFolioFiscalOrig;
        private double msubTotal;
        private string mTipoCambio;
        private string mtipoDeComprobante;
        private double mtotal;
        private double mversion = 2.2;
        public clsReceptor Receptor = new clsReceptor();
        private ArrayList schemaLocation = new ArrayList();
        private ArrayList xmlns = new ArrayList();

        public cCFD()
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

        public string getXML(string filename, bool omitSeal)
        {
            XmlElement element3;
            XmlElement element4;
            XmlElement element5;
            Exception exception2;
            bool flag = true;
            if (!this.isOK(omitSeal))
            {
                return "";
            }
            this.InitCFD();
            XmlElement documentElement = this.cfd.DocumentElement;
            documentElement.SetAttribute("xmlns", "http://www.sat.gob.mx/cfd/2");
            documentElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            foreach (string str2 in this.xmlns)
            {
                documentElement.SetAttribute(str2.Split(new char[] { ';' })[0], str2.Split(new char[] { ';' })[1]);
            }
            string str = "";
            foreach (string str3 in this.schemaLocation)
            {
                str = str + " " + str3;
            }
            documentElement.SetAttribute("schemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv22.xsd" + str);
            documentElement.SetAttribute("version", this.version.ToString("#.0"));
            if ((this.serie != "") && (this.serie != null))
            {
                documentElement.SetAttribute("serie", this.toUTF8(this.serie));
            }
            documentElement.SetAttribute("folio", this.toUTF8(this.folio.ToString()));
            documentElement.SetAttribute("fecha", this.fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
            if ((this.sello != "") && (this.sello != null))
            {
                documentElement.SetAttribute("sello", this.toUTF8(this.sello));
            }
            documentElement.SetAttribute("noAprobacion", this.noAprobacion.ToString());
            documentElement.SetAttribute("anoAprobacion", this.anoAprobacion.ToString());
            documentElement.SetAttribute("formaDePago", this.toUTF8(this.formaDePago));
            documentElement.SetAttribute("noCertificado", this.toUTF8(this.noCertificado));
            if ((this.certificado != "") && (this.certificado != null))
            {
                documentElement.SetAttribute("certificado", this.toUTF8(this.certificado));
            }
            if ((this.condicionesDePago != "") && (this.condicionesDePago != null))
            {
                documentElement.SetAttribute("condicionesDePago", this.toUTF8(this.condicionesDePago));
            }
            documentElement.SetAttribute("subTotal", this.subTotal.ToString());
            if (this.descuento > 0.0)
            {
                documentElement.SetAttribute("descuento", this.descuento.ToString());
            }
            if ((this.motivoDescuento != "") && (this.motivoDescuento != null))
            {
                documentElement.SetAttribute("motivoDescuento", this.toUTF8(this.motivoDescuento));
            }
            if ((this.TipoCambio != "") && (this.TipoCambio != null))
            {
                documentElement.SetAttribute("TipoCambio", this.toUTF8(this.TipoCambio));
            }
            if ((this.Moneda != "") && (this.Moneda != null))
            {
                documentElement.SetAttribute("Moneda", this.toUTF8(this.Moneda));
            }
            documentElement.SetAttribute("total", this.total.ToString());
            documentElement.SetAttribute("tipoDeComprobante", this.tipoDeComprobante);
            documentElement.SetAttribute("metodoDePago", this.toUTF8(this.metodoDePago));
            documentElement.SetAttribute("LugarExpedicion", this.toUTF8(this.LugarExpedicion));
            if ((this.NumCtaPago != "") && (this.NumCtaPago != null))
            {
                documentElement.SetAttribute("NumCtaPago", this.toUTF8(this.NumCtaPago));
            }
            if ((this.FolioFiscalOrig != "") && (this.FolioFiscalOrig != null))
            {
                documentElement.SetAttribute("FolioFiscalOrig", this.toUTF8(this.FolioFiscalOrig));
                if ((this.SerieFolioFiscalOrig != "") && (this.SerieFolioFiscalOrig != null))
                {
                    documentElement.SetAttribute("SerieFolioFiscalOrig", this.toUTF8(this.SerieFolioFiscalOrig));
                }
                DateTime fechaFolioFiscalOrig = this.FechaFolioFiscalOrig;
                documentElement.SetAttribute("FechaFolioFiscalOrig", this.FechaFolioFiscalOrig.ToString("yyyy-MM-ddTHH:mm:ss"));
                documentElement.SetAttribute("MontoFolioFiscalOrig", this.MontoFolioFiscalOrig.ToString());
            }
            if (!this.Emisor.isOK())
            {
                this.lastError = "Error Emisor. " + this.Emisor.lastError;
                return "";
            }
            XmlElement newChild = this.cfd.CreateElement("Emisor");
            newChild.SetAttribute("rfc", this.toUTF8(this.Emisor.rfc));
            if ((this.Emisor.nombre != "") && (this.Emisor.nombre != null))
            {
                newChild.SetAttribute("nombre", this.toUTF8(this.Emisor.nombre));
            }
            if (this.Emisor.DomicilioFiscal.isOK())
            {
                element3 = this.cfd.CreateElement("DomicilioFiscal");
                element3.SetAttribute("calle", this.toUTF8(this.Emisor.DomicilioFiscal.calle));
                if ((this.Emisor.DomicilioFiscal.noExterior != "") && (this.Emisor.DomicilioFiscal.noExterior != null))
                {
                    element3.SetAttribute("noExterior", this.toUTF8(this.Emisor.DomicilioFiscal.noExterior));
                }
                if ((this.Emisor.DomicilioFiscal.noInterior != "") && (this.Emisor.DomicilioFiscal.noInterior != null))
                {
                    element3.SetAttribute("noInterior", this.toUTF8(this.Emisor.DomicilioFiscal.noInterior));
                }
                if ((this.Emisor.DomicilioFiscal.colonia != "") && (this.Emisor.DomicilioFiscal.colonia != null))
                {
                    element3.SetAttribute("colonia", this.toUTF8(this.Emisor.DomicilioFiscal.colonia));
                }
                if ((this.Emisor.DomicilioFiscal.localidad != "") && (this.Emisor.DomicilioFiscal.localidad != null))
                {
                    element3.SetAttribute("localidad", this.toUTF8(this.Emisor.DomicilioFiscal.localidad));
                }
                if ((this.Emisor.DomicilioFiscal.referencia != "") && (this.Emisor.DomicilioFiscal.referencia != null))
                {
                    element3.SetAttribute("referencia", this.toUTF8(this.Emisor.DomicilioFiscal.referencia));
                }
                element3.SetAttribute("municipio", this.toUTF8(this.Emisor.DomicilioFiscal.municipio));
                element3.SetAttribute("estado", this.toUTF8(this.Emisor.DomicilioFiscal.estado));
                element3.SetAttribute("pais", this.toUTF8(this.Emisor.DomicilioFiscal.pais));
                element3.SetAttribute("codigoPostal", this.toUTF8(this.Emisor.DomicilioFiscal.codigoPostal));
                newChild.AppendChild(element3);
                element3 = null;
            }
            if (this.Emisor.ExpedidoEn.isOK())
            {
                element3 = this.cfd.CreateElement("ExpedidoEn");
                if ((this.Emisor.ExpedidoEn.calle != "") && (this.Emisor.ExpedidoEn.calle != null))
                {
                    element3.SetAttribute("calle", this.toUTF8(this.Emisor.ExpedidoEn.calle));
                }
                if ((this.Emisor.ExpedidoEn.noExterior != "") && (this.Emisor.ExpedidoEn.noExterior != null))
                {
                    element3.SetAttribute("noExterior", this.toUTF8(this.Emisor.ExpedidoEn.noExterior));
                }
                if ((this.Emisor.ExpedidoEn.noInterior != "") && (this.Emisor.ExpedidoEn.noInterior != null))
                {
                    element3.SetAttribute("noInterior", this.toUTF8(this.Emisor.ExpedidoEn.noInterior));
                }
                if ((this.Emisor.ExpedidoEn.colonia != "") && (this.Emisor.ExpedidoEn.colonia != null))
                {
                    element3.SetAttribute("colonia", this.toUTF8(this.Emisor.ExpedidoEn.colonia));
                }
                if ((this.Emisor.ExpedidoEn.localidad != "") && (this.Emisor.ExpedidoEn.localidad != null))
                {
                    element3.SetAttribute("localidad", this.toUTF8(this.Emisor.ExpedidoEn.localidad));
                }
                if ((this.Emisor.ExpedidoEn.referencia != "") && (this.Emisor.ExpedidoEn.referencia != null))
                {
                    element3.SetAttribute("referencia", this.toUTF8(this.Emisor.ExpedidoEn.referencia));
                }
                if ((this.Emisor.ExpedidoEn.municipio != "") && (this.Emisor.ExpedidoEn.municipio != null))
                {
                    element3.SetAttribute("municipio", this.toUTF8(this.Emisor.ExpedidoEn.municipio));
                }
                if ((this.Emisor.ExpedidoEn.estado != "") && (this.Emisor.ExpedidoEn.estado != null))
                {
                    element3.SetAttribute("estado", this.toUTF8(this.Emisor.ExpedidoEn.estado));
                }
                element3.SetAttribute("pais", this.toUTF8(this.Emisor.ExpedidoEn.pais));
                if ((this.Emisor.ExpedidoEn.codigoPostal != "") && (this.Emisor.ExpedidoEn.codigoPostal != null))
                {
                    element3.SetAttribute("codigoPostal", this.toUTF8(this.Emisor.ExpedidoEn.codigoPostal));
                }
                newChild.AppendChild(element3);
                element3 = null;
            }

            foreach (clsRegimenFiscal fiscal in this.Emisor.RegimenFiscal)
            {
                element3 = this.cfd.CreateElement("RegimenFiscal");
                element3.SetAttribute("Regimen", this.toUTF8(fiscal.Regimen));
                newChild.AppendChild(element3);
                element3 = null;
            }
            documentElement.AppendChild(newChild);
            newChild = null;
            if (!this.Receptor.isOK())
            {
                this.lastError = "Error Receptor. " + this.Receptor.lastError;
                return "";
            }
            newChild = this.cfd.CreateElement("Receptor");
            newChild.SetAttribute("rfc", this.toUTF8(this.Receptor.rfc));
            if (this.Receptor.nombre != "")
            {
                newChild.SetAttribute("nombre", this.toUTF8(this.Receptor.nombre));
            }
            if (DateTime.Now.Year < 0x7db)
            {
                flag = true;
            }
            else
            {
                flag = this.Receptor.Domicilio.isOK();
            }
            if (flag)
            {
                element3 = this.cfd.CreateElement("Domicilio");
                if ((this.Receptor.Domicilio.calle != "") && (this.Receptor.Domicilio.calle != null))
                {
                    element3.SetAttribute("calle", this.toUTF8(this.Receptor.Domicilio.calle));
                }
                if ((this.Receptor.Domicilio.noExterior != "") && (this.Receptor.Domicilio.noExterior != null))
                {
                    element3.SetAttribute("noExterior", this.toUTF8(this.Receptor.Domicilio.noExterior));
                }
                if ((this.Receptor.Domicilio.noInterior != "") & (this.Receptor.Domicilio.noInterior != null))
                {
                    element3.SetAttribute("noInterior", this.toUTF8(this.Receptor.Domicilio.noInterior));
                }
                if ((this.Receptor.Domicilio.colonia != "") && (this.Receptor.Domicilio.colonia != null))
                {
                    element3.SetAttribute("colonia", this.toUTF8(this.Receptor.Domicilio.colonia));
                }
                if ((this.Receptor.Domicilio.localidad != "") && (this.Receptor.Domicilio.localidad != null))
                {
                    element3.SetAttribute("localidad", this.toUTF8(this.Receptor.Domicilio.localidad));
                }
                if ((this.Receptor.Domicilio.referencia != "") && (this.Receptor.Domicilio.referencia != null))
                {
                    element3.SetAttribute("referencia", this.toUTF8(this.Receptor.Domicilio.referencia));
                }
                if ((this.Receptor.Domicilio.municipio != "") && (this.Receptor.Domicilio.municipio != null))
                {
                    element3.SetAttribute("municipio", this.toUTF8(this.Receptor.Domicilio.municipio));
                }
                if ((this.Receptor.Domicilio.estado != "") && (this.Receptor.Domicilio.estado != null))
                {
                    element3.SetAttribute("estado", this.toUTF8(this.Receptor.Domicilio.estado));
                }
                element3.SetAttribute("pais", this.toUTF8(this.Receptor.Domicilio.pais));
                if ((this.Receptor.Domicilio.codigoPostal != "") && (this.Receptor.Domicilio.codigoPostal != null))
                {
                    element3.SetAttribute("codigoPostal", this.toUTF8(this.Receptor.Domicilio.codigoPostal));
                }
                newChild.AppendChild(element3);
                element3 = null;
            }
            documentElement.AppendChild(newChild);
            newChild = null;
            if (this.Conceptos.Count < 1)
            {
                this.lastError = "Falta elemento requerido: Conceptos";
                return "";
            }
            newChild = this.cfd.CreateElement("Conceptos");
            foreach (clsConcepto concepto in this.Conceptos)
            {
                if (!concepto.isOK())
                {
                    this.lastError = "Error en concepto. " + concepto.lastError;
                    return "";
                }
                element3 = this.cfd.CreateElement("Concepto");
                element3.SetAttribute("cantidad", concepto.cantidad.ToString());
                element3.SetAttribute("unidad", this.toUTF8(concepto.unidad));
                if ((concepto.noIdentificacion != "") && (concepto.noIdentificacion != null))
                {
                    element3.SetAttribute("noIdentificacion", this.toUTF8(concepto.noIdentificacion));
                }
                element3.SetAttribute("descripcion", this.toUTF8(concepto.descripcion));
                element3.SetAttribute("valorUnitario", concepto.valorUnitario.ToString());
                element3.SetAttribute("importe", concepto.importe.ToString());
                if (concepto.InformacionAduanera.Count > 0)
                {
                    foreach (clsInformacionAduanera aduanera in concepto.InformacionAduanera)
                    {
                        if (!aduanera.isOK())
                        {
                            this.lastError = "Error en informacionAduanera de Concepto. " + aduanera.lastError;
                            return "";
                        }
                        element4 = this.cfd.CreateElement("InformacionAduanera");
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
                    element4 = this.cfd.CreateElement("CuentaPredial");
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
                        element4 = this.cfd.CreateElement("Parte");
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
                                element5 = this.cfd.CreateElement("InformacionAduanera");
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
                newChild.AppendChild(element3);
            }
            documentElement.AppendChild(newChild);
            newChild = null;
            if (!this.Impuestos.isOK())
            {
                this.lastError = "Error Impuestos. " + this.Impuestos.lastError;
                return "";
            }
            newChild = this.cfd.CreateElement("Impuestos");
            if (this.Impuestos.totalImpuestosRetenidos > 0.0)
            {
                newChild.SetAttribute("totalImpuestosRetenidos", this.toUTF8(this.Impuestos.totalImpuestosRetenidos.ToString()));
            }
            if (this.Impuestos.totalImpuestosTrasladados > 0.0)
            {
                newChild.SetAttribute("totalImpuestosTrasladados", this.toUTF8(this.Impuestos.totalImpuestosTrasladados.ToString()));
            }
            if (this.Impuestos.Retenciones.Count > 0)
            {
                element4 = this.cfd.CreateElement("Retenciones");
                foreach (clsRetencion retencion in this.Impuestos.Retenciones)
                {
                    if (!retencion.isOK())
                    {
                        this.lastError = "Error en Retencion de Impuestos." + retencion.lastError;
                        return "";
                    }
                    element5 = this.cfd.CreateElement("Retencion");
                    element5.SetAttribute("impuesto", this.toUTF8(retencion.impuesto));
                    element5.SetAttribute("importe", retencion.importe.ToString());
                    element4.AppendChild(element5);
                    element5 = null;
                }
                newChild.AppendChild(element4);
                element4 = null;
            }
            if (this.Impuestos.Traslados.Count > 0)
            {
                element4 = this.cfd.CreateElement("Traslados");
                foreach (clsTraslado traslado in this.Impuestos.Traslados)
                {
                    if (!traslado.isOK())
                    {
                        this.lastError = "Error en Traslado de Impuestos." + traslado.lastError;
                        return "";
                    }
                    element5 = this.cfd.CreateElement("Traslado");
                    element5.SetAttribute("impuesto", this.toUTF8(traslado.impuesto));
                    element5.SetAttribute("tasa", traslado.tasa.ToString());
                    element5.SetAttribute("importe", traslado.importe.ToString());
                    element4.AppendChild(element5);
                    element5 = null;
                }
                newChild.AppendChild(element4);
                element4 = null;
            }
            documentElement.AppendChild(newChild);
            newChild = null;
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
            this.cfd.AppendChild(this.cfd.CreateElement("", "Comprobante", ""));
        }

        private bool isOK()
        {
            return this.isOK(false);
        }

        private bool isOK(bool omitSeal)
        {
            if (this.version != 2.2)
            {
                this.lastError = "Valor incorrecto en version: debe ser 2.2";
                return false;
            }
            if (this.serie == null)
            {
                this.serie = "";
            }
            if (this.serie.Length > 10)
            {
                this.lastError = "Valor incorrecto en atributo: serie";
                return false;
            }
            if (this.folio <= 0L)
            {
                this.lastError = "Valor incorrecto en atributo: folio, el valor no puede ser menor o igual que 0";
                return false;
            }
            if (this.folio.ToString().Length > 20)
            {
                this.lastError = "Valor incorrecto en atributo: folio";
                return false;
            }
            if (!omitSeal && ((this.sello == "") || (this.sello == null)))
            {
                this.lastError = "Falta atributo requerido: sello";
                return false;
            }
            if (this.noAprobacion < 1L)
            {
                this.lastError = "Valor incorrecto en atributo: noAprobacion";
                return false;
            }
            if (this.anoAprobacion.ToString().Length != 4)
            {
                this.lastError = "Valor incorrecto en atributo: anoAprobacion";
                return false;
            }
            if ((this.formaDePago == "") || (this.formaDePago == null))
            {
                this.lastError = "Falta atributo requerido: formaDePago";
                return false;
            }
            if (this.noCertificado == null)
            {
                this.lastError = "Valor incorrecto en atributo: noCertificado";
                return false;
            }
            if ((this.noCertificado == "") || (this.noCertificado.Length != 20))
            {
                this.lastError = "Valor incorrecto en atributo: noCertificado";
                return false;
            }
            if (this.subTotal < 0.0)
            {
                this.lastError = "Valor incorrecto en atributo: subTotal";
                return false;
            }
            if (this.total < 0.0)
            {
                this.lastError = "Valor incorrecto en atributo: total";
                return false;
            }
            if ((this.tipoDeComprobante == "") || (this.tipoDeComprobante == null))
            {
                this.lastError = "Falta atributo requerido: tipoDeComprobante";
                return false;
            }
            if ((this.metodoDePago == "") || (this.metodoDePago == null))
            {
                this.lastError = "Falta atributo requerido: metodoDePago";
                return false;
            }
            if ((this.LugarExpedicion == "") || (this.LugarExpedicion == null))
            {
                this.lastError = "Falta atributo requerido: LugarExpedicion";
                return false;
            }
            if (((this.FolioFiscalOrig != "") && (this.FolioFiscalOrig != null)) && (this.MontoFolioFiscalOrig < 0.0))
            {
                this.lastError = "Valor incorreto en atributo: MontoFolioFiscalOrig";
                return false;
            }
            return true;
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
                this.noAprobacion = mxml.longAttValue(documentElement, "noAprobacion");
                this.anoAprobacion = mxml.intAttValue(documentElement, "anoAprobacion");
                this.formaDePago = mxml.stringAttValue(documentElement, "formaDePago");
                this.noCertificado = mxml.stringAttValue(documentElement, "noCertificado");
                this.certificado = mxml.stringAttValue(documentElement, "certificado");
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
            return this.cfd.CreateElement(sName);
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
            byte[] bytes = encoding.GetBytes(stext);
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

        public int anoAprobacion
        {
            get
            {
                return this.manoAprobacion;
            }
            set
            {
                this.manoAprobacion = value;
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

        public long noAprobacion
        {
            get
            {
                return this.mnoAprobacion;
            }
            set
            {
                this.mnoAprobacion = value;
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
                if (((value.Trim().ToLower() != "ingreso") && (value.Trim().ToLower() != "egreso")) && (value.Trim().ToLower() != "traslado"))
                {
                    value = "";
                }
                this.mtipoDeComprobante = value.Trim().ToLower();
            }
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
                    value = 2.0;
                }
                this.mversion = value;
            }
        }
    }
}

