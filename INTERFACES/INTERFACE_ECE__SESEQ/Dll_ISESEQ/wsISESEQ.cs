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
using Dll_ISESEQ.Informacion;
using Dll_ISESEQ.wsClases; 

namespace Dll_ISESEQ
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
    public class wsISESEQ
    {
        /// <summary>
        /// Proporciona funciones generales que se implementan en la clase
        /// </summary>
        basSeguridad funciones; // = new basSeguridad();
        clsCriptografo Cryp = new clsCriptografo();
        string sFileConexion = "SII_INT_ISESEQ";
        string sURL_wsSESEQ = "";
        string sCLUES_Default = "";
        string sCLUES_NombreUnidad_Default = ""; 

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

            try
            {
                if (DtGeneral.EsEquipoDeDesarrollo) bRegresa = true;
            }
            catch { }

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
        #endregion Funciones y Procedimientos Estandard

        #region Funciones y Procedimientos Publicos
        /// <summary>
        /// Obtiene y envia los Acuses de Surtido de Recetas
        /// </summary>
        /// <param name="IdEmpresa"></param>
        /// <param name="IdEstado"></param>
        /// <param name="IdFarmacia"></param>
        /// <param name="EnviarXML">[0 Solo mostrar xml generado |1 Enviar acuse y mostrar xml generado] </param>
        /// <returns></returns>
        [WebMethod(Description = "Obtener acuses de recetas electrónicas")]
        public string GetAcusesReceta(string IdEmpresa, string IdEstado, string IdFarmacia, int EnviarXML)
        {
            string sRegresa = "";
            bool bEnviarXML = EnviarXML == 1;
            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                General.DatosConexion = datosCnn; 
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                ResponseAcuseXML receta = new ResponseAcuseXML(datosCnn, IdEmpresa, IdEstado, IdFarmacia);

                //receta.IdEmpresa = IdEmpresa;
                //receta.IdEstado = IdEstado;
                //receta.IdFarmacia = IdFarmacia;

                if (!receta.EnviarAcusesReceta(bEnviarXML))
                {
                    sRegresa = "Ocurrio un error al generar el xml.";
                    sRegresa = "Ocurrio un error al generar el xml.\n\n";
                    sRegresa += receta.Respuesta_Acuse;
                }
                else 
                {
                    sRegresa = receta.Respuesta_Acuse; 
                }

            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa;
        }

        /// <summary>
        /// Obtiene y envia los Acuses de Cancelacion de Recetas
        /// </summary>
        /// <param name="IdEmpresa"></param>
        /// <param name="IdEstado"></param>
        /// <param name="IdFarmacia"></param>
        /// <param name="EnviarXML">[0 Solo mostrar xml generado |1 Enviar acuse y mostrar xml generado] </param>
        /// <returns></returns>
        [WebMethod(Description = "Obtener acuses de recetas electrónicas")]
        public string GetAcusesCancelacionReceta(string IdEmpresa, string IdEstado, string IdFarmacia, int EnviarXML)
        {
            string sRegresa = "";
            bool bEnviarXML = EnviarXML == 1;
            try
            {
                //DtGeneral.EmpresaConectada = IdEmpresa;
                //DtGeneral.EstadoConectado = IdEstado;
                //DtGeneral.FarmaciaConectada = IdFarmacia;

                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                General.DatosConexion = datosCnn;
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                ResponseAcuseXML receta = new ResponseAcuseXML(datosCnn, IdEmpresa, IdEstado, IdFarmacia);

                //receta.IdEmpresa = IdEmpresa;
                //receta.IdEstado = IdEstado;
                //receta.IdFarmacia = IdFarmacia;

                if (!receta.EnviarAcusesCancelacionReceta(bEnviarXML))
                {
                    sRegresa = "Ocurrio un error al generar el xml.\n\n";
                    sRegresa += receta.Respuesta_Acuse;
                }
                else
                {
                    sRegresa = receta.Respuesta_Acuse;
                }

            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa;
        }

        [WebMethod(Description = "Información de receta electrónica generada")]
        public string RecepcionDeRecetaElectronica(string Informacion_XML, string GUID)
        {
            ResponseRecetaElectronica respuesta = new ResponseRecetaElectronica();
            TipoProcesoReceta tpProceso = TipoProcesoReceta.Ninguno; 
            string sRegresa = "";

            Informacion_XML = Informacion_XML.Trim(); 
            ////clsGrabarError.LogFileError("RecepcionDeRecetaElectronica : " + Informacion_XML); 

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                RecetaElectronica receta = new RecetaElectronica(datosCnn);
                ColectivoElectronico colectivo = new ColectivoElectronico(datosCnn); 

                clsGrabarError.LogFileError("");
                clsGrabarError.LogFileError(Informacion_XML); 
                //clsGrabarError.LogFileError("RecepcionDeRecetaElectronica : " + datosCnn.CadenaConexion); 


                if (GetType(GUID))
                {
                    GetLocalConfigurationKey(); 

                    sRegresa = "WS001_" + GetType();
                    tpProceso = receta.TipoDeDocumento(Informacion_XML); 


                    if (
                        tpProceso == TipoProcesoReceta.Receta || tpProceso == TipoProcesoReceta.SurteReceta || 
                        tpProceso == TipoProcesoReceta.Colectivo || tpProceso == TipoProcesoReceta.ColectivoMedicamentos  
                        )
                    {
                        sRegresa = receta.Guardar(Informacion_XML).GetStringList();
                    }

                    ////if (tpProceso == TipoProcesoReceta.Colectivo || tpProceso == TipoProcesoReceta.ColectivoMedicamentos)
                    ////{
                    ////    colectivo.CLUES_Default = sCLUES_Default;
                    ////    colectivo.CLUES_NombreUnidad_Default = sCLUES_NombreUnidad_Default;
                    ////    sRegresa = colectivo.Guardar(Informacion_XML).GetStringList();
                    ////}

                }

            }
            catch (Exception ex)
            {
                respuesta.Estatus = 1;
                respuesta.Error = "Error al accesar la base de datos";
                sRegresa = respuesta.GetStringList();

                //sRegresa += ex.Message; 

                clsGrabarError.LogFileError("RecepcionDeRecetaElectronica : " + ex.Message); 
            }

            return sRegresa;
        }

        [WebMethod(Description = "Información de inventario")]
        public string DescargarInventario(string Informacion_XML, string GUID, int TipoUnidad)
        {
            ResponseRecetaElectronica respuesta = new ResponseRecetaElectronica();
            TipoProcesoReceta tpProceso = TipoProcesoReceta.Ninguno;
            string sRegresa = "";


            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                InformacionInventarios inventario = new InformacionInventarios(datosCnn);


                if (GetType(GUID))
                {
                    GetLocalConfigurationKey();

                    sRegresa = "WS001_" + GetType();

                    sRegresa = inventario.DescargarInventario(Informacion_XML, TipoUnidad).GetResponse();
                }

            }
            catch (Exception ex)
            {
                respuesta.Estatus = 1;
                respuesta.Error = "Error al accesar la base de datos" + ex.Message;
                sRegresa = respuesta.GetStringList();

                //sRegresa += ex.Message; 

                clsGrabarError.LogFileError("RecepcionDeRecetaElectronica : " + ex.Message);
            }

            return sRegresa;
        }

        [WebMethod(Description = "Acuse de surtido de receta electrónica")]
        public string AcuseSurtidoDeRecetaElectronica(string Informacion_XML)
        {
            string sRegresa = "";
            string sGUID = GetType();

            try
            {
                GetLocalConfigurationKey();

                wsAcuseProcesos_RE.ws_Cnn_ISESEQ web = new wsAcuseProcesos_RE.ws_Cnn_ISESEQ();
                web.Url = sURL_wsSESEQ;

                sRegresa = web.AcuseSurtidoDeRecetaElectronica(Informacion_XML, sGUID); 
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message; 
            }

            return sRegresa;
        }

        [WebMethod(Description = "Validar firma de entrega de colectivo")]
        public string ValidarFirmaDeEntregaColectivo(string Folio, string Firma)
        {
            string sRegresa = "";
            string sGUID = GetType();

            try
            {
                GetLocalConfigurationKey();

                wsAcuseProcesos_RE.ws_Cnn_ISESEQ web = new wsAcuseProcesos_RE.ws_Cnn_ISESEQ();
                web.Url = sURL_wsSESEQ;

                sRegresa = web.ValidarFirmaDeEntregaColectivo(Folio, Firma, sGUID);
            }
            catch (Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa;
        }

        [WebMethod(Description = "Acuse de digitalización")]
        public string AcuseDigitalizacion( string Informacion_XML )
        {
            string sRegresa = "";
            string sGUID = GetType();

            try
            {
                GetLocalConfigurationKey();

                wsAcuseProcesos_RE.ws_Cnn_ISESEQ web = new wsAcuseProcesos_RE.ws_Cnn_ISESEQ();
                web.Url = sURL_wsSESEQ;

                sRegresa = web.AcuseDigitalizacionRecetaElectronica(Informacion_XML, sGUID);
            }
            catch(Exception ex)
            {
                sRegresa = ex.Message;
            }

            return sRegresa;
        }

        [WebMethod(Description = "Cancelación de receta electrónica")]
        public string CancelacionDeRecetaElectronica(string Informacion_XML, string GUID)
        {
            ResponseRecetaElectronica respuesta = new ResponseRecetaElectronica();
            string sRegresa = "";

            clsGrabarError.LogFileError("CancelacionDeRecetaElectronica : " + Informacion_XML); 

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                RecetaElectronica_Cancelacion receta = new RecetaElectronica_Cancelacion(datosCnn);

                if (GetType(GUID))
                {
                    sRegresa = "WS002_" + GetType();
                    sRegresa = receta.Guardar(Informacion_XML).GetStringList(); 
                }

                ////sRegresa = consulta.Consultar(Id, Tipo, Plan, IdFarmacia, Ranking).GetString();

            }
            catch (Exception ex)
            {
                respuesta.Estatus = 1;
                respuesta.Error = "Error al accesar la base de datos";
                sRegresa = respuesta.GetStringList();
            }

            return sRegresa;
        }

        [WebMethod(Description = "Acuse de cancelación de receta electrónica")]
        public string AcuseDeCancelacionDeRecetaElectronica(string Informacion_XML)
        {
            string sRegresa = "";
            string sGUID = GetType();

            try
            {
                GetLocalConfigurationKey();

                wsAcuseProcesos_RE.ws_Cnn_ISESEQ web = new wsAcuseProcesos_RE.ws_Cnn_ISESEQ();
                web.Url = sURL_wsSESEQ;

                sRegresa = web.AcuseDeCancelacionDeRecetaElectronica(Informacion_XML, sGUID); 

            }
            catch (Exception ex)
            {
                sRegresa = ex.Message; 
            }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Publicos


        #region Funciones y Procedimientos Publicos ( Información de la operación) 
        [WebMethod(Description = "Enviar información de la operación de la unidad")]
        public string EnviarInformacionOperacion( string Informacion_XML, string GUID )
        {
            ResponseRecetaElectronica respuesta = new ResponseRecetaElectronica();
            TipoProcesoReceta tpProceso = TipoProcesoReceta.Ninguno;
            string sRegresa = "";


            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                InformacionInventarios inventario = new InformacionInventarios(datosCnn);


                if(GetType(GUID))
                {
                    GetLocalConfigurationKey();

                    sRegresa = "WS001_" + GetType();

                    sRegresa = inventario.DescargarInventario(Informacion_XML, 0).GetResponse();

                }

            }
            catch(Exception ex)
            {
                respuesta.Estatus = 1;
                respuesta.Error = "Error al accesar la base de datos" + ex.Message;
                sRegresa = respuesta.GetStringList();

                //sRegresa += ex.Message; 

                clsGrabarError.LogFileError("RecepcionDeRecetaElectronica : " + ex.Message);
            }

            return sRegresa;
        }
        #endregion Funciones y Procedimientos Publicos ( Información de la operación) 

        #region Funciones y Procedimientos Privados

        private void GetLocalConfigurationKey()
        {
            string myKey = "";
            string myConnectString;
            string filePath = ""; //// General.UnidadSO + ":\\inetpub\\wwwroot\\WebService_Cnn\\" + General.ArchivoIni; // Config.ini";
            string BaseDir = AppDomain.CurrentDomain.BaseDirectory.ToString();
            int EqualPosition = 0;

            int lenWebServiceName = AppDomain.CurrentDomain.BaseDirectory.ToString().Length;
            filePath = BaseDir.Substring(0, lenWebServiceName) + "wsSII_INT_ISESEQ.ini"; //"Config.ini"           

            sURL_wsSESEQ = sURL_wsSESEQ;
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
                                    sURL_wsSESEQ = myConnectString.Substring(EqualPosition + 1).Trim();
                                }

                                if (myKey == "CLUES".ToUpper())
                                {
                                    sCLUES_Default = myConnectString.Substring(EqualPosition + 1).Trim();
                                }
                              
                                if (myKey == "CLUES_UNIDAD".ToUpper())
                                {
                                    sCLUES_NombreUnidad_Default = myConnectString.Substring(EqualPosition + 1).Trim();
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

            myKey += "";
        }
        #endregion Funciones y Procedimientos Privados    
    }
}
