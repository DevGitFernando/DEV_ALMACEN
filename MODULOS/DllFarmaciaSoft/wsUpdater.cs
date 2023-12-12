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
using SC_SolutionsSystem.SistemaOperativo;

using SC_CompressLib;
using SC_CompressLib.Utils; 

namespace DllFarmaciaSoft
{
    [WebService(Description = "Modulo conexión", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsUpdater
    {
        /// <summary>
        /// Proporciona funciones generales que se implementan en la clase
        /// </summary>
        basSeguridad funciones; // = new basSeguridad();
        clsCriptografo Cryp = new clsCriptografo();
        string sConfig = "UpdateVersion"; 

        #region Metodos Standard
        //[WebMethod(Description = "Obtener información")]
        private DataSet Conexion() 
        {
            return AbrirConexionEx("FileConfig");
        }

        //[WebMethod(Description = "Obtener información")]
        private DataSet ConexionEx(string ArchivoIni)
        {
            return AbrirConexionEx(ArchivoIni);
        }

        /// <summary>
        /// Obtiene los datos de conexion con el servidor de BD
        /// </summary>
        /// <returns>Regresa un Dataset con la información completa para la conexion con el servidor.</returns>
        //[WebMethod(Description = ".")]
        private DataSet AbrirConexionEx(string ArchivoIni)
        {
            clsDatosConexion datosCnn = new clsDatosConexion();
            funciones = new basSeguridad(ArchivoIni);

            datosCnn.Servidor = funciones.Servidor;
            datosCnn.BaseDeDatos = funciones.BaseDeDatos;
            datosCnn.Usuario = funciones.Usuario;
            datosCnn.Password = funciones.Password;
            datosCnn.TipoDBMS = funciones.TipoDBMS;

            return datosCnn.DatosCnn();
        } 

        [WebMethod(Description = "Obtener información del servidor.")]
        public DataSet GetExecute(DataSet InformacionCliente, string Solicitud)
        {
            DataSet dtsRetorno = new DataSet();

            try
            {
                wsTestConexion testConexion = new wsTestConexion();
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sConfig));
                datosCnn.Servidor = testConexion.RevisarServidor(datosCnn.Servidor); 

                clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
                clsLeer myReader = new clsLeer(ref myCnn);
                clsGrabarError manError = new clsGrabarError();
                clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente);

                myCnn.FormatoDeFecha = FormatoDeFecha.Ninguno;
                if (myCnn.Abrir())
                {
                    if (!myReader.Exec(Solicitud))
                    {
                        dtsRetorno = myReader.ListaDeErrores();
                        manError.GrabarError(myReader.Error, datosCnn, DatosCliente, myReader.QueryEjecutado);
                    }
                    else
                    {
                        // Regresar siempre el resultado de la ejecucion, el contenido se validara del lado del cliente
                        dtsRetorno = myReader.DataSetClase;
                    }
                    myCnn.Cerrar();
                }
                else
                {
                    dtsRetorno = myCnn.ListaDeErrores();
                }
            }
            catch { }
            return dtsRetorno;
        }

        [WebMethod(Description = "Generar reporte.")]
        private byte[] Reporte(DataSet InformacionReporteWeb, DataSet InformacionCliente)
        {
            string sTablaCnn = "Conexion";
            clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sConfig));

            InformacionReporteWeb.Tables.Remove(sTablaCnn);
            InformacionReporteWeb.Tables.Add(datosCnn.DatosCnn().Tables[sTablaCnn].Copy());

            clsImprimir myReporte = new clsImprimir(InformacionReporteWeb);
            clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente);
            clsGrabarError manError = new clsGrabarError();

            byte[] btReporte = null;

            try
            {
                myReporte.DatosCliente = DatosCliente;
                myReporte.CargarReporte();
                if (!myReporte.ErrorAlGenerar)
                {
                    myReporte.ExportarReporteMemoria(ref btReporte);
                }
                else
                {
                    //manError.GrabarError(new Exception("Error al generar reporte : " + myReporte.NombreReporte + "   " + myReporte.MensajeError), myReporte.DatosCnn, DatosCliente, myReporte.MensajeError);
                    manError.LogError("[ Error al generar reporte : " + myReporte.NombreReporte + " ] " + " [" + myReporte.MensajeError + "] ", FileAttributes.Normal);
                }
            }
            catch (Exception ex)
            {
                btReporte = null;
                //manError.GrabarError(new Exception("Error al generar reporte.. : " + myReporte.NombreReporte + "   " + ex.Message), myReporte.DatosCnn, DatosCliente, ex.Message);
                manError.LogError("[ Error al generar reporte.. : " + myReporte.NombreReporte + " ] " + " [" + ex.Message + "] ", FileAttributes.Normal);
            }

            return btReporte;
        }

        [WebMethod(Description = "Probar conexión.")]
        public string ProbarConexion()
        {
            string sResultado = "Prueba";
            bool bRegresa = true;

            clsDatosConexion datosCnn = new clsDatosConexion();
            clsConexionSQL cnn = new clsConexionSQL();
            clsLeer leer = new clsLeer();
            wsTestConexion testConexion = new wsTestConexion(); 

            try
            {
                datosCnn = new clsDatosConexion(AbrirConexionEx(sConfig));
                datosCnn.Servidor = testConexion.RevisarServidor(datosCnn.Servidor); 
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
                    ex.Source = ex.Source;
                    bRegresa = false;
                    sResultado = "Error al crear la conexión. \n";
                }
            }

            if (bRegresa)
            {
                leer = new clsLeer(ref cnn);
                try
                {
                    if (!leer.Exec("Select getdate() as Fecha"))
                    {
                        sResultado = leer.Error.Message;
                    }
                    else
                    {
                        //sResultado = "Fecha servidor :  " + leer.CampoFecha("Fecha").ToLongDateString().ToUpper();
                        sResultado = "Fecha servidor :  " + leer.CampoFecha("Fecha").ToString().ToUpper();
                    }
                }
                catch { }
            }

            return sResultado;

        }
        #endregion Metodos Standard 

        #region Metodos Versiones
        [WebMethod(Description = "Actualizar versión del Modulo.")]
        public DataSet RevisarVersion(string Modulo, string Version)
        {
            return RevisarVersion_Modulo(Modulo, Version, Version); 
        }

        [WebMethod(Description = "Actualizar versión del Modulo.")]
        public DataSet RevisarVersion_Modulo(string Modulo, string Version, string VersionModulo)
        {
            DataSet dtsRetorno = new DataSet();
            string sSql = string.Format(" Select IdModulo, Nombre, Version From Net_Modulos (NoLock) " +
                " Where Nombre = '{0}' and cast(replace(Version, '.', '') as bigint) > cast(replace('{1}', '.', '') as bigint) ", 
                Modulo, Version);

            sSql = string.Format(" Select IdModulo, Nombre, Version " + 
                " From Net_Modulos (NoLock) " +
                " Where Nombre = '{0}' and cast(replace(dbo.fg_FormatoVersion(Version), '.', '') as bigint) > cast(replace(dbo.fg_FormatoVersion('{1}'), '.', '') as bigint) ",
                Modulo, Version);


            //// Validando versión de módulo en execución 
            sSql = string.Format("Select IdModulo, Nombre, Version \n " +
                " From Net_Modulos (NoLock) \n " +
                " Where Nombre = '{0}' and \n " +
                " ( \n " +
                "   ( cast(replace(dbo.fg_FormatoVersion(Version), '.', '') as bigint) > cast(replace(dbo.fg_FormatoVersion('{1}'), '.', '') as bigint) ) \n " +
                "   or " +
                "   ( cast(replace(dbo.fg_FormatoVersion(VersionArchivo), '.', '') as bigint) > cast(replace(dbo.fg_FormatoVersion('{2}'), '.', '') as bigint) ) \n " +
                " ) ",
                Modulo, Version, VersionModulo);


            //// Validando versión de base de datos 
            if (Version.Trim().ToUpper() == VersionModulo.Trim().ToUpper())
            {
                sSql = string.Format("Select IdModulo, Nombre, Version \n " +
                    " From Net_Modulos (NoLock) \n " +
                    " Where Nombre = '{0}' and \n " +
                    " ( \n " +
                    "   ( cast(replace(dbo.fg_FormatoVersion(Version), '.', '') as bigint) > cast(replace(dbo.fg_FormatoVersion('{1}'), '.', '') as bigint) ) \n " +
                    " ) ",
                    Modulo, Version);
            }


            // dbo.fg_FormatoVersion( 
            try
            {
                wsTestConexion testConexion = new wsTestConexion(); 
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sConfig));
                datosCnn.Servidor = testConexion.RevisarServidor(datosCnn.Servidor); 

                clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
                clsLeer myReader = new clsLeer(ref myCnn);
                clsGrabarError manError = new clsGrabarError();
                //clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente);

                myCnn.FormatoDeFecha = FormatoDeFecha.Ninguno; 
                if (myCnn.Abrir())
                {
                    if (!myReader.Exec(sSql))
                    {
                        dtsRetorno = myReader.ListaDeErrores();
                        manError.GrabarError(myReader, "RevisarVersion");
                    }
                    else
                    {
                        // Regresar siempre el resultado de la ejecucion, el contenido se validara del lado del cliente
                        dtsRetorno = myReader.DataSetClase;
                    }
                    myCnn.Cerrar();
                }
                else
                {
                    dtsRetorno = myCnn.ListaDeErrores();
                }
            }
            catch { }
            return dtsRetorno;
        }

        //////[WebMethod(Description = "Actualizar versión del Modulo.")]
        //////private DataSet GetVersionTest(string Modulo, string Version)
        //////{
        //////    DataSet dtsRetorno = new DataSet();

        //////    try
        //////    {
        //////        wsTestConexion testConexion = new wsTestConexion(); 
        //////        clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sConfig));
        //////        datosCnn.Servidor = testConexion.RevisarServidor(datosCnn.Servidor); 

        //////        clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
        //////        clsLeer myReader = new clsLeer(ref myCnn);
        //////        clsGrabarError manError = new clsGrabarError();
        //////        // clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente);

        //////        myCnn.FormatoDeFecha = FormatoDeFecha.Ninguno;
        //////        if (myCnn.Abrir())
        //////        {
        //////            string sSql = string.Format(" Exec sp_Net_CheckUpdateVersion '{0}', '{1}', '{2}'  ", Modulo, Version, 0);

        //////            if (!myReader.Exec(sSql))
        //////            {
        //////                dtsRetorno = myReader.ListaDeErrores();
        //////                //manError.GrabarError(myReader.Error, datosCnn, DatosCliente, myReader.QueryEjecutado);
        //////            }
        //////            else
        //////            {
        //////                //// Regresar siempre el resultado de la ejecucion, el contenido se validara del lado del cliente  
        //////                dtsRetorno = myReader.DataSetClase; 
        //////            }
        //////            myCnn.Cerrar();
        //////        }
        //////        else
        //////        {
        //////            dtsRetorno = myCnn.ListaDeErrores();
        //////        }
        //////    }
        //////    catch { }
        //////    return dtsRetorno;
        //////} 

        private string MarcaTiempo()
        {
            string sMarca = "";
            basGenerales Fg = new basGenerales(); 
            DateTime dt = DateTime.Now;

            sMarca += Fg.PonCeros(dt.Year, 4);
            sMarca += Fg.PonCeros(dt.Month, 2);
            sMarca += Fg.PonCeros(dt.Day, 2);
            sMarca += "_";
            sMarca += Fg.PonCeros(dt.Hour, 2);
            sMarca += Fg.PonCeros(dt.Minute, 2);
            sMarca += Fg.PonCeros(dt.Second, 2);

            return sMarca;
        }

        [WebMethod(Description = "Actualizar versión del Modulo.")]
        public DataSet GetVersion(DataSet InformacionCliente, string Solicitud)
        {
            DataSet dtsRetorno = new DataSet();

            try
            {
                wsTestConexion testConexion = new wsTestConexion(); 
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sConfig));
                datosCnn.Servidor = testConexion.RevisarServidor(datosCnn.Servidor); 

                clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
                clsLeer myReader = new clsLeer(ref myCnn);
                clsGrabarError manError = new clsGrabarError();
                clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente);

                myCnn.FormatoDeFecha = FormatoDeFecha.Ninguno;
                if (myCnn.Abrir())
                {
                    if (!myReader.Exec(Solicitud))
                    {
                        dtsRetorno = myReader.ListaDeErrores();
                        //manError.GrabarError(myReader.Error, datosCnn, DatosCliente, myReader.QueryEjecutado);
                    }
                    else
                    {
                        //// Regresar siempre el resultado de la ejecucion, el contenido se validara del lado del cliente  
                        dtsRetorno = myReader.DataSetClase; 
                    }
                    myCnn.Cerrar();
                }
                else
                {
                    dtsRetorno = myCnn.ListaDeErrores();
                }
            }
            catch { }
            return dtsRetorno;
        }
        #endregion Metodos Versiones
    }
}
