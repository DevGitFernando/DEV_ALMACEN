using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.IO;
using System.Diagnostics; 


namespace MA_Updater
{
    public static class Program
    {
        //static RevisarActualizaciones checkVersion; //= new RevisarActualizaciones(); 
        static string sName = "MA Updater";
        static string sFile = "MA Farmacia.exe";
        static string sFileDescarga = "";
        static string sVersion = "";
        // static bool bServidorLocal = false;

        public static Argumentos appArgumentos;


        static string[] sFile_Lista = 
        {
            "MA Servicios.exe", "MA Oficina Central.exe", "MA Inventarios.exe", "MA Farmacia.exe", 
            "MA Configuracion.exe", "MA Auditor.exe", "MA Almacen.exe", 
            "MA Checador.exe", "MA Configuracion RH.exe", "MA Recursos Humanos.exe", "MA Facturacion.exe", 
            "Almacen.exe", "Auditor Farmacia.exe", "Configuracion.exe", "Configuracion RH.exe", 
            "Farmacia.exe", "Inventarios.exe", "OficinaCentral.exe", "RecursosHumanos.exe" 
            //, "", "", "", "" 
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
                General.ArchivoIni = "MA Farmacia";
                //checkVersion = new RevisarActualizaciones(Application.StartupPath, sFile); 


                string[] argsAux = { "MMA Farmacia.exe", "V1.0.11.1", "S1" };
                string[] argsAux2 = { "iS" };
                appArgumentos = new Argumentos(args);
                //appArgumentos.ListaDeParametros(); 

                //appArgumentos = new Argumentos(argsAux2);

                //// Revisar si la se esta ejecutando actualización de Almacén 
                if (appArgumentos.GetValor("a") == "S")
                {
                    sFile = "MA Almacen.exe";
                    sFileDescarga = sFile;
                    General.ArchivoIni = "MA Almacen";
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
                        //General.TerminarProceso(sFile);
                        TerminarProcesos(sFile_Lista); 
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
                    sFile = "MA Almacen.exe";
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
                            General.TerminarProceso(sFile);
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

        #region Terminar Procesos 
        private static Process GetProceso(string NombreProceso)
        {
            Process myProceso = null;
            try
            {
                myProceso = Process.GetProcessesByName(FormatearNombreProceso(NombreProceso))[0];
            }
            catch { }

            return myProceso;
        }

        private static Process[] GetProcesos(string NombreProceso)
        {
            Process[] myProceso = null;
            try
            {
                myProceso = Process.GetProcessesByName(FormatearNombreProceso(NombreProceso));
            }
            catch { }

            return myProceso;
        }

        private static string FormatearNombreProceso(string NombreProceso)
        {
            NombreProceso = NombreProceso.Replace(".EXE", "");
            NombreProceso = NombreProceso.Replace(".exe", "");
            NombreProceso = NombreProceso.Replace(".DLL", "");
            NombreProceso = NombreProceso.Replace(".dll", "");
            return NombreProceso;
        }

        private static void TerminarProceso(string NombreProceso)
        {
            try
            {
                GetProceso(NombreProceso).Kill();
            }
            catch { }
        }

        private static void TerminarProcesos(string[] NombresProcesos)
        {
            try
            {
                foreach (string NombreProceso in NombresProcesos)
                {
                    try
                    {
                        Process[] myProceso = GetProcesos(NombreProceso);
                        foreach (Process proceso in myProceso)
                        {
                            proceso.Kill();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static bool ProcesoEnEjecucion()
        {
            return ProcesoEnEjecucion(Process.GetCurrentProcess().ProcessName);
        }

        private static bool ProcesoEnEjecucionUnica()
        {
            return ProcesoEnEjecucionUnica(Process.GetCurrentProcess().ProcessName);
        }

        private static bool ProcesoEnEjecucionUnica(string NombreProceso)
        {
            NombreProceso = FormatearNombreProceso(NombreProceso);
            bool bRegresa = Process.GetProcessesByName(NombreProceso).Length == 1;

            return bRegresa;
        }

        private static bool ProcesoEnEjecucion(string NombreProceso)
        {
            // NombreProceso = NombreProceso.Replace(".EXE", "").Replace(".exe", "").Replace(".DLL", "").Replace(".dll", "");
            NombreProceso = FormatearNombreProceso(NombreProceso);
            bool bRegresa = Process.GetProcessesByName(NombreProceso).Length > 1;

            return bRegresa;
        }
        #endregion Terminar Procesos

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
