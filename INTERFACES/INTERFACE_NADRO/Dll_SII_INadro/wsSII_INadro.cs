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


////using DllFarmaciaSoft.Pedidos; 
////using Dll_SII_INadro.PedidosUnidades; 
////using DllFarmaciaSoft.Conexiones;

using Dll_SII_INadro.PedidosUnidades; 

namespace Dll_SII_INadro
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

    [WebService(Description = "Módulo Interface de Comunicación", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsSII_INadro
    {
        /// <summary>
        /// Proporciona funciones generales que se implementan en la clase
        /// </summary>
        basSeguridad funciones; // = new basSeguridad();
        clsCriptografo Cryp = new clsCriptografo();
        string sFileConexion = "FarmaciaPtoVta";


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

        [WebMethod(Description = "Obtener información del servidor.")]
        public virtual DataSet Execute(DataSet InformacionCliente, string Sentencia)
        {
            DataSet dtsRetorno = new DataSet();

            try 
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
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

        [WebMethod(Description = "Probar conexión.")]
        public virtual string ProbarConexion()
        {
            string sResultado = "Prueba"; 
            bool bRegresa = true;

            clsDatosConexion datosCnn = new clsDatosConexion();
            clsConexionSQL cnn = new clsConexionSQL();
            clsLeer leer = new clsLeer() ;

            try
            {
                datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
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

        #region Obtener Informacion 
        [WebMethod(Description = "Validar la Referencia del Pedido recibido")]
        public virtual DataSet ValidarReferenciaPedido(string IdEmpresa, string IdEstado, string IdFarmacia, string FolioReferencia)
        {
            DataSet dtsRetorno = new DataSet();

            ////try
            ////{
            ////    clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
            ////    datosCnn.Servidor = SetServidor(datosCnn.Servidor);

            ////    clsConexionSQL myCnn = new clsConexionSQL(datosCnn);
            ////    clsLeer myReader = new clsLeer(ref myCnn);
            ////    clsGrabarError manError = new clsGrabarError();
            ////    clsDatosCliente DatosCliente = new clsDatosCliente(InformacionCliente);

            ////    myCnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.Limite30;
            ////    myCnn.FormatoDeFecha = FormatoDeFecha.Ninguno;
            ////    if (myCnn.Abrir())
            ////    {
            ////        if (!myReader.Exec(Sentencia))
            ////        {
            ////            dtsRetorno = myReader.ListaDeErrores();
            ////            manError.GrabarError(myReader.Error, datosCnn, DatosCliente, myReader.QueryEjecutado);
            ////        }
            ////        else
            ////        {
            ////            // Regresar siempre el resultado de la ejecucion, el contenido se validara del lado del cliente
            ////            dtsRetorno = myReader.DataSetClase;
            ////        }
            ////        myCnn.Cerrar();
            ////    }
            ////    else
            ////    {
            ////        dtsRetorno = myCnn.ListaDeErrores();
            ////    }
            ////}
            ////catch { }

            return dtsRetorno;
        }

        [WebMethod(Description = "Obtener información de Pedido.")]
        public virtual DataSet InformacionDePedido(string Empresa, string Estado, string Farmacia, string FolioReferencia)
        {
            DataSet dtsPedidos = new DataSet();
            
            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsGetPedidoUnidad Pedido = new clsGetPedidoUnidad(datosCnn, Empresa, Estado, Farmacia, FolioReferencia);

                dtsPedidos = Pedido.InformacionPedido();

            }
            catch (Exception ex)
            {
                dtsPedidos = PrepararError(ex);
            }

            return dtsPedidos;
        }
        #endregion Obtener Informacion

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
        #endregion Funciones y Procedimientos Estandard Extendidos

    }
}
