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

using DllProveedores.Clases;
using DllProveedores.OrdenesCompra;

namespace DllProveedores
{
    [WebService(Description = "Modulo conexión", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsCnnProveedores
    {
        /// <summary>
        /// Proporciona funciones generales que se implementan en la clase
        /// </summary>
        basSeguridad funciones; // = new basSeguridad();
        clsCriptografo Cryp = new clsCriptografo();
        string sConfig = "Proveedores"; 

        #region Metodos Standar 
        //[WebMethod(Description = "Obtener información")]
        public DataSet Conexion()
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
                        myCnn.DeshacerTransaccion();

                    dtsRetorno = myReader.ListaDeErrores();
                    manError.GrabarError(myReader.Error, datosCnn, DatosCliente, myReader.QueryEjecutado);
                }
                else
                {
                    if (UsarTransaccion)
                        myCnn.CompletarTransaccion();

                    // Regresar siempre el resultado de la ejecucion, el contenido se validara del lado del cliente
                    dtsRetorno = myReader.DataSetClase;
                }

                myCnn.Cerrar();
            }
            else
                dtsRetorno = myCnn.ListaDeErrores();

            return dtsRetorno;
        }

        [WebMethod(Description = "Obtener información del servidor.")]
        public DataSet ExecuteExt(DataSet InformacionCliente, string Solicitud, string Sentencia)
        {
            DataSet dtsRetorno = new DataSet();

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(Solicitud));
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
                    dtsRetorno = myCnn.ListaDeErrores();
            }
            catch { }
            return dtsRetorno;
        }

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
                    dtsRetorno = myCnn.ListaDeErrores();
            }
            catch { }
            return dtsRetorno;
        }

        [WebMethod(Description = "Generar reporte.")]
        public byte[] Reporte(DataSet InformacionReporteWeb, DataSet InformacionCliente)
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

        #region Metodos Publicos 
        [WebMethod(Description = "Confirmar Pedido Proveedor.")]
        public DataSet ConfirmarPedidoProveedor(DataSet InformacionCliente, DataSet dtsInformacionWeb, int iTipo)
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

                switch (iTipo)
                {
                    case 1:
                        {
                            clsConfirmarPedidoContado Confirmar = new clsConfirmarPedidoContado(dtsInformacionWeb, datosCnn);
                            if (!Confirmar.GuardarPedido())
                                dtsRetorno = Confirmar.ListaDeErrores;

                            break;
                        }
                    case 2:
                        {
                            clsConfirmarPedidoCredito Confirmar = new clsConfirmarPedidoCredito(dtsInformacionWeb, datosCnn);
                            if (!Confirmar.GuardarPedido())
                                dtsRetorno = Confirmar.ListaDeErrores;

                            break;
                        }
                    default:
                        break;

                }

            }
            catch { }
            return dtsRetorno;
        }

        [WebMethod(Description = "Confirmacion De Orden De Compra.")]
        public DataSet EmbarcarOrdenCompra(DataSet InformacionCliente, DataSet dtsInformacionWeb)
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


                clsOrdCompraLotes Embarcar = new clsOrdCompraLotes(dtsInformacionWeb, datosCnn);
                if (!Embarcar.GuardarPedido())
                    dtsRetorno = Embarcar.ListaDeErrores;

            }
            catch { }
            return dtsRetorno;
        }
        #endregion Metodos Publicos 
    }
}
