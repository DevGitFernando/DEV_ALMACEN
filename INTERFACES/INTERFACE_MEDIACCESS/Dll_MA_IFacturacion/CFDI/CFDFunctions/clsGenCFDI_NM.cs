using System;
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
using Dll_MA_IFacturacion.CFDI.Timbrar; 

using Microsoft.VisualBasic; 
using Microsoft.VisualBasic.FileIO;

namespace Dll_MA_IFacturacion.CFDI.CFDFunctions
{
    public class clsGenCFDI_NM
    {
        clsConexionSQL cnnConexion = null; //new clsConexionSQL(General.DatosConexion); 
        clsGrabarError Error; 

        private SqlConnection connection;
        private clsInfoEmisor infoEmisor;
        private cMain main;
        private clsGenCFD ogCFD;
        public double pISH = 0.0;
        private clsProccess proccess;
        public CFDFunctionsEx.clsComprobante_NM myComprobante = new CFDFunctionsEx.clsComprobante_NM();
        private clsCFDI_NM ccfdi = new clsCFDI_NM();
        private string sLastError = "";
        PACs_Timbrado tpPAC = PACs_Timbrado.Ninguno;

        string sGUID = ""; 
        public bool EsVistaPrevia = true;
        string sFormato = "###,###,###,###,##0.#0";
        string sFormatoIva = "###,###,###,###,##0.#0";

        public clsConexionSQL Conexion
        {
            get { return cnnConexion; }
            set { cnnConexion = value; }
        }

        public clsGenCFD CFD
        {
            get { return ogCFD; }
        }

        public string GUID
        {
            get { return sGUID; }
            set { sGUID = value; }
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

        public bool CancelarCFDI(long ID)
        {
            bool bRegresa = false;
            ////clsLeer leer = new clsLeer(ref cnnConexion);
            ////clsGrabarError Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, "clsGenCFDI_NM");

            ////string sUUID = ""; 
            ////string sSql = string.Format(
            ////    "Select Keyx, IdEmisor, Serie, Folio, uf_CadenaOriginal, uf_SelloDigital, uf_CFDFolio, uf_IVenta, uf_CFDI_Info, uf_Tipo, " + 
            ////    " uf_CanceladoSAT, uf_CadenaOriginalSAT, uf_SelloDigitalSAT, uf_FolioSAT, uf_NoCertificadoSAT, uf_FechaHoraCerSAT, " + 
            ////    " uf_CBB, uf_ackCancelacion_SAT, uf_Xml_Base, uf_Xml_Timbrado, uf_Xml_Impresion, uf_Pdf " + 
            ////    "From CFDI_XML (NoLock) " + 
            ////    "Where Keyx = '{0}' ", ID);

            ////if (!leer.Exec(sSql))
            ////{
            ////    Error.GrabarError(leer, "cancelarCFDI"); 
            ////}
            ////else
            ////{
            ////    if (!leer.Leer())
            ////    {
            ////    }
            ////    else
            ////    {
            ////        sUUID = leer.Campo("uf_FolioSAT"); 
            ////        bRegresa = CancelarCFDI(sUUID); 
            ////    }
            ////}

            return bRegresa; 
        }

        public bool CancelarCFDI(string UUID)
        {
            bool bRegresa = false;

            ////clsConexionSQL cnn = new clsConexionSQL(General.DatosConexion); 
            clsLeer leer = new clsLeer(ref cnnConexion);
            clsTimbreFDInfo info = new clsTimbreFDInfo();
            clsLeer leerTimbre = new clsLeer();
            DataSet dtsTimbre = new DataSet();

            string sXmlAcuseCancelacion = "";
            string sSql = string.Format("Update F Set uf_CanceladoSAT = 1 " +
                "From CFDI_NM_XML F (NoLock) " +
                "Where uf_FolioSAT = '{0}' ", UUID);

            Preparar_ConexionTimbrado(DtIFacturacion.EmisorRFC);
            bRegresa = clsTimbrar.CancelarCFDI(UUID);

            if (!bRegresa)
            {
                sLastError = "Error al cancelar el comprobante: " + clsTimbrar.MensajeError;
            }
            else
            {
                ////SC_SolutionsSystem.FuncionesGenerales.basGenerales Fg = new FuncionesGenerales.basGenerales();
                sXmlAcuseCancelacion = clsTimbrar.XmlAcuseCancelacion;
                sXmlAcuseCancelacion = General.Fg.ConvertirStringB64FromString(clsTimbrar.XmlAcuseCancelacion); 
                sSql = string.Format("Update F Set uf_CanceladoSAT = 1, uf_ackCancelacion_SAT = '{1}', uf_CanceladoSAT_FechaCancelacion = getdate() " +
                "From CFDI_NM_XML F (NoLock) " +
                "Where uf_FolioSAT = '{0}' ", UUID, General.Fg.ConvertirArchivoEnStringB64(sXmlAcuseCancelacion));

                sSql = string.Format("Exec spp_Mtto_CFDI_NM_XML_CancelarDocumentos @uf_FolioSAT = '{0}', @uf_ackCancelacion_SAT = '{1}' ", UUID, sXmlAcuseCancelacion); 
                if (!leer.Exec(sSql))
                {
                    bRegresa = false;
                    sLastError = "El comprobante se canceló correctamente pero no pudo marcarse como cancelado.";
                }
                else
                {
                    sLastError = "CFDI cancelado correctamente.";
                    bRegresa = true;
                }
            }

            return bRegresa;
        }

        private string certificadoBase64(string filename)
        {
            string str = this.proccess.doBase64FromFile(filename);
            if (str != null)
            {
                return str;
            }
            this.Log("No se pudo incluir el certificado de sello digital. " + this.proccess.lastError);
            return null;
        }

        private clsCFDI_NM generarCFD(CFDFunctionsEx.clsComprobante_NM Comprobante)
        {
            clsCFDI_NM ccfdi = new clsCFDI_NM();
            clsTraslado traslado = null;
            clsRetencion retencion = null;
            clsOtrosImpuestosLocales locales = new clsOtrosImpuestosLocales();

            Comprobante.CargarComprobante();          

            locales.connection = this.connection; 
            string sSerie = Comprobante.Serie;
            long num = Convert.ToInt32(Comprobante.Folio);
            long num2 = 0L;
            string sNoCertificado = Comprobante.NoCertificado;
            double dTotalRetencion = 0.0;
            double dTotalTraslado = 0.0;

            //////////// Pendiente 
            //////if (!this.disponibleParaGenerar(infoVenta, true))
            //////{
            //////    return null;
            //////}

            try
            {
                //////////// Datos generales del comprobante                  
                ccfdi.version = 3.2; 
                ccfdi.serie = Comprobante.Serie;
                ccfdi.folio = Convert.ToInt32(Comprobante.Folio);
                ccfdi.Emisor.rfc = Comprobante.Emisor.RFC;
                ccfdi.Emisor.nombre = Comprobante.Emisor.Nombre;
                ccfdi.fecha = Comprobante.Fecha.AddHours(-5);
                ccfdi.sello = "";
                ccfdi.formaDePago = Comprobante.FormaDePago;
                ccfdi.noCertificado = Comprobante.NoCertificado;
                ccfdi.certificado = Comprobante.Certificado; 
                ccfdi.condicionesDePago = Comprobante.CondicionesDePago;
                ccfdi.subTotal = Comprobante.SubTotal;
                ccfdi.total = Comprobante.Total; 
                ccfdi.descuento = Comprobante.Descuento;
                ccfdi.motivoDescuento = Comprobante.MotivoDescuento;
                ccfdi.TipoCambio = Comprobante.TipoDeCambio;
                ccfdi.Moneda = Comprobante.Moneda;
                ccfdi.tipoDeComprobante = Comprobante.TipoDeComprobante.ToString();
                ccfdi.metodoDePago = Comprobante.MetodoDePago;
                ccfdi.LugarExpedicion = Comprobante.Emisor.ExpedidoEn.Municipio + ", " + Comprobante.Emisor.ExpedidoEn.Estado;
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

                //////////// Informacion adicional ... Observaciones 
                ccfdi.InformacionAdicional = Comprobante.InformacionAdicional; 

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

                //// Regimenes fiscales 
                foreach (CFDFunctionsEx.clsRegimenFiscal regimen in Comprobante.RegimenFiscal.RegimenesFiscales)
                {
                    clsRegimenFiscal fiscal = new clsRegimenFiscal();
                    fiscal.Regimen = regimen.Regimen;
                    ccfdi.Emisor.RegimenFiscal.Add(fiscal);
                    fiscal = null;
                }

                //////////// Receptor 
                ccfdi.Receptor.rfc = Comprobante.Receptor.RFC;
                ccfdi.Receptor.nombre = Comprobante.Receptor.Nombre;
                ccfdi.Receptor.curp = Comprobante.Receptor.CURP;
                ccfdi.Receptor.nss = Comprobante.Receptor.NSS; 

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


                ////// Conceptos de nómina 
                ccfdi.ListaDeConceptos.DataSetClase = Comprobante.DatosComprobante;  
            }
            catch (Exception exception2)
            {
                this.Log("Error al generar nómina electrónica. " + exception2.Message);
                return null;
            }

            return ccfdi; 
        }

        private void LogProceso(string Mensaje)      
        {
            clsGrabarError.LogFileError(Mensaje); 
        }

        private void Preparar_ConexionTimbrado(string RFC)
        {
            clsTimbrar.EnProduccion = DtIFacturacion.PAC_Informacion.EnProduccion;
            clsTimbrar.RFC_Emisor = RFC;
            clsTimbrar.Url = DtIFacturacion.PAC_Informacion.Url;
            clsTimbrar.Usuario = DtIFacturacion.PAC_Informacion.Usuario;
            clsTimbrar.Password = DtIFacturacion.PAC_Informacion.Password;
            clsTimbrar.CertificadoPKCS12 = DtIFacturacion.PAC_Informacion.CertificadoPKCS12;
            clsTimbrar.PasswordPKCS12 = DtIFacturacion.PAC_Informacion.PasswordPKCS12;
            clsTimbrar.PAC = DtIFacturacion.PAC_Informacion.PAC;
        }

        private long generarNominaElectronica(CFDFunctionsEx.clsComprobante_NM Comprobante, string xsltfile, string privateKeyFileName, string pwd)
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
            string strXML_Final = "";
            string strXML_Impresion = "";
            string sError = "";
            bool bConexionInterna = false;


            LogProceso("Iniciando  generacion de documento."); 
            try
            {
                sLastError = "";

                if (proccess == null)
                {
                    proccess = new clsProccess(); 
                }

                if (main == null)
                {
                    main = new cMain(); 
                }

                fileName = this.proccess.getTempFileName();
                filenameXML = fileName + ".xml";
                filenameImpresionXML = fileName + "_Impresion_NM.xml";

                str8 = this.rutaXSLT_Nomina();
                ccfdi = this.generarCFD(Comprobante);
                LogProceso("CFDI estructurado");

                if (ccfdi == null)
                {
                    sLastError = "Ocurrió un error al generar del cfdi.";
                    LogProceso(sLastError); 
                    v = 0L;
                }
                else
                {
                    strXML = ccfdi.getXML(filenameXML, true);
                    if (strXML == "")
                    {
                        sLastError = "Ocurrió un error al generar el xml.";
                        LogProceso(sLastError); 
                        return 0; 
                    }

                    LogProceso("XML generado"); 
                    strCadenaOriginal = this.proccess.createOriginalString(filenameXML, xsltfile);
                    if (strCadenaOriginal == "")
                    {
                        sLastError = "Ocurrió un error al generar la cadena original.";
                        LogProceso(sLastError); 
                        return 0;
                    }

                    LogProceso("Cadena original generada");
                    strSelloDigital = this.proccess.getDigitalSeal(filenameXML, xsltfile, privateKeyFileName, pwd);
                    if (strSelloDigital == "")
                    {
                        sLastError = "Ocurrió un error al generar el sello del comprobante.";
                        LogProceso(sLastError); 
                        return 0;
                    }

                    LogProceso("Sello generado");  //// REVISAR SELLO 
                    ccfdi.cadenaOriginal = strCadenaOriginal; 
                    ccfdi.sello = strSelloDigital;

                    LogProceso("Sello y Cadena Original asignados.");
                    strXML = ccfdi.getXML(filenameXML, true); // false 
                    LogProceso("XML Final terminado");
                    if (strXML == "")
                    {
                        sLastError = "Ocurrió un error al generar el xml sellado.";
                        LogProceso(sLastError); 
                        return 0;
                    }

                    LogProceso("XML final generado"); 
                    strXML_Final = File.ReadAllText(filenameXML);
                    if (strXML_Final == "")
                    {
                        sLastError = "Ocurrió un error leer el xml.";
                        LogProceso(sLastError); 
                        return 0;
                    }

                    strXML_Impresion = ccfdi.getXML(filenameXML, true);
                    if (strXML_Impresion == "")
                    {
                        sLastError = "Ocurrió un error al generar el xml final.";
                        LogProceso(sLastError); 
                        return 0;
                    }


                    Preparar_ConexionTimbrado(ccfdi.Emisor.rfc);
                    clsTimbrar.TipoDeDocumento = Comprobante.TipoDeDocumentoElectronico;

                    ////clsTimbrar.TipoDeDocumento = Comprobante.TipoDeDocumentoElectronico;
                    ////clsTimbrar.EnProduccion = DtIFacturacion.PAC_Informacion.EnProduccion;
                    ////clsTimbrar.RFC_Emisor = ccfdi.Emisor.rfc;
                    ////clsTimbrar.Url = DtIFacturacion.PAC_Informacion.Url;
                    ////clsTimbrar.Usuario = DtIFacturacion.PAC_Informacion.Usuario;
                    ////clsTimbrar.Password = DtIFacturacion.PAC_Informacion.Password;
                    ////clsTimbrar.CertificadoPKCS12 = DtIFacturacion.PAC_Informacion.CertificadoPKCS12;
                    ////clsTimbrar.PasswordPKCS12 = DtIFacturacion.PAC_Informacion.PasswordPKCS12;
                    ////clsTimbrar.PAC = DtIFacturacion.PAC_Informacion.PAC;

                    if (this.guardarCFD(Comprobante, filenameXML, strXML))
                    {
                        bTimbreCorrecto = Timbrar(strXML_Final, Comprobante.NombreFile);
                        v = bTimbreCorrecto ? 1 : 0; 
                    }                    
                }

            }
            catch (Exception exception2)
            {
                exception = exception2;
                sLastError  = "Error en método facturacionelectronica() " + exception.Message;               
                //this.Log("Error en método facturacionelectronica() " + exception.Message);
                v= 0L;
            }

            ////////// Control de transaccion interna 
            //////if (bConexionInterna)
            //////{
            //////    if (!bTimbreCorrecto)
            //////    {
            //////        cnnConexion.DeshacerTransaccion();
            //////    }
            //////    else
            //////    {
            //////        cnnConexion.CompletarTransaccion();
            //////    }

            //////    // Finalizar le preoceso de guardado  
            //////    cnnConexion.Cerrar();
            //////}

            if (sLastError != "")
            {
                LogProceso(sLastError); 
                this.Log(sLastError, "generarNominaElectronica"); 
            } 

            return v; 
        }

        private bool guardarCFD(CFDFunctionsEx.clsComprobante_NM Comprobante, string XmlFile, string XML)
        {
            bool bRegresa = false;
            string sExec = EsVistaPrevia ? " spp_Mtto_CFDI_NM_XML_VP " : " spp_Mtto_CFDI_NM_XML ";
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

                string sSql = ""; 
                sSql = string.Format("Exec {0} ", sExec);
                sSql += string.Format(" @IdEmisor = '{0}', @Serie = '{1}', @Folio = '{2}', " +
                     " @uf_CadenaOriginal = '{3}', @uf_SelloDigital = '{4}', @uf_CFDFolio = '{5}', @uf_IVenta = '{6}', " +
                     " @uf_CFDI_Info = '{7}', @uf_Tipo = '{8}', @uf_CanceladoSAT = '{9}', @uf_CadenaOriginalSAT = '{10}', " +
                     " @uf_SelloDigitalSAT = '{11}', @uf_FolioSAT = '{12}', @uf_NoCertificadoSAT = '{13}', " +
                     " @uf_FechaHoraCerSAT = null, @uf_CBB = '{15}', @uf_ackCancelacion_SAT  = '{16}', " +
                     " @uf_Xml_Base = '{17}', @uf_Xml_Timbrado = '{18}', @uf_Xml_Impresion = '{19}', @uf_Pdf = '{20}', @IdPAC = '{21}', " +
                     " @GUID = '{22}', @IdRegistro = '{23}' ",
                     DtIFacturacion.EmisorConectado, ccfdi.serie, ccfdi.folio,
                     ccfdi.cadenaOriginal, ccfdi.sello, 0, sFolioRelacionado,
                     1, 3, 0, "", "", "", "", "null", "", "",
                     XML, "", XML_Impresion, "", Fg.PonCeros((int)clsTimbrar.PAC, 3), sGUID, DtIFacturacion.EmisorIdRegistroPatronal);

                if (!leer.Exec(sSql)) 
                {
                    sLastError = "Ocurrió un error al guardar el comprobante electrónico.";
                    LogProceso(sLastError);
                    LogProceso(leer.MensajeError); 
                }
                else
                {
                    if (leer.Leer())
                    {
                        lRetorno = leer.CampoInt("FolioDocumento");
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        if (File.Exists(path_Aux))
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
            catch(Exception ex)
            {

            }

            bRegresa = lRetorno > 0;
            return bRegresa; 
        }

        private void Log(string msg)
        {
            Log(msg, ""); 
        }

        private void Log(string msg, string Funcion)
        {
            if (Error == null)
            {
                Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, "clsGenCFDI_NM"); 
            }

            Error.GrabarError(msg, Funcion); 

        }

        public long obtenerNominaElectronica(CFDFunctionsEx.clsComprobante_NM Comprobante)
        {
            string xsltfile = DtIFacturacion.Ruta_XSLT_CadenaOriginal;
            string privateKeyFileName = DtIFacturacion.RutaKey;
            string pwd = DtIFacturacion.Password_Certificado;

            if (ogCFD == null)
            {
                ogCFD = new clsGenCFD();
            }

            if (this.ogCFD.esInvalido(privateKeyFileName, "Error al acceder a información de certificado de sello digital."))
            {
                return 0L;
            }

            return this.generarNominaElectronica(Comprobante, xsltfile, privateKeyFileName, pwd);
        }

        private string rutaXSLT_Nomina()
        {
            return DtIFacturacion.Ruta_XSLT_CadenaOriginal_TFD;
        }

        public void setObjects(clsGenCFD gCfd)
        {
            this.main = gCfd.main;
            this.ogCFD = gCfd;
            this.connection = gCfd.connection;
            this.infoEmisor = gCfd.infoEmisor;
            this.proccess = gCfd.main.proccess;
            this.pISH = gCfd.pISH;
        }

        private bool Timbrar(string ArchivoXml, string Referencia)
        {
            bool bRegresa = false;

            if (EsVistaPrevia)
            {
                bRegresa = GenerarTimbre_VP(ArchivoXml, Referencia); 
            }
            else
            {
                bRegresa = GenerarTimbre(ArchivoXml, Referencia); 
            }

            return bRegresa; 
        }

        private DataTable GetExtras()
        {
            ArrayList Datos = new ArrayList();
            DatosExtraImpresion dato = new DatosExtraImpresion();

            dato = new DatosExtraImpresion();
            dato.Nombre = "Email";
            dato.Valor = DtIFacturacion.EmisorMail;
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Telefonos";
            dato.Valor = DtIFacturacion.EmisorTelefonos;
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Fax";
            dato.Valor = DtIFacturacion.EmisorFax;
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "CantidadLetra";
            dato.Valor = General.LetraMoneda(ccfdi.total);
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_01";
            dato.Valor = "";
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_02";
            dato.Valor = ""; 
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_03";
            dato.Valor = ""; 
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_04";
            dato.Valor = ""; 
            Datos.Add(dato);

            dato = new DatosExtraImpresion();
            dato.Nombre = "Observaciones_05";
            dato.Valor = ""; 
            Datos.Add(dato); 

            DataTable dt = new DataTable("Extras");
            object[] obj = new object[Datos.Count];
            int i = -1;

            foreach (DatosExtraImpresion d in Datos)
            {
                i++;
                obj[i] = d.Valor.ToString();
                dt.Columns.Add(d.Nombre, System.Type.GetType("System.String"));
            }

            dt.Rows.Add(obj);

            return dt;
        }

        private bool GenerarTimbre_VP(string ArchivoXml, string Referencia)
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
            if (!this.main.uiCBB.getImgCBB(ccfdi.Emisor.rfc, ccfdi.Receptor.rfc, ccfdi.total.ToString(), "VistaPrevia", ruta))
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


                object[] obj2 = { DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), "VistaPrevia", "0", "VistaPrevia", "VistaPrevia", "1.0" };
                dtsTimbre.Tables["TimbreFiscal"].Rows.Add(obj2);
                dtsTimbre.WriteXml(path_Aux, XmlWriteMode.WriteSchema);


                XML_Impresion = Fg.ConvertirArchivoEnStringB64(path_Aux);
                ogCFD.DeleteFile(path_Aux);

                sXmlTimbrado = ArchivoXml;
                string sSql = "";

                sSql = string.Format(
                     "Set DateFormat YMD Update X Set uf_CadenaOriginalSAT = '{5}', uf_SelloDigitalSAT = '{6}', " +
                     "   uf_FolioSAT = '{7}', uf_NoCertificadoSAT = '{8}', " +
                     "   uf_FechaHoraCerSAT = '{9}', uf_CBB = '{10}', " +
                     "   uf_Xml_Timbrado = '{11}', uf_Xml_Impresion = '{12}' " +
                     "From CFDI_XML_VP X (NoLock) " +
                     "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' ",
                     "DtGeneral.EmpresaConectada", "DtGeneral.EstadoConectado", "DtGeneral.FarmaciaConectada",
                     ccfdi.serie, ccfdi.folio,
                     sCadenaOriginalSAT, "VistaPrevia", "VistaPrevia", "", Fg.Mid(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), 1, 19),
                     Fg.ConvertirArchivoEnBytes(ruta), sXmlTimbrado, XML_Impresion);


                sSql = string.Format(
                    "Set DateFormat YMD Update X Set uf_CadenaOriginalSAT = '{3}', uf_SelloDigitalSAT = '{4}', " +
                    "   uf_FolioSAT = '{5}', uf_NoCertificadoSAT = '{6}', " +
                    "   uf_FechaHoraCerSAT = '{7}', uf_CBB = '{8}', " +
                    "   uf_Xml_Timbrado = '{9}', uf_Xml_Impresion = '{10}', GUID = '{11}' " +
                    "From CFDI_NM_XML_VP X (NoLock) " +
                    "Where IdEmisor = '{0}' and Serie = '{1}' and Folio = '{2}' and IdRegistro = '{12}' ",
                    DtIFacturacion.EmisorConectado,
                     ccfdi.serie, ccfdi.folio,
                     sCadenaOriginalSAT, "VistaPrevia", "VistaPrevia", "", Fg.Mid(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), 1, 19),
                     Fg.ConvertirArchivoEnBytes(ruta), sXmlTimbrado, XML_Impresion, sGUID, DtIFacturacion.EmisorIdRegistroPatronal);

                if (!leer.Exec(sSql))
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

        private bool GenerarTimbre(string ArchivoXml, string Referencia)
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
            clsGrabarError Error = new clsGrabarError(General.DatosConexion, DtIFacturacion.DatosApp, "clsGenCFDI_NM"); 


            bRegresa = clsTimbrar.Timbrar(ArchivoXml);
            if (!bRegresa)
            {
                sLastError = clsTimbrar.MensajeError;
            }
            else
            {
                sXmlTimbrado = clsTimbrar.XmlConTimbreFiscal; 
                bRegresa = info.loadInfo(sXmlTimbrado);
                sCadenaOriginalSAT = proccess.createOriginalStringByXMLString(sXmlTimbrado, this.rutaXSLT_Nomina());

                if (bRegresa)
                {
                    ruta = this.proccess.getTempFileName();
                    if (!this.main.uiCBB.getImgCBB(ccfdi.Emisor.rfc, ccfdi.Receptor.rfc, ccfdi.total.ToString(), info.UUID, ruta))
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

                        object[] obj2 = { info.FechaTimbrado.ToString("yyyy-MM-ddTHH:mm:ss"), info.UUID, info.noCertificadoSAT, info.selloCFD, info.selloSAT, info.version };
                        dtsTimbre.Tables["TimbreFiscal"].Rows.Add(obj2);
                        dtsTimbre.WriteXml(path_Aux, XmlWriteMode.WriteSchema);


                        XML_Impresion = Fg.ConvertirArchivoEnStringB64(path_Aux);
                        //////sXmlTimbrado = Fg.ConvertirStringB64FromString(sXmlTimbrado);
                        ogCFD.DeleteFile(path_Aux);


                        ////string sSql = string.Format(
                        ////    "Set DateFormat YMD Update X Set uf_CadenaOriginalSAT = '{5}', uf_SelloDigitalSAT = '{6}', " +
                        ////    "   uf_FolioSAT = '{7}', uf_NoCertificadoSAT = '{8}', " +
                        ////    "   uf_FechaHoraCerSAT = '{9}', uf_CBB = '{10}', " +
                        ////    "   uf_Xml_Timbrado = '{11}', uf_Xml_Impresion = '{12}' " +
                        ////    "From FACT_CFDI_XML X (NoLock) " +
                        ////    "Where IdEmpresa = '{0}' and IdEstado = '{1}' and IdFarmacia = '{2}' and Serie = '{3}' and Folio = '{4}' ",
                        ////    "DtGeneral.EmpresaConectada", "DtGeneral.EstadoConectado", "DtGeneral.FarmaciaConectada", 
                        ////    ccfdi.serie, ccfdi.folio, 
                        ////    sCadenaOriginalSAT, info.selloSAT, info.UUID, info.noCertificadoSAT,
                        ////    Fg.Mid(info.FechaTimbrado.ToString("yyyy-MM-ddTHH:mm:ss"), 1, 19),
                        ////    Fg.ConvertirArchivoEnBytes(ruta), sXmlTimbrado, XML_Impresion);

                        string sSql = string.Format(
                            "Set DateFormat YMD Update X Set uf_CadenaOriginalSAT = '{3}', uf_SelloDigitalSAT = '{4}', " +
                            "   uf_FolioSAT = '{5}', uf_NoCertificadoSAT = '{6}', " +
                            "   uf_FechaHoraCerSAT = '{7}', uf_CBB = '{8}', " +
                            "   uf_Xml_Timbrado = '{9}', uf_Xml_Impresion = '{10}', GUID = '{11}' " +
                            "From CFDI_NM_XML X (NoLock) " +
                            "Where IdEmisor = '{0}' and Serie = '{1}' and Folio = '{2}' and IdRegistro = '{12}'",
                            DtIFacturacion.EmisorConectado, 
                            ccfdi.serie, ccfdi.folio,
                            sCadenaOriginalSAT, info.selloSAT, info.UUID, info.noCertificadoSAT,
                            Fg.Mid(info.FechaTimbrado.ToString("yyyy-MM-ddTHH:mm:ss"), 1, 19),
                            Fg.ConvertirArchivoEnBytes(ruta), sXmlTimbrado, XML_Impresion, sGUID, DtIFacturacion.EmisorIdRegistroPatronal);

                        if (!leer.Exec(sSql))
                        {
                            bRegresa = false;
                            Error.GrabarError(leer, "GenerarTimbre");
                        }
                        else
                        {
                            bRegresa = true;
                            Guardar_XML_Timbrado(ccfdi.Receptor.rfc, ccfdi.serie, ccfdi.folio.ToString(), info.UUID); 
                        }
                    }
                    ogCFD.DeleteFile(ruta);
                }
            }

            return bRegresa; 
        }

        private void Guardar_XML_Timbrado(string RFC, string Serie, string Folio, string UUID)
        {
            basGenerales Fg = new basGenerales();
            string sFile = string.Format("{0}__{1}____{2}____{3}.xml", Serie, Fg.PonCeros(Folio, 10), RFC, UUID.ToUpper());
            string path = Path.Combine(DtIFacturacion.RutaCFDI_DocumentosGenerados, ccfdi.Emisor.rfc);

            Fg.ConvertirStringEnArchivo(sFile, path, clsTimbrar.XmlConTimbreFiscal, true); 
        }
    }
}

