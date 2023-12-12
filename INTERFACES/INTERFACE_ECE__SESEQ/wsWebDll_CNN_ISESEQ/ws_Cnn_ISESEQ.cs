using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using Dll_INT_CNNSESEQ; 

namespace wsWebDll_CNN_ISESEQ
{
    [WebService(Description = "Módulo Interface de Comunicación", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class ws_Cnn_ISESEQ : Dll_INT_CNNSESEQ.ws_Cnn_ISESEQ 
    {
    }
}