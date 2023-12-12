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
using System.Windows.Forms;

using Microsoft.VisualBasic;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using SII_Interface_Sinteco.wsISinteco;

using DllFarmaciaSoft; 

namespace SII_Interface_Sinteco.Conexion
{
    public class ISinteco
    {
        string sURL = "";
        bool bConexionConfigurada = false;
        string sMensajeError = "";

        clsConexionSQL cnn;
        clsLeer leer;
        clsGrabarError Error;
        clsLeer leerPedidos;
        basGenerales Fg = new basGenerales();

        wsISinteco.msmqinws sinteco_message;       

        public ISinteco()
        {
            InicializarConexiones(); 
        }

        #region Propiedades 
        public bool EnvioHabilitado
        {
            get { return bConexionConfigurada; }
        }

        public string MensajeError
        {
            get { return sMensajeError; }
            set { sMensajeError = value; }
        }
        #endregion Propiedades

        #region Funciones y Procedimientos Private
        private void InicializarConexiones()
        {
            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn);
            leerPedidos = new clsLeer(ref cnn);
            Error = new clsGrabarError(General.DatosConexion, Gn_ISINTECO.DatosApp, "ISinteco");
        }
        #endregion Funciones y Procedimientos Private 

        #region Funciones y Procedimientos Publico
        public bool EnviarMensaje(string XML)
        {
            bool bRegresa = false;
            int iRegresa = 0;
            sMensajeError = ""; 

            try
            {
                iRegresa = sinteco_message.PutMessage(XML, "SII_Almacen", 0);
            }
            catch (Exception ex)
            {
                iRegresa = 0; 
                sMensajeError = ex.Message;
            }

            bRegresa = iRegresa > 0;

            return bRegresa;
        }

        public bool GetConfiguracion()
        {
            bool bRegresa = false;
            string sSSL = ""; 
            string sSql = string.Format(
                "Select Top 1 IdEmpresa, IdEstado, IdFarmacia, Orden, Servidor, WebService, Pagina, SSL, Status " +
                "From SINTECO_CFG_Conexion (NoLock) " +
                "Where IdEmpresa = '{0}' And IdEstado = '{1}' and IdFarmacia = '{2}' and Status = 'A' ",
                DtGeneral.EmpresaConectada, DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada);

            if (!leer.Exec(sSql))
            {
                bConexionConfigurada = false;

                Error.GrabarError(leer, "GetConfiguracion()");
                General.msjError("Ocurrió un error al obtener los datos de conexión.");
            }
            else
            {
                if (!leer.Leer())
                {
                    bConexionConfigurada = false;                   
                }
                else
                {
                    sSSL = leer.CampoBool("SSL") == true ? "s" : ""; 
                    sURL = string.Format("http{0}://{1}/{2}/{3}.asmx", sSSL, leer.Campo("Servidor"), leer.Campo("WebService"), leer.Campo("Pagina"));
                    bConexionConfigurada = true;
                }
            }

            if (bConexionConfigurada)
            {
                sinteco_message = new msmqinws();
                sinteco_message.Url = sURL; 
            }

            return bRegresa;
        }
        #endregion Funciones y Procedimientos Publico
    }
}
