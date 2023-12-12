using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.IO; 
using System.Diagnostics; 

namespace UpdaterOficinaCentralRegional
{
    static class Program
    {
        //static RevisarActualizaciones checkVersion; //= new RevisarActualizaciones(); 
        // Servicio Oficina Central Regional
        static string sFile = "Servicio Oficina Central Regional.exe"; // "Farmacia.exe";
        static string sFileDescarga = "";
        static string sVersion = "";
        // static bool bServidorLocal = false;

        public static Argumentos appArgumentos; 

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FileInfo xf = new FileInfo(Application.ExecutablePath);

            // if (!General.ProcesoEnEjecucion(xf.Name))
            {
                General.ArchivoIni = "FarmaciaPtoVta";
                //checkVersion = new RevisarActualizaciones(Application.StartupPath, sFile); 

                //clsSvrIIS IIS = new clsSvrIIS();

                //IIS.Detener();
                //IIS.Iniciar(); 
                //IIS.ReiniciarEquipo(); 

                ////clsShutDown shutDown = new clsShutDown();
                ////shutDown.Apagar_y_Reiniciar(); 


                string[] argsAux = { "MFarmacia.exe", "V1.0.11.1", "S1" };
                string[] argsAux2 = { "iS", "DS" };
                appArgumentos = new Argumentos(args);
                // appArgumentos = new Argumentos(argsAux2);

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

                    // Revisar si se esta actualizando el Servidor Central 
                    if (appArgumentos.GetValor("H") == "S")
                    {
                        General.ServidorCentral = true;
                        sFile = "Servicio Oficina Central.exe";
                        sVersion = "0.0.0.0"; 
                    }
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

                // Revisar si la se esta ejecutando una Instacion Manual 
                if (appArgumentos.GetValor("i") == "S")
                {
                    General.ActualizacionManual = true; 
                    if (appArgumentos.GetValor("D") == "S")
                    {
                        General.Desempacado = true;
                    }
                }

                // Revisar si se requeiren los scripts originales 
                // Solo Servidor Central 
                if (appArgumentos.GetValor("E") == "N")
                {
                    General.WithOutEncryption = true;
                }

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

/* 
 * M ==> Modulo 
 * V ==> Version 
 * S ==> Es Servidor de Red Local 
 * C ==> Mensaje de Confirmacion 
 * I ==> Mostrar Interface 
 * i ==> Actualizacion Manual 
 * E ==> Habilitar WithEncryption 
 * D ==> Archivos desempacados 
 * H ==> Servidor Central 
*/
    }
}
