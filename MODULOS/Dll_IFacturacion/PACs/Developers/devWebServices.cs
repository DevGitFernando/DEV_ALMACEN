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
using Dll_IFacturacion.PAC_Dev33;
#endregion USING WEB SERVICES

namespace Dll_IFacturacion.VirtualSoft
{
    public class devWebServices
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
        public devWebServices()
        {
        }

        public devWebServices(string Servidor)
        {
            this.sServidor = Servidor;
        }

        private Binding GetBinding()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            //binding.Name = "wcfRecepcionASMXSoap";
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            binding.UseDefaultWebProxy = true;

            binding.MaxBufferSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;

            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
            binding.ReaderQuotas.MaxDepth = int.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
            binding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;

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
        public WSTimbradoCFDIService GetReceiver()
        {
            Binding binding = GetBinding();
            EndpointAddress address = GetAddress(this.Receiver);

            WSTimbradoCFDIService recepcion = new WSTimbradoCFDIService();
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
