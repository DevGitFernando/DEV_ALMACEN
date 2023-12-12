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

using DllTransferenciaSoft;
using DllTransferenciaSoft.Zip;
using DllTransferenciaSoft.IntegrarInformacion;  

// using DllFarmaciaSoft.wsFarmaciaSoftGn;

namespace DllTransferenciaSoft
{
    [WebService(Description = "Modulo información", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsCnnOficinaCentralRegional : DllFarmaciaSoft.wsConexion
    {
        //[WebMethod(Description = "Probar conexión al servidor")]
        //public bool TestConection()
        //{
        //    return true;
        //}

        [WebMethod(Description = "Iniciar el Servicio de Transferencias.")]
        public int IniciarServicio(string Servicio)
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
                    string sSql = " Select * From CFGS_ConfigurarIntegracion (NoLock) ";
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
            string sFile_Ini = "Replicacion SQL";
            string sDirectorio = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\\ReplicacionSQL";
            string sFileName = Guid.NewGuid().ToString() + "";
            string sFileIntegracion = ""; 


            ZipUtil zip = new ZipUtil(); 

            try
            {
                sFileName = sFile_Ini.Replace("_", "").Replace("-", "");

                string sRuta = "";
                DllFarmaciaSoft.wsConexion Datos = new DllFarmaciaSoft.wsConexion();
                clsDatosConexion datosCnn = new clsDatosConexion(Datos.ConexionEx(sFile_Ini));
                clsGrabarError Error = new clsGrabarError(datosCnn, Transferencia.DatosApp, "wsCnnOficinaCentral");

                datosCnn.Servidor = SetServidor(datosCnn.Servidor);
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsLeer leer = new clsLeer(ref cnn);
                DllTransferenciaSoft.IntegrarInformacion.clsCliente cliente = new DllTransferenciaSoft.IntegrarInformacion.clsCliente(General.DatosConexion);

                cnn.DatosConexion.ConectionTimeOut = TiempoDeEspera.Limite30;
                Error.LogError(datosCnn.CadenaDeConexion);


                sDirectorio = Path.Combine(sDirectorio, sFileName); 
                if (!Directory.Exists(sDirectorio))
                {
                    Directory.CreateDirectory(sDirectorio);
                }


                try
                {
                    sFileIntegracion = Path.Combine(sDirectorio, NombreArchivo);  
                    File.WriteAllBytes(sDirectorio + @"\\" + NombreArchivo, Archivo);


                    cliente.EsIntegracionManual = true;
                    cliente.RutaIntegracionManual = sDirectorio;
                    cliente.ArchivoIntegracionManual = sFileIntegracion;
                    bRegresa = cliente.Integrar();

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
    }
}
