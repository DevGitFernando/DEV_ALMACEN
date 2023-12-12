﻿using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.FuncionesGenerales;
using Dll_MA_IFacturacion.CFDI;
using Dll_MA_IFacturacion.CFDI.EDOInvoice;
using Dll_MA_IFacturacion.CFDI.geCFD;
using Dll_MA_IFacturacion.CFDI.geCFD_33;

////using psecfdi.FoliosDigitales;

using DllFarmaciaSoft;
using Dll_MA_IFacturacion.CFDI.Timbrar;

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;


namespace Dll_MA_IFacturacion.CFDI.CFDFunctions
{
    public class clsGenCFDI_33_Pago
    {

        clsConexionSQL cnnConexion = null; //new clsConexionSQL(General.DatosConexion); 
        clsGrabarError Error;

        private SqlConnection connection;
        private clsInfoEmisor infoEmisor;
        private cMain main;
        private clsGenCFD ogCFD;
        public double pISH = 0.0;
        private clsProccess proccess;
        public CFDFunctionsEx.clsComprobante myComprobante = new CFDFunctionsEx.clsComprobante();
        private clsCFDI_33_Pagos ccfdi = new clsCFDI_33_Pagos();
        private string sLastError = "";
        PACs_Timbrado tpPAC = PACs_Timbrado.Ninguno;

        eVersionCFDI tpVersionCFDI = eVersionCFDI.Ninguna;

        public bool EsVistaPrevia = true;
        string sFormato = "###,###,###,###,##0.#0";
        string sFormatoIva = "###,###,###,###,##0.#0";
        string sURL_Validacion_SAT = @"https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx";

        public clsGenCFDI_33_Pago()
        {
            tpVersionCFDI = eVersionCFDI.Version__3_3;
        }

        public clsConexionSQL Conexion
        {
            get { return cnnConexion; }
            set { cnnConexion = value; }
        }

        public clsGenCFD CFD
        {
            get { return ogCFD; }
        }

        public string LastError
        {
            get { return sLastError; }
        }

        public PACs_Timbrado PAC
        {
            get { return tpPAC; }
            set { tpPAC = value; }
        }

        private string certificadoBase64( string filename )
        {
            string str = this.proccess.doBase64FromFile(filename);
            if(str != null)
            {
                return str;
            }
            this.Log("No se pudo incluir el certificado de sello digital. " + this.proccess.lastError);
            return null;
        }

        private clsCFDI_33_Pagos generarCFD( CFDFunctionsEx.clsComprobante Comprobante )
        {
            clsCFDI_33_Pagos ccfdi = new clsCFDI_33_Pagos();
            clsTraslado traslado = null;
            clsRetencion retencion = null;
            clsOtrosImpuestosLocales locales = new clsOtrosImpuestosLocales();

            Comprobante.DatosComprobante = Comprobante.DatosComprobante;
            Comprobante.CargarComprobante();

            locales.connection = this.connection;
            string sSerie = Comprobante.Serie;
            long num = Convert.ToInt32("0" + Comprobante.Folio);
            long num2 = 0L;
            string sNoCertificado = Comprobante.NoCertificado;
            double dTotalRetencion = 0.0;
            double dTotalTraslado = 0.0;

            try
            {
                //////////// Datos generales del comprobante                  
                ccfdi.version = 3.3;
                ccfdi.serie = Comprobante.Serie;
                ccfdi.folio = Convert.ToInt32(Comprobante.Folio);
                ccfdi.Emisor.rfc = Comprobante.Emisor.RFC;
                ccfdi.Emisor.nombre = Comprobante.Emisor.Nombre;
                ccfdi.fecha = Comprobante.Fecha.AddHours(-5);
                ccfdi.sello = "";
                ccfdi.noCertificado = Comprobante.NoCertificado;
                ccfdi.certificado = Comprobante.Certificado;
                ccfdi.condicionesDePago = Comprobante.CondicionesDePago;
                ccfdi.subTotal = Comprobante.SubTotal;
                ccfdi.total = Comprobante.Total;
                ccfdi.descuento = Comprobante.Descuento;
                ccfdi.motivoDescuento = "";
                ccfdi.TipoCambio = Comprobante.TipoDeCambio;
                ccfdi.Moneda = Comprobante.Moneda;

                //ccfdi.tipoDeComprobante = Comprobante.TipoDeComprobante.ToString();
                ccfdi.tipoDeComprobante = "P";  // Pago
                ccfdi.EfectoDeComprobante = Comprobante.EfectoDeComprobante;
                ccfdi.EfectoDeComprobante_Descripcion = Comprobante.EfectoDeComprobante_Descripcion;


                ccfdi.metodoDePago = Comprobante.MetodoDePago;
                ccfdi.metodoDePagoDescripcion = Comprobante.MetodoDePagoDescripcion;
                ccfdi.formaDePago = Comprobante.FormaDePago;
                ccfdi.formaDePago_Descripcion = Comprobante.FormaDePago_Descripcion;


                //ccfdi.LugarExpedicion = Comprobante.Emisor.ExpedidoEn.Municipio + ", " + Comprobante.Emisor.ExpedidoEn.Estado;
                ccfdi.LugarExpedicion = Comprobante.Emisor.ExpedidoEn.CodigoPostal;

                ccfdi.NumCtaPago = Comprobante.NumCtaPago;
                ccfdi.Observaciones_01 = Comprobante.Observaciones_01;
                ccfdi.Observaciones_02 = Comprobante.Observaciones_02;
                ccfdi.Observaciones_03 = Comprobante.Observaciones_03;
                ccfdi.Referencia = Comprobante.Referencia;

                ccfdi.TipoDeDocumento = Comprobante.TipoDeDocumento;
                ccfdi.TipoDeInsumo = Comprobante.TipoDeInsumo;
                ccfdi.RubroFinanciamiento = Comprobante.RubroFinanciamiento;
                ccfdi.FuenteFinanciamiento = Comprobante.FuenteFinanciamiento;
                ccfdi.TipoDeDocumentoElectronico = Comprobante.TipoDeDocumentoElectronico;


                //////////// Domicilio fiscal emisor  
                ccfdi.Emisor.DomicilioFiscal.calle = Comprobante.Emisor.DomicilioFiscal.Calle;
                ccfdi.Emisor.DomicilioFiscal.noExterior = Comprobante.Emisor.DomicilioFiscal.NoExterior;
                ccfdi.Emisor.DomicilioFiscal.noInterior = Comprobante.Emisor.DomicilioFiscal.NoInterior;
                ccfdi.Emisor.DomicilioFiscal.colonia = Comprobante.Emisor.DomicilioFiscal.Colonia;
                ccfdi.Emisor.DomicilioFiscal.localidad = "";
                ccfdi.Emisor.DomicilioFiscal.referencia = Comprobante.Emisor.DomicilioFiscal.Referencia;
                ccfdi.Emisor.DomicilioFiscal.municipio = Comprobante.Emisor.DomicilioFiscal.Municipio;
                ccfdi.Emisor.DomicilioFiscal.estado = Comprobante.Emisor.DomicilioFiscal.Estado;
                ccfdi.Emisor.DomicilioFiscal.pais = Comprobante.Emisor.DomicilioFiscal.Pais;
                ccfdi.Emisor.DomicilioFiscal.codigoPostal = Comprobante.Emisor.DomicilioFiscal.CodigoPostal;

                //////////// Domicilio de expedicion  
                ccfdi.Emisor.ExpedidoEn.calle = Comprobante.Emisor.ExpedidoEn.Calle;
                ccfdi.Emisor.ExpedidoEn.noExterior = Comprobante.Emisor.ExpedidoEn.NoExterior;
                ccfdi.Emisor.ExpedidoEn.noInterior = Comprobante.Emisor.ExpedidoEn.NoInterior;
                ccfdi.Emisor.ExpedidoEn.colonia = Comprobante.Emisor.ExpedidoEn.Colonia;
                ccfdi.Emisor.ExpedidoEn.localidad = "";
                ccfdi.Emisor.ExpedidoEn.referencia = Comprobante.Emisor.ExpedidoEn.Referencia;
                ccfdi.Emisor.ExpedidoEn.municipio = Comprobante.Emisor.ExpedidoEn.Municipio;
                ccfdi.Emisor.ExpedidoEn.estado = Comprobante.Emisor.ExpedidoEn.Estado;
                ccfdi.Emisor.ExpedidoEn.pais = Comprobante.Emisor.ExpedidoEn.Pais;
                ccfdi.Emisor.ExpedidoEn.codigoPostal = Comprobante.Emisor.ExpedidoEn.CodigoPostal;

                ccfdi.Emisor.RegimenFiscal_33.Regimen = Comprobante.RegimenFiscal_33.IdRegimen;
                ccfdi.Emisor.RegimenFiscal_33.Descripcion = Comprobante.RegimenFiscal_33.Regimen;

                //////// Regimenes fiscales 
                ////foreach (CFDFunctionsEx.clsRegimenFiscal regimen in Comprobante.RegimenFiscal.RegimenesFiscales)
                ////{
                ////    //// REV_AQUI
                ////    clsRegimenFiscal fiscal = new clsRegimenFiscal();
                ////    fiscal.Regimen = regimen.IdRegimen;
                ////    fiscal.Descripcion = regimen.Regimen;
                ////    ccfdi.Emisor.RegimenFiscal.Add(fiscal);
                ////    fiscal = null;
                ////}

                //////////// Receptor 
                ccfdi.Receptor.rfc = Comprobante.Receptor.RFC;
                ccfdi.Receptor.nombre = Comprobante.Receptor.Nombre;
                ccfdi.Receptor.UsoDeCFDI = Comprobante.Receptor.UsoDeCFDI;
                ccfdi.Receptor.UsoDeCFDI_Descripcion = Comprobante.Receptor.UsoDeCFDI_Descripcion;


                ccfdi.Receptor.Domicilio.calle = Comprobante.Receptor.Domicilio.Calle;
                ccfdi.Receptor.Domicilio.noExterior = Comprobante.Receptor.Domicilio.NoExterior;
                ccfdi.Receptor.Domicilio.noInterior = Comprobante.Receptor.Domicilio.NoInterior;
                ccfdi.Receptor.Domicilio.colonia = Comprobante.Receptor.Domicilio.Colonia;
                ccfdi.Receptor.Domicilio.localidad = "";
                ccfdi.Receptor.Domicilio.referencia = Comprobante.Receptor.Domicilio.Referencia;
                ccfdi.Receptor.Domicilio.municipio = Comprobante.Receptor.Domicilio.Municipio;
                ccfdi.Receptor.Domicilio.estado = Comprobante.Receptor.Domicilio.Estado;
                ccfdi.Receptor.Domicilio.pais = Comprobante.Receptor.Domicilio.Pais;
                ccfdi.Receptor.Domicilio.codigoPostal = Comprobante.Receptor.Domicilio.CodigoPostal;

                ccfdi.TipoDeRelacion = Comprobante.TipoDeRelacion;
                ccfdi.TipoDeRelacion_Descripcion = Comprobante.TipoDeRelacion_Descripcion;
                ccfdi.UUIDs_Relacionados = Comprobante.UUIDs_Relacionados;
                ccfdi.Informacion_Pago = Comprobante.Informacion_Pago; 


                //////////// Conceptos y Retenciones 
                clsConcepto concepto = null;
                foreach(CFDFunctionsEx.clsConcepto comprobante in Comprobante.Conceptos.Conceptos)
                {
                    if(comprobante.RetencionISR > 0.0)
                    {
                        dTotalRetencion += comprobante.RetencionISR;
                    }
                    if(comprobante.RetencionIVA > 0.0)
                    {
                        dTotalTraslado += comprobante.RetencionIVA;
                    }

                    concepto = new clsConcepto();
                    concepto.ClaveProdServ = comprobante.ClaveProdServ;
                    concepto.ClaveUnidad = comprobante.ClaveUnidad;
                    concepto.Impuesto_Base = comprobante.Impuesto_Base;
                    concepto.Impuesto_Importe = comprobante.Impuesto_Importe;

                    concepto.Impuesto_ImpuestoClave = comprobante.Impuesto_ImpuestoClave;
                    concepto.Impuesto_Impuesto = comprobante.Impuesto_Impuesto;
                    concepto.Impuesto_TasaOCuota = comprobante.Impuesto_TasaOCuota;
                    concepto.Impuesto_TipoFactor = comprobante.Impuesto_TipoFactor;

                    concepto.cantidad = comprobante.Cantidad;
                    concepto.unidad = comprobante.Unidad;
                    concepto.noIdentificacion = comprobante.NoIdentificacion;
                    concepto.descripcion = comprobante.Descripcion;
                    concepto.notas = comprobante.Notas;
                    concepto.tasaiva = comprobante.Iva;
                    concepto.importeiva = comprobante.ImporteIva;

                    if((comprobante.Notas != null) && this.main.getVarBoolValue(clsVars.varAdicConcatenarNotas()))
                    {
                        string str8 = this.main.getVarStrValue(clsVars.varAdicCadenaUnion());
                        switch(str8)
                        {
                            case null:
                                str8 = " ";
                                break;

                            case "":
                                str8 = " ";
                                break;
                        }
                        concepto.descripcion = concepto.descripcion + str8 + comprobante.Notas;
                    }

                    concepto.valorUnitario = this.ogCFD.roundValue(comprobante.ValorUnitario);
                    concepto.importe = this.ogCFD.roundValue(comprobante.Cantidad * comprobante.ValorUnitario);
                    ccfdi.Conceptos.Add(concepto);
                    concepto = null;
                }

                ccfdi.Impuestos.totalImpuestosRetenidos = this.ogCFD.roundValue(Comprobante.RetencionIVA + Comprobante.RetencionISR);
                ccfdi.Impuestos.totalImpuestosTrasladados = this.ogCFD.roundValue(((Comprobante.Impuesto1 + Comprobante.Impuesto2) + Comprobante.IEPS) + Comprobante.ISH);

                if(dTotalRetencion > 0.0)
                {
                    retencion = null;
                    retencion = new clsRetencion();
                    retencion.impuesto = "ISR";
                    retencion.importe = this.ogCFD.roundValue(dTotalRetencion);
                    ccfdi.Impuestos.Retenciones.Add(retencion);
                }

                if(dTotalTraslado > 0.0)
                {
                    retencion = null;
                    retencion = new clsRetencion();
                    retencion.impuesto = "IVA";
                    retencion.importe = this.ogCFD.roundValue(dTotalTraslado);
                    ccfdi.Impuestos.Retenciones.Add(retencion);
                }

                string str10 = "";
                double num7 = 0.0;
                double num8 = 0.0;
                double iTasa = 0.0;
                double iImporte = 0.0;
                bool flag = false;
                bool flag2 = false;
                str10 = "";
                num8 = 0.0;
                num7 = 0.0;
                flag = false;

                ////foreach (CFDFunctionsEx.clsConcepto comprobante in Comprobante.Conceptos.Conceptos)
                ////{
                ////}

                //// Regimenes fiscales 
                foreach(CFDFunctionsEx.clsTraslado t in Comprobante.Traslados.Traslados)
                {
                    traslado = new clsTraslado();
                    traslado.impuesto = t.Impuesto.ToString().ToUpper();
                    traslado.importe = t.Importe;
                    traslado.tasa = t.Tasa;

                    traslado.Impuesto_Importe = t.Impuesto_Importe;
                    traslado.Impuesto_ImpuestoClave = t.Impuesto_ImpuestoClave;
                    traslado.Impuesto_Impuesto = t.Impuesto_Impuesto;
                    traslado.Impuesto_TasaOCuota = t.Impuesto_TasaOCuota;
                    traslado.Impuesto_TipoFactor = t.Impuesto_TipoFactor;

                    ccfdi.Impuestos.Traslados.Add(traslado);
                    traslado = null;
                }

                //////////// Traslados 
                str10 = "";
                num8 = 0.0;
                num7 = 0.0;
                flag = false;

                if(Comprobante.IEPS > 0.0)
                {
                    traslado = new clsTraslado();
                    traslado.impuesto = "IEPS";
                    traslado.tasa = Convert.ToDouble((double)(Math.Round((double)(Comprobante.IEPS / (Comprobante.SubTotal - Comprobante.Descuento)), 2) * 100.0));
                    traslado.importe = Comprobante.IEPS;
                    ccfdi.Impuestos.Traslados.Add(traslado);
                    traslado = null;
                }

                foreach(clsTraslado traslado2 in ccfdi.Impuestos.Traslados)
                {
                    traslado2.importe = this.ogCFD.roundValue(traslado2.importe);
                }

                if(Comprobante.ISH > 0.0)
                {
                    clsTrasladoLocal t = new clsTrasladoLocal();
                    t.ImpLocTrasladado = "ISH";
                    t.TasadeTraslado = this.pISH;
                    t.Importe = Comprobante.ISH;
                    locales.add_and_sum_Traslado(t);
                }

                if((locales.TotaldeTraslados > 0.0) || (locales.TotaldeRetenciones > 0.0))
                {
                    clsMyElement element = new clsMyElement();
                    element.Prefix = "cfdi";
                    element.Name = "Complemento";
                    element.URI = "http://www.sat.gob.mx/cfd/3";
                    clsMyElement element2 = new clsMyElement();
                    element2.Prefix = "implocal";
                    element2.Name = "ImpuestosLocales";
                    element2.URI = "http://www.sat.gob.mx/implocal";
                    clsMyAttribute attribute = null;
                    attribute = new clsMyAttribute();
                    attribute.Name = "version";
                    attribute.Value = "1.0";
                    element2.Attributes.Add(attribute);
                    attribute = null;
                    attribute = new clsMyAttribute();
                    attribute.Name = "TotaldeRetenciones";
                    if(locales.TotaldeRetenciones == 0.0)
                    {
                        attribute.Value = "0.00";
                    }
                    else
                    {
                        attribute.Value = locales.TotaldeRetenciones.ToString("#.00");
                    }

                    element2.Attributes.Add(attribute);
                    attribute = null;
                    attribute = new clsMyAttribute();
                    attribute.Name = "TotaldeTraslados";
                    if(locales.TotaldeTraslados == 0.0)
                    {
                        attribute.Value = "0.00";
                    }
                    else
                    {
                        attribute.Value = locales.TotaldeTraslados.ToString("#.00");
                    }

                    element2.Attributes.Add(attribute);
                    if(locales.TotaldeRetenciones > 0.0)
                    {
                        foreach(clsRetencionLocal local2 in locales.RetencionesLocales)
                        {
                            clsMyElement element3 = new clsMyElement();
                            element3.Prefix = "implocal";
                            element3.Name = "RetencionesLocales";
                            element3.URI = "http://www.sat.gob.mx/implocal";
                            attribute = null;
                            attribute = new clsMyAttribute();
                            attribute.Name = "ImpLocRetenido";
                            attribute.Value = local2.ImpLocRetenido;
                            element3.Attributes.Add(attribute);
                            attribute = null;
                            attribute = new clsMyAttribute();
                            attribute.Name = "TasadeRetencion";
                            attribute.Value = local2.TasadeRetencion.ToString("#.00");
                            element3.Attributes.Add(attribute);
                            attribute = null;
                            attribute = new clsMyAttribute();
                            attribute.Name = "Importe";
                            attribute.Value = local2.Importe.ToString("#.00");
                            element3.Attributes.Add(attribute);
                            element2.Elements.Add(element3);
                        }
                    }

                    if(locales.TotaldeTraslados > 0.0)
                    {
                        foreach(clsTrasladoLocal local3 in locales.TrasladosLocales)
                        {
                            clsMyElement element4 = new clsMyElement();
                            element4.Prefix = "implocal";
                            element4.Name = "TrasladosLocales";
                            element4.URI = "http://www.sat.gob.mx/implocal";
                            attribute = null;
                            attribute = new clsMyAttribute();
                            attribute.Name = "ImpLocTrasladado";
                            attribute.Value = local3.ImpLocTrasladado;
                            element4.Attributes.Add(attribute);
                            attribute = null;
                            attribute = new clsMyAttribute();
                            attribute.Name = "TasadeTraslado";
                            attribute.Value = local3.TasadeTraslado.ToString("#.00");
                            element4.Attributes.Add(attribute);
                            attribute = null;
                            attribute = new clsMyAttribute();
                            attribute.Name = "Importe";
                            attribute.Value = local3.Importe.ToString("#.00");
                            element4.Attributes.Add(attribute);
                            element2.Elements.Add(element4);
                        }
                    }

                    element.Elements.Add(element2);
                    ccfdi.new_xmlns("xmlns:implocal", "http://www.sat.gob.mx/implocal");
                    ccfdi.new_schemaLocation("http://www.sat.gob.mx/implocal");
                    ccfdi.new_schemaLocation("http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd");
                    ccfdi.Complemento = element.getElement(ccfdi.cfdXmlDocument);
                }

            }
            catch(Exception exception2)
            {
                this.Log("Error al generar factura electrónica. " + exception2.Message);
                return null;
            }

            return ccfdi;
        }

        private void LogProceso( string Mensaje )
        {
            clsGrabarError.LogFileError(Mensaje);
        }

        private long generarFacturaElectronica( CFDFunctionsEx.clsComprobante Comprobante, string xsltfile, string privateKeyFileName, string pwd )
        {
            Exception exception;
            string fileName = "";
            long v = 0L;
            bool bTimbreCorrecto = false;
            string str8 = "";
            string filenameXML = "";
            string filenameImpresionXML = "";
            string strXML = "";
            string strCadenaOriginal = "";
            string strSelloDigital = "";
            string strSelloDigital_Segmento = "";
            string strXML_Final = "";
            string strXML_Impresion = "";
            string sError = "";
            bool bConexionInterna = false;


            LogProceso("Iniciando  generacion de documento.");
            try
            {
                sLastError = "";

                if(proccess == null)
                {
                    proccess = new clsProccess();
                }

                if(main == null)
                {
                    main = new cMain();
                }

                fileName = this.proccess.getTempFileName();
                filenameXML = fileName + ".xml";
                filenameImpresionXML = fileName + "_Impresion.xml";

                str8 = this.rutaXSLT_TDF();
                ccfdi = this.generarCFD(Comprobante);
                LogProceso("CFDI estructurado");

                if(ccfdi == null)
                {
                    sLastError = "Ocurrió un error al generar del cfdi.";
                    LogProceso(sLastError);
                    v = 0L;
                }
                else
                {
                    strXML = ccfdi.getXML(filenameXML, true);
                    if(strXML == "")
                    {
                        LogProceso(sLastError);
                        sLastError = "Ocurrió un error al generar el xml.";
                        LogProceso(sLastError);
                        return 0;
                    }

                    LogProceso("XML generado");
                    strCadenaOriginal = this.proccess.createOriginalString(filenameXML, xsltfile);
                    if(strCadenaOriginal == "")
                    {
                        LogProceso(sLastError);
                        sLastError = "Ocurrió un error al generar la cadena original.";
                        LogProceso(sLastError);
                        return 0;
                    }

                    LogProceso("Cadena original generada");
                    strSelloDigital = this.proccess.getDigitalSeal(filenameXML, xsltfile, privateKeyFileName, pwd, "3.3");
                    strSelloDigital_Segmento = General.Fg.Right(strSelloDigital, 6);
                    if(strSelloDigital == "")
                    {
                        LogProceso(sLastError);
                        sLastError = "Ocurrió un error al generar el sello del comprobante.";
                        LogProceso(sLastError);
                        return 0;
                    }

                    LogProceso("Sello generado");
                    ccfdi.cadenaOriginal = strCadenaOriginal;
                    ccfdi.sello = strSelloDigital;

                    LogProceso("Sello y Cadena Original asignados.");
                    strXML = ccfdi.getXML(filenameXML, false);
                    LogProceso("XML Final terminado");
                    if(strXML == "")
                    {
                        LogProceso(sLastError);
                        sLastError = "Ocurrió un error al generar el xml sellado.";
                        LogProceso(sLastError);
                        return 0;
                    }

                    LogProceso("XML final generado");
                    strXML_Final = File.ReadAllText(filenameXML);
                    if(strXML_Final == "")
                    {
                        LogProceso(sLastError);
                        sLastError = "Ocurrió un error leer el xml.";
                        LogProceso(sLastError);
                        return 0;
                    }

                    strXML_Impresion = ccfdi.getXML(filenameXML, false);
                    if(strXML_Impresion == "")
                    {
                        LogProceso(sLastError);
                        sLastError = "Ocurrió un error al generar el xml final.";
                        LogProceso(sLastError);
                        return 0;
                    }


                    clsTimbrar.EnProduccion = DtIFacturacion.PAC_Informacion.EnProduccion;
                    clsTimbrar.RFC_Emisor = ccfdi.Emisor.rfc;
                    clsTimbrar.Url = DtIFacturacion.PAC_Informacion.Url;
                    clsTimbrar.Usuario = DtIFacturacion.PAC_Informacion.Usuario;
                    clsTimbrar.Password = DtIFacturacion.PAC_Informacion.Password;
                    clsTimbrar.CertificadoPKCS12 = DtIFacturacion.PAC_Informacion.CertificadoPKCS12;
                    clsTimbrar.PasswordPKCS12 = DtIFacturacion.PAC_Informacion.PasswordPKCS12;
                    clsTimbrar.PAC = DtIFacturacion.PAC_Informacion.PAC;

                    if(this.guardarCFD(Comprobante, filenameXML, strXML))
                    {
                        bTimbreCorrecto = Timbrar(strXML_Final, Comprobante.NombreFile, strSelloDigital_Segmento);
                        v = bTimbreCorrecto ? 1 : 0;

                        if(!bTimbreCorrecto)
                        {
                            Guardar_Log_CFDI(Comprobante, Comprobante.NombreFile, strXML);
                        }
                    }
                }

            }
            catch(Exception exception2)
            {
                exception = exception2;
                sLastError = "Error en método facturacionelectronica() " + exception.Message;
                //this.Log("Error en método facturacionelectronica() " + exception.Message);
                v = 0L;
            }

            ////fileName = this.proccess.getTempFileName();
            ////filenameXML = fileName + ".xml";
            ////filenameImpresionXML = fileName + "_Impresion.xml";

            Limpiar_Archivos_Temporales(fileName);
            Limpiar_Archivos_Temporales(filenameXML);
            Limpiar_Archivos_Temporales(filenameImpresionXML);

            if(sLastError != "")
            {
                LogProceso(sLastError);
                this.Log(sLastError, "generarFacturaElectronica");
            }

            return v;
        }

        private bool Guardar_Log_CFDI( CFDFunctionsEx.clsComprobante Comprobante, string XmlFile, string XML )
        {
            bool bRegresa = false;
            string path = "";

            try
            {
                path = string.Format(@"LogError__{0}_____{1}", DtIFacturacion.MarcaDeTiempo(), Comprobante.NombreXml);
                bRegresa = General.Fg.ConvertirStringEnArchivo(path, DtIFacturacion.RutaCFDI_Log, XML, true);

                if(!bRegresa)
                {
                    using(StreamWriter writer = new StreamWriter(Path.Combine(DtIFacturacion.RutaCFDI_Log, path), false))
                    {
                        writer.Write(XML);
                    }

                }
            }
            catch { }

            return bRegresa;
        }

        private void Limpiar_Archivos_Temporales( string FileName )
        {
            string sError = "";

            try
            {
                if(File.Exists(FileName))
                {
                    File.Delete(FileName);
                }
            }
            catch(Exception ex)
            {
                sError = ex.Message;
            }
        }

        private bool guardarCFD( CFDFunctionsEx.clsComprobante Comprobante, string XmlFile, string XML )
        {
            bool bRegresa = false;
            string sExec = EsVistaPrevia ? " spp_Mtto_CFDI_XML_VP " : " spp_Mtto_CFDI_XML ";
            string sVistaPrevia = "VISTA_PREVIA.xml";

            //clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion);
            clsLeer leer = new clsLeer(ref cnnConexion);

            long lRetorno = 0;
            SC_SolutionsSystem.FuncionesGenerales.basGenerales Fg = new SC_SolutionsSystem.FuncionesGenerales.basGenerales();
            string XML_Impresion = "";
            string path = "";
            string path_Aux = "";
            string sFolioRelacionado = ccfdi.FolioRelacionado == "" ? ccfdi.folio.ToString() : ccfdi.FolioRelacionado;

            try
            {
                LogProceso("Guardando CFD");
                path = Path.Combine(DtIFacturacion.RutaCFDI_DocumentosGenerados, ccfdi.Emisor.rfc);
                path_Aux = Path.Combine(DtIFacturacion.RutaCFDI_DocumentosImpresion, ccfdi.Emisor.rfc);

                DtIFacturacion.CrearDirectorio(path);
                DtIFacturacion.CrearDirectorio(path_Aux);

                path = Path.Combine(path, EsVistaPrevia ? sVistaPrevia : Comprobante.NombreXml);
                path_Aux = Path.Combine(path_Aux, EsVistaPrevia ? sVistaPrevia : Comprobante.NombreXmlImpresion);
                ccfdi.Impresion.WriteXml(path_Aux, XmlWriteMode.WriteSchema);
                XML_Impresion = Fg.ConvertirArchivoEnStringB64(path_Aux);

                string sSql = string.Format("Exec {0} ", sExec);
                sSql += string.Format(
                    " @IdEmpresa = '{0}', @IdEstado = '{1}', @IdFarmacia = '{2}', @Serie = '{3}', @Folio = {4}, " +
                    " @uf_CadenaOriginal = '{5}', @uf_SelloDigital = '{6}', @uf_CFDFolio = '{7}', @uf_IVenta = '{8}', " +
                    " @uf_CFDI_Info = '{9}', @uf_Tipo = '{10}', @uf_CanceladoSAT = '{11}', @uf_CadenaOriginalSAT = '{12}', " +
                    " @uf_SelloDigitalSAT = '{13}', @uf_FolioSAT = '{14}', @uf_NoCertificadoSAT = '{15}', " +
                    " @uf_FechaHoraCerSAT = {16}, @uf_CBB = '{17}', @uf_ackCancelacion_SAT = '{18}', @uf_Xml_Base = '{19}', " +
                    " @uf_Xml_Timbrado = '{20}', @uf_Xml_Impresion = '{21}', @uf_Pdf = '{22}', @IdPAC = '{23}' ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    ccfdi.serie, ccfdi.folio,
                    ccfdi.cadenaOriginal, ccfdi.sello, 0, sFolioRelacionado,
                    1, 3, 0, "", "", "", "", "null", "", "",
                    XML, "", XML_Impresion, "", Fg.PonCeros((int)clsTimbrar.PAC, 3));

                if(!leer.Exec(sSql))
                {
                    sLastError = "Ocurrió un error al guardar el comprobante electrónico.";
                    LogProceso(sLastError);
                    LogProceso(leer.MensajeError);
                }
                else
                {
                    if(leer.Leer())
                    {
                        lRetorno = leer.CampoInt("FolioDocumento");
                        if(File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        if(File.Exists(path_Aux))
                        {
                            File.Delete(path_Aux);
                        }

                        try
                        {
                            File.Copy(XmlFile, path, true);
                            ccfdi.Impresion.WriteXml(path_Aux, XmlWriteMode.WriteSchema);
                        }
                        catch { }
                    }
                }
                LogProceso("Guardado de CFD terminado.");
            }
            catch
            {

            }

            bRegresa = lRetorno > 0;
            return bRegresa;
        }

        private void Log( string msg )
        {
            Log(msg, "");
        }

        private void Log( string msg, string Funcion )
        {
            if(Error == null)
            {
                Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, "clsGenCFDI_33_Pago");
            }

            Error.GrabarError(msg, Funcion);

        }

        public long obtenerFacturaElectronica( CFDFunctionsEx.clsComprobante Comprobante )
        {
            ////LogProceso("Facturación electróncica 0001");
            string xsltfile = DtIFacturacion.Ruta_XSLT_CadenaOriginal;
            string privateKeyFileName = DtIFacturacion.RutaKey;
            string pwd = DtIFacturacion.Password_Certificado;

            ////LogProceso("Facturación electróncica 0002");
            if(ogCFD == null)
            {
                ogCFD = new clsGenCFD();
            }

            ////LogProceso("Facturación electróncica 0003");
            if(this.ogCFD.esInvalido(privateKeyFileName, "Error al acceder a información de certificado de sello digital."))
            {
                return 0L;
            }

            tpVersionCFDI = eVersionCFDI.Version__3_3;

            ////LogProceso("Facturación electróncica 0004");
            return this.generarFacturaElectronica(Comprobante, xsltfile, privateKeyFileName, pwd);
        }

        private string rutaXSLT_TDF()
        {
            string sRutaFile = DtIFacturacion.Ruta_XSLT_CadenaOriginal_TFD;

            //sRutaFile = DtIFacturacion.Ruta_XSLT_CadenaOriginal_Base + @"\cadenaoriginal_TFD_1_1.xslt"; 

            return sRutaFile; // DtIFacturacion.Ruta_XSLT_CadenaOriginal_TFD;
        }

        public void setObjects( clsGenCFD gCfd )
        {
            this.main = gCfd.main;
            this.ogCFD = gCfd;
            this.connection = gCfd.connection;
            this.infoEmisor = gCfd.infoEmisor;
            this.proccess = gCfd.main.proccess;
            this.pISH = gCfd.pISH;
        }

        private bool Timbrar( string ArchivoXml, string Referencia, string Segmento_Sello )
        {
            bool bRegresa = false;

            if(EsVistaPrevia)
            {
                bRegresa = GenerarTimbre_VP(ArchivoXml, Referencia, Segmento_Sello);
            }
            else
            {
                bRegresa = GenerarTimbre(ArchivoXml, Referencia, Segmento_Sello);
            }

            return bRegresa;
        }

        private DataTable GetExtras()
        {
            ArrayList Datos = new ArrayList();
            DatosExtraImpresion dato = new DatosExtraImpresion();

            dato = new DatosExtraImpresion();
            dato.Nombre = "StatusDocumento";
            dato.Valor = "A";
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_01";
            dato.Valor = ccfdi.Observaciones_01;
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_02";
            dato.Valor = ccfdi.Observaciones_02;
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_03";
            dato.Valor = ccfdi.Observaciones_03;
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Referencia";
            dato.Valor = ccfdi.Referencia;
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "TipoDeDocumento";
            dato.Valor = ccfdi.TipoDeDocumento;
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "TipoDeInsumo";
            dato.Valor = ccfdi.TipoDeInsumo;
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "RubroFinanciamiento";
            dato.Valor = ccfdi.RubroFinanciamiento;
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "FuenteFinanciamiento";
            dato.Valor = ccfdi.FuenteFinanciamiento;
            Datos.Add(dato);


            dato = new DatosExtraImpresion();
            dato.Nombre = "CantidadLetra";
            dato.Valor = General.LetraMoneda(ccfdi.total);
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "FechaCancelacionCFDI";
            dato.Valor = "";
            Datos.Add(dato);


            DataTable dt = new DataTable("Extras");
            object[] obj = new object[Datos.Count];
            int i = -1;

            foreach(DatosExtraImpresion d in Datos)
            {
                i++;
                obj[i] = d.Valor.ToString();
                dt.Columns.Add(d.Nombre, System.Type.GetType("System.String"));
            }

            dt.Rows.Add(obj);

            return dt;
        }

        private bool GenerarTimbre_VP( string ArchivoXml, string Referencia, string Segmento_Sello )
        {
            //clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
            clsLeer leer = new clsLeer(ref cnnConexion);
            clsTimbreFDInfo info = new clsTimbreFDInfo();
            clsLeer leerTimbre = new clsLeer();
            DataSet dtsTimbre = new DataSet();

            DataTable leerCBB = new DataTable();
            object[] objImagenes = new object[1];
            leerCBB = new DataTable("CBB");
            leerCBB.Columns.Add("CBB", System.Type.GetType("System.Byte[]"));

            LogProceso("Iniciando Timbrado VP");
            SC_SolutionsSystem.FuncionesGenerales.basGenerales Fg = new SC_SolutionsSystem.FuncionesGenerales.basGenerales();
            bool bRegresa = false;
            string sXmlTimbrado = "";
            string sCadenaOriginalSAT = "";
            string ruta = "";

            ruta = this.proccess.getTempFileName();
            if(!this.main.uiCBB.getImgCBB(sURL_Validacion_SAT, ccfdi.Emisor.rfc, ccfdi.Receptor.rfc, ccfdi.total.ToString(), "VistaPrevia", ruta, Segmento_Sello, tpVersionCFDI))
            {
                bRegresa = false;
                LogProceso("No fue posible generar el Código QR");
            }
            else
            {
                objImagenes[0] = File.ReadAllBytes(ruta);
                dtsTimbre = ccfdi.Impresion;
                dtsTimbre.DataSetName = ccfdi.Impresion.DataSetName;
                dtsTimbre.Tables.Remove("CBB");
                leerCBB.Rows.Add(objImagenes);
                dtsTimbre.Tables.Add(leerCBB.Copy());
                dtsTimbre.Tables.Add(GetExtras().Copy());

                string path_Aux = proccess.getTempFileName();
                string XML_Impresion = "";


                object[] obj2 = { DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), "VistaPrevia", "0", "VistaPrevia", "VistaPrevia", "1.0", "VistaPrevia" };
                dtsTimbre.Tables["TimbreFiscal"].Rows.Add(obj2);
                dtsTimbre.WriteXml(path_Aux, XmlWriteMode.WriteSchema);


                XML_Impresion = Fg.ConvertirArchivoEnStringB64(path_Aux);
                ogCFD.DeleteFile(path_Aux);

                sXmlTimbrado = ArchivoXml;
                string sSql = string.Format(
                    "Set DateFormat YMD Update X Set uf_CadenaOriginalSAT = '{5}', uf_SelloDigitalSAT = '{6}', " +
                    "   uf_FolioSAT = '{7}', uf_NoCertificadoSAT = '{8}', " +
                    "   uf_FechaHoraCerSAT = '{9}', uf_CBB = '{10}', " +
                    "   uf_Xml_Timbrado = '{11}', uf_Xml_Impresion = '{12}' " +
                    "From CFDI_XML_VP X (NoLock) " +
                    "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' ",
                    DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                    ccfdi.serie, ccfdi.folio,
                    sCadenaOriginalSAT, "VistaPrevia", "VistaPrevia", "", Fg.Mid(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), 1, 19),
                    Fg.ConvertirArchivoEnBytes(ruta), sXmlTimbrado, XML_Impresion);


                if(!leer.Exec(sSql))
                {
                    bRegresa = false;
                    LogProceso("GenerarTimbre_VP  ...... " + leer.MensajeError);
                }
                else
                {
                    bRegresa = true;
                }
            }
            ogCFD.DeleteFile(ruta);


            return bRegresa;
        }

        private bool GenerarTimbre( string ArchivoXml, string Referencia, string Segmento_Sello )
        {
            //clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
            clsLeer leer = new clsLeer(ref cnnConexion);
            clsTimbreFDInfo info = new clsTimbreFDInfo();
            clsLeer leerTimbre = new clsLeer();
            DataSet dtsTimbre = new DataSet();

            DataTable leerCBB = new DataTable();
            object[] objImagenes = new object[1];
            leerCBB = new DataTable("CBB");
            leerCBB.Columns.Add("CBB", System.Type.GetType("System.Byte[]"));


            SC_SolutionsSystem.FuncionesGenerales.basGenerales Fg = new SC_SolutionsSystem.FuncionesGenerales.basGenerales();
            bool bRegresa = false;
            string sXmlTimbrado = "";
            string sCadenaOriginalSAT = "";
            string ruta = "";
            clsGrabarError Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, "clsGenCFDI_Ex");


            bRegresa = clsTimbrar.Timbrar(ArchivoXml);
            if(!bRegresa)
            {
                sLastError = clsTimbrar.MensajeError;
            }
            else
            {
                sXmlTimbrado = clsTimbrar.XmlConTimbreFiscal;
                bRegresa = info.loadInfo(sXmlTimbrado, tpVersionCFDI);
                sCadenaOriginalSAT = proccess.createOriginalStringByXMLString(sXmlTimbrado, this.rutaXSLT_TDF());

                if(bRegresa)
                {
                    ruta = this.proccess.getTempFileName();
                    if(!this.main.uiCBB.getImgCBB(sURL_Validacion_SAT, ccfdi.Emisor.rfc, ccfdi.Receptor.rfc, ccfdi.total.ToString(), info.UUID, ruta, Segmento_Sello, tpVersionCFDI))
                    {
                        bRegresa = false;
                    }
                    else
                    {
                        objImagenes[0] = File.ReadAllBytes(ruta);
                        dtsTimbre = ccfdi.Impresion;
                        dtsTimbre.DataSetName = ccfdi.Impresion.DataSetName;
                        dtsTimbre.Tables.Remove("CBB");
                        leerCBB.Rows.Add(objImagenes);
                        dtsTimbre.Tables.Add(leerCBB.Copy());
                        dtsTimbre.Tables.Add(GetExtras().Copy());

                        string path_Aux = proccess.getTempFileName();
                        string XML_Impresion = "";

                        object[] obj2 = { info.FechaTimbrado.ToString("yyyy-MM-ddTHH:mm:ss"), info.UUID, info.noCertificadoSAT, info.selloCFD, info.selloSAT, info.version, info.rfcProvCertif };
                        dtsTimbre.Tables["TimbreFiscal"].Rows.Add(obj2);
                        dtsTimbre.WriteXml(path_Aux, XmlWriteMode.WriteSchema);


                        XML_Impresion = Fg.ConvertirArchivoEnStringB64(path_Aux);
                        //////sXmlTimbrado = Fg.ConvertirStringB64FromString(sXmlTimbrado);
                        ogCFD.DeleteFile(path_Aux);


                        string sSql = string.Format(
                            "Set DateFormat YMD Update X Set uf_CadenaOriginalSAT = '{5}', uf_SelloDigitalSAT = '{6}', " +
                            "   uf_FolioSAT = '{7}', uf_NoCertificadoSAT = '{8}', " +
                            "   uf_FechaHoraCerSAT = '{9}', uf_CBB = '{10}', " +
                            "   uf_Xml_Timbrado = '{11}', uf_Xml_Impresion = '{12}' " +
                            "From CFDI_XML X (NoLock) " +
                            "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' ",
                            DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada,
                            ccfdi.serie, ccfdi.folio,
                            sCadenaOriginalSAT, info.selloSAT, info.UUID, info.noCertificadoSAT,
                            Fg.Mid(info.FechaTimbrado.ToString("yyyy-MM-ddTHH:mm:ss"), 1, 19),
                            Fg.ConvertirArchivoEnBytes(ruta), sXmlTimbrado, XML_Impresion);

                        if(!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            Error.GrabarError(leer, "GenerarTimbre");
                        }
                        else
                        {
                            bRegresa = true;
                            //Guardar_XML_Timbrado(ccfdi.Receptor.rfc, ccfdi.serie, ccfdi.folio.ToString(), info.UUID); 
                        }
                    }
                    ogCFD.DeleteFile(ruta);
                }
            }

            return bRegresa;
        }

        private void Guardar_XML_Timbrado( string RFC, string Serie, string Folio, string UUID )
        {
            basGenerales Fg = new basGenerales();
            string sFile = string.Format("{0}__{1}____{2}____{3}.xml", Serie, Fg.PonCeros(Folio, 10), RFC, UUID.ToUpper());
            string path = Path.Combine(DtIFacturacion.RutaCFDI_DocumentosGenerados, ccfdi.Emisor.rfc);

            Fg.ConvertirStringEnArchivo(sFile, path, clsTimbrar.XmlConTimbreFiscal, true);
        }
    }
}

