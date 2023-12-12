#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Reflection;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Data.Odbc;
using System.ServiceModel;
using System.ServiceModel.Channels;


using Dll_MA_IFacturacion;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
#endregion USING

#region USING WEB SERVICES
using Dll_MA_IFacturacion.PAC_fd_svrTFD;
#endregion USING WEB SERVICES 

namespace Dll_MA_IFacturacion.PACs.FD
{
    public enum eTipoMensajes
    {
        Ninguno = 0, Timbrado = 1, Cancelacion = 2, Invocacion_Servicio__FoliosDigitales = 3, 
        DescargaXML = 4 
    }

    public enum eMensaje
    {
        Ninguno = 0, 
        T301 = 301, 
        T302 = 302, 
        T303 = 303, 
        T304 = 304, 
        T305 = 305, 
        T306 = 306, 
        T307 = 307,
        T308 = 308,
        T401 = 401, 
        T402 = 402, 
        T403 = 403, 
 
        C201 = 201,
        C202 = 202,
        C203 = 203,
        C204 = 204,
        C205 = 205, 
 
        I801 = 801, 
        I805 = 805,
        I806 = 806,
        I807 = 807,
        I808 = 808,
        I809 = 809,
        I811 = 811,
        I815 = 815,
        I816 = 816,
        I817 = 817,
        I818 = 818, 

        S901 = 901 
    }

    public class FoliosDigitalesMensajes 
    {
        eTipoMensajes tpTipo = eTipoMensajes.Ninguno;
        eMensaje eCodigo = eMensaje.Ninguno; 
        string sMensaje = "";
        string sDescripcion = "";

        public FoliosDigitalesMensajes(eTipoMensajes Tipo, eMensaje Codigo, string Mensaje, string Descripcion)
        {
            this.tpTipo = Tipo; 
            this.eCodigo = Codigo; 
            this.sMensaje = Mensaje; 
            this.sDescripcion = Descripcion;  
        }

        public eTipoMensajes Tipo
        {
            get { return tpTipo; }
        }

        public eMensaje Codigo      
        {
            get { return eCodigo; }
        }

        public string Mensaje
        {
            get { return sMensaje; }
        }

        public string Descripcion
        {
            get { return sDescripcion; }
        }
    }

    public class fdGenerarDocumento
    {
        #region Declaracion de variables 
        clsDatosConexion datosDeConexion;
        clsGrabarError Error;
        basGenerales Fg = new basGenerales();
        int iNumIntentosTimbrado = 120;
        string sMensajeDeError = "";
        bool bOcurrioError_AlGenerar = false;

        bool bEnProduccion = DtIFacturacion.PAC_Informacion.EnProduccion;
        string sUrlTimbrado = DtIFacturacion.PAC_Informacion.Url;
        string sUsuarioTimbrado = DtIFacturacion.PAC_Informacion.Usuario;
        string sPasswordTimbrado = DtIFacturacion.PAC_Informacion.Password;
        static string sRFC_Emisor = "";

        string sCertificadoPKCS12_Base64 = DtIFacturacion.PAC_Informacion.CertificadoPKCS12;
        string sPasswordPKCS12 = DtIFacturacion.PAC_Informacion.PasswordPKCS12;

        string sXmlConTimbreFiscal = "";
        string sXmlAcuseCancelacion = "";
        int iCreditos_Disponibles = 0; 
        
        
        static FoliosDigitalesMensajes resultadoProceso = new FoliosDigitalesMensajes(eTipoMensajes.Ninguno, eMensaje.Ninguno, "", ""); 

        static Dictionary<eMensaje, FoliosDigitalesMensajes> listaMensajes; 

        //static psecfdi.FoliosDigitales.PAC_FoliosDigitales.WS_FD_TFDSoapClient PAC; 

        #endregion Declaracion de variables

        #region Constructor y Destructor de Clase 
        public fdGenerarDocumento(clsDatosConexion Conexion)
        {
            datosDeConexion = Conexion;

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(AcceptAllCertificatePolicy);

            CargarListaMensajes();

            Error = new clsGrabarError(datosDeConexion, DtIFacturacion.DatosApp, "fdGenerarDocumento");
            Error.NombreLogErorres = "fd_CtlErrores";


            if (!bEnProduccion)
            {
                //////sUsuarioTimbrado = "WSDL_PAX";
                //////sPasswordTimbrado = "wqfCr8O3xLfEhMOHw4nEjMSrxJnvv7bvvr4cVcKuKkBEM++/ke+/gCPvv4nvvrfvvaDvvb/vvqTvvoA=";
            }
        }

        private bool AcceptAllCertificatePolicy(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        #endregion Constructor y Destructor de Clase

        #region Propiedades 
        public bool Error_AlGenerar
        {
            get { return bOcurrioError_AlGenerar; }
        }

        public string MensajeError 
        {
            get 
            {
                string sMsj = "";
                if (bOcurrioError_AlGenerar)
                {
                    sMsj = sMensajeDeError; 
                }
                return sMsj; 
            }
        }

        public string RFC_Emisor
        {
            get { return sRFC_Emisor; }
            set { sRFC_Emisor = value; }
        }

        public string XmlConTimbreFiscal
        {
            get { return sXmlConTimbreFiscal; }
        }

        public string XmlAcuseCancelacion
        {
            get { return sXmlAcuseCancelacion; }
        }

        public int Creditos_Disponibles
        {
            get { return iCreditos_Disponibles; }
        }

        public FoliosDigitalesMensajes Resultado
        {
            get { return resultadoProceso; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos 
        public bool GenerarDocumento(string CadenaXML, string Referencia)
        {
            bool bRegresa = false;

            bRegresa = timbrarCFDI(CadenaXML, Referencia);

            ////if (bEnProduccion)
            ////{
            ////    bRegresa = timbrarCFD(CadenaXML, Referencia);
            ////}
            ////else
            ////{
            ////    bRegresa = timbrarPruebaCFDI(CadenaXML); 
            ////}

            return bRegresa;
        }

        private bool timbrarCFDI(string CadenaXML, string Referencia)
        {
            bool bRegresa = false;
            fdWebServices xsa = new fdWebServices(sUrlTimbrado);
            soapHttpEndpointHttps Timbrar;
            RespuestaTFD33 Respuesta = new RespuestaTFD33();

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                CredentialCache cache = new CredentialCache();

                ////ResetRespuesta();

                //if (bEnProduccion)
                {
                    Timbrar = xsa.GetReceiver();
                    //Timbrar.Endpoint.Binding.SendTimeout = new TimeSpan(0, 2, 30);

                    Respuesta = Timbrar.TimbrarCFDI(sUsuarioTimbrado, sPasswordTimbrado, CadenaXML, Referencia);
                    ////bRegresa = validarRespuestaTimbrado(Respuesta); 
                }


                if (!Respuesta.OperacionExitosa)
                {
                    bOcurrioError_AlGenerar = true;
                    sMensajeDeError = Respuesta.MensajeError;
                }
                else
                {
                    bRegresa = true;
                    sXmlConTimbreFiscal = Respuesta.XMLResultado;
                }
            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message);
                sMensajeDeError = ex.Message;
                bOcurrioError_AlGenerar = true;
            }

            if (bOcurrioError_AlGenerar)
            {
                Error.GrabarError(sMensajeDeError, "GenerarDocumento()");
            }

            return bRegresa;
        }

        ////private bool timbrarPruebaCFDI(string CadenaXML)
        ////{
        ////    bool bRegresa = false;
        ////    fdWebServices xsa = new fdWebServices(sUrlTimbrado);
        ////    WS_FD_TFDSoapClient Timbrar; 
        ////    ArrayOfString Respuesta = new ArrayOfString();

        ////    try
        ////    {
        ////        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
        ////        CredentialCache cache = new CredentialCache();

        ////        Timbrar = xsa.GetReceiver();
        ////        Timbrar.Endpoint.Binding.SendTimeout = new TimeSpan(0, 2, 30); 
        ////        ResetRespuesta();
                
        ////        Respuesta = Timbrar.TimbrarPruebaCFDI(sUsuarioTimbrado, sPasswordTimbrado, CadenaXML);
        ////        bRegresa = validarRespuestaTimbrado(Respuesta);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        clsGrabarError.LogFileError(ex.Message);
        ////        sMensajeDeError = ex.Message;
        ////        bOcurrioError_AlGenerar = true;
        ////    }

        ////    if (bOcurrioError_AlGenerar)
        ////    {
        ////        Error.GrabarError(sMensajeDeError, "GenerarDocumento()");
        ////    }

        ////    return bRegresa;
        ////}

        public bool CancelarDocumento(string CFDI)
        {
            string[] listaCFDI = { CFDI };

            return CancelarDocumento(listaCFDI);
        }

        public bool CancelarDocumento(string[] ListaCFDI)
        {
            //ArrayOfString listaCFDI = new ArrayOfString();
            List<string> listaCFDI = new List<string>();

            foreach (string s in ListaCFDI)
            {
                listaCFDI.Add(s);
            }

            return CancelarDocumento(listaCFDI);
        }

        public bool CancelarDocumento(List<string> ListaCFDI)
        {
            bool bRegresa = false;

            fdWebServices xsa = new fdWebServices(sUrlTimbrado);
            soapHttpEndpointHttps cancelar;
            RespuestaCancelacion Respuesta = new RespuestaCancelacion();
            List<DetalleCancelacion> RespuestaCancelacionDetallada = new List<DetalleCancelacion>();

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                CredentialCache cache = new CredentialCache();

                cancelar = xsa.GetReceiver();
                //cancelar.Endpoint.Binding.SendTimeout = new TimeSpan(0, 2, 30); 
                ////ResetRespuesta();

                Respuesta = cancelar.CancelarCFDI(sUsuarioTimbrado, sPasswordTimbrado, sRFC_Emisor, ListaCFDI.ToArray(), sCertificadoPKCS12_Base64, sPasswordPKCS12);
                ////bRegresa = validarRespuestaCancelacion(Respuesta);

                if (!Respuesta.OperacionExitosaSpecified)
                {
                    bOcurrioError_AlGenerar = true;
                    sMensajeDeError = Respuesta.MensajeError + "\n\n";
                    sMensajeDeError += string.Format("Código :  {0} \n", Respuesta.DetallesCancelacion[0].CodigoResultado);
                    sMensajeDeError += string.Format("UUID :  {0} \n", Respuesta.DetallesCancelacion[0].UUID);
                    sMensajeDeError += string.Format("Mensaje :  {0} \n", Respuesta.DetallesCancelacion[0].MensajeResultado); 
                }
                else
                {
                    bRegresa = true;
                    sXmlAcuseCancelacion = Respuesta.XMLAcuse;
                }
            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message);
                sMensajeDeError = ex.Message;
                bOcurrioError_AlGenerar = true;
            }

            return bRegresa;
        }

        public bool ObtenerXML(string UUID)
        {
            bool bRegresa = false;
            //fdWebServices xsa = new fdWebServices(sUrlTimbrado);
            //WS_FD_TFDSoapClient descargarXML;
            //ArrayOfString Respuesta = new ArrayOfString();

            //try
            //{
            //    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
            //    CredentialCache cache = new CredentialCache();

            //    descargarXML = xsa.GetReceiver();
            //    descargarXML.Endpoint.Binding.SendTimeout = new TimeSpan(0, 2, 30);
            //    ResetRespuesta();

            //    Respuesta = descargarXML.ObtenerXML(sUsuarioTimbrado, sPasswordTimbrado, UUID, sRFC_Emisor);
            //    bRegresa = validarRespuestaDescargarXML(Respuesta); 

            //}
            //catch (Exception ex)
            //{
            //    clsGrabarError.LogFileError(ex.Message);
            //    sMensajeDeError = ex.Message;
            //    bOcurrioError_AlGenerar = true;
            //}

            return bRegresa;
        }

        public bool ConsultarCreditos()
        {
            PAC_Info RFC_Emisor = new PAC_Info();
            RFC_Emisor.Usuario = sUsuarioTimbrado;
            RFC_Emisor.Password = sPasswordTimbrado; 
            RFC_Emisor.Url = sUrlTimbrado;

            return ConsultarCreditos(RFC_Emisor); 
        }

        public bool ConsultarCreditos(PAC_Info RFC_Emisor)
        {
            bool bRegresa = false;
            fdWebServices xsa = new fdWebServices(RFC_Emisor.Url);
            soapHttpEndpointHttps Timbrar;

            RespuestaCreditos Respuesta = new RespuestaCreditos();
            List<DetallesPaqueteCreditos> RespuestaDetalleCreditos = new List<DetallesPaqueteCreditos>();
            iCreditos_Disponibles = 0;

            try
            {
                bOcurrioError_AlGenerar = false;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                CredentialCache cache = new CredentialCache();


                //if (bEnProduccion)
                {
                    bRegresa = bEnProduccion;
                    bRegresa = false;
                    Timbrar = xsa.GetReceiver();
                    //Timbrar.Endpoint.Binding.SendTimeout = new TimeSpan(0, 2, 30);

                    Respuesta = Timbrar.ConsultarCreditos(RFC_Emisor.Usuario, RFC_Emisor.Password);
                    ////bRegresa = validarRespuestaConsultaCreditos(Respuesta);
                }

                if (Respuesta.OperacionExitosa)
                {
                    bRegresa = true;
                    //RespuestaDetalleCreditos = Respuesta.Paquetes();
                    foreach (DetallesPaqueteCreditos Paquete in Respuesta.Paquetes)
                    {
                        iCreditos_Disponibles += Paquete.TimbresRestantes;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGrabarError.LogFileError(ex.Message);
                sMensajeDeError = ex.Message;
                bOcurrioError_AlGenerar = true;
            }

            if (bOcurrioError_AlGenerar)
            {
                Error.GrabarError(sMensajeDeError, "GenerarDocumento()");
            }

            return bRegresa;
        }

        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados 
        public bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private string toUTF8(string Cadena)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(Cadena);
            return encoding.GetString(bytes);
        }

        private void ResetRespuesta()
        {
            sXmlConTimbreFiscal = "";
            sXmlAcuseCancelacion = "";
            iCreditos_Disponibles = 0; 
            resultadoProceso = new FoliosDigitalesMensajes(eTipoMensajes.Ninguno, eMensaje.Ninguno, "", "");
        }

        ////private bool validarRespuestaTimbrado(ArrayOfString Respuesta)
        ////{
        ////    bool bRegresa = true;
        ////    int iMsj = 0;
        ////    sXmlConTimbreFiscal = "";
        ////    sXmlAcuseCancelacion = ""; 

        ////    if ( Respuesta[0].ToString() != "" ) 
        ////    {
        ////        try 
        ////        {
        ////            iMsj = Convert.ToInt32("0" + Respuesta[0]);
        ////        }catch{}
        ////        bRegresa = false; 
        ////    }

        ////    if (bRegresa && Respuesta[1].ToString() != "")
        ////    {
        ////        bRegresa = false;
        ////    }

        ////    if (bRegresa && Respuesta[2].ToString() != "")
        ////    {
        ////        bRegresa = false;
        ////    }

        ////    if (!bRegresa)
        ////    {
        ////        bOcurrioError_AlGenerar = true;

        ////        eMensaje msj = (eMensaje)iMsj; 
        ////        if (listaMensajes.ContainsKey(msj))
        ////        {
        ////            resultadoProceso = (FoliosDigitalesMensajes)listaMensajes[msj];
        ////            sMensajeDeError = resultadoProceso.Descripcion;
        ////        }
        ////    }
        ////    else
        ////    {
        ////        sXmlConTimbreFiscal = Respuesta[3];
        ////    }

        ////    bOcurrioError_AlGenerar = !bRegresa; 
        ////    return bRegresa; 
        ////}

        ////private bool validarRespuestaCancelacion(ArrayOfString Respuesta)
        ////{
        ////    bool bRegresa = true;
        ////    int iMsj = 0;
        ////    string sMsj = "";
        ////    string sResultado = "";
        ////    sXmlConTimbreFiscal = "";
        ////    sXmlAcuseCancelacion = "";

        ////    if (Respuesta[0].ToString() != "")
        ////    {
        ////        try
        ////        {
        ////            sMsj = Respuesta[0];
        ////            string[] sIdMsj = sMsj.Split('|');

        ////            iMsj = Convert.ToInt32(sIdMsj[0]);
        ////        }
        ////        catch { }
        ////        bRegresa = false;
        ////    }

        ////    if (bRegresa && Respuesta[1].ToString() != "")
        ////    {
        ////        bRegresa = false;
        ////        ////sMsj = Respuesta[1].ToUpper();

        ////        ////if (sMsj.ToUpper().Contains(""))
        ////        ////{
        ////        ////    bRegresa = true; 
        ////        ////}

        ////        sMsj = Respuesta[1];
        ////        string[] sIdMsj = sMsj.Split('|');
        ////        iMsj = Convert.ToInt32(sIdMsj[1]);

        ////        if (((eMensaje)iMsj) == eMensaje.C201 | ((eMensaje)iMsj) == eMensaje.C202)
        ////        {
        ////            bRegresa = true; 
        ////            ////sXmlAcuseCancelacion = Respuesta[2];
        ////        }
        ////    }

        ////    ////if (bRegresa && Respuesta[3].ToString() != "")
        ////    ////{
        ////    ////    bRegresa = false;
        ////    ////}

        ////    if (!bRegresa)
        ////    {
        ////        eMensaje msj = (eMensaje)iMsj;
        ////        if (listaMensajes.ContainsKey(msj))
        ////        {
        ////            resultadoProceso = (FoliosDigitalesMensajes)listaMensajes[msj];
        ////        }
        ////    }
        ////    else
        ////    {
        ////        sXmlAcuseCancelacion = Respuesta[2];
        ////    }


        ////    bOcurrioError_AlGenerar = !bRegresa; 
        ////    return bRegresa;
        ////}

        ////private bool validarRespuestaConsultaCreditos(ArrayOfString Respuesta)
        ////{
        ////    bool bRegresa = true;
        ////    int iMsj = 0;
        ////    string sMsj = "";
        ////    sXmlConTimbreFiscal = "";
        ////    sXmlAcuseCancelacion = "";

        ////    if (Respuesta[2].ToString() == "")
        ////    {
        ////        bRegresa = false;
        ////    }

        ////    if (bRegresa && Respuesta[2].ToString() != "")
        ////    {
        ////        try
        ////        {
        ////            iCreditos_Disponibles = Convert.ToInt32("0" + Respuesta[2]);
        ////        }
        ////        catch 
        ////        {
        ////            bRegresa = false;
        ////        }
        ////    }

        ////    if (!bRegresa)
        ////    {
        ////        eMensaje msj = (eMensaje)iMsj;
        ////        if (listaMensajes.ContainsKey(msj))
        ////        {
        ////            resultadoProceso = (FoliosDigitalesMensajes)listaMensajes[msj];
        ////        }
        ////    }

        ////    return bRegresa;
        ////}

        ////private bool validarRespuestaDescargarXML(ArrayOfString Respuesta)
        ////{
        ////    bool bRegresa = true;
        ////    int iMsj = 0;
        ////    string sMsj = "";
        ////    sXmlConTimbreFiscal = "";
        ////    sXmlAcuseCancelacion = "";

        ////    if (Respuesta[0].ToString() == "")
        ////    {
        ////        bRegresa = false;
        ////    }

        ////    if (bRegresa && Respuesta[0].ToString() != "")
        ////    {
        ////        try
        ////        {
        ////            if (bRegresa && Respuesta[3].ToString() != "")
        ////            {
        ////                sXmlConTimbreFiscal = Respuesta[3];
        ////                bRegresa = true;
        ////            }
        ////        }
        ////        catch
        ////        {
        ////            bRegresa = false;
        ////        }
        ////    }


        ////    if (!bRegresa)
        ////    {
        ////        eMensaje msj = eMensaje.S901;
        ////        if (listaMensajes.ContainsKey(msj))
        ////        {
        ////            resultadoProceso = (FoliosDigitalesMensajes)listaMensajes[msj];
        ////        }
        ////    }

        ////    return bRegresa;
        ////}

        private void CargarListaMensajes()
        {
            listaMensajes = new Dictionary<eMensaje, FoliosDigitalesMensajes>();

            listaMensajes.Add(eMensaje.Ninguno, new FoliosDigitalesMensajes(eTipoMensajes.Ninguno, eMensaje.Ninguno, "", ""));        

            listaMensajes.Add(eMensaje.C201, new FoliosDigitalesMensajes(eTipoMensajes.Cancelacion, eMensaje.C201, "UUID Cancelado.", "Cancelación existosa ante el SAT."));
            listaMensajes.Add(eMensaje.C202, new FoliosDigitalesMensajes(eTipoMensajes.Cancelacion, eMensaje.C202, "UUID Previamente cancelado.", "El UUID ya está cancelado en los registros del SAT."));
            listaMensajes.Add(eMensaje.C203, new FoliosDigitalesMensajes(eTipoMensajes.Cancelacion, eMensaje.C203, "UUID no corresponde al emisor.", "El UUID enviado para cancelar no corresponde al RFC de emisor enviado."));
            listaMensajes.Add(eMensaje.C204, new FoliosDigitalesMensajes(eTipoMensajes.Cancelacion, eMensaje.C204, "UUID no aplicable para cancelación.", "El UUID no se registro correctamente ante el SAT."));
            listaMensajes.Add(eMensaje.C205, new FoliosDigitalesMensajes(eTipoMensajes.Cancelacion, eMensaje.C205, "UUID no existe.", "El UUID no existe en los registros del SAT."));

            listaMensajes.Add(eMensaje.T301, new FoliosDigitalesMensajes(eTipoMensajes.Timbrado, eMensaje.T301, "XML mal formado.", "El XML recibido no cumple con los estandares del SAT.")); 
            listaMensajes.Add(eMensaje.T302, new FoliosDigitalesMensajes(eTipoMensajes.Timbrado, eMensaje.T302, "Sello mal formado o inválido.", "El sello que contiene el XML se generó de manera incorrecta."));
            listaMensajes.Add(eMensaje.T303, new FoliosDigitalesMensajes(eTipoMensajes.Timbrado, eMensaje.T303, "Sello no corresponde a emisor o caduco.", "El XML se selló con un CSD que no corresponde al RFC del emisor."));
            listaMensajes.Add(eMensaje.T304, new FoliosDigitalesMensajes(eTipoMensajes.Timbrado, eMensaje.T304, "Certificado revocado o caducado.", "El XML se selló con un CSD que se encuentra con revocado en la LCO o ya caducó su vigencia."));
            listaMensajes.Add(eMensaje.T305, new FoliosDigitalesMensajes(eTipoMensajes.Timbrado, eMensaje.T305, "La fecha de emisión no está dentro de la vigencia del CSD del emisor.", "El XML se genero con una fecha fuera del rango de vigencia del CSD según LCO."));
            listaMensajes.Add(eMensaje.T306, new FoliosDigitalesMensajes(eTipoMensajes.Timbrado, eMensaje.T306, "El certificado no es del tipo CSD.", "El XML se selló con la FIEL."));
            listaMensajes.Add(eMensaje.T307, new FoliosDigitalesMensajes(eTipoMensajes.Timbrado, eMensaje.T307, "El CFDI contiene un timbre previo.", "El XML ya contiene el complemento Timbre Fiscal Digital."));
            listaMensajes.Add(eMensaje.T308, new FoliosDigitalesMensajes(eTipoMensajes.Timbrado, eMensaje.T308, "Certificado no expedido por el SAT.", "El XML se selló con un certificado no emitido por el SAT."));
            listaMensajes.Add(eMensaje.T401, new FoliosDigitalesMensajes(eTipoMensajes.Timbrado, eMensaje.T401, "Fecha y hora de generación fuera de rango.", "El XML se genero antes de 72 horas o en una fecha/hora posterior a la actual."));
            listaMensajes.Add(eMensaje.T402, new FoliosDigitalesMensajes(eTipoMensajes.Timbrado, eMensaje.T402, "RFC del emisor no se encuentra en el régimen de contribuyentes.", "El RFC del emisor no se encuentra en la LCO."));
            listaMensajes.Add(eMensaje.T403, new FoliosDigitalesMensajes(eTipoMensajes.Timbrado, eMensaje.T403, "La fecha de emisión no es posterior al 01 de enero 2011.", "La fecha de generación del XML es menor al 1 de Enero de 2011."));

            listaMensajes.Add(eMensaje.I801, new FoliosDigitalesMensajes(eTipoMensajes.Invocacion_Servicio__FoliosDigitales, eMensaje.I801, "El comprobante ya fue timbrado por FD.", "XML timbrado previamente por Folios Digitales."));
            listaMensajes.Add(eMensaje.I805, new FoliosDigitalesMensajes(eTipoMensajes.Invocacion_Servicio__FoliosDigitales, eMensaje.I805, "El comprobante contiene el nodo Addenda", "El XML contiene el nodo Addenda. No se debe timbrar un XML con Addenda, pero se puede agregar a su XML después del timbrado."));
            listaMensajes.Add(eMensaje.I806, new FoliosDigitalesMensajes(eTipoMensajes.Invocacion_Servicio__FoliosDigitales, eMensaje.I806, "Error genérico de invocación de servicio.", "Alguno de los datos enviados para accesar al servicio es incorrecto."));
            listaMensajes.Add(eMensaje.I807, new FoliosDigitalesMensajes(eTipoMensajes.Invocacion_Servicio__FoliosDigitales, eMensaje.I807, "Error de autenticación de usuario.", "El usuario no existe o esta mal la contraseña."));
            listaMensajes.Add(eMensaje.I808, new FoliosDigitalesMensajes(eTipoMensajes.Invocacion_Servicio__FoliosDigitales, eMensaje.I808, "El usuario no cuenta con permiso de acceso.", "El usuario existe, pero se le revocó el permiso de acceso por uso indebido del servicio."));
            listaMensajes.Add(eMensaje.I809, new FoliosDigitalesMensajes(eTipoMensajes.Invocacion_Servicio__FoliosDigitales, eMensaje.I809, "El paquete de timbres ha expirado.", "El paquete de timbres adquirido ya expiro."));
            listaMensajes.Add(eMensaje.I811, new FoliosDigitalesMensajes(eTipoMensajes.Invocacion_Servicio__FoliosDigitales, eMensaje.I811, "El RFC del usuario no corresponde al del emisor de CFDI.", "Se esta tratando de timbrar un XML de otro contribuyente."));
            listaMensajes.Add(eMensaje.I815, new FoliosDigitalesMensajes(eTipoMensajes.Invocacion_Servicio__FoliosDigitales, eMensaje.I815, "Ha alcanzado el limite de intentos de autenticación. Intente despues de 30 minutos.", "Despues de 3 intentos fallidos, se bloqueará el usuario por 30 minutos."));
            listaMensajes.Add(eMensaje.I816, new FoliosDigitalesMensajes(eTipoMensajes.Invocacion_Servicio__FoliosDigitales, eMensaje.I816, "No se pudo realizar el envió al SAT.", "Ocurrió un error al tratar de accesar el servicio del SAT para la entrega del CFDI."));
            listaMensajes.Add(eMensaje.I817, new FoliosDigitalesMensajes(eTipoMensajes.Invocacion_Servicio__FoliosDigitales, eMensaje.I817, "Se excedió el número de UUIDs a cancelar.", "Sólo se pueden cancelar un máximo de 500 facturas por petición."));
            listaMensajes.Add(eMensaje.I818, new FoliosDigitalesMensajes(eTipoMensajes.Invocacion_Servicio__FoliosDigitales, eMensaje.I818, "El CSD no existe en la LCO.", "El CSD del emisor aún no se encuentra en la LCO del SAT."));

            listaMensajes.Add(eMensaje.S901, new FoliosDigitalesMensajes(eTipoMensajes.DescargaXML, eMensaje.S901, "Error al descargar el XML", "Error al descargar el XML"));

        }
        #endregion Funciones y Procedimientos Privados
    }
}
