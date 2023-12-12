using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace wsWebDll_SII_IMediaccess
{
    [WebService(Description = "Módulo Interface de Comunicación EPharma", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsEPharma : Dll_SII_IMediaccess.wsEPharma
    {
    }
}
