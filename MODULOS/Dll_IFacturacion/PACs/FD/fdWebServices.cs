#region USING 
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;
using System.ServiceModel;
using System.ServiceModel.Channels; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
#endregion USING

#region USING WEB SERVICES 
using Dll_IFacturacion.PAC_fd_svrTFD; 
#endregion USING WEB SERVICES 

namespace Dll_IFacturacion.FD
{
    public class fdWebServices
    {
        internal enum TipoUrl
        {
            Receiver = 1, CancelCFCService = 2, CFDService = 3, DescargarDocumentos = 4
        }

        #region Declaracion de variables
        string sServidor = "";
        ////string xsaCancelCFDService = "http://xsa5.factura-e.biz/xsamanager/services/CancelCFDService.asmx";
        ////string xsaCFDService = "http://xsa5.factura-e.biz/xsamanager/services/CancelCFDService.asmx";
        ////string xsaFileReceiver = "http://xsa5.factura-e.biz/xsamanager/services/CancelCFDService.asmx";
        #endregion Declaracion de variables

        #region Constructor y Destructor de Clases
        public fdWebServices()
        {
        }

        public fdWebServices(string Servidor)
        {
            this.sServidor = Servidor;
        }

        private Binding GetBinding()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            int timeOutMinutos = 3;
            binding.Name = "soapHttpEndpoint";
            binding.Security.Mode = BasicHttpSecurityMode.None;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            binding.Security.Transport.Realm = "";
            binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            binding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;


            //binding.Name = "BasicHttpBinding_IService"; 
            binding.CloseTimeout = TimeSpan.FromMinutes(timeOutMinutos);
            binding.OpenTimeout = TimeSpan.FromMinutes(timeOutMinutos);
            binding.ReceiveTimeout = TimeSpan.FromMinutes(timeOutMinutos);
            binding.SendTimeout = TimeSpan.FromMinutes(timeOutMinutos);
            binding.AllowCookies = false;
            binding.BypassProxyOnLocal = false;
            binding.HostNameComparisonMode = System.ServiceModel.HostNameComparisonMode.StrongWildcard;
            binding.MessageEncoding = System.ServiceModel.WSMessageEncoding.Text;
            binding.TextEncoding = System.Text.Encoding.UTF8;
            binding.TransferMode = System.ServiceModel.TransferMode.Buffered;
            binding.UseDefaultWebProxy = true;


            binding.MaxBufferSize = int.MaxValue;
            binding.MaxBufferPoolSize = long.MaxValue;
            binding.MaxReceivedMessageSize = long.MaxValue;

            binding.MaxBufferSize = 2147483647;
            binding.MaxBufferPoolSize = 52428800;
            binding.MaxReceivedMessageSize = 2147483647;



            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            binding.ReaderQuotas.MaxDepth = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;

            binding.ReaderQuotas.MaxDepth = 32;
            binding.ReaderQuotas.MaxStringContentLength = 5242880;
            binding.ReaderQuotas.MaxArrayLength = 16384;
            binding.ReaderQuotas.MaxBytesPerRead = 4096;
            binding.ReaderQuotas.MaxNameTableCharCount = 16384;

            return binding; 
        }

        private EndpointAddress GetAddress(string Url)
        {
            Uri serviceUri = new Uri(Url);
            EndpointAddress endpointAddress = new EndpointAddress(serviceUri);
            return endpointAddress;
        }
        #endregion Constructor y Destructor de Clases

        #region Generar Urls
        public string Servidor
        {
            get { return sServidor; }
            set { sServidor = value; }
        }

        public string Receiver
        {
            get { return GetUrl(TipoUrl.Receiver); }
        }

        public string CancelCFD
        {
            get { return GetUrl(TipoUrl.CancelCFCService); }
        }

        public string CFDService
        {
            get { return GetUrl(TipoUrl.CFDService); }
        }

        public string DescargarDocumentos
        {
            get { return GetUrl(TipoUrl.DescargarDocumentos); }
        }

        private string GetUrl(TipoUrl Tipo)
        {
            string sRegresa = "";
            ////////En la Url se especifica si es Http ó Https 
            sRegresa = string.Format("{0}/ws-folios/WS-TFD.asmx", sServidor);
            sRegresa = string.Format("{0}", sServidor);

            ////switch (Tipo) 
            ////{
            ////    case TipoUrl.Receiver:
            ////        sRegresa = string.Format("{0}/ws-folios/TimbrarCFD", sServidor);
            ////        break;

            ////    case TipoUrl.CancelCFCService:
            ////        sRegresa = string.Format("{0}/ws-folios/CancelarCFDI", sServidor);
            ////        break;

            ////    case TipoUrl.CFDService:
            ////        sRegresa = string.Format("{0}/ws-folios/TimbrarCFD", sServidor);
            ////        break;


            ////    case TipoUrl.DescargarDocumentos:
            ////        sRegresa = string.Format("{0}/xsamanager/downloadCfdWebView", sServidor);
            ////        break;

            ////    default:
            ////        sRegresa = "";
            ////        break;
            ////}

            return sRegresa;
        }
        #endregion Generar Urls

        #region Web Services (WCF)
        public soapHttpEndpointHttps GetReceiver()
        {
            Binding binding = GetBinding();
            EndpointAddress address = GetAddress(this.Receiver);

            soapHttpEndpointHttps recepcion = new soapHttpEndpointHttps();
            recepcion.Url = address.Uri.ToString();
            return recepcion;
        }


        ////public CancelCFDServicePortTypeClient GetCancelCFD()
        ////{
        ////    Binding binding = GetBinding();
        ////    EndpointAddress address = GetAddress(this.CancelCFD);

        ////    CancelCFDServicePortTypeClient cancelacion = new CancelCFDServicePortTypeClient(binding, address);
        ////    return cancelacion;
        ////}

        ////public CFDServicePortTypeClient GetCFDService()
        ////{
        ////    Binding binding = GetBinding();
        ////    EndpointAddress address = GetAddress(this.CFDService);

        ////    CFDServicePortTypeClient cfd = new CFDServicePortTypeClient(binding, address);
        ////    return cfd;
        ////}
        #endregion Web Services (WCF)
    }
}
