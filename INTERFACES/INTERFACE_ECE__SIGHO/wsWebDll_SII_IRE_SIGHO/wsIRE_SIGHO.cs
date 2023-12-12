using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using Dll_IRE_SIGHO;

namespace wsWebDll_SII_IRE_SIGHO
{
    [WebService(Description = "Módulo Interface de Comunicación", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsIRE_SIGHO : Dll_IRE_SIGHO.wsIRE_SIGHO 
    {
    }
}
