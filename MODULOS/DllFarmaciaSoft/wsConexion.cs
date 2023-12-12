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


using DllFarmaciaSoft.Pedidos; 
using DllFarmaciaSoft.OrdenesDeCompra; 
using DllFarmaciaSoft.Conexiones; 

namespace DllFarmaciaSoft
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
            if ( sServer == "1" ) 
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

    [WebService(Description = "Modulo conexión", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsConexion
    {
        /// <summary>
        /// Proporciona funciones generales que se implementan en la clase
        /// </summary>
        basSeguridad funciones; // = new basSeguridad();
        clsCriptografo Cryp = new clsCriptografo();

        #region Funciones y Procedimientos Estandard 
        public string SetServidor(string Servidor)
        {
            string sRegresa = "";
            wsTestConexion test = new wsTestConexion();

            sRegresa = test.RevisarServidor(Servidor); 

            return sRegresa; 
        }


        [WebMethod(Description = "Obtener información")]
        public virtual DataSet Conexion()
        {
            return AbrirConexionEx("FileConfig");
        }

        [WebMethod(Description = "Obtener información")]
        public virtual DataSet ConexionEx(string ArchivoIni)
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
            datosCnn.ForzarImplementarPuerto = funciones.ForzarPuerto.Contains("1");  

            return datosCnn.DatosCnn();
        }

        [WebMethod(Description = "Obtener información del servidor.")]
        public virtual DataSet Execute(DataSet Parametros, DataSet InformacionCliente, bool UsarTransaccion, string Contenedor, string Sentencia)
        {
            DataSet dtsRetorno = new DataSet();
            clsDatosConexion datosCnn = new clsDatosConexion(Parametros);
            datosCnn.Servidor = SetServidor(datosCnn.Servidor); 
            
            clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
            clsLeer myReader = new clsLeer(ref myCnn);
            clsGrabarError manError = new clsGrabarError();
            clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente);

            myCnn.FormatoDeFecha = FormatoDeFecha.Ninguno;
            if (myCnn.Abrir())
            {
                if (UsarTransaccion)
                {
                    myCnn.IniciarTransaccion();
                }

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
        public virtual DataSet ExecuteExt(DataSet InformacionCliente, string Solicitud, string Sentencia)
        {
            DataSet dtsRetorno = new DataSet();

            try 
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(Solicitud));
                datosCnn.Servidor = SetServidor(datosCnn.Servidor);

                clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
                clsLeer myReader = new clsLeer(ref myCnn);
                clsGrabarError manError = new clsGrabarError();
                clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente);

                myCnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.Limite30; 
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
                } 
            }
            catch { }
            return dtsRetorno;
        }

        [WebMethod(Description = "Plantilla de reporte excel.")]
        public virtual byte[] ReporteExcel(string NombrePlantilla, string Informacion)
        {
            byte[] btReporte = null;
            string sFile = Path.Combine(Informacion + @"\\Plantillas", NombrePlantilla);

            if (!File.Exists(sFile))
            {
                clsGrabarError.LogFileError(string.Format("No se encontro el archivo ==> {0}", sFile)); 
            }
            else 
            {
                try
                {
                    basGenerales F = new basGenerales();
                    btReporte = F.ConvertirArchivoEnBytes(sFile);
                }
                catch (Exception ex)
                {
                    clsGrabarError.LogFileError(string.Format("Ocurrió un error al procesar el archivo ==> {0}", sFile)); 
                }
            }  

            return btReporte; 
        }

        [WebMethod(Description = "Generar reporte.")]
        public virtual byte[] Reporte(DataSet InformacionReporteWeb, DataSet InformacionCliente)
        {
            clsGrabarError manError = new clsGrabarError();
            manError.LogError("Generando reporte."); 
            byte[] btReporte = null;

            try
            {
                clsImprimir myReporte = new clsImprimir(InformacionReporteWeb);
                clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente); 

                try
                {
                    manError.LogError(myReporte.NombreReporte);
                    ////manError.LogError(myReporte.DatosCnn.CadenaConexion); 

                    myReporte.DatosCliente = DatosCliente;
                    myReporte.CargarReporte();
                    if (!myReporte.ErrorAlGenerar)
                    {
                        myReporte.ExportarReporteMemoria(ref btReporte);
                    }
                    else
                    {
                        //manError.GrabarError(new Exception("Error al generar reporte : " + myReporte.NombreReporte + "   " + myReporte.MensajeError), myReporte.DatosCnn, DatosCliente, myReporte.MensajeError);
                        manError.LogError("[ 003 -- Error al generar reporte : " + myReporte.NombreReporte + " ] " + " [" + myReporte.MensajeError + "] ", FileAttributes.Normal);
                    }
                }
                catch (Exception ex)
                {
                    btReporte = null;
                    //manError.GrabarError(new Exception("Error al generar reporte.. : " + myReporte.NombreReporte + "   " + ex.Message), myReporte.DatosCnn, DatosCliente, ex.Message);
                    manError.LogError("[ 002 -- Error al generar reporte.. : " + myReporte.NombreReporte + " ] " + " [" + ex.Message + "] ", FileAttributes.Normal);
                }
            }
            catch (Exception ex1)
            {
                btReporte = null; 
                manError.LogError("[ 001 -- Error al generar reporte.. : ||||| " + ex1.Message + "] ", FileAttributes.Normal);
            }

            return btReporte;
        }

        [WebMethod(Description = "Probar conexión.")]
        public virtual string ProbarConexion(string ArchivoIni)
        {
            string sResultado = "Prueba"; 
            bool bRegresa = true;

            clsDatosConexion datosCnn = new clsDatosConexion();
            clsConexionSQL cnn = new clsConexionSQL();
            clsLeer leer = new clsLeer() ;

            try
            {
                datosCnn = new clsDatosConexion(AbrirConexionEx(ArchivoIni));
                datosCnn.Servidor = SetServidor(datosCnn.Servidor);
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

                        if (DtGeneral.EsEquipoDeDesarrollo)
                        {
                            string sRegresa = sResultado;
                            sResultado = string.Format("Servidor: {0}\n", cnn.DatosConexion.Servidor);
                            sResultado += string.Format("Puerto: {0}\n", cnn.DatosConexion.Puerto);
                            sResultado += string.Format("Base de datos: {0}\n", cnn.DatosConexion.BaseDeDatos);
                            sResultado += sRegresa;
                        } 
                    }
                }
                catch { }
            }

            return sResultado;

        }

        [WebMethod(Description = "Probar conexión auxiliar.")]
        private string ProbarConexionAuxiliar(string ArchivoIni, string Conexion)
        {
            string sResultado = "Prueba";
            bool bRegresa = true;

            clsDatosConexion datosCnn = new clsDatosConexion();
            clsConexionSQL cnn = new clsConexionSQL();
            clsLeer leer = new clsLeer();

            try
            {
                datosCnn = new clsDatosConexion(AbrirConexionEx(ArchivoIni));
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

                    if (Conexion == "1")
                    {
                        if (!leer.Exec("Select getdate() as Fecha"))
                        {
                            sResultado += leer.Error.Message;
                        }
                        else
                        {
                            //sResultado = "Fecha servidor :  " + leer.CampoFecha("Fecha").ToLongDateString().ToUpper();
                            sResultado += "Fecha servidor :  " + leer.CampoFecha("Fecha").ToString().ToUpper();

                            if (DtGeneral.EsEquipoDeDesarrollo)
                            { 
                                string sRegresa = sResultado;
                                sResultado = string.Format("Servidor: {0}\n", cnn.DatosConexion.Servidor);
                                sResultado += string.Format("Base de datos: {0}\n", cnn.DatosConexion.BaseDeDatos);
                                sResultado += sRegresa; 
                            } 
                        }
                    }
                }
                catch { }
            }

            return sResultado;

        }

        [WebMethod(Description = "Probar conexión al servidor, clientes prueban la conexión")]
        public virtual bool TestConection()
        {
            return true;
        }

        [WebMethod(Description = "Directorio de Trabajo wwww")]
        public virtual DataSet wwwDirectorio()
        {
            // regresa la Ruta de Acceso completa donde se encuentra instalado el WebService solicitado. 
            DataSet dtsRetorno = new DataSet();
            DataTable dt = new DataTable("Directorio");
            clsCriptografo crypto = new clsCriptografo(); 
            string sDirectorioBase = "";

            try
            {
                sDirectorioBase = AppDomain.CurrentDomain.BaseDirectory.ToString();
                object[] obj = { crypto.Encriptar(sDirectorioBase) }; 
                dt.Columns.Add("Ruta", System.Type.GetType("System.String"));
                dt.Rows.Add(obj);

                dtsRetorno.Tables.Add(dt); 

                //DirectoryInfo x = new DirectoryInfo(sDirectorioBase);
                //DirectoryInfo xP = x.Parent;

                //sDirectorioBase = ""; 
                //foreach (DirectoryInfo d in xP.GetDirectories())
                //{
                //    sDirectorioBase += d.Name + "\n\n\t"; 
                //} 
            }
            catch (Exception ex1) 
            {
                sDirectorioBase = ex1.Message;
                object[] obj = { crypto.Encriptar(sDirectorioBase) };
                dt.Columns.Add("Ruta", System.Type.GetType("System.String"));
                dt.Rows.Add(obj);

                dtsRetorno.Tables.Add(dt); 
            }

            return dtsRetorno; 
        }
        #endregion Funciones y Procedimientos Estandard

        #region Funciones y Procedimientos Estandard Extendidos
        private DataSet PrepararError(Exception ErrorDetectado)
        {
            clsLeer leerError = new clsLeer();
            DataSet dtsRetorno = leerError.ListaDeErrores();            
            string sMensaje = "", sNumError = "", sSqlEstado = "";

            try
            {
                sMensaje = ErrorDetectado.Message.Replace("'", "" + Strings.Chr(34) + "");
                sNumError = "0";
                sSqlEstado = "";

                object[] obj = { sMensaje, sNumError, sSqlEstado };
                dtsRetorno.Tables["Errores"].Rows.Add(obj);
            }
            catch 
            { 
            }
            
            return dtsRetorno; 
        }

        [WebMethod(Description = "Obtener información del servidor.")]
        public virtual DataSet ExecuteRemoto(DataSet Informacion, DataSet InformacionCliente)
        {
            DataSet dtsRetorno = new DataSet();
            try
            {
                clsConexionClienteUnidad conCliente = new clsConexionClienteUnidad(Informacion, InformacionCliente);
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(conCliente.ArchivoConexionCentral));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);

                conCliente.DatosDeConexionCliente = datosCnn;
                dtsRetorno = conCliente.ObtenerInformacion();

            }
            catch (Exception ex)
            {
                dtsRetorno = PrepararError(ex); 
            } 
            return dtsRetorno;
        }

        [WebMethod(Description = "Generar reporte.")]
        public virtual byte[] ReporteRemoto(DataSet Informacion, DataSet InformacionCliente, DataSet InformacionReporteWeb)
        {
            clsGrabarError manError = new clsGrabarError();
            clsConexionClienteUnidad conCliente = new clsConexionClienteUnidad(Informacion, InformacionCliente, InformacionReporteWeb);
            clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(conCliente.ArchivoConexionCentral));
            clsConexionSQL cnn = new clsConexionSQL(datosCnn);
            // clsConexionClienteUnidad conCliente = new clsConexionClienteUnidad(datosCnn, Informacion, InformacionCliente, InformacionReporteWeb);

            conCliente.DatosDeConexionCliente = datosCnn;

            clsImprimir myReporte = new clsImprimir(InformacionReporteWeb);
            clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente);

            //manError.LogError("1 ==> Inicio "); 

            byte[] btReporte = null;

            if (conCliente.Impresion())
            {
                // manError.LogError(datosCnn.CadenaDeConexion);
                try
                {
                    myReporte.DatosCnn = conCliente.DatosDeConexionUnidad;
                    //manError.LogError("3 ==> " + conCliente.DatosDeConexionUnidad);
                    myReporte.DatosCliente = DatosCliente;
                    myReporte.RutaReporte = conCliente.RutaReportes; 
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
            else
            {
                manError.LogError("No se pudo establecer la conexion");
            }

            return btReporte;
        }
        #endregion Funciones y Procedimientos Estandard Extendidos

        #region Informacion Especial
        ////[WebMethod(Description = "Obtener información de Orden De Compra BASE.")]
        ////public virtual DataSet InformacionOrdenCompra( string Empresa, string Estado, string Destino, string Folio )
        ////{
        ////    return InformacionOrdenCompra(Empresa, Estado, "", Destino, Folio);
        ////}

        [WebMethod(Description = "Obtener información de Orden De Compra.")] 
        public virtual DataSet InformacionOrdenCompra(string Empresa, string Estado, string Destino, string Folio)
        {
            DataSet dtsOrdenCompra = new DataSet();
            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(DtGeneral.CfgIniComprasInformacion));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsGetOrdenDeCompra OrdenCompra = new clsGetOrdenDeCompra(datosCnn, Empresa, Estado, "", Destino, Folio);

                dtsOrdenCompra = OrdenCompra.InformacionOrdenCompra(true);
            }
            catch { }
            return dtsOrdenCompra;
        }

        [WebMethod(Description = "Obtener información de Pedidos.")]
        public virtual DataSet InformacionPedidos(string Empresa, string Estado, string Farmacia, int TipoDePedido)
        {
            DataSet dtsPedidos = new DataSet();
            try 
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(DtGeneral.CfgIniPuntoDeVenta));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsGetPedidos Pedido = new clsGetPedidos(datosCnn, Empresa, Estado, Farmacia);

                if (TipoDePedido == 1)
                {
                    dtsPedidos = Pedido.PedidosCEDIS();
                }

                if (TipoDePedido == 2)
                {
                    dtsPedidos = Pedido.PedidosDistribuidor();
                }

            }
            catch (Exception ex)
            {
                dtsPedidos = PrepararError(ex);
            } 

            return dtsPedidos;
        }

        [WebMethod(Description = "Envio Informacion de Transferencias")]
        public virtual DataSet InformacionTransferencias(string Estado, string FarmaciaOrigen, string FarmaciaDestino, string Folio)
        {
            DataSet dtsResultado = new DataSet();

            dtsResultado = InformacionTransferencias_General(Estado, FarmaciaOrigen, Estado, FarmaciaDestino, Folio);

            return dtsResultado; 
        }

        [WebMethod(Description = "Envio Informacion de Transferencias")]
        public virtual DataSet InformacionTransferenciasEstatales(string EstadoOrigen, string FarmaciaOrigen, string EstadoDestino, string FarmaciaDestino, string Folio)
        {
            DataSet dtsResultado = new DataSet();

            dtsResultado = InformacionTransferencias_General(EstadoOrigen, FarmaciaOrigen, EstadoDestino, FarmaciaDestino, Folio);

            return dtsResultado;
        }

        [WebMethod(Description = "Envio Informacion de Transferencias")]
        private DataSet InformacionTransferencias_General(string EstadoOrigen, string FarmaciaOrigen, string EstadoDestino, string FarmaciaDestino, string Folio)
        {
            clsGrabarError manError = new clsGrabarError();
            string sTablaTransfencias = " CFGC_EnvioDetallesTrans ", NomTabla = "";
            string sValorActualizado = "1";
            bool bExecuto = true;
            bool bExito = false; 

             
            DataSet dtsTransferencia = new DataSet("Base_Resultado"); 
            DataTable dtSinTransferencia = new DataTable("Resultado"); 

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(DtGeneral.CfgIniPuntoDeVenta));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsLeer leerCat = new clsLeer(ref cnn);
                clsLeer leer = new clsLeer(ref cnn);

                string sSql = string.Format(" Select * From {0} (NoLock) " +
                                            " Where Status = 'A' Order By IdOrden, NombreTabla ", sTablaTransfencias);
                
                string sSqlBuscar = string.Format(
                    " Select '1' as Salida, cast( (case when IdFarmaciaRecibe = '{3}' then 1 else 0 end) as bit ) as Correcto, " + 
		                " IdEmpresa, Empresa, IdEstadoEnvia, Estado, IdFarmaciaEnvia, Farmacia, Folio, " +
                        " IdEstadoRecibe, EstadoRecibe, IdFarmaciaRecibe, FarmaciaRecibe   " + 
	                " From vw_TransferenciaEnvio_Enc (NoLock) " +
                    " Where IdEstadoEnvia = '{0}' and IdFarmaciaEnvia = '{1}' and Folio = '{2}'",
                    EstadoOrigen, FarmaciaOrigen, Folio, FarmaciaDestino);


                if (!leer.Exec("Resultado", sSqlBuscar))
                {
                    dtsTransferencia = leer.ListaDeErrores();
                }
                else
                {
                    if (!leer.Leer())
                    {
                        object[] objRow = { '0', string.Format("Folio de Transferencia '{0}' no encontrado. ", Folio) };

                        dtSinTransferencia.Columns.Add("Salida", System.Type.GetType("System.String"));
                        dtSinTransferencia.Columns.Add("Mensaje", System.Type.GetType("System.String"));
                        dtSinTransferencia.Rows.Add(objRow);

                        dtsTransferencia.Tables.Add(dtSinTransferencia); 
                    }
                    else
                    {
                        bExito = leer.CampoBool("Correcto");
                        if (!bExito)
                        {
                            dtsTransferencia = leer.DataSetClase; 
                        } 
                    }
                }

                if (bExito)
                {
                    if (!leerCat.Exec(sSql))
                    {
                        dtsTransferencia = leerCat.ListaDeErrores();
                    }
                    else
                    {
                        if (cnn.Abrir())
                        {
                            cnn.IniciarTransaccion();
                            while (leerCat.Leer())
                            {
                                NomTabla = leerCat.Campo("NombreTabla");

                                string sQuery = string.Format(" Exec spp_CFG_ObtenerDatos '{0}', " +
                                               " [ Where IdEstadoEnvia = '{1}' and IdFarmaciaEnvia = '{2}' And IdEstadoRecibe = '{3}' ], " +
                                               " '{6}', [ and IdFarmaciaRecibe = '{4}' And FolioTransferencia = '{5}' ] ",
                                               NomTabla, EstadoOrigen, FarmaciaOrigen, EstadoDestino, FarmaciaDestino, Folio, sValorActualizado); 

                                if (!leer.Exec(NomTabla, sQuery))
                                {
                                    bExecuto = false;
                                    break;
                                }
                                else
                                {
                                    dtsTransferencia.Tables.Add(leer.DataTableClase.Copy());

                                    string sUpdate = string.Format(" Update {0} Set Actualizado = 1, Status = 'T' Where IdEstadoEnvia = '{1}' " +
                                        " and IdFarmaciaEnvia = '{2}' And IdEstadoRecibe = '{3}' and IdFarmaciaRecibe = '{4}' And FolioTransferencia = '{5}'",
                                                                   NomTabla, EstadoOrigen, FarmaciaOrigen, EstadoDestino, FarmaciaDestino, Folio);

                                    if (!leer.Exec(sUpdate))
                                    {
                                        bExecuto = false;
                                        break;
                                    }
                                }
                            }

                            if (bExecuto)
                            {
                                cnn.CompletarTransaccion();
                            }
                            else
                            {
                                cnn.DeshacerTransaccion();
                                dtsTransferencia = leer.ListaDeErrores();
                            }
                        }
                        else
                        {
                            dtsTransferencia = cnn.ListaDeErrores();
                        }
                    }
                }
            }
            catch (Exception ex1)
            {
                manError.LogError(ex1.Message); 
            }

            return dtsTransferencia;
        }

        [WebMethod(Description = "Obtener Informacion de Ordenes de Compras a recepcionar diario")]
        public virtual DataSet InformacionRecepcionDiariaOrdenesCompras(string Empresa, string Estado, string EntregarEn)
        {
            clsGrabarError manError = new clsGrabarError();
            
            DataSet dtsListaOC = new DataSet("Resultado");
            
            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(DtGeneral.CfgIniComprasCentral));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsLeer leer = new clsLeer(ref cnn);

                string sSql = string.Format(" Exec spp_Rpt_ListadoOC_RecepcionDiaria '{0}', '{1}', '{2}' ", Empresa, Estado, EntregarEn);
                

                if (!leer.Exec(sSql))
                {
                    dtsListaOC = leer.ListaDeErrores();
                }
                else
                {
                    if (leer.Leer())
                    {
                        dtsListaOC = leer.DataSetClase;
                    }
                }                
            }
            catch (Exception ex1)
            {
                manError.LogError(ex1.Message);
            }

            return dtsListaOC;
        }
        #endregion Informacion Especial 
    }
}
