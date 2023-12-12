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

using Dll_MA_IFacturacion;
using Dll_MA_IFacturacion.CFDI;
using Dll_MA_IFacturacion.CFDI.CFDFunctionsEx;

namespace Dll_MA_IFacturacion.CFDI.geCFD
{
    public class clsCFDI_NM
    {
        private string defPrefix = "cfdi";
        private string defPrefix_nm = "nomina";
        private string defURI = "http://www.sat.gob.mx/cfd/3";
        private string defURI_nm = "http://www.sat.gob.mx/nomina";
        private ArrayList schemaLocation = new ArrayList();
        private ArrayList xmlns = new ArrayList();
        private XmlDocument cfd = new XmlDocument();
        

        public clsEmisor Emisor = new clsEmisor();
        public clsReceptor Receptor = new clsReceptor();
        public ArrayList Conceptos = new ArrayList();
        public clsImpuestos Impuestos = new clsImpuestos();
        public clsInformacionAdicional InformacionAdicional = new clsInformacionAdicional(); 

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
        private string mLugarExpedicion = "";
        private string mmetodoDePago = "";
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
        private double mtotal = 0;
        private string mCadenaOriginal = "";
        private string mTipoDeDocumento = "";

        clsLeer conceptosDeNomina = new clsLeer(); 

        DataSet dtsImpresion;
        DataTable dtComprobante;
        DataTable dtEmisor;
        DataTable dtEmisorDomicilioFiscal;
        DataTable dtEmisorDomicilioExpedidoEn;
        DataTable dtEmisorRegimen;
        DataTable dtReceptor;
        DataTable dtReceptorDomililio;
        DataTable dtConceptos;
        DataTable dtConceptosDetalles;
        DataTable dtConceptos_InformacionAduanera;
        DataTable dtImpuestos;
        DataTable dtRetenciones;
        DataTable dtTraslados;
        DataTable dtTimbreFiscal;
        DataTable dtLogo;
        DataTable dtCBB;
        DataTable dtInformacionAdicional;

        private string sObservaciones_01 = "";
        private string sObservaciones_02= "";
        private string sObservaciones_03 = "";
        private string sReferencia = "";

        private string sTipoDocumento = ""; 
        private string sTipoInsumo = "";  
        private string sRubroFinanciamiento = ""; 
        private string sFuenteFinanciamiento = "";  


        public clsCFDI_NM()
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
            LogProceso("isOK", Mensaje);
        }

        private void LogProceso(string Funcion, string Mensaje)
        {
            ////clsGrabarError.LogFileError(string.Format("clsCFDI_NM.{0}:    {1}", Funcion, Mensaje));
            clsLog_CFDI.Log("clsCFDI_NM", Funcion, Mensaje); 
        }

        public string getXML(string filename, bool omitSeal)
        {
            this.InitCFD_DataSet(); //// Preparar el xml para la impresion 
            string sCadena = ""; 

            XmlElement element3;
            XmlElement element4;
            XmlElement element5;
            Exception exception2;
            if (!this.isOK(omitSeal))
            {
                LogProceso(lastError); 
                return "";
            }

            this.InitCFD();
            XmlElement documentElement = this.cfd.DocumentElement;
            documentElement.SetAttribute("xmlns:nomina", "http://www.sat.gob.mx/cfd/3");
            documentElement.SetAttribute("xmlns:cfdi", "http://www.sat.gob.mx/cfd/3");
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
            
            //writer14.WriteAttributeString("xsi", "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/nomina http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina11.xsd");

            documentElement.SetAttribute("schemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/nomina http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina11.xsd" + str);
            documentElement.SetAttribute("version", this.version.ToString("#.0"));
            if ((this.serie != "") && (this.serie != null))
            {
                documentElement.SetAttribute("serie", this.toUTF8(this.serie));
            }

            if (this.folio > 0L)
            {
                documentElement.SetAttribute("folio", this.toUTF8(this.folio.ToString()));
            }

            documentElement.SetAttribute("fecha", this.fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
            if ((this.sello != "") && (this.sello != null))
            {
                documentElement.SetAttribute("sello", this.toUTF8(this.sello));
            }

            documentElement.SetAttribute("formaDePago", this.toUTF8(this.formaDePago));
            documentElement.SetAttribute("noCertificado", this.toUTF8(this.noCertificado));
            documentElement.SetAttribute("certificado", this.toUTF8(this.certificado));
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
                LogProceso(lastError); 
                return "";
            }

            XmlElement newChild = this.newElement("Emisor");
            newChild.SetAttribute("rfc", this.toUTF8(this.Emisor.rfc));
            if ((this.Emisor.nombre != "") && (this.Emisor.nombre != null))
            {
                newChild.SetAttribute("nombre", this.toUTF8(this.Emisor.nombre));
            }

            if (this.Emisor.DomicilioFiscal.isOK())
            {
                element3 = this.newElement("DomicilioFiscal");
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
                element3 = this.newElement("ExpedidoEn");
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
                element3 = this.newElement("RegimenFiscal");
                element3.SetAttribute("Regimen", this.toUTF8(fiscal.Regimen));
                newChild.AppendChild(element3);
                element3 = null;

                object[] obj = { toUTF8(fiscal.Regimen) };
                dtEmisorRegimen.Rows.Add(obj); 
            }

            documentElement.AppendChild(newChild);
            newChild = null;
            if (!this.Receptor.isOK())
            {
                this.lastError = "Error Receptor. " + this.Receptor.lastError;
                LogProceso(lastError); 
                return "";
            }

            newChild = this.newElement("Receptor");
            newChild.SetAttribute("rfc", this.toUTF8(this.Receptor.rfc));
            if (this.Receptor.nombre != "")
            {
                newChild.SetAttribute("nombre", this.toUTF8(this.Receptor.nombre));
            }

            if (this.Receptor.Domicilio.isOK())
            {
                element3 = this.newElement("Domicilio");
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


            clsLeer concepto = new clsLeer();
            concepto.DataTableClase = conceptosDeNomina.Tabla("Conceptos"); 

            documentElement.AppendChild(newChild);
            newChild = null;
            if (concepto.Registros < 1)
            {
                this.lastError = "Falta elemento requerido: Conceptos";
                LogProceso(lastError); 
                return "";
            }


            int iConceptos = 0;  
            newChild = this.newElement("Conceptos"); 
            while (concepto.Leer()) 
            {
                iConceptos++;
                element3 = this.newElement("Concepto");
                element3.SetAttribute("cantidad", concepto.Campo("Cantidad").ToString());
                element3.SetAttribute("unidad", this.toUTF8(concepto.Campo("Unidad")));

                element3.SetAttribute("descripcion", this.toUTF8(concepto.Campo("Descripcion")));
                element3.SetAttribute("valorUnitario", concepto.Campo("ValorUnitario").ToString());
                element3.SetAttribute("importe", concepto.Campo("ValorUnitario").ToString());

                newChild.AppendChild(element3);
            }


            documentElement.AppendChild(newChild);
            newChild = null;
            if (!this.Impuestos.isOK())
            {
                this.lastError = "Error Impuestos. " + this.Impuestos.lastError;
                LogProceso(lastError); 
                return "";
            }

            clsLeer impuestos = new clsLeer();
            impuestos.DataTableClase = conceptosDeNomina.Tabla("Impuestos");
            impuestos.Leer(); 

            newChild = this.newElement("Impuestos");
            newChild.SetAttribute("totalImpuestosRetenidos", this.toUTF8(impuestos.CampoDouble("totalImpuestosRetenidos").ToString()));
            newChild.SetAttribute("totalImpuestosTrasladados", this.toUTF8(impuestos.CampoDouble("totalImpuestosTrasladados").ToString()));

            element4 = this.newElement("Retenciones");
            element5 = this.newElement("Retencion");
            element5.SetAttribute("impuesto", this.toUTF8("ISR"));
            element5.SetAttribute("importe", impuestos.CampoDouble("totalImpuestosTrasladados").ToString());
            element4.AppendChild(element5);
            element5 = null; 
            newChild.AppendChild(element4);
            element4 = null;

            element4 = this.newElement("Traslados");
            element5 = this.newElement("Traslado");
            element5.SetAttribute("impuesto", this.toUTF8("IVA"));
            element5.SetAttribute("tasa", "0.00");
            element5.SetAttribute("importe", impuestos.CampoDouble("totalImpuestosTrasladados").ToString());
            element4.AppendChild(element5);
            element5 = null;
            newChild.AppendChild(element4);
            element4 = null;

            documentElement.AppendChild(newChild);
            newChild = null;


            ////documentElement.SetAttribute("schemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/nomina http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina11.xsd" + str);
            ////documentElement.SetAttribute("version", this.version.ToString("#.0"));
            newChild = this.newElement("Complemento");
            documentElement.AppendChild(newChild);
            ////documentElement.SetAttribute("nomina", "Nomina", "http://www.sat.gob.mx/nomina"); 

            clsLeer leerReceptor = new clsLeer();
            leerReceptor.DataTableClase = conceptosDeNomina.Tabla("Receptor");
            leerReceptor.Leer();


            element4 = this.newElement_NM("Nomina");
            element4.SetAttribute("Version", "1.1");
            if (leerReceptor.Campo("RegistroPatronal") != "")
            {
                element4.SetAttribute("RegistroPatronal", leerReceptor.Campo("RegistroPatronal"));
            }
            element4.SetAttribute("NumEmpleado", leerReceptor.Campo("NumEmpleado"));
            element4.SetAttribute("CURP", leerReceptor.Campo("CURP"));
            element4.SetAttribute("TipoRegimen", leerReceptor.Campo("TipoRegimen"));
            if (leerReceptor.Campo("NSS") != "")
            {
                element4.SetAttribute("NumSeguridadSocial", leerReceptor.Campo("NSS"));
            }
            element4.SetAttribute("FechaPago", leerReceptor.CampoFecha("FechaPago").ToString("yyyy-MM-dd"));
            element4.SetAttribute("FechaInicialPago", leerReceptor.CampoFecha("FechaInicialPago").ToString("yyyy-MM-dd"));
            element4.SetAttribute("FechaFinalPago", leerReceptor.CampoFecha("FechaFinalPago").ToString("yyyy-MM-dd"));
            element4.SetAttribute("NumDiasPagados", leerReceptor.Campo("NumDiasPagados"));
            element4.SetAttribute("FechaInicioRelLaboral", leerReceptor.CampoFecha("FechaInicioRelLaboral").ToString("yyyy-MM-dd")); 
            if (leerReceptor.Campo("Departamento") != "")
            {
                element4.SetAttribute("Departamento", leerReceptor.Campo("Departamento"));
            }
            if (leerReceptor.Campo("Clabe") != "")
            {
                element4.SetAttribute("CLABE", leerReceptor.Campo("Clabe"));
            }
            if (leerReceptor.Campo("Banco") != "")
            {
                element4.SetAttribute("Banco", leerReceptor.Campo("Banco"));
            }
            if (leerReceptor.Campo("Antiguedad") != "")
            {
                element4.SetAttribute("Antiguedad", leerReceptor.Campo("Antiguedad"));
            }
            if (leerReceptor.Campo("Puesto") != "")
            {
                element4.SetAttribute("Puesto", leerReceptor.Campo("Puesto"));
            }
            if (leerReceptor.Campo("TipoContrato") != "")
            {
                element4.SetAttribute("TipoContrato", leerReceptor.Campo("TipoContrato"));
            }
            if (leerReceptor.Campo("TipoJornada") != "")
            {
                element4.SetAttribute("TipoJornada", leerReceptor.Campo("TipoJornada"));
            }
            if (leerReceptor.Campo("PeriodicidadPago") != "")
            {
                element4.SetAttribute("PeriodicidadPago", leerReceptor.Campo("PeriodicidadPago"));
            }
            if (leerReceptor.Campo("RiesgoPuesto") != "")
            {
                element4.SetAttribute("RiesgoPuesto", leerReceptor.Campo("RiesgoPuesto"));
            }
            if (leerReceptor.Campo("SalarioBaseCotApor") != "")
            {
                element4.SetAttribute("SalarioBaseCotApor", leerReceptor.Campo("SalarioBaseCotApor"));
            }
            if (leerReceptor.Campo("SalarioDiarioIntegrado") != "")
            {
                element4.SetAttribute("SalarioDiarioIntegrado", leerReceptor.Campo("SalarioDiarioIntegrado"));
            }

            newChild.AppendChild(element4); //// Cerrar el complemento nómina
            ////element4 = null;


            double dPercepciones_Gravadas = 0;
            double dPercepciones_Exentas = 0;
            double dDeducciones_Gravadas = 0;
            double dDeducciones_Exentas = 0;
            double dIncapacidades = 0;
            double dHorasExtras = 0;
            NM_TipoDetalle tipo = NM_TipoDetalle.Ninguno;

            clsLeer conceptosDetalles = new clsLeer();
            conceptosDetalles.DataTableClase = conceptosDeNomina.Tabla("ConceptosDetalles");

            while (conceptosDetalles.Leer())
            {
                tipo = (NM_TipoDetalle)((int)conceptosDetalles.CampoInt("Tipo"));
                switch (tipo)
                {
                    case NM_TipoDetalle.Percepcion:
                        dPercepciones_Gravadas += conceptosDetalles.CampoDouble("Dato_001");
                        dPercepciones_Exentas += conceptosDetalles.CampoDouble("Dato_002"); 
                        break;

                    case NM_TipoDetalle.Deduccion:
                        dDeducciones_Gravadas += conceptosDetalles.CampoDouble("Dato_001");
                        dDeducciones_Exentas += conceptosDetalles.CampoDouble("Dato_002");
                        break;
                        
                    case NM_TipoDetalle.Incapacidad:
                        dIncapacidades += conceptosDetalles.CampoDouble("Dato_002");
                        break;

                    case NM_TipoDetalle.Horas_Extras:
                        dHorasExtras += conceptosDetalles.CampoDouble("Dato_002");
                        break; 
                }
            }

            ////////////// Agregando conceptos  
            clsLeer conceptoNomina = new clsLeer();
            conceptoNomina.DataRowsClase = conceptosDeNomina.Tabla("ConceptosDetalles").Select(" Tipo = 1 ");

            element5 = this.newElement_NM("Percepciones");
            element5.SetAttribute("TotalExento", dPercepciones_Exentas.ToString());
            element5.SetAttribute("TotalGravado", dPercepciones_Gravadas.ToString());             
            while (conceptoNomina.Leer())
            {
                element3 = this.newElement_NM("Percepcion");
                element3.SetAttribute("TipoPercepcion", conceptoNomina.Campo("Clave"));
                element3.SetAttribute("Clave", conceptoNomina.Campo("ClaveInterna"));
                element3.SetAttribute("Concepto", conceptoNomina.Campo("Descripcion"));
                element3.SetAttribute("ImporteGravado", conceptoNomina.CampoDouble("Dato_001").ToString());
                element3.SetAttribute("ImporteExento", conceptoNomina.CampoDouble("Dato_002").ToString());                 
                element5.AppendChild(element3); 
            }
            element4.AppendChild(element5);
            element5 = null;

            element3 = null; 
            if (dDeducciones_Gravadas > 0 || dDeducciones_Exentas > 0)
            {
                conceptoNomina.DataRowsClase = conceptosDeNomina.Tabla("ConceptosDetalles").Select(" Tipo = 2 ");
                element5 = this.newElement_NM("Deducciones");
                element5.SetAttribute("TotalExento", dDeducciones_Exentas.ToString());
                element5.SetAttribute("TotalGravado", dDeducciones_Gravadas.ToString()); 
                while (conceptoNomina.Leer())
                {
                    element3 = this.newElement_NM("Deduccion");
                    element3.SetAttribute("TipoDeduccion", conceptoNomina.Campo("Clave"));
                    element3.SetAttribute("Clave", conceptoNomina.Campo("ClaveInterna"));
                    element3.SetAttribute("Concepto", conceptoNomina.Campo("Descripcion"));
                    element3.SetAttribute("ImporteGravado", conceptoNomina.CampoDouble("Dato_001").ToString());
                    element3.SetAttribute("ImporteExento", conceptoNomina.CampoDouble("Dato_002").ToString());
                    element5.AppendChild(element3);
                }
                element5.AppendChild(element3);
                element3 = null;

                element4.AppendChild(element5);
                element5 = null;
            }

            element3 = null; 
            if (dIncapacidades > 0 )
            {
                conceptoNomina.DataRowsClase = conceptosDeNomina.Tabla("ConceptosDetalles").Select(" Tipo = 3 ");
                element5 = this.newElement_NM("Incapacidades");
                while (conceptoNomina.Leer())
                {
                    element3 = this.newElement_NM("Incapacidad");
                    element3.SetAttribute("DiasIncapacidad", conceptoNomina.CampoDouble("Dato_001").ToString());
                    element3.SetAttribute("TipoIncapacidad", conceptoNomina.Campo("Clave"));
                    element3.SetAttribute("Descuento", conceptoNomina.CampoDouble("Dato_002").ToString());
                    element5.AppendChild(element3);
                }
                element5.AppendChild(element3);
                element3 = null;

                element4.AppendChild(element5);
                element5 = null;
            }

            element3 = null; 
            if (dHorasExtras > 0)
            {
                conceptoNomina.DataRowsClase = conceptosDeNomina.Tabla("ConceptosDetalles").Select(" Tipo = 4 ");
                element5 = this.newElement_NM("HorasExtras");
                while (conceptoNomina.Leer())
                {
                    element3 = this.newElement_NM("HorasExtra");
                    element3.SetAttribute("Dias", conceptoNomina.Campo("ClaveInterna"));
                    element3.SetAttribute("TipoHoras", conceptoNomina.Campo("Descripcion"));
                    element3.SetAttribute("HorasExtra", conceptoNomina.CampoDouble("Dato_001").ToString());
                    element3.SetAttribute("ImportePagado", conceptoNomina.CampoDouble("Dato_002").ToString());
                    element5.AppendChild(element3);
                }
                element5.AppendChild(element3);
                element3 = null;

                element4.AppendChild(element5);
                element5 = null;
            }

            ////newChild.AppendChild(element4); //// Cerrar el complemento nómina
            ////element4 = null;
            ///////////////////////////////////////////////////////////////////////////// 


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
            Preparar_Dts_12_InformacionAdicional(); 
        }

        public DataSet Impresion
        {
            get
            {
                dtsImpresion = new DataSet("ImpresionCFDI_NM");
                dtsImpresion.Tables.Add(dtComprobante.Copy());
                dtsImpresion.Tables.Add(dtEmisor.Copy());
                dtsImpresion.Tables.Add(dtEmisorDomicilioFiscal.Copy());
                dtsImpresion.Tables.Add(dtEmisorDomicilioExpedidoEn.Copy());
                dtsImpresion.Tables.Add(dtEmisorRegimen.Copy());
                dtsImpresion.Tables.Add(dtReceptor.Copy());
                dtsImpresion.Tables.Add(dtReceptorDomililio.Copy());
                dtsImpresion.Tables.Add(dtConceptos.Copy());
                dtsImpresion.Tables.Add(dtConceptosDetalles.Copy());
                //dtsImpresion.Tables.Add(dtConceptos_InformacionAduanera.Copy());
                dtsImpresion.Tables.Add(dtImpuestos.Copy());
                dtsImpresion.Tables.Add(dtRetenciones.Copy());
                dtsImpresion.Tables.Add(dtTraslados.Copy());
                dtsImpresion.Tables.Add(dtTimbreFiscal.Copy());
                dtsImpresion.Tables.Add(dtLogo.Copy());
                dtsImpresion.Tables.Add(dtCBB.Copy());
                dtsImpresion.Tables.Add(dtInformacionAdicional.Copy()); 
                
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
            dtComprobante.Columns.Add("MetodoDePago", System.Type.GetType("System.String"));
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

            object[] obj = { 
                version.ToString(), toUTF8(tipoDeComprobante), 
                fecha.ToString("yyyy-MM-ddTHH:mm:ss"), FechaFolioFiscalOrig.ToString("yyyy-MM-ddTHH:mm:ss"), 
                toUTF8(serie), toUTF8(SerieFolioFiscalOrig), 
                toUTF8(folio.ToString()), toUTF8(FolioFiscalOrig), 
                toUTF8(noCertificado), toUTF8(certificado), 
                toUTF8(sello), toUTF8(formaDePago), toUTF8(metodoDePago), mMoneda, MontoFolioFiscalOrig, 
                toUTF8(motivoDescuento), toUTF8(NumCtaPago), toUTF8(condicionesDePago), 
                toUTF8(LugarExpedicion), toUTF8(TipoCambio), descuento, subTotal, total, mTipoDeDocumento 
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
        }

        private void Preparar_Dts_06_Receptor()
        {
            dtReceptor = new DataTable("Receptor");

            ////dtReceptor.Columns.Add("RFC", System.Type.GetType("System.String"));
            ////dtReceptor.Columns.Add("Nombre", System.Type.GetType("System.String"));

            ////object[] obj = { toUTF8(Receptor.rfc), toUTF8(Receptor.nombre) };
            ////dtReceptor.Rows.Add(obj);

            dtReceptor = conceptosDeNomina.Tabla("Receptor");
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
            dtConceptos = new DataTable("Conceptos");

            ////dtConceptos.Columns.Add("Orden", System.Type.GetType("System.Int32"));
            ////dtConceptos.Columns.Add("NoIdentificacion", System.Type.GetType("System.String"));
            ////dtConceptos.Columns.Add("Descripcion", System.Type.GetType("System.String"));
            ////dtConceptos.Columns.Add("Unidad", System.Type.GetType("System.String"));
            ////dtConceptos.Columns.Add("Cantidad", System.Type.GetType("System.Double"));
            ////dtConceptos.Columns.Add("ValorUnitario", System.Type.GetType("System.Double"));
            ////dtConceptos.Columns.Add("TasaIVA", System.Type.GetType("System.Double"));
            ////dtConceptos.Columns.Add("ImporteIva", System.Type.GetType("System.Double"));
            ////dtConceptos.Columns.Add("Importe", System.Type.GetType("System.Double"));
            ////dtConceptos.Columns.Add("Notas", System.Type.GetType("System.String"));


            ////dtConceptos_InformacionAduanera = new DataTable("ConceptosInformacionAduanera");
            ////dtConceptos_InformacionAduanera.Columns.Add("Orden", System.Type.GetType("System.Int32"));
            ////dtConceptos_InformacionAduanera.Columns.Add("Id", System.Type.GetType("System.Int32"));
            ////dtConceptos_InformacionAduanera.Columns.Add("Numero", System.Type.GetType("System.String"));
            ////dtConceptos_InformacionAduanera.Columns.Add("Fecha", System.Type.GetType("System.String"));
            ////dtConceptos_InformacionAduanera.Columns.Add("Aduana", System.Type.GetType("System.String")); 

            dtConceptos = conceptosDeNomina.Tabla("Conceptos");
            dtConceptosDetalles = conceptosDeNomina.Tabla("ConceptosDetalles"); 

        }

        private void Preparar_Dts_09_Traslados()
        {
            ////dtTraslados = new DataTable("Traslados");
            ////dtTraslados.Columns.Add("Impuesto", System.Type.GetType("System.String"));
            ////dtTraslados.Columns.Add("Tasa", System.Type.GetType("System.Int32"));
            ////dtTraslados.Columns.Add("Importe", System.Type.GetType("System.Double"));

            ////dtRetenciones = new DataTable("Retenciones"); 
            ////dtRetenciones.Columns.Add("Impuesto", System.Type.GetType("System.String"));
            ////dtRetenciones.Columns.Add("Importe", System.Type.GetType("System.Double"));

            dtTraslados = conceptosDeNomina.Tabla("Impuestos");
            dtRetenciones = conceptosDeNomina.Tabla("Impuestos");
            dtTraslados.TableName = "Traslados";
            dtRetenciones.TableName = "Retenciones";
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

            ////object[] obj = { "", "", "", "", "", "" };
            ////dtTimbreFiscal.Rows.Add(obj);
        }

        private void Preparar_Dts_11_Logo()
        {
            dtLogo = new DataTable("Logo");
            dtLogo.Columns.Add("Logo", System.Type.GetType("System.Byte[]"));
            dtLogo = DtIFacturacion.Logo.Copy();

            dtCBB = new DataTable("CBB");
            dtCBB.Columns.Add("CBB", System.Type.GetType("System.Byte[]"));
            
        }

        private void Preparar_Dts_12_InformacionAdicional()
        {           
            dtInformacionAdicional = new DataTable("InformacionAdicional");

            dtInformacionAdicional.Columns.Add("Observaciones_01", System.Type.GetType("System.String"));
            dtInformacionAdicional.Columns.Add("Observaciones_02", System.Type.GetType("System.String"));
            dtInformacionAdicional.Columns.Add("Observaciones_03", System.Type.GetType("System.String"));
            dtInformacionAdicional.Columns.Add("Observaciones_04", System.Type.GetType("System.String"));
            dtInformacionAdicional.Columns.Add("Observaciones_05", System.Type.GetType("System.String"));
            dtInformacionAdicional.Columns.Add("Observaciones_06", System.Type.GetType("System.String"));
            dtInformacionAdicional.Columns.Add("Observaciones_07", System.Type.GetType("System.String"));
            dtInformacionAdicional.Columns.Add("Observaciones_08", System.Type.GetType("System.String"));
            dtInformacionAdicional.Columns.Add("Observaciones_09", System.Type.GetType("System.String"));
            dtInformacionAdicional.Columns.Add("Observaciones_10", System.Type.GetType("System.String"));

            object[] obj = { 
                toUTF8(InformacionAdicional.Observaciones_01), toUTF8(InformacionAdicional.Observaciones_02),
                toUTF8(InformacionAdicional.Observaciones_03), toUTF8(InformacionAdicional.Observaciones_04),
                toUTF8(InformacionAdicional.Observaciones_05), toUTF8(InformacionAdicional.Observaciones_06),
                toUTF8(InformacionAdicional.Observaciones_07), toUTF8(InformacionAdicional.Observaciones_08),
                toUTF8(InformacionAdicional.Observaciones_09), toUTF8(InformacionAdicional.Observaciones_10)
                           };
            dtInformacionAdicional.Rows.Add(obj);
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
                if (this.version != 3.2)
                {
                    this.lastError = "Valor incorrecto en version: debe ser 3.2";
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

        public XmlElement newElement_NM(string sName)
        {
            return this.cfd.CreateElement(this.defPrefix_nm, sName, this.defURI_nm);
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

        public clsLeer ListaDeConceptos
        {
            get { return conceptosDeNomina; }
            set { conceptosDeNomina = value; }
        }

    }
}

