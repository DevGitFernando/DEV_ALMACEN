using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace wsDllWebService
{
    [WebService(Description = "Modulo información", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsCnnInformacion : DllFarmaciaSoft.wsCnnInformacion
    {
    }
}