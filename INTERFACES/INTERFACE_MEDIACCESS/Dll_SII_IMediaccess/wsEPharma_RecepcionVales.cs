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

using Dll_SII_IMediaccess.ValesRecepcion; 

namespace Dll_SII_IMediaccess
{
    [WebService(Description = "Módulo Interface de Comunicación Mediaccess", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsEPharma_RecepcionVales
    {
        /// <summary>
        /// Proporciona funciones generales que se implementan en la clase
        /// </summary>
        basSeguridad funciones; // = new basSeguridad();
        clsCriptografo Cryp = new clsCriptografo();
        string sFileConexion = "SII Mediaccess";
        string sFileConexion_Consulta = "SII Mediaccess Query";

        #region Funciones y Procedimientos Estandard
        public string SetServidor(string Servidor)
        {
            string sRegresa = "";
            wsTestConexion test = new wsTestConexion();

            sRegresa = test.RevisarServidor(Servidor);

            return sRegresa;
        }


        [WebMethod(Description = "Obtener información")]
        private DataSet Conexion()
        {
            return AbrirConexionEx("FileConfig");
        }

        [WebMethod(Description = "Obtener información")]
        private DataSet ConexionEx(string ArchivoIni)
        {
            return AbrirConexionEx(ArchivoIni);
        }

        /// <summary>
        /// Obtiene los datos de conexion con el servidor de BD
        /// </summary>
        /// <returns>Regresa un Dataset con la información completa para la conexion con el servidor.</returns>
        [WebMethod(Description = ".")]
        private DataSet AbrirConexionEx(string ArchivoIni)
        {
            clsDatosConexion datosCnn = new clsDatosConexion();
            funciones = new basSeguridad(ArchivoIni);

            datosCnn.Servidor = funciones.Servidor;
            datosCnn.BaseDeDatos = funciones.BaseDeDatos;
            datosCnn.Usuario = funciones.Usuario;
            datosCnn.Password = funciones.Password;
            datosCnn.TipoDBMS = funciones.TipoDBMS;
            datosCnn.Puerto = funciones.Puerto;

            return datosCnn.DatosCnn();
        }

        [WebMethod(Description = "Comprobar fecha.")]
        private string ComprobarFecha()
        {
            string sResultado = "Prueba";
            bool bRegresa = true;

            clsDatosConexion datosCnn = new clsDatosConexion();
            clsConexionSQL cnn = new clsConexionSQL();
            clsLeer leer = new clsLeer();

            try
            {
                datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                datosCnn.Servidor = SetServidor(datosCnn.Servidor);
            }
            catch (Exception ex)
            {
                bRegresa = false;
                sResultado = "Error al obtener los datos de conexion. \n" + ex.Message;
            }

            if (bRegresa)
            {
                try
                {
                    cnn = new clsConexionSQL(datosCnn);
                }
                catch (Exception ex)
                {
                    bRegresa = false;
                    sResultado = "Error al crear la conexión. \n" + ex.Message;
                }
            }

            if (bRegresa)
            {
                leer = new clsLeer(ref cnn);
                try
                {
                    sResultado += cnn.DatosConexion.CadenaDeConexion;
                    sResultado += "\n\n\n\n ";
                    sResultado = "";

                    if (!leer.Exec("Select getdate() as Fecha"))
                    {
                        sResultado += cnn.DatosConexion.CadenaDeConexion;
                        sResultado += "\n\n\n\n ";

                        sResultado += leer.Error.Message;
                    }
                    else
                    {
                        //sResultado = "Fecha servidor :  " + leer.CampoFecha("Fecha").ToLongDateString().ToUpper();
                        sResultado += "Fecha servidor :  " + leer.CampoFecha("Fecha").ToString().ToUpper();

                        ////////if (DtGeneral.EsEquipoDeDesarrollo)
                        ////////{
                        ////////    string sRegresa = sResultado;
                        ////////    sResultado = string.Format("Servidor: {0}\n", cnn.DatosConexion.Servidor);
                        ////////    sResultado += string.Format("Puerto: {0}\n", cnn.DatosConexion.Puerto);
                        ////////    sResultado += string.Format("Base de datos: {0}\n", cnn.DatosConexion.BaseDeDatos);
                        ////////    sResultado += sRegresa;
                        ////////} 
                    }
                }
                catch { }
            }

            return sResultado;

        }
        #endregion Funciones y Procedimientos Estandard

        #region Funciones y Procedimientos Publicos

        public bool TestConection()
        {
            return true;
        }

        [WebMethod(Description = "Recepción de atención de vales.")]
        public ResponseSolicitud EnviarInformacionDeVale(ValesRecepcionRegistrarInformacion Solicitud)
        {
            ResponseSolicitud respuesta = new ResponseSolicitud();
            ValesRecepcionRegistrar registro;
            clsDatosConexion datosCnn = new clsDatosConexion();

            try
            {
                datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                registro = new ValesRecepcionRegistrar(datosCnn);

                respuesta = registro.RegistrarSolicitud(Solicitud);
            }
            catch (Exception ex)
            {
                respuesta.Error = true;
                respuesta.Mensaje = "Ocurrió un error al procesar la solicitud de servicio.";
                respuesta.Estatus = 0;
            }

            return respuesta;
        }
        #endregion Funciones y Procedimientos Publicos 
    }
}
