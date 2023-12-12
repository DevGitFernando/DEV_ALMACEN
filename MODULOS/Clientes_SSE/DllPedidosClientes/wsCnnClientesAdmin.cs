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

namespace DllPedidosClientes
{
    [WebService(Description = "Modulo conexión", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsCnnClientesAdmin
    {
        /// <summary>
        /// Proporciona funciones generales que se implementan en la clase
        /// </summary>
        basSeguridad funciones; // = new basSeguridad();
        clsCriptografo Cryp = new clsCriptografo();
        string sConfig = "CteRegional"; 

        #region Metodos Standar 
        //[WebMethod(Description = "Obtener información")]
        public DataSet Conexion()
        {
            return AbrirConexionEx("FileConfig");
        }

        [WebMethod(Description = "Obtener información")]
        public DataSet ConexionEx(string ArchivoIni)
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
            datosCnn.Puerto = funciones.Puerto; 
            datosCnn.BaseDeDatos = funciones.BaseDeDatos;
            datosCnn.Usuario = funciones.Usuario;
            datosCnn.Password = funciones.Password;
            datosCnn.TipoDBMS = funciones.TipoDBMS;

            return datosCnn.DatosCnn();
        }

        //[WebMethod(Description = "Obtener información del servidor.")]
        private DataSet Execute(DataSet Parametros, DataSet InformacionCliente, bool UsarTransaccion, string Contenedor, string Sentencia)
        {
            DataSet dtsRetorno = new DataSet();
            clsDatosConexion datosCnn = new clsDatosConexion(Parametros);
            clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
            clsLeer myReader = new clsLeer(ref myCnn);
            clsGrabarError manError = new clsGrabarError();
            clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente);

            myCnn.FormatoDeFecha = FormatoDeFecha.Ninguno;
            if (myCnn.Abrir())
            {
                if (UsarTransaccion)
                    myCnn.IniciarTransaccion();

                if (!myReader.Exec(Contenedor, Sentencia))
                {
                    if (UsarTransaccion)
                    {
                        myCnn.DeshacerTransaccion();
                    }

                    dtsRetorno = myReader.ListaDeErrores();
                    manError.GrabarError(myReader.Error, datosCnn, DatosCliente, myReader.QueryEjecutado);
                }
                else
                {
                    if (UsarTransaccion)
                    {
                        myCnn.CompletarTransaccion();
                    }

                    // Regresar siempre el resultado de la ejecucion, el contenido se validara del lado del cliente
                    dtsRetorno = myReader.DataSetClase;
                }

                myCnn.Cerrar();
            }
            else
            {
                dtsRetorno = myCnn.ListaDeErrores();
            }

            return dtsRetorno;
        }

        [WebMethod(Description = "Obtener información del servidor.")]
        public DataSet ExecuteExt(DataSet InformacionCliente, string Solicitud, string Sentencia)
        {
            DataSet dtsRetorno = new DataSet();
            clsGrabarError manError = new clsGrabarError();

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(Solicitud));
                clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
                clsLeer myReader = new clsLeer(ref myCnn);
                // clsGrabarError manError = new clsGrabarError();
                clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente);

                myCnn.FormatoDeFecha = FormatoDeFecha.Ninguno;
                if (myCnn.Abrir())
                {
                    if (!myReader.Exec(Sentencia))
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
                    manError.LogError(myCnn.Error.Message);
                }
            }
            catch (Exception ex ) 
            {
                manError.LogError(ex.Message);
            }
            return dtsRetorno;
        }

        ////[WebMethod(Description = "Obtener información del servidor.")]
        ////public DataSet ExecuteExtTest(string Solicitud, string Sentencia)
        ////{
        ////    DataSet dtsRetorno = new DataSet();

        ////    try
        ////    {
        ////        clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(Solicitud));
        ////        clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
        ////        clsLeer myReader = new clsLeer(ref myCnn);
        ////        clsGrabarError manError = new clsGrabarError();
        ////        clsDatosCliente DatosCliente = new clsDatosCliente("", "", "", ""); // new clsDatosCliente(InformacionCliente);

        ////        myCnn.FormatoDeFecha = FormatoDeFecha.Ninguno;
        ////        if (myCnn.Abrir())
        ////        {
        ////            if (!myReader.Exec(Sentencia))
        ////            {
        ////                dtsRetorno = myReader.ListaDeErrores();
        ////                manError.GrabarError(myReader.Error, datosCnn, DatosCliente, myReader.QueryEjecutado);
        ////            }
        ////            else
        ////            {
        ////                // Regresar siempre el resultado de la ejecucion, el contenido se validara del lado del cliente
        ////                dtsRetorno = myReader.DataSetClase;
        ////            }
        ////            myCnn.Cerrar();
        ////        }
        ////        else
        ////        {
        ////            dtsRetorno = myCnn.ListaDeErrores();
        ////        }
        ////    }
        ////    catch { }
        ////    return dtsRetorno;
        ////}

        [WebMethod(Description = "Obtener información del servidor.")]
        public DataSet GetExecute(DataSet InformacionCliente, string Solicitud)
        {
            DataSet dtsRetorno = new DataSet();

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sConfig));
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
        public byte[] Reporte(DataSet InformacionReporteWeb, DataSet InformacionCliente)
        {
            // string sTablaCnn = "Conexion";
            // clsDatosConexion datosCnn = new clsDatosConexion(); // = new clsDatosConexion(AbrirConexionEx(sConfig));

            //InformacionReporteWeb.Tables.Remove(sTablaCnn);
            //InformacionReporteWeb.Tables.Add(datosCnn.DatosCnn().Tables[sTablaCnn].Copy()); 
 
            clsImprimir myReporte = new clsImprimir(InformacionReporteWeb);
            clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente);
            clsGrabarError manError = new clsGrabarError();

            ////try
            ////{
            ////    myReporte = new clsImprimir(InformacionReporteWeb);
            ////    DatosCliente = new clsDatosCliente(InformacionCliente);
            ////}
            ////catch (Exception ex)
            ////{
            ////    manError.LogError("[ Error al generar reporte.. : " + "" + " ] " + " [" + ex.Message + "] ", FileAttributes.Normal);
            ////}

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
            catch ( Exception ex )
            {
                btReporte = null;
                //manError.GrabarError(new Exception("Error al generar reporte.. : " + myReporte.NombreReporte + "   " + ex.Message), myReporte.DatosCnn, DatosCliente, ex.Message);
                manError.LogError("[ Error al generar reporte.. : " + myReporte.NombreReporte + " ] " + " [" + ex.Message + "] ", FileAttributes.Normal);
            }

            return btReporte;
        }

        [WebMethod(Description = "Probar conexión.")]
        public string ProbarConexion(string ArchivoIni)
        {
            string sResultado = "Prueba";
            bool bRegresa = true;

            clsDatosConexion datosCnn = new clsDatosConexion();
            clsConexionSQL cnn = new clsConexionSQL();
            clsLeer leer = new clsLeer() ;

            try
            {
                datosCnn = new clsDatosConexion(AbrirConexionEx(ArchivoIni));
            }
            catch ( Exception ex )
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
                catch ( Exception ex ) 
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
        #endregion Metodos Standar 

        #region Metodos Especiales 
        [WebMethod(Description = "Ejecutar Sentencia.")]
        public DataSet EjecutarSentencia(string Estado, string Farmacia, string Sql, string NombreRpt, string TablaFarmacia)
        {
            DataSet dtsRetorno = new DataSet();
            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx("SII-Regional"));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsConexionCliente conCliente = new clsConexionCliente(datosCnn, Estado, Farmacia, Sql, NombreRpt, TablaFarmacia);

                dtsRetorno = conCliente.ObtenerInformacion();

            }
            catch { }
            return dtsRetorno;
        }

        [WebMethod(Description = "Generar Reporte Cliente.")]
        public byte[] ReporteExtendidoGeneral(string Estado, string Farmacia, DataSet InformacionReporteWeb, DataSet InformacionCliente)
        {
            return GenerarReporte(Estado, Farmacia, InformacionReporteWeb, InformacionCliente, true); 
        }

        [WebMethod(Description = "Generar Reporte Cliente.")]
        public byte[] ReporteExtendido(string Estado, string Farmacia, DataSet InformacionReporteWeb, DataSet InformacionCliente)
        {
            return GenerarReporte(Estado, Farmacia, InformacionReporteWeb, InformacionCliente, false); 
        }

        [WebMethod(Description = "Generar Reporte Cliente.")]
        private byte[] GenerarReporte(string Estado, string Farmacia, DataSet InformacionReporteWeb, DataSet InformacionCliente, bool EsRegional)
        {
            bool bGenerarImpresion = true; 
            clsGrabarError manError = new clsGrabarError();
            clsDatosApp dp = new clsDatosApp("DtGeneralPedidos", "3.0.0.0"); 


            clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx("SII-Regional"));
            manError = new clsGrabarError(datosCnn, dp, "DllPedidosClientes"); 

            clsConexionSQL cnn = new clsConexionSQL(datosCnn); 
            clsConexionCliente conCliente = new clsConexionCliente(datosCnn, Estado, Farmacia, "", "", ""); 
            clsImprimir myReporte = new clsImprimir(InformacionReporteWeb);
            clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente); 


            byte[] btReporte = null;
            if (!EsRegional)
            {
                if (!conCliente.Impresion())
                {
                    bGenerarImpresion = false;
                    manError.LogError("No se pudo establecer la conexion");
                }
            }

            if ( bGenerarImpresion ) 
            {
                //manError.LogError(conCliente.DatosDeConexionCliente.CadenaDeConexion); 
                try
                {
                    myReporte.DatosCnn = conCliente.DatosDeConexionCliente;
                    myReporte.DatosCliente = DatosCliente;
                    myReporte.RutaReporte = conCliente.RutaReportes;

                    //manError.LogError(conCliente.DatosDeConexionCliente.CadenaDeConexion);
                    //manError.LogError(conCliente.RutaReportes); 

                    myReporte.CargarReporte();
                    if (!myReporte.ErrorAlGenerar)
                    {
                        myReporte.ExportarReporteMemoria(ref btReporte);
                    }
                    else
                    {
                        //manError.GrabarError(new Exception("Error al generar reporte : " + myReporte.NombreReporte + "   " + myReporte.MensajeError), myReporte.DatosCnn, DatosCliente, myReporte.MensajeError);
                        manError.LogError("01==>[ Error al generar reporte : " + myReporte.NombreReporte + " ] " + " [" + myReporte.MensajeError + "] ", FileAttributes.Normal);
                    }
                }
                catch (Exception ex)
                {
                    btReporte = null;
                    //manError.GrabarError(new Exception("Error al generar reporte.. : " + myReporte.NombreReporte + "   " + ex.Message), myReporte.DatosCnn, DatosCliente, ex.Message);
                    manError.LogError("02==>[ Error al generar reporte.. : " + myReporte.NombreReporte + " ] " + " [" + ex.Message + "] ", FileAttributes.Normal);
                }
            }

            return btReporte;
        }

        [WebMethod(Description = "Ejecutar Sentencia Claves.")]
        public DataSet SentenciaClaves(string Estado, string Farmacia, string Sql, string TablaClaves, DataSet dtsTablas)
        {
            DataSet dtsRetorno = new DataSet();
            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx("SII-Regional"));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsConexionCliente conCliente = new clsConexionCliente(datosCnn, Estado, Farmacia, Sql, "", "");

                try
                {
                    dtsRetorno = conCliente.InformacionClavesSSA(dtsTablas, TablaClaves);
                }
                catch { }
            }
            catch { }
            return dtsRetorno;
        }
        #endregion Metodos Especiales
    }
}
