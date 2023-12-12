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

using System.Text;
using System.IO;
using System.Configuration; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;
using Dll_IRE_SIGHO.Informacion;
using Dll_IRE_SIGHO.wsClases;
using Dll_IRE_SIGHO.Clases;

namespace Dll_IRE_SIGHO
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

    [WebService(Description = "Módulo Interface de Comunicación", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsIRE_SIGHO
    {
        /// <summary>
        /// Proporciona funciones generales que se implementan en la clase
        /// </summary>
        basSeguridad funciones; // = new basSeguridad();
        clsCriptografo Cryp = new clsCriptografo();
        string sFileConexion = "SII_INT_IRE_SIGHO";
        string sURL_wsSIGHO = "";

        #region Seguridad
        string sFirma_GUID = "";
        string key_generica = "1nt3rf4c3_3c4fr3tn1";

        private string GetType()
        {
            string sRegresa = "";
            DateTime dt = DateTime.Now;
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();

            sRegresa = string.Format("{3}{0}{1}{2}{3}{1}{0}{2}{3}{2}{0}{1}{3}",
                dt.Year, dt.Month, dt.Day, key_generica);

            sRegresa = Encrypt(sRegresa, true);

            return sRegresa;
        }

        private string Encrypt(string toEncrypt, bool useHashing)
        {
            string sRegresa = "";

            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                string key = key_generica;

                //System.Windows.Forms.MessageBox.Show(key);
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();

                sRegresa = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                sRegresa = "";
            }

            return sRegresa;
        }

        private bool GetType(string Type)
        {
            bool bRegresa = false;
            string sRegresa = GetType();

            bRegresa = sRegresa == Type;

            
            if (DtGeneral.EsEquipoDeDesarrollo) bRegresa = true;


            return bRegresa;
        }
        #endregion Seguridad

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
        #endregion Funciones y Procedimientos Estandard

        #region Funciones y Procedimientos Publicos
        /// <summary>
        /// Obtiene las Recetas Electrónicas generadas en el Recetario 
        /// </summary>
        /// <param name="CLUES"></param>
        /// <param name="TipoDocumento"></param>
        /// <returns></returns>
        [WebMethod(Description = "Obtener recetas electrónicas")]
        public DataSet GetRecetasElectronicas(string CLUES, string TipoDocumento)
        {
            DataSet dtsRegresa = new DataSet();

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                General.DatosConexion = datosCnn;
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                RecetaElectronica receta = new RecetaElectronica(datosCnn);

                dtsRegresa = receta.GetRecetas(CLUES, TipoDocumento);

            }
            catch (Exception ex)
            {
                //sRegresa = ex.Message;
                dtsRegresa = PrepararError(ex);
            }

            return dtsRegresa;
        }

        /// <summary>
        /// Obtiene las Recetas Electrónicas generadas en el Recetario 
        /// </summary>
        /// <param name="CLUES"></param>
        /// <param name="TipoDocumento"></param>
        /// <returns></returns>
        [WebMethod(Description = "Obtener información de receta electrónica ")]
        public string GetRecetaElectronica(string CLUES, string FolioDeReceta)
        {
            string sRegresa = "";

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                General.DatosConexion = datosCnn;
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                ClsReplicacioneRecetaElectronica receta = new ClsReplicacioneRecetaElectronica(datosCnn);

                receta.ObtenerRecetasElectronica_Especifica(CLUES, FolioDeReceta);

                sRegresa = receta.sFolioGeneral;
            }
            catch (Exception ex)
            {
                //sRegresa = ex.Message;
                sRegresa = "";
            }

            return sRegresa;
        }

        /// <summary>
        /// Envia los acuses de surtido de receta 
        /// </summary>
        /// <param name="IdEmpresa"></param>
        /// <param name="IdEstado"></param>
        /// <param name="IdFarmacia"></param> 
        /// <returns></returns>
        [WebMethod(Description = "Recepción de acuse de atención de recetas electrónicas")]
        public DataSet SendAcuseRecetasElectronicas(string CLUES, DataSet Informacion)
        {
            DataSet dtsRegresa = new DataSet();
            string sRegresa = "";
            string sInformacion_XML = "";

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                General.DatosConexion = datosCnn;
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                RecetaElectronica receta = new RecetaElectronica(datosCnn);

                dtsRegresa = receta.Guardar(CLUES, Informacion).GetDataSet();
            }
            catch (Exception ex)
            {
                //sRegresa = ex.Message;
                dtsRegresa = PrepararError(ex);
            }

            return dtsRegresa;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados

        private void GetLocalConfigurationKey()
        {
            string myKey = "";
            string myConnectString;
            string filePath = ""; //// General.UnidadSO + ":\\inetpub\\wwwroot\\WebService_Cnn\\" + General.ArchivoIni; // Config.ini";
            string BaseDir = AppDomain.CurrentDomain.BaseDirectory.ToString();
            int EqualPosition = 0;

            int lenWebServiceName = AppDomain.CurrentDomain.BaseDirectory.ToString().Length;
            filePath = BaseDir.Substring(0, lenWebServiceName) + "wsSII_INT_ISIADISSEP.ini"; //"Config.ini"           

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
                                if (myKey == "URL".ToUpper())
                                {
                                    sURL_wsSIGHO = myConnectString.Substring(EqualPosition + 1).Trim();
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
}
