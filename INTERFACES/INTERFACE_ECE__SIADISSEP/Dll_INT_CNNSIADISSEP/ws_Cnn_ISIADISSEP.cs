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

using System.Text;
using System.IO;
using System.Configuration; 

using Microsoft.VisualBasic;

namespace Dll_INT_CNNSIADISSEP
{
    [WebService(Description = "Módulo Interface de Comunicación", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class ws_Cnn_ISIADISSEP
    {
        #region Seguridad
        string sFirma_GUID = "";
        string key_generica = "1nt3rf4c3_3c4fr3tn1";

        string sURL_wsSII = "";
        string sURL_wsSIADISSEP = "";
        string sFileLog = "ISIADISSEP_Log.txt";


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

        private bool GetType(string Type)
        {
            bool bRegresa = false;
            string sRegresa = GetType();

            bRegresa = sRegresa == Type;

            bRegresa = true; 

            return bRegresa;
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

        private void Log(string Mensaje)
        {
            try
            {

                string filePath = ""; //// General.UnidadSO + ":\\inetpub\\wwwroot\\WebService_Cnn\\" + General.ArchivoIni; // Config.ini";
                string BaseDir = AppDomain.CurrentDomain.BaseDirectory.ToString();

                int lenWebServiceName = AppDomain.CurrentDomain.BaseDirectory.ToString().Length;
                filePath = BaseDir.Substring(0, lenWebServiceName) + sFileLog; // "wsSII_CNN_ISIADISSEP.ini"; //"Config.ini"          


                StreamWriter file = new System.IO.StreamWriter(filePath, true); 
                file.WriteLine(string.Format("HORA: {0} \t\t\t Mensaje: {1}"), DateTime.Now.ToString(), Mensaje);

                file.Close();
                file = null;
            }
            catch { }
        }
        #endregion Seguridad

        #region Funciones y Procedimientos Publicos
        /// <summary>
        /// Recepción de receta electrónica ( SIADISSEP - SII ) 
        /// </summary>
        /// <param name="Informacion_XML">Información en formato XML</param>
        /// <returns>Resultado</returns>
        [WebMethod(Description = "Información de receta electrónica generada")]
        public string RecepcionDeRecetaElectronica(string Informacion_XML)
        {
            string sRegresa = "";
            string sGUID = GetType();

            Log(Informacion_XML); 
            try
            {
                GetLocalConfigurationKey();

                ws_Interface_SII_SIADISSEP.wsISIADISSEP web = new ws_Interface_SII_SIADISSEP.wsISIADISSEP();
                web.Url = sURL_wsSII; 

                sRegresa = web.RecepcionDeRecetaElectronica(Informacion_XML, sGUID);
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa;
        }

        /// <summary>
        /// Aviso de atención de receta electrónica ( SII - SIADISSEP )
        /// </summary>
        /// <param name="Informacion_XML">Información en formato XML</param>
        /// <param name="GUID">Identificador</param>
        /// <returns>Resultado</returns>
        [WebMethod(Description = "Acuse de surtido de receta electrónica")]
        public string AcuseSurtidoDeRecetaElectronica(string Informacion_XML, string GUID)
        {
            string sRegresa = "";
            string sGUID = GetType();

            Log(Informacion_XML); 
            try
            {
                if (GetType(GUID))
                {
                    GetLocalConfigurationKey();

                    ////wsRecetaElectronica_SSEP.WsPrueba web = new wsRecetaElectronica_SSEP.WsPrueba();
                    ////web.Url = sURL_wsSIADISSEP;
                    ////sRegresa = web.reciberespuesta(Informacion_XML);


                    wsRecetaElectronica_SSEP__20.RecieveXMLService web_20 = new wsRecetaElectronica_SSEP__20.RecieveXMLService();
                    web_20.Url = sURL_wsSIADISSEP;
                    sRegresa = web_20.recieve(Informacion_XML);
                }
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa;
        }

        /// <summary>
        /// Solicitud de cancelación de receta electrónica ( SIADISSEP - SII )
        /// </summary>
        /// <param name="Informacion_XML">Información en formato XML</param>
        /// <returns>Resultado</returns>
        [WebMethod(Description = "Cancelación de receta electrónica")]
        public string CancelacionDeRecetaElectronica(string Informacion_XML)
        {
            string sRegresa = "";
            string sGUID = GetType();

            Log(Informacion_XML); 
            try
            {
                GetLocalConfigurationKey();

                ws_Interface_SII_SIADISSEP.wsISIADISSEP web = new ws_Interface_SII_SIADISSEP.wsISIADISSEP();
                web.Url = sURL_wsSII; 
                sRegresa = web.CancelacionDeRecetaElectronica(Informacion_XML, sGUID);
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa;
        }

        /// <summary>
        /// Enviar aviso de cancelación de receta electrónica ( SII - SIADISSEP )
        /// </summary>
        /// <param name="Informacion_XML">Información en formato XML</param>
        /// <param name="GUID">Identificador</param>
        /// <returns>Resultado</returns>
        [WebMethod(Description = "Acuse de cancelación de receta electrónica")]
        public string AcuseDeCancelacionDeRecetaElectronica(string Informacion_XML, string GUID)
        {
            string sRegresa = "";
            string sGUID = GetType();

            Log(Informacion_XML); 
            try
            {
                if (GetType(GUID))
                {
                    GetLocalConfigurationKey();

                    ////wsRecetaElectronica_SSEP.WsPrueba web = new wsRecetaElectronica_SSEP.WsPrueba();
                    ////web.Url = sURL_wsSIADISSEP;
                    ////sRegresa = web.reciberespuesta(Informacion_XML);


                    wsRecetaElectronica_SSEP__20.RecieveXMLService web_20 = new wsRecetaElectronica_SSEP__20.RecieveXMLService();
                    web_20.Url = sURL_wsSIADISSEP;
                    sRegresa = web_20.recieve(Informacion_XML);

                }
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa;
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
            filePath = BaseDir.Substring(0, lenWebServiceName) + "wsSII_CNN_ISIADISSEP.ini"; //"Config.ini"           

            sURL_wsSII = "";
            sURL_wsSIADISSEP = ""; 
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
                                    sURL_wsSII = myConnectString.Substring(EqualPosition + 1).Trim();
                                }

                                if (myKey == "URL_REMOTA".ToUpper())
                                {
                                    sURL_wsSIADISSEP = myConnectString.Substring(EqualPosition + 1).Trim();
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
