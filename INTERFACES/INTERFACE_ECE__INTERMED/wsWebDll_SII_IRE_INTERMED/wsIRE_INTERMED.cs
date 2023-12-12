using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using Dll_IRE_INTERMED; 

namespace wsWebDll_SII_IRE_INTERMED
{
    [WebService(Description = "Módulo Interface de Comunicación", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsIRE_INTERMED : Dll_IRE_INTERMED.wsIRE_INTERMED 
    {
    }
}
