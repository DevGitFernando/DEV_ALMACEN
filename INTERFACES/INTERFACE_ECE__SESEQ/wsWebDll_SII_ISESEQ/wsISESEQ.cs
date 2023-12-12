using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using Dll_ISESEQ; 

namespace wsWebDll_SII_ISESEQ
{
    [WebService(Description = "Módulo Interface de Comunicación", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsISESEQ : Dll_ISESEQ.wsISESEQ 
    {
    }
}