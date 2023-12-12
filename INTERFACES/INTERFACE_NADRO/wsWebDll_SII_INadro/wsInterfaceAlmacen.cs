using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace wsWebDll_SII_INadro
{
    [WebService(Description = "Modulo información", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsInterfaceAlmacen : Dll_SII_INadro.wsSII_INadro
    {
    }
}
