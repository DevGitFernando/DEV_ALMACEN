using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.IO; 
using System.Diagnostics; 

namespace UpdateFarmacia
{
    public static class Program
    {
        //static RevisarActualizaciones checkVersion; //= new RevisarActualizaciones(); 
        static string sName = "Updater Farmacia";
        static string sFile = "Farmacia.exe";
        static string sFileDescarga = "";
        static string sVersion = "";
        // static bool bServidorLocal = false;

        public static Argumentos appArgumentos;


        static string[] sFiles = { 
                                   "Farmacia.exe", "Farmacia Unidosis.exe", "Almacen.exe", "Almacen Unidosis.exe", "Auditor Farmacia.exe", 
                                   "Administracion Regional.exe", "Administracion Unidad.exe", "Administración.exe",  
                                   "Compras Cuentas x Pagar.exe", "Compras.exe", "Compras Regional.exe", "Configuración de replicación.exe", 
                                   "Configuracion Integrar BD.exe", "Configuración Servicio Cliente Regional.exe", "Configuración Servicio Cliente.exe", 
                                   "Configuración Servicio Oficina Central Regional.exe", "Configuración Servicio Oficina Central.exe", "Configuracion.exe", 
                                   "DtDemonioReplicacion.exe", "Facturacion.exe", "Inventarios.exe", "OficinaCentral.exe", 
                                   "Registros Sanitarios.exe", "Servicio Cliente Regional.exe", "Servicio Cliente.exe", "Servicio Integrador BD.exe", 
                                   "Servicio Oficina Central Regional.exe", "Servicio Oficina Central.exe", 
                                   "Checador.exe", "Configuración RH.exe", "Recursos Humanos.exe",  
                                   "ISIIECE SERVICIOS.exe", "ISIADISSEP SERVICIOS.exe", "ISIIECE SERVICIOS.exe", "IFarmatel.exe", 
                                   "SII Encoder Almacen.exe", "SII Encoder Farmacia.exe", "SII INT EPharma.exe", "SII Interface Sinteco.exe", 
                                   
                                   "MA Almacen.exe", "MA Auditor.exe", "MA Checador.exe", "MA Configuracion RH.exe", 
                                   "MA Configuracion.exe", "MA Facturacion.exe", "MA Farmacia.exe", "MA Inventarios.exe", 
                                   "MA Oficina Central.exe", "MA Recursos Humanos.exe", "MA Servicios.exe"  
                                   //".exe", ".exe", ".exe", ".exe", 
                                   //".exe", ".exe", ".exe", ".exe", 
                                   //".exe", ".exe", ".exe", ".exe" 
                                 }; 
            

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FileInfo xf = new FileInfo(Application.ExecutablePath);


            // if (!General.ProcesoEnEjecucion(xf.Name))
            {
                General.ArchivoIni = "FarmaciaPtoVta";
                //checkVersion = new RevisarActualizaciones(Application.StartupPath, sFile); 


                string[] argsAux = { "MFarmacia.exe", "V1.0.11.1", "S1" };
                string[] argsAux2 = { "iS" };
                appArgumentos = new Argumentos(args); 
                //appArgumentos.ListaDeParametros(); 

                //appArgumentos = new Argumentos(argsAux2);

                //// Revisar si la se esta ejecutando actualización de Almacén 
                if (appArgumentos.GetValor("a") == "S")
                {
                    sFile = "Almacen.exe";
                    sFileDescarga = sFile;
                    General.ArchivoIni = "AlmacenPtoVta";
                }

                if (!appArgumentos.ArgumentosValidos)
                {
                    sVersion = "0.0.0.0";
                    try
                    {
                        FileVersionInfo f = FileVersionInfo.GetVersionInfo(Path.Combine(Application.StartupPath, sFile));
                        sVersion = f.ProductVersion;
                    }
                    catch { }

                    sFileDescarga = sFile;
                }
                else
                {
                    sFileDescarga = appArgumentos.GetValor("M"); 
                    sVersion = appArgumentos.GetValor("V"); 
                }

                if (appArgumentos.GetValor("C").ToUpper() == "S")
                {
                    try
                    {
                        General.TerminarProceso(sFile);
                    }
                    catch { }
                    General.msjUser("Presione Aceptar para continuar con el proceso de Actualización");
                }

                //// Revisar si la se esta ejecutando una Instalacion Manual 
                if (appArgumentos.GetValor("i") == "S")
                {
                    General.ActualizacionManual = true; 
                }

                //// Revisar si se requeiren los scripts originales 
                // Solo Servidor Central 
                if (appArgumentos.GetValor("E") == "N")
                {
                    General.WithOutEncryption = true;
                }

                //// Revisar si la se esta ejecutando actualización de Almacén 
                if (appArgumentos.GetValor("a") == "S")
                {
                    General.EsAlmacen = true;
                    sFile = "Almacen.exe"; 
                }

                //// Revisar si la se esta ejecutando actualización de Servidores Centrales 
                if (appArgumentos.GetValor("s") == "S")
                {
                    General.EsServidorGeneral = true;
                }

                // Permitir solo una instancia del Proceso en Memoria 
                if (!General.ProcesoEnEjecucion(sName))
                {
                    // Revisar el Tipo de Solicitud 
                    if (appArgumentos.GetValor("I").ToUpper() != "S")
                    {
                        try
                        {
                            TerminarModulos();
                            //General.TerminarProceso(sFile);
                        }
                        catch { }

                        // Asegurar que la Interface solamente se ejecute una vez en el equipo. 
                        if (!General.ProcesoEnEjecucionUnica(sFile))
                        {
                            // checkVersion.CheckVersion();  
                            Application.Run(new FrmUpdater(Application.StartupPath, sFile, sFileDescarga, sVersion)); 
                        }
                    }
                    else 
                    {
                        // Iniciar la Aplicacion de Modo Silencioso 
                        // General.msjAviso("Modo silencioso activado"); 
                        RevisarActualizaciones checkVersion = new RevisarActualizaciones(Application.StartupPath, sFile, sFileDescarga, sVersion);
                        checkVersion.CheckVersion();
                    }
                }
            }
        }

        private static void TerminarModulos()
        {
            General.TerminarProcesos(sFiles);

            ///General.TerminarProceso(sFile);
        }

/* 
 * M ==> Modulo 
 * V ==> Version 
 * S ==> Es Servidor de Red Local 
 * C ==> Mensaje de Confirmacion 
 * I ==> Mostrar Interface 
 * i ==> Actualizacion Manual 
 * E ==> Habilitar WithEncryption 
 * a ==> Es Almacen 
 * s ==> Servidor Central | Regional 
*/
    }
}
