using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UpdateCompras
{
    static class Program
    {
        //static RevisarActualizaciones checkVersion; //= new RevisarActualizaciones(); 
        static string sFile = "Compras.exe";
        public static int ModuloCompras = 1;
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
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            appArgumentos = new Argumentos(args);
            General.ArchivoIni = "Compras";


            // Revisar si la se esta ejecutando una Instacion Manual 
            if (appArgumentos.GetValor("m") == "R")
            {
                ModuloCompras = 2;
                sFile = "Compras Regional.exe"; 
                General.ArchivoIni = "ComprasRegional";
            }

            //checkVersion = new RevisarActualizaciones(Application.StartupPath, sFile); 

            // Asegurar que la Interface solamente se ejecute una vez en el equipo. 
            if (!General.ProcesoEnEjecucionUnica(sFile))
            {
                // checkVersion.CheckVersion();
                TerminarModulos();

                Application.Run(new FrmUpdater(Application.StartupPath, sFile)); 

            }
        }

        private static void TerminarModulos()
        {
            General.TerminarProcesos(sFiles);

            ///General.TerminarProceso(sFile);
        }
    }
}
