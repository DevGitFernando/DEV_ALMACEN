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

using Dll_SII_IMediaccess.Clases; 
using Dll_SII_IMediaccess.wsClases; 

namespace Dll_SII_IMediaccess
{
    public sealed class wsTestConexion
    {
        string sServer = "0";

        public wsTestConexion()
        {
            GetLocalConfigurationKey();
        }

        #region Funciones y Procedimientos Publicos
        public string RevisarServidor(string Servidor)
        {
            string sRegresa = "";

            sRegresa = Servidor;
            if (sServer == "1")
            {
                sRegresa = ProcesarServidor(Servidor);
            }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private string ProcesarServidor(string Servidor)
        {
            string sRegresa = "";
            bool bSeModifico = false;
            int EqualPosition = 0;
            bool bTieneInstancia = false;

            sRegresa = Servidor;
            if (Servidor.Contains(@"\") || Servidor.Contains(@","))
            {
                EqualPosition = Servidor.IndexOf(@"\", 0);
                if (EqualPosition > 0)
                {
                    bSeModifico = true;
                    bTieneInstancia = true;
                    sRegresa = @"localhost\" + Servidor.Substring(EqualPosition + 1).Trim();
                }

                if (!bTieneInstancia)
                {
                    EqualPosition = Servidor.IndexOf(@",", 0);
                    if (EqualPosition > 0)
                    {
                        bSeModifico = true;
                        sRegresa = @"localhost" + Servidor.Substring(EqualPosition + 1).Trim();
                    }
                }
            }

            if (!bSeModifico)
            {
                if (Servidor.Contains(@"."))
                {
                    bSeModifico = true;
                    sRegresa = @"localhost";
                }
            }

            return sRegresa;
        }

        private void GetLocalConfigurationKey()
        {
            string myKey = "";
            string myConnectString;
            string filePath = General.UnidadSO + ":\\inetpub\\wwwroot\\WebService_Cnn\\" + General.ArchivoIni; // Config.ini";
            string BaseDir = AppDomain.CurrentDomain.BaseDirectory.ToString();
            int EqualPosition = 0;

            int lenWebServiceName = AppDomain.CurrentDomain.BaseDirectory.ToString().Length;
            filePath = BaseDir.Substring(0, lenWebServiceName) + "TestConexion.ini"; //"Config.ini"           

            if (!File.Exists(filePath))
            {
                return;
            }

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (sr.Peek() >= 0)
                    {
                        myConnectString = sr.ReadLine();

                        if (myConnectString.Length != 0)
                        {
                            EqualPosition = myConnectString.IndexOf("=", 0);
                            if (EqualPosition > 0)
                            {
                                myKey = myConnectString.Substring(0, EqualPosition).ToUpper().Trim();
                                if (myKey == "Servidor".ToUpper())
                                {
                                    sServer = myConnectString.Substring(EqualPosition + 1).Trim();
                                }
                            }
                        }
                    }
                }
                //return myKey;                
            }
            catch
            {
                //return myKey; 
            }
        }
        #endregion Funciones y Procedimientos Privados
    }

    [WebService(Description = "Módulo Interface de Comunicación Mediaccess", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsIMediaccess
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
        /// <summary>
        /// Busqueda de Medicamentos 
        /// </summary>
        /// <param name="Id">ID de Producto ó Formula</param>
        /// <param name="Tipo">Tipo 1: Producto</param>
        /// <param name="Plan">Plan del paciente para determinar ranking</param>
        /// <param name="IdFarmacia">Identificador de la farmacia asociada al proveedor</param>
        /// <returns></returns>
        [WebMethod(Description = "Consulta de medicamentos en base al ID")]
        public string BusquedaMedicamentosxID(int Id, int Tipo, string Plan, string IdFarmacia, int Ranking)
        {
            ResponseBusquedaMedicamento respuesta = new ResponseBusquedaMedicamento();
            string sRegresa = "";

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion_Consulta));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                ConsultaProductos consulta = new ConsultaProductos(datosCnn);

                sRegresa = consulta.Consultar(Id, Tipo, Plan, IdFarmacia, Ranking).GetString(); 
                ////consulta.Consultar(Id, Tipo, Plan, IdFarmacia); 

            }
            catch (Exception ex)
            {
                respuesta.Estatus = 1;
                respuesta.Error = "Error al accesar la base de datos";
                sRegresa = respuesta.GetString(); 
            }

            return sRegresa;
        }

        /// <summary>
        /// Receta electrónica generada
        /// </summary>
        /// <param name="Folio">Folio de receta</param>
        /// <param name="IdFarmacia">IdFarmacia relacionada</param>
        /// <param name="Paciente">Nombre del beneficiario atendido</param>
        /// <param name="Medico">Nombre del médico que realizó la atención</param>
        /// <param name="Especialidad">Especialidad médica del tratante</param>
        /// <param name="Copago">Método de pago</param>
        /// <param name="Plan">Plan de beneficios</param>
        /// <param name="Fecha">Fecha de emisión de la receta</param>
        /// <param name="Eligibilidad">Código para validación de la atención</param>
        /// <param name="ICD1">Diagnóstico CIE-10</param>
        /// <param name="ICD2">Diagnóstico CIE-10</param>
        /// <param name="ICD3">Diagnóstico CIE-10</param>
        /// <param name="ICD4">Diagnóstico CIE-10</param>
        /// <param name="Datos_Receta">Medicamentos preescritos al paciente</param>
        /// <returns></returns>
        [WebMethod(Description = "Información de receta electrónica generada")]
        public string PublicacionReM(Int64 Folio, string IdFarmacia, string Paciente, string Medico, string Especialidad,
            int Copago, string Plan, string Fecha, string Eligibilidad, string ICD1, string ICD2, string ICD3, string ICD4, 
            string Datos_Receta)
        {
            ResponsePublicacionReM respuesta = new ResponsePublicacionReM();
            string sRegresa = ""; 

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);

                RecetaElectronica rec = new RecetaElectronica(datosCnn);

                sRegresa = rec.Guardar(Folio.ToString(), IdFarmacia, Paciente, Medico, Especialidad, Copago, Plan, Fecha, Eligibilidad, ICD1, ICD2, ICD3, ICD4, Datos_Receta).GetString(); 
            }
            catch (Exception ex)
            {
                respuesta.Estatus = 1;
                respuesta.Error = "Error al accesar la base de datos";
                sRegresa = respuesta.GetString();
            }

            return sRegresa;
        }

        /// <summary>
        /// Validación de existencias para la generación de recetas
        /// </summary>
        /// <param name="Datos_Receta">Medicamentos requeridos en la receta</param>
        /// <returns></returns>
        [WebMethod(Description = "Validación de disponibilidad de existencia")]
        public string ComprobarExistenciaDisponible(string IdFarmacia, string Datos_Receta)
        {
            ResponseBusquedaMedicamento respuesta = new ResponseBusquedaMedicamento();
            string sRegresa = "";

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion_Consulta));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                ConsultaProductos consulta = new ConsultaProductos(datosCnn);

                sRegresa = consulta.ValidarDisponibilidadExistencia(IdFarmacia, Datos_Receta).GetString_Receta(); 
            }
            catch (Exception ex)
            {
                respuesta.Estatus = 1;
                respuesta.Error = "Error al accesar la base de datos";
                sRegresa = respuesta.GetString(); 
            }

            return sRegresa;
        }

        /// <summary>
        /// Devuelve los registros de la unidad y periodo
        /// </summary>
        /// <param name="Datos_Receta"></param>
        /// <returns></returns>
        [WebMethod(Description = "Surtido de la unidad - periodo.")]
        public DataSet ListadoDeSurtido(string ReferenciaUnidad, string año, string mes)
        {
            DataSet dtsRegresa = new DataSet();
            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion_Consulta));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                ResponseSurtidos consulta = new ResponseSurtidos(datosCnn);

                dtsRegresa = consulta.ListadoDeSurtido(ReferenciaUnidad, año, mes);
            }
            catch (Exception ex) {}

            return dtsRegresa;
        }
        #endregion Funciones y Procedimientos Publicos 
    }
}
