using System;
using System.Collections;
using System.Collections.Generic;
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
using Dll_INT_CNNSESEQ.wsRecetaElectronica_SESEQ;

//using Dll_INT_CNNSESEQ.ws_Interface_SII_SSESEQ

namespace Dll_INT_CNNSESEQ
{
    [WebService(Description = "Módulo Interface de Comunicación", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class ws_Cnn_ISESEQ
    {
        #region Seguridad
        string sFirma_GUID = "";
        string key_generica = "1nt3rf4c3_3c4fr3tn1";

        string sURL_wsSII = "";
        string sURL_wsSESEQ = "";
        string sURL_wsSESEQ_Colectivo = "";
        string sFileLog = "ISESEQ_Log.txt";


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

        private string Log(string Mensaje)
        {
            string sRegresa = ""; 

            try
            {

                string filePath = ""; //// General.UnidadSO + ":\\inetpub\\wwwroot\\WebService_Cnn\\" + General.ArchivoIni; // Config.ini";
                string BaseDir = AppDomain.CurrentDomain.BaseDirectory.ToString();

                int lenWebServiceName = AppDomain.CurrentDomain.BaseDirectory.ToString().Length;
                filePath = BaseDir.Substring(0, lenWebServiceName) + sFileLog; // "wsSII_CNN_ISESEQ.ini"; //"Config.ini"          


                StreamWriter file = new System.IO.StreamWriter(filePath, true); 
                file.WriteLine(string.Format("HORA: {0} \t\t\t Mensaje: {1}"), DateTime.Now.ToString(), Mensaje);

                file.Close();
                file = null;
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa; 
        }
        #endregion Seguridad

        #region Funciones y Procedimientos Publicos
        /// <summary>
        /// Recepción de receta electrónica ( SESEQ - SII ) 
        /// </summary>
        /// <param name="Informacion_XML">Información en formato XML</param>
        /// <returns>Resultado</returns>
        [WebMethod(Description = "Información de inventario")]
        public string DescargarInventario(string Informacion_XML, int TipoUnidad)
        {
            string sRegresa = "";
            string sGUID = GetType();
            string sLog = "";

            sLog = Log(Informacion_XML);
            try
            {
                GetLocalConfigurationKey();

                ws_Interface_SII_SESEQ.wsISESEQ web = new ws_Interface_SII_SESEQ.wsISESEQ();
                web.Url = sURL_wsSII;

                sRegresa = web.DescargarInventario(Informacion_XML, sGUID, TipoUnidad);
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa;
        }


        /// <summary>
        /// Recepción de receta electrónica ( SESEQ - SII ) 
        /// </summary>
        /// <param name="Informacion_XML">Información en formato XML</param>
        /// <returns>Resultado</returns>
        [WebMethod(Description = "Información de receta electrónica generada")]
        public string RecepcionDeRecetaElectronica(string Informacion_XML)
        {
            string sRegresa = "";
            string sGUID = GetType();
            string sLog = "";

            sLog = Log(Informacion_XML); 
            try
            {
                GetLocalConfigurationKey();

                ws_Interface_SII_SESEQ.wsISESEQ web = new ws_Interface_SII_SESEQ.wsISESEQ();
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
        /// Aviso de atención de receta electrónica ( SII - SESEQ )
        /// </summary>
        /// <param name="Informacion_XML">Información en formato XML</param>
        /// <param name="GUID">Identificador</param>
        /// <returns>Resultado</returns>
        [WebMethod(Description = "Acuse de surtido de receta electrónica")]
        public string AcuseSurtidoDeRecetaElectronica(string Informacion_XML, string GUID)
        {
            string sRegresa = "";
            string sGUID = GetType();
            DataSet dtsInformacion = new DataSet();

            List<datos_entrada> listaDeSurtido = new List<datos_entrada>();
            datos_entrada[] datosDeSurtido = null;
            datos_entrada datos = new datos_entrada(); 
            datos_salida respuesta = new datos_salida();

            List<wsColectivoElectronico_SESEQ.datos_entrada> listaDeSurtidoColectivo = new List<wsColectivoElectronico_SESEQ.datos_entrada>();
            wsColectivoElectronico_SESEQ.datos_entrada[] datosSurtidoColectivo = null;
            wsColectivoElectronico_SESEQ.datos_entrada datosColectivo = new wsColectivoElectronico_SESEQ.datos_entrada();
            wsColectivoElectronico_SESEQ.datos_salida respuestaColectivo = new wsColectivoElectronico_SESEQ.datos_salida();

            string []items = Informacion_XML.Split('|');
            string sTipoDePeticion = "";
            bool bContinuar = true; 

            Log(Informacion_XML); 
            try
            {
                if (GetType(GUID))
                {
                    GetLocalConfigurationKey();

                    dtsInformacion.ReadXml(new XmlTextReader(new StringReader(Informacion_XML)));

                    if (dtsInformacion.Tables.Count == 0)
                    {
                        sRegresa = "No existe información para enviar.";
                        bContinuar = false;
                    }

                    if (bContinuar)
                    {
                        if (dtsInformacion.Tables[0].Rows.Count == 0)
                        {
                            sRegresa = "No existe información para enviar.";
                            bContinuar = false; 
                        }
                    }

                    if (bContinuar)
                    {
                        sTipoDePeticion = dtsInformacion.Tables[0].Rows[0]["TipoDePeticion"].ToString();

                        foreach (DataRow dtRow in dtsInformacion.Tables[0].Rows)
                        {
                            if(sTipoDePeticion == "1")
                            {
                                datos = new datos_entrada();

                                datos.noReceta = dtRow["noReceta"].ToString();
                                datos.fechaSurtido = dtRow["fechaSurtido"].ToString();
                                datos.clave = dtRow["clave"].ToString();
                                datos.surtido = dtRow["surtido"].ToString();

                                datos.lote = dtRow["lote"].ToString();
                                datos.caducidad = dtRow["caducidad"].ToString();

                                listaDeSurtido.Add(datos);
                            }


                            if (sTipoDePeticion == "2")
                            {
                                datosColectivo = new wsColectivoElectronico_SESEQ.datos_entrada();

                                datosColectivo.folio = dtRow["noReceta"].ToString();
                                datosColectivo.fechaSurtido = dtRow["fechaSurtido"].ToString();
                                datosColectivo.clave = dtRow["clave"].ToString();
                                datosColectivo.surtido = dtRow["surtido"].ToString();

                                datosColectivo.lote = dtRow["lote"].ToString();
                                datosColectivo.caducidad = dtRow["caducidad"].ToString();

                                listaDeSurtidoColectivo.Add(datosColectivo);
                            }
                        }

                        if (sTipoDePeticion == "1")
                        {
                            datosDeSurtido = listaDeSurtido.ToArray();

                            wsRecetaElectronica_SESEQ.WebServiceSESEQ web = new wsRecetaElectronica_SESEQ.WebServiceSESEQ();
                            web.Url = sURL_wsSESEQ;
                            respuesta = web.recepcionSurtidoFarmacia(datosDeSurtido);

                            sRegresa = respuesta.mensaje;
                        }

                        if (sTipoDePeticion == "2")
                        {
                            datosSurtidoColectivo = listaDeSurtidoColectivo.ToArray();

                            wsColectivoElectronico_SESEQ.WebServiceSESEQ web = new wsColectivoElectronico_SESEQ.WebServiceSESEQ();
                            web.Url = sURL_wsSESEQ_Colectivo;
                            respuestaColectivo = web.recepcionSolicitudColectivo(datosSurtidoColectivo);

                            sRegresa = respuestaColectivo.mensaje;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa;
        }

        /// <summary>
        /// Aviso de atención de receta electrónica ( SII - SESEQ )
        /// </summary>
        /// <param name="Informacion_XML">Información en formato XML</param>
        /// <param name="GUID">Identificador</param>
        /// <returns>Resultado</returns>
        [WebMethod(Description = "Acuse de surtido de receta electrónica")]
        public string AcuseDigitalizacionRecetaElectronica( string Informacion_XML, string GUID )
        {
            string sRegresa = "";
            string sGUID = GetType();
            DataSet dtsInformacion = new DataSet();

            List<datos_imagen> listaDeSurtido = new List<datos_imagen>();
            datos_imagen[] datosDeSurtido = null;
            datos_imagen datos = new datos_imagen();
            datos_salida respuesta = new datos_salida();

            List<wsColectivoElectronico_SESEQ.datos_imagen> listaDeSurtidoColectivo = new List<wsColectivoElectronico_SESEQ.datos_imagen>();
            wsColectivoElectronico_SESEQ.datos_imagen[] datosSurtidoColectivo = null;
            wsColectivoElectronico_SESEQ.datos_imagen datosColectivo = new wsColectivoElectronico_SESEQ.datos_imagen();
            wsColectivoElectronico_SESEQ.datos_salida respuestaColectivo = new wsColectivoElectronico_SESEQ.datos_salida();

            string[] items = Informacion_XML.Split('|');
            string sTipoDePeticion = "";
            bool bContinuar = true;

            Log(Informacion_XML);
            try
            {
                if (GetType(GUID))
                {
                    GetLocalConfigurationKey();

                    dtsInformacion.ReadXml(new XmlTextReader(new StringReader(Informacion_XML)));

                    if (dtsInformacion.Tables.Count == 0)
                    {
                        sRegresa = "No existe información para enviar.";
                        bContinuar = false;
                    }

                    if (bContinuar)
                    {
                        if (dtsInformacion.Tables[0].Rows.Count == 0)
                        {
                            sRegresa = "No existe información para enviar.";
                            bContinuar = false;
                        }
                    }

                    if ( bContinuar )
                    {
                        sTipoDePeticion = dtsInformacion.Tables[0].Rows[0]["TipoDePeticion"].ToString();

                        foreach (DataRow dtRow in dtsInformacion.Tables[0].Rows)
                        {
                            if (sTipoDePeticion == "1")
                            {
                                datos = new datos_imagen();

                                datos.noReceta = dtRow["noReceta"].ToString();
                                datos.idImagen = dtRow["idImagen"].ToString();
                                datos.tipoImagen = dtRow["tipoImagen"].ToString();
                                datos.imagenB64 = dtRow["imagenB64"].ToString();

                                listaDeSurtido.Add(datos);
                            }

                            if (sTipoDePeticion == "2")
                            {
                                datosColectivo = new wsColectivoElectronico_SESEQ.datos_imagen();

                                datosColectivo.noReceta = dtRow["noReceta"].ToString();
                                datosColectivo.idImagen = dtRow["idImagen"].ToString();
                                datosColectivo.tipoImagen = dtRow["tipoImagen"].ToString();
                                datosColectivo.imagenB64 = dtRow["imagenB64"].ToString();

                                listaDeSurtidoColectivo.Add(datosColectivo);
                            }

                        }


                        if (sTipoDePeticion == "1")
                        {
                            datosDeSurtido = listaDeSurtido.ToArray();

                            wsRecetaElectronica_SESEQ.WebServiceSESEQ web = new wsRecetaElectronica_SESEQ.WebServiceSESEQ();
                            web.Url = sURL_wsSESEQ;
                            respuesta = web.recepcionImagenFarmacia(datosDeSurtido);

                            sRegresa = respuesta.mensaje;
                        }

                        if (sTipoDePeticion == "2")
                        {
                            datosSurtidoColectivo = listaDeSurtidoColectivo.ToArray();

                            wsColectivoElectronico_SESEQ.WebServiceSESEQ web = new wsColectivoElectronico_SESEQ.WebServiceSESEQ();
                            web.Url = sURL_wsSESEQ_Colectivo;
                            respuestaColectivo = web.recepcionImagenColectivo(datosSurtidoColectivo);

                            sRegresa = respuestaColectivo.mensaje;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa;
        }

        [WebMethod(Description = "Validación de Recepción de colectivos")]
        public string ValidarFirmaDeEntregaColectivo(string Folio, string Firma, string GUID)
        {
            string sRegresa = "";
            string sGUID = GetType();

            List<wsColectivoElectronico_SESEQ.datos_validar_recepcion> listaDeColectivos = new List<wsColectivoElectronico_SESEQ.datos_validar_recepcion>();
            wsColectivoElectronico_SESEQ.datos_validar_recepcion[] datosSurtidoColectivo = null;
            wsColectivoElectronico_SESEQ.datos_validar_recepcion datosColectivo = new wsColectivoElectronico_SESEQ.datos_validar_recepcion();
            wsColectivoElectronico_SESEQ.datos_salida respuestaColectivo = new wsColectivoElectronico_SESEQ.datos_salida();

            //string[] items = Informacion_XML.Split('|');
            string sTipoDePeticion = "";
            bool bContinuar = true;

            try
            {
                if (GetType(GUID))
                {
                    GetLocalConfigurationKey();

                    datosColectivo = new wsColectivoElectronico_SESEQ.datos_validar_recepcion();
                    datosColectivo.folio = Folio;
                    datosColectivo.firma = Firma;
                    listaDeColectivos.Add(datosColectivo);

                    datosSurtidoColectivo = listaDeColectivos.ToArray();

                    wsColectivoElectronico_SESEQ.WebServiceSESEQ web = new wsColectivoElectronico_SESEQ.WebServiceSESEQ();
                    web.Url = sURL_wsSESEQ_Colectivo;
                    respuestaColectivo = web.validarRecepcionColectivo(datosSurtidoColectivo);

                    sRegresa = respuestaColectivo.mensaje;
                }
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa;
        }


        /// <summary>
        /// Solicitud de cancelación de receta electrónica ( SESEQ - SII )
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

                ws_Interface_SII_SESEQ.wsISESEQ web = new ws_Interface_SII_SESEQ.wsISESEQ();
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
        /// Enviar aviso de cancelación de receta electrónica ( SII - SESEQ )
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
                    ////web.Url = sURL_wsSESEQ;
                    ////sRegresa = web.reciberespuesta(Informacion_XML);


                    wsRecetaElectronica_SSEP__20.RecieveXMLService web_20 = new wsRecetaElectronica_SSEP__20.RecieveXMLService();
                    web_20.Url = sURL_wsSESEQ;
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
            filePath = BaseDir.Substring(0, lenWebServiceName) + "wsSII_CNN_ISESEQ.ini"; //"Config.ini"           

            sURL_wsSII = "";
            sURL_wsSESEQ = "";
            sURL_wsSESEQ_Colectivo = "";

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

                                #region SESEQ 
                                if (myKey == "URL_REMOTA".ToUpper())
                                {
                                    sURL_wsSESEQ = myConnectString.Substring(EqualPosition + 1).Trim();
                                }

                                if (myKey == "URL_REMOTA_COLECTIVO".ToUpper())
                                {
                                    sURL_wsSESEQ_Colectivo = myConnectString.Substring(EqualPosition + 1).Trim();
                                }
                                #endregion SESEQ 
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

            if (sURL_wsSESEQ_Colectivo == "") sURL_wsSESEQ_Colectivo = sURL_wsSESEQ; 
        }
        #endregion Funciones y Procedimientos Privados    
    }
}
