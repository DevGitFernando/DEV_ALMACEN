﻿using System;
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
using System.Text;
using System.Reflection;
using System.Security.Cryptography;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft; 

// using DllFarmaciaSoft.wsFarmaciaSoftGn;
using DllTransferenciaSoft.ObtenerInformacion;
using DllTransferenciaSoft;
using DllTransferenciaSoft.Zip;
using DllTransferenciaSoft.IntegrarInformacion;  


namespace DllTransferenciaSoft
{
    [WebService(Description = "Modulo información", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsCnnOficinaCentral : DllFarmaciaSoft.wsConexion
    {
        //[WebMethod(Description = "Probar conexión al servidor")]
        //public bool TestConection()
        //{
        //    return true;
        //}

        #region Funciones y Procedimientos Privados
        [WebMethod(Description = "Iniciar el Servicio de Transferencias.")]
        private int IniciarServicio(string Servicio)
        {
            int iRegresa = 0;

            try
            {
                string sTituloError = "IniciarServicio";
                string sRuta = "";
                string sSql = " Select * From Net_CFGS_Respaldos (NoLock) ";
                string ArchivoCgf = "OficinaCentralRI";

                DllFarmaciaSoft.wsConexion Datos = new DllFarmaciaSoft.wsConexion();
                clsDatosConexion datosCnn = new clsDatosConexion(Datos.ConexionEx(ArchivoCgf));
                clsGrabarError Error = new clsGrabarError(datosCnn, Transferencia.DatosApp, "wsCnnCliente");

                datosCnn.Servidor = SetServidor(datosCnn.Servidor); 
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsLeer leer = new clsLeer(ref cnn);

                if (!leer.Exec(sSql))
                {
                    Error.GrabarError(leer, sTituloError);
                    Error.LogError(leer.Error.Message);
                    iRegresa = 1;
                }
                else
                {
                    if (!leer.Leer())
                    {
                        Error.GrabarError("Ruta No Configurada.", sTituloError);
                        Error.LogError("Ruta No Configurada.");
                        iRegresa = 2;
                    }
                    else
                    {
                        sRuta = leer.Campo("RutaDeArchivosDeSistema") + @"\" + "Servicio Oficina Central.exe";

                        if (!General.ProcesoEnEjecucion(sRuta))
                        {
                            // Iniciar el servicio detenido 
                            Process svr = new Process();
                            svr.StartInfo.FileName = sRuta;
                            svr.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            svr.Start();
                            iRegresa = 3;
                        }
                    }
                }

            }
            catch { }

            return iRegresa;
        }

        //[WebMethod(Description = "Descargar información")]
        private DataSet DescargarInformacionCatalogos(string IdEstado, string IdFarmacia, bool EsGeneral, string Tipo, DataSet ListaCatalogos)
        {
            //// DataSet ListaCatalogos
            clsCriptografo Cryp = new clsCriptografo();
            DataSet dtsCatalogos = new DataSet("Informacion");
            DataTable dtTabla = new DataTable("DatosDescarga");
            DataRow dtRow;
            basGenerales Fg = new basGenerales();

            // string sRegresa = "xxx";
            string sFarmaciaSolicita = IdFarmacia;
            byte[] btArchivo = null;
            TipoServicio tipoDeServicio = TipoServicio.Ninguno;

            string sServidor = "";
            string sUsuario = "";
            string sPassword = "";
            string sRutaFTP = "";


            if (Tipo == "1")
            {
                tipoDeServicio = TipoServicio.OficinaCentralRegional;
                //// Siempre enviar 1 se suplantara al Servidor Regional 
                //IdFarmacia = "0001";
            }

            if (Tipo == "2")
            {
                tipoDeServicio = TipoServicio.OficinaCentral;
            }

            clsDatosConexion datosCnn = new clsDatosConexion(base.ConexionEx(DtGeneral.CfgIniOficinaCentral));
            datosCnn.Servidor = SetServidor(datosCnn.Servidor);

            clsObtenerInformacion cliente = new clsObtenerInformacion(DtGeneral.CfgIniOficinaCentral, datosCnn,
                DtGeneral.IdOficinaCentral, DtGeneral.IdFarmaciaCentral, tipoDeServicio,
                IdEstado, IdFarmacia, sFarmaciaSolicita, ListaCatalogos);

            try
            {

                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsLeer leer = new clsLeer(ref cnn);
                string sSql = string.Format("Select IdEstado, ServidorFTP, UserFTP, PasswordFTP, DirectorioDeTrabajo, Status " +
                    " From CFGS_ConfigurarFTP_Catalogos (NoLock) Where IdEstado = '{0}'  ", IdEstado);

                if (!leer.Exec(sSql))
                {
                    dtsCatalogos = leer.ListaDeErrores();
                    clsGrabarError.LogFileError(leer.MensajeError, FileAttributes.Normal);
                }
                else
                {
                    if (leer.Leer())
                    {
                        sServidor = leer.Campo("ServidorFTP");
                        sUsuario = leer.Campo("UserFTP");
                        sPassword = leer.Campo("PasswordFTP");
                        sRutaFTP = leer.Campo("DirectorioDeTrabajo"); // +@"\\" + Fg.PonCeros(IdFarmacia, 4); 

                        cliente.SoloEstadoEspecificado = EsGeneral;
                        cliente.EnviarA_FTP = true;
                        cliente.RutaFTP = sRutaFTP;
                        cliente.GenerarArchivos();
                        //btArchivo = cliente.ArchivoDeCatalogos();

                        dtTabla.Columns.Add("Campo1", System.Type.GetType("System.String"));
                        dtTabla.Columns.Add("Campo2", System.Type.GetType("System.String"));
                        dtTabla.Columns.Add("Campo3", System.Type.GetType("System.String"));
                        dtTabla.Columns.Add("Campo4", System.Type.GetType("System.String"));

                        object[] objRow = { 
                                              Cryp.Encriptar(sServidor), 
                                              Cryp.Encriptar(sUsuario), 
                                              sPassword, 
                                              Cryp.Encriptar(cliente.ArchivoGenerado_FTP) 
                                          };
                        dtTabla.Rows.Add(objRow);
                        dtsCatalogos.Tables.Add(dtTabla.Copy());
                    }
                }

                // sRegresa = btArchivo.Length.ToString(); 
            }
            catch (Exception ex1)
            {
                ex1.Source = ex1.Source;
                // clsGrabarError.LogFileError(ex1.Message); 
            }

            return dtsCatalogos;
        }

        [WebMethod(Description = "Obtener catalogos.")]
        private string CatalogosTest(string IdEstado, string IdFarmacia, string Tipo)
        {
            // DataSet ListaCatalogos
            DataSet dtsCatalogos = new DataSet();
            string sRegresa = "xxx";
            // byte[] btArchivo = null;
            TipoServicio tipoDeServicio = TipoServicio.Ninguno;


            tipoDeServicio = TipoServicio.OficinaCentral;
            if (Tipo == "2")
                tipoDeServicio = TipoServicio.OficinaCentralRegional;


            clsDatosConexion datosCnn = new clsDatosConexion(base.ConexionEx(DtGeneral.CfgIniOficinaCentral));
            datosCnn.Servidor = SetServidor(datosCnn.Servidor);

            clsObtenerInformacion cliente = new clsObtenerInformacion(DtGeneral.CfgIniOficinaCentral, datosCnn,
                DtGeneral.IdOficinaCentral, DtGeneral.IdFarmaciaCentral, tipoDeServicio,
                IdEstado, IdFarmacia, IdFarmacia, dtsCatalogos);

            try
            {
                cliente.GenerarArchivos();
                // cliente.ArchivoDeCatalogos(); 
                sRegresa = cliente.ArchivoGenerado;
            }
            catch (Exception ex1)
            {
                sRegresa = ex1.Message;
            }

            ////try
            ////{
            ////    btArchivo = cliente.ArchivoDeCatalogos();
            ////    sRegresa = btArchivo.Length.ToString(); 
            ////}
            ////catch (Exception ex1)
            ////{
            ////    sRegresa = " ............  " + ex1.Message;
            ////}

            return sRegresa;
        }

        private void Registrar_ResultadoIntegracion(clsDatosConexion DatosConexion, 
            string IdEstado, string IdFarmacia, 
            string NombreArchivo, bool Resultado)
        {
            clsConexionSQL cnn = new clsConexionSQL(DatosConexion);
            clsLeer leer = new clsLeer(ref cnn);

            string sIdEstado = IdEstado;
            string sIdFarmacia = IdFarmacia;
            string sSql = "";

            sSql = string.Format("Insert Into CFG_RegistroIntegracionBD_Archivos  ( IdEstado, IdFarmacia, NombreArchivo, Resultado, FechaRegistro ) \n " + 
                " Select '{0}', '{1}', '{2}', '{3}', getdate() ", 
                sIdEstado, sIdFarmacia, NombreArchivo, Convert.ToInt32(Resultado));

            if (!leer.Exec(sSql))
            {
                clsGrabarError.LogFileError(string.Format("Registrar_ResultadoIntegracion : {0}", leer.MensajeError));
            }

        }

        private Encoding GetEncoder()
        {
            System.Text.Encoding codificacion = Encoding.Default;
            string myKey = "";
            string sValor = ""; 
            string myConnectString;
            string filePath = ""; //// General.UnidadSO + ":\\inetpub\\wwwroot\\WebService_Cnn\\" + General.ArchivoIni; // Config.ini";
            string BaseDir = AppDomain.CurrentDomain.BaseDirectory.ToString();
            int EqualPosition = 0;

            int lenWebServiceName = AppDomain.CurrentDomain.BaseDirectory.ToString().Length;
            filePath = BaseDir.Substring(0, lenWebServiceName) + "SII_Encode.ini"; //"Config.ini"           

            if (File.Exists(filePath))
            {
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
                                    if (myKey == "Encode".ToUpper())
                                    {
                                        sValor = myConnectString.Substring(EqualPosition + 1).Trim().ToUpper();
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

            switch (sValor)
            {
                case "ASCII":
                    codificacion = Encoding.ASCII;
                    break;

                case "UTF32":
                    codificacion = Encoding.UTF32;
                    break;

                case "UTF7":
                    codificacion = Encoding.UTF7;
                    break;

                case "UTF8":
                    codificacion = Encoding.UTF8;
                    break;

                case "DEFAULT":
                    codificacion = Encoding.Default;
                    break;

                default:
                    codificacion = Encoding.Default;
                    break; 
            }

            return codificacion; 
        }
        #endregion Funciones y Procedimientos Privados

        #region Funciones y Procedimientos Publicos
        [WebMethod(Description = "Recibir información de Farmacias a Oficina Central.")]
        public bool Informacion(string ArchivoCgf, string NombreArchivo, byte []Archivo)
        {
            bool bRegresa = false;

            try 
            {

                string sRuta = "";
                DllFarmaciaSoft.wsConexion Datos = new DllFarmaciaSoft.wsConexion();
                clsDatosConexion datosCnn = new clsDatosConexion(Datos.ConexionEx(ArchivoCgf));
                clsGrabarError Error = new clsGrabarError(datosCnn, Transferencia.DatosApp, "wsCnnOficinaCentral");

                datosCnn.Servidor = SetServidor(datosCnn.Servidor);
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsLeer leer = new clsLeer(ref cnn);

                cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.Limite30; 
                try
                {
                    string sSql = " Select * From CFGSC_ConfigurarIntegracion (NoLock) ";
                    if (!leer.Exec(sSql))
                    {
                        Error.LogError(leer.Error.Message);
                        Error.GrabarError(leer, "EnviarArchivo");
                    }
                    else
                    {
                        if (!leer.Leer())
                        {
                            Error.GrabarError("No se Encontro la Información de Configuración de Integración.", "EnviarArchivo");
                        }
                        else 
                        {
                            sRuta = leer.Campo("RutaArchivosRecibidos");
                            File.WriteAllBytes(sRuta + @"\\" + NombreArchivo, Archivo);
                            bRegresa = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.LogError(ex.Message);
                    Error.GrabarError(ex, "EnviarArchivo");
                }
            }
            catch (Exception ex)
            {
                General.Error.LogError(ex.Message);
            }            
            return bRegresa;
        }

        [WebMethod(Description = "Replicación de informacion.")]
        public bool ReplicacionInformacion(string NombreArchivo, byte[] Archivo)
        {
            bool bRegresa = false;
            ZipUtil zip = new ZipUtil();

            string sFile_Ini = "Replicacion SQL";
            string sDirectorio = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\\ReplicacionSQL";
            string sDirectorio_Integrados = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\\ReplicacionSQL_Integrados";
            string sFileName = Guid.NewGuid().ToString() + "";
            string sFileIntegracion = "";
            string sIdEstado = "";
            string sIdFarmacia = ""; 

            basGenerales Fg = new basGenerales();
            DateTime dFechaRecepcion = DateTime.Now;
            string sFechaRecepcion = string.Format(@"{0}-{1}-{2}\{3}\\",
                Fg.PonCeros(dFechaRecepcion.Year, 4), Fg.PonCeros(dFechaRecepcion.Month, 2),
                Fg.PonCeros(dFechaRecepcion.Day, 2), Fg.PonCeros(dFechaRecepcion.Hour, 2)); 


            try
            {
                Encoding encode = GetEncoder(); 

                sIdEstado = Fg.Mid(NombreArchivo, 1, 2);
                sIdFarmacia = Fg.Mid(NombreArchivo, 3, 4);
                sFileName = sFileName.Replace("_", "").Replace("-", "");

                string sRuta = "";
                DllFarmaciaSoft.wsConexion Datos = new DllFarmaciaSoft.wsConexion();
                clsDatosConexion datosCnn = new clsDatosConexion(Datos.ConexionEx(sFile_Ini));
                clsGrabarError Error = new clsGrabarError(datosCnn, Transferencia.DatosApp, "wsCnnOficinaCentral");

                datosCnn.Servidor = SetServidor(datosCnn.Servidor);
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsLeer leer = new clsLeer(ref cnn);
                DllTransferenciaSoft.IntegrarInformacion.clsCliente cliente = new DllTransferenciaSoft.IntegrarInformacion.clsCliente(datosCnn, encode);


                cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.Limite30;

                sFileName = Fg.Left(NombreArchivo, 6);
                sFileName += "____" + NombreArchivo.ToUpper().Replace(".", "").Replace("SII", "");
                ////sFileName = sFileName.Replace("_", "").Replace("-", "");


                sDirectorio += @"\\" + sFechaRecepcion;
                sDirectorio = Path.Combine(sDirectorio, sFileName);
                if (!Directory.Exists(sDirectorio))
                {
                    Directory.CreateDirectory(sDirectorio);
                }

                if (!Directory.Exists(sDirectorio_Integrados))
                {
                    Directory.CreateDirectory(sDirectorio_Integrados);
                }
                 

                try
                {
                    sFileIntegracion = Path.Combine(sDirectorio, NombreArchivo);
                    File.WriteAllBytes(sDirectorio + @"\\" + NombreArchivo, Archivo);

                    sFileIntegracion = NombreArchivo.Replace("SII", "").Replace(".", ""); 

                    cliente.EsIntegracionManual = true;
                    cliente.EsIntegracionWeb = true;
                    cliente.RutaIntegracionManual = sDirectorio;
                    cliente.ArchivoIntegracionManual = sFileIntegracion;
                    bRegresa = cliente.Integrar();

                    Registrar_ResultadoIntegracion(datosCnn, sIdEstado, sIdFarmacia, NombreArchivo, bRegresa);

                    if (bRegresa)
                    {
                        try
                        {
                            File.Move(sFileIntegracion, Path.Combine(sDirectorio_Integrados, NombreArchivo));
                        }
                        catch { }
                    }

                }
                catch (Exception ex)
                {
                    Error.LogError(ex.Message);
                    Error.GrabarError(ex, "EnviarArchivo");
                }
                finally
                {
                    try
                    {
                        if (File.Exists(sFileIntegracion))
                        {
                            File.Delete(sFileIntegracion);
                        }
                    }
                    catch 
                    { 
                    }
                }
            }
            catch (Exception ex)
            {
                General.Error.LogError(ex.Message);
            }
            return bRegresa;
        }

        [WebMethod(Description = "Obtener catalogos.")]
        public byte[] Catalogos(string IdEstado, string IdFarmacia, string Tipo, DataSet ListaCatalogos)
        {
            // DataSet ListaCatalogos
            DataSet dtsCatalogos = new DataSet();
            // string sRegresa = "xxx";
            string sFarmaciaSolicita = IdFarmacia; 
            byte[] btArchivo = null;
            TipoServicio tipoDeServicio = TipoServicio.Ninguno;

            if (Tipo == "1")
            {
                tipoDeServicio = TipoServicio.OficinaCentral;
                // Siempre enviar 1 se suplantara al Servidor Regional 
                IdFarmacia = "0001";
            }

            if (Tipo == "2")
            {
                tipoDeServicio = TipoServicio.OficinaCentralRegional;
            }

            clsDatosConexion datosCnn = new clsDatosConexion(base.ConexionEx(DtGeneral.CfgIniOficinaCentral));
            datosCnn.Servidor = SetServidor(datosCnn.Servidor);

            clsObtenerInformacion cliente = new clsObtenerInformacion(DtGeneral.CfgIniOficinaCentral, datosCnn,
                DtGeneral.IdOficinaCentral, DtGeneral.IdFarmaciaCentral, tipoDeServicio,
                IdEstado, IdFarmacia, sFarmaciaSolicita, ListaCatalogos);

            try
            {
                cliente.GenerarArchivos();
                btArchivo = cliente.ArchivoDeCatalogos();

                try
                {
                    File.Delete(""); 
                }
                catch { }

                // sRegresa = btArchivo.Length.ToString(); 
            }
            catch (Exception ex1)
            {
                ex1.Source = ex1.Source; 
                // clsGrabarError.LogFileError(ex1.Message); 
            }
            return btArchivo;
        }

        [WebMethod(Description = "Descargar información")]
        public DataSet InformacionCatalogos(string IdEstado, string IdFarmacia, bool EsGeneral, string Tipo, DataSet ListaCatalogos)
        {
            return DescargarInformacionCatalogos(IdEstado, IdFarmacia, EsGeneral, Tipo, ListaCatalogos);
        }

        [WebMethod(Description = "Descargar información")]
        public DataSet InformacionGeneralDeCatalogos(string IdEstado, string IdFarmacia, bool EsGeneral, string Tipo)
        {
            return DescargarInformacionCatalogos(IdEstado, IdFarmacia, EsGeneral, Tipo, new DataSet());
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
