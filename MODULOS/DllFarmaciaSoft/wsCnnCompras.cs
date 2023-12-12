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
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;


namespace DllFarmaciaSoft
{
    [WebService(Description = "Modulo conexión", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsCnnCompras : DllFarmaciaSoft.wsConexion 
    {
        [WebMethod(Description = "Nombre módulo")]
        public string Modulo()
        {
            return "Compras";
        }
    }
}
