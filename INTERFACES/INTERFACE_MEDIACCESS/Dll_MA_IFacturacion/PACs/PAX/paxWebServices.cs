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
using Dll_MA_IFacturacion.PAC_pax_svrRecepcionService;
using Dll_MA_IFacturacion.PAC_pax_svrCancelacionService;

using Dll_MA_IFacturacion.PAC_pax_test_svrRecepcionService;
using Dll_MA_IFacturacion.PAC_pax_test_svrCancelacionService;  
#endregion USING WEB SERVICES 

namespace Dll_MA_IFacturacion.PACs.PAX
{
    public class paxWebServices
    {
        internal enum TipoUrl
        {
            Receiver = 1, CancelCFCService = 2, CFDService = 3, DescargarDocumentos = 4
        }

        #region Declaracion de variables
        string sServidor = "";
        ////string xsaCancelCFDService = "https://www.paxfacturacion.com.mx:453/webservices/wcfCancelaasmx.asmx"; 
        ////string xsaFileReceiver = "https://www.paxfacturacion.com.mx:453/webservices/wcfRecepcionasmx.asmx"; 
        #endregion Declaracion de variables

        #region Constructor y Destructor de Clases
        public paxWebServices()
        {
        }

        public paxWebServices(string Servidor)
        {
            this.sServidor = Servidor;
        }

        private Binding GetBinding(string ServiceName)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            //binding.Namespace = this.Receiver; 
            binding.Name = ServiceName;
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

        #region Web Services (WCF)
        public PAC_pax_svrRecepcionService.wcfRecepcionASMXSoapClient GetReceiver()
        {
            Binding binding = GetBinding("wcfRecepcionASMXSoap");
            EndpointAddress address = GetAddress(this.Receiver);

            PAC_pax_svrRecepcionService.wcfRecepcionASMXSoapClient recepcion = new PAC_pax_svrRecepcionService.wcfRecepcionASMXSoapClient(binding, address);

            return recepcion;
        }

        public PAC_pax_test_svrRecepcionService.wcfRecepcionASMXSoapClient GetReceiverTest()
        {
            Binding binding = GetBinding("wcfRecepcionASMXSoap1");
            EndpointAddress address = GetAddress(this.Receiver);

            PAC_pax_test_svrRecepcionService.wcfRecepcionASMXSoapClient recepcion = new PAC_pax_test_svrRecepcionService.wcfRecepcionASMXSoapClient(binding, address);

            return recepcion;
        }

        public PAC_pax_svrCancelacionService.wcfCancelaASMXSoapClient GetCancelCFD()
        {
            Binding binding = GetBinding("wcfCancelaASMXSoap");
            EndpointAddress address = GetAddress(this.CancelCFD);

            PAC_pax_svrCancelacionService.wcfCancelaASMXSoapClient cancelacion = new PAC_pax_svrCancelacionService.wcfCancelaASMXSoapClient(binding, address);

            return cancelacion;
        }

        public PAC_pax_test_svrCancelacionService.wcfCancelaASMXSoapClient GetCancelCFDTest()
        {
            Binding binding = GetBinding("wcfCancelaASMXSoap1");
            EndpointAddress address = GetAddress(this.CancelCFD);

            PAC_pax_test_svrCancelacionService.wcfCancelaASMXSoapClient cancelacion = new PAC_pax_test_svrCancelacionService.wcfCancelaASMXSoapClient(binding, address);

            return cancelacion;
        }
        #endregion Web Services (WCF)

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

        //////public string CFDService
        //////{
        //////    get { return GetUrl(TipoUrl.CFDService); }
        //////}

        //////public string DescargarDocumentos
        //////{
        //////    get { return GetUrl(TipoUrl.DescargarDocumentos); }
        //////}

        private string GetUrl(TipoUrl Tipo)
        {
            string sRegresa = "";
            ////////En la Url se especifica si es Http ó Https   

            switch (Tipo)
            {
                case TipoUrl.Receiver:
                    sRegresa = string.Format("{0}/webservices/wcfRecepcionasmx.asmx", sServidor);
                    sRegresa = string.Format("{0}/webservices/wcfRecepcionASMX.asmx", sServidor);
                    break;

                case TipoUrl.CancelCFCService:
                    sRegresa = string.Format("{0}/webservices/wcfCancelaasmx.asmx", sServidor);
                    break;


                ////case TipoUrl.DescargarDocumentos:
                ////    sRegresa = string.Format("{0}/xsamanager/downloadCfdWebView", sServidor);
                ////    break; 

                default:
                    sRegresa = "";
                    break;
            }

            return sRegresa;
        }
        #endregion Generar Urls
    }
}