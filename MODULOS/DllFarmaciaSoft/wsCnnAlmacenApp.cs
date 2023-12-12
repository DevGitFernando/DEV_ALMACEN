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

using Microsoft.VisualBasic;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;
using DllFarmaciaSoft.App_Almacen;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace DllFarmaciaSoft
{
    [WebService(Description = "Modulo conexión", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsCnnAlmacenApp : DllFarmaciaSoft.wsConexion
    {
        #region Override 
        //[WebMethod(Description = "Obtener información")]
        //public override DataSet Conexion()
        //{
        //    return base.ConexionEx("AlmacenPtoVta");
        //}

        //[WebMethod(Description = "Obtener información")]
        //public override DataSet ConexionEx( string ArchivoIni )
        //{
        //    return base.ConexionEx("AlmacenPtoVta");
        //}
        #endregion Override 

        [WebMethod(Description = "Checar conexión")]
        public  string InicioSesionConexion()
        {
            return "Conexión";
        }

        [WebMethod(Description = "Datos de Inicio de Sesión")]
        public string DatosSesion()
        {
            Dictionary<string, object> lst = new Dictionary<string, object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            DataSet dtsRetorno = new DataSet();
            clsDatosConexion datosCnn = new clsDatosConexion(base.ConexionEx("AlmacenPtoVta"));
            datosCnn.Servidor = SetServidor(datosCnn.Servidor);

            clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
            clsLeer myReader = new clsLeer(ref myCnn);
            clsGrabarError manError = new clsGrabarError();

            lst.Add("Error", true);
            lst.Add("Mensaje", "No se pudo encontrar información de configuración, verifique");
            lst.Add("Tabla", "");

            clsLogin clsLg = new clsLogin();

            General.DatosConexion = datosCnn;

            DataSet dstResult = clsLg.ObtenerDatosIniciarSesion();

            if(dstResult.Tables.Count > 0)
            {
                lst["Error"] = false;
                lst["Mensaje"] = "";
                lst["Tabla"] = JsonConvert.SerializeObject(dstResult);
            }

            return serializer.Serialize(lst);
        }

        [WebMethod(Description = "Inicio de Sesión")]
        public string InicioSesion(string sIdEmpresa, string sIdEstado, string sIdFarmacia, string sUsuario, string sContraseña)
        {
            Dictionary<string, object> lst = new Dictionary<string, object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            DataSet dtsRetorno = new DataSet();
            clsDatosConexion datosCnn = new clsDatosConexion(base.ConexionEx("AlmacenPtoVta"));
            datosCnn.Servidor = SetServidor(datosCnn.Servidor);

            clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
            clsLeer myReader = new clsLeer(ref myCnn);
            clsGrabarError manError = new clsGrabarError();

            lst.Add("Error", true);
            lst.Add("Mensaje", "No se pudo autenticar al usuario, verifique");
            lst.Add("IdPersonal", "");

            clsLogin clsLg = new clsLogin();

            General.DatosConexion = datosCnn;

            string  IdResult = clsLg.AutenticarUsuarioLogin(sIdEmpresa, sIdEstado, sIdFarmacia, sUsuario, sContraseña);

            if (IdResult != "")
            {
                lst["Error"] = false;
                lst["Mensaje"] = "";
                lst["IdPersonal"] = IdResult;
            }

            return serializer.Serialize(lst);
        }

        [WebMethod(Description = "Validar folios")]
        public string ValidarFolios(string sIdEmpresa, string sIdEstado, string sIdFarmacia, string sFolioPedido, string sFolioSurtido)
        {
            Dictionary<string, object> lst = new Dictionary<string, object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            clsDatosConexion datosCnn = new clsDatosConexion(base.ConexionEx("AlmacenPtoVta"));
            datosCnn.Servidor = SetServidor(datosCnn.Servidor);

            clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
            clsLeer myReader = new clsLeer(ref myCnn);
            clsGrabarError manError = new clsGrabarError();

            clsGeneral clsg = new clsGeneral();
            basGenerales Fg = new basGenerales();

            General.DatosConexion = datosCnn;

            lst.Add("Error", true);
            lst.Add("Mensaje", "Hubo un error al validar la información, verifique");
            lst.Add("Resultado", false);

            bool bResult = clsg.VerificaFolios(sIdEmpresa, sIdEstado, sIdFarmacia, Fg.PonCeros(sFolioPedido, 6), Fg.PonCeros(sFolioSurtido, 8));

            if (bResult)
            {
                lst["Error"] = false;
                lst["Mensaje"] = "";
                lst["Resultado"] = bResult;
            }

            return serializer.Serialize(lst);
        }

        [WebMethod(Description = "Obtención de datos")]
        public string GetDatosPedido(string sIdEmpresa, string sIdEstado, string sIdFarmacia, string sFolioSurtido, int iPedidoManual, int iEsValidacion)
        {
            Dictionary<string, object> lst = new Dictionary<string, object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            DataSet dtsRetorno = new DataSet();
            clsDatosConexion datosCnn = new clsDatosConexion(base.ConexionEx("AlmacenPtoVta"));
            datosCnn.Servidor = SetServidor(datosCnn.Servidor);

            clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
            clsLeer myReader = new clsLeer(ref myCnn);
            clsGrabarError manError = new clsGrabarError();

            lst.Add("Error", true);
            lst.Add("Mensaje", "No se pudo autenticar al usuario, verifique");
            lst.Add("Tabla", "");

            clsGeneral clsGral = new clsGeneral();

            General.DatosConexion = datosCnn;

            dtsRetorno = clsGral.ObtenerDatos(sIdEmpresa, sIdEstado, sIdFarmacia, sFolioSurtido, iPedidoManual, iEsValidacion);

            if (dtsRetorno != null)
            {
                lst["Error"] = false;
                lst["Mensaje"] = "";
                //lst["Tabla"] = JsonConvert.SerializeObject(dtsRetorno);
                lst["Tabla"] = JsonConvert.SerializeObject(dtsRetorno);
            }

            return serializer.Serialize(lst);
        }

        [WebMethod(Description = "Envío de información al servidor")]
        public string GuardarSurtido(string sIdEmpresa, string sIdEstado, string sIdFarmacia, string sFolioSurtido, string sIdPersonalSurtido, DataSet dtsDatos)
        {
            Dictionary<string, object> lst = new Dictionary<string, object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            basGenerales Fg = new basGenerales();
            clsDatosConexion datosCnn = new clsDatosConexion(base.ConexionEx("AlmacenPtoVta"));
            //datosCnn.Servidor = SetServidor(datosCnn.Servidor); Quitarle el comment

            lst.Add("Error", true);
            lst.Add("Mensaje", "Error al guardar la información");
            lst.Add("Resultado", false);

            clsGeneral clsGral = new clsGeneral();

            General.DatosConexion = datosCnn;

            bool bRetorno = clsGral.EnvioInformacion( sIdEmpresa,  sIdEstado,  sIdFarmacia, Fg.PonCeros(sFolioSurtido, 8), sIdPersonalSurtido, dtsDatos);

            if (bRetorno)
            {
                lst["Error"] = false;
                lst["Mensaje"] = "";
                lst["Resultado"] = bRetorno;
            }

            return serializer.Serialize(lst);
        }

        [WebMethod(Description = "Prueba para el WS")]

        public void TestWs(string sFolioS, string sIdPersonalSurtido,  string JSon)
        {
            DataSet dtsResult = JsonConvert.DeserializeObject<DataSet>(JSon);

            GuardarSurtido("001", "11", "2005", sFolioS, sIdPersonalSurtido, dtsResult);
        }
    }
}
