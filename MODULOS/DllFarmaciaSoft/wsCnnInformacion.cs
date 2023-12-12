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

using DllFarmaciaSoft.OrdenesDeCompra;

namespace DllFarmaciaSoft
{
    [WebService(Description = "Modulo conexión", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsCnnInformacion : DllFarmaciaSoft.wsConexion 
    {
        [WebMethod(Description = "Nombre módulo")]
        public string Modulo()
        {
            return "Compras"; 
        }

        [WebMethod(Description = "Obtener información de Orden De Compra.")]
        public override DataSet InformacionOrdenCompra(string Empresa, string Estado, string Destino, string Folio)
        {
            DataSet dtsOrdenCompra = new DataSet();
            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(base.ConexionEx(DtGeneral.CfgIniComprasInformacion));
                datosCnn.Servidor = SetServidor(datosCnn.Servidor);
   
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsGetOrdenDeCompra OrdenCompra = new clsGetOrdenDeCompra(datosCnn, Empresa, Estado, "", Destino, Folio);

                dtsOrdenCompra = OrdenCompra.InformacionOrdenCompra(true);
            }
            catch { }
            return dtsOrdenCompra;
        }
    }
}
