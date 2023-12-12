using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Usuarios_y_Permisos;

using DllFarmaciaSoft;
using DllTransferenciaSoft.ExportarBD; 
using DllTransferenciaSoft.Servicio;

namespace DllTransferenciaSoft
{
    #region Enumeradores publicos 
    public enum TipoServicio : int
    {
        Ninguno = 0, Cliente = 1, OficinaCentral = 2, OficinaCentralRegional = 3, ClienteOficinaCentralRegional = 4 
    }

    public enum Iconos : int
    {
        PlayActivo = 0,
        PlayInactivo = 1,
        StopActivo = 2,
        StopInactivo = 3
    }

    public enum Peridiocidad : int
    {
        Ninguno = 0,
        Diariamente = 1,
        Semanalmente = 2
    }

    public enum TipoTiempo : int
    {
        Ninguno = 0,
        Minutos = 1,
        Horas = 2
    }

    public enum TiempoIntegracion : int
    {
        Ninguno = 0,
        Segundos = 1,
        Minutos = 2,
        Horas = 3,
    }

    public enum Datos : int
    {
        Procesado = 1, Obtener = 2
    }

    /// <summary>
    /// Listado de tipos de envio de informacion
    /// </summary>
    public enum DestinoArchivos : int
    {
        /// <summary>
        /// Enviar información a todas las farmacias desde la Oficina Central
        /// </summary>
        TodasLasFarmacias = 0,

        /// <summary>
        /// Desde Farmacia a Oficina Central
        /// </summary>
        OficinaCentral = 1,

        /// <summary>
        /// Desde Farmacia a Oficina Central transferencias
        /// </summary>
        Farmacia_A_OficinaCentral = 2,

        /// <summary>
        /// Desde Farmacia a Farmacia transferencias
        /// </summary>
        Farmacia_A_Farmacia = 3,

        /// <summary>
        /// Desde Farmacia a Almacen Pedidos
        /// </summary>        
        Farmacia_A_Almacen = 4,

        /// <summary>
        /// Desde Almacen Pedidos a Farmacia 
        /// </summary>        
        Almacen_A_Farmacia = 5 


    }

    public enum TipoDeArchivo : int
    {
        Informacion = 1, BaseDeDatos = 2, Sistema = 3
    }

    public enum TamañoFiles
    {
        Byte = 1, KB = 2, MB = 3, GB = 4 
    }

    public enum LongFiles : long
    {
        Byte = 1, KB = 1024, MB = (1024 * 1024), GB = ((1024 * 1024) * 1024)
    }

    public enum TipoDeArchivo_Updater : int
    {
        Exe = 1, Dll = 2, Reporte = 3, Excel = 4, Otro = 5
    } 
    #endregion Enumeradores publicos 

    public static class Transferencia
    {
        // private static string sModulo = "DllTransferenciaSoft";
        // private static string sVersion = "1.0.0.0";
        private static clsDatosApp dpDatosApp = new clsDatosApp("DllTransferenciaSoft", "1.0.0.0");
        public static string ExtArchivosGenerados = "SII";
        public static string ExtArchivosZip = "zip";

        // private static bool bServidorFTP = false; 
        private static string sServidorFTP = ""; 

        public static readonly string xType = "39b6d4cf7fc673edf5c9fcfc602ffdd7"; // Transferencia 
        public static readonly string xTypx = "33e790cd8a7fad434930d99b67ba8efd"; // Backup 
        public static readonly string xTypy = "552b4c85ce6b66ac23f9914be02290ca"; // Install 
        public static readonly string xTyps = "bfbc39827d526874e0fb798509f109fc"; // Servicio Remoto  
        public static readonly string xTypu = "7bb30fdbb0e7406917f1937980529469"; // Updater  

        //public static Form FrmMainServicio; 
        //public static bool AbrirServicio = false;
        //public static FrmServicio SvrDemonio;
        public static bool EjecutandoProcesos = false;
        private static string sSeparadorSql = "--#SQL";
        private static int iRegistrosPaquete = 100;
        private static int iBloquesPaquete = 10;
        private static TipoServicio tpServicioEnEjecucion = TipoServicio.Ninguno; 

        private static clsConexionSQL cnn;
        private static clsLeer leer;

        #region Directorios de Trabajo 
        private static readonly string sRutaFTP = "RESPALDOS_DBS"; 
        #endregion Directorios de Trabajo

        #region Contructor
        static Transferencia()
        {
            ////clsAbrirForma.AssemblyActual("DllTransferenciaSoft");
            ////dpDatosApp = clsAbrirForma.DatosApp;

            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
            //dpDatosApp = new clsDatosApp("DllTransferenciaSoft", "1.0.5.1");
        }
        #endregion Contructor

        #region Propieades
        public static clsDatosApp DatosApp
        {
            get { return dpDatosApp; }
            set { dpDatosApp = value; }
        }

        public static string Modulo
        {
            get { return dpDatosApp.Modulo; }
        }
        public static string Version
        {
            get { return dpDatosApp.Version; }
        }

        public static string SQL
        {
            get { return sSeparadorSql; }
        }
        public static int BloquesRegistros
        {
            get { return iBloquesPaquete; }
            set { iBloquesPaquete = value; }
        }
        public static int RegistrosSQL
        {
            get { return iRegistrosPaquete; }
            set { iRegistrosPaquete = value; }
        }

        public static TipoServicio ServicioEnEjecucion
        {
            get { return tpServicioEnEjecucion; }
            set { tpServicioEnEjecucion = value; }
        }

        public static string ServidorFTP
        {
            get { return sServidorFTP; }
            set { sServidorFTP = value; }
        }

        #region Directorios de Trabajo
        public static string DirectorioFTP
        {
            get { return sRutaFTP; }
        }
        #endregion Directorios de Trabajo

        #endregion Propieades

        #region Funciones y Procedimientos Publicos 
        #region Base de Datos 
        private  static string sRutaExportarInformacionBD = "";

        public static string RutaExportarInformacionBD
        {
            get 
            {
                if (sRutaExportarInformacionBD == "")
                {
                    sRutaExportarInformacionBD = Application.StartupPath + @"\\BackUp_BD\"; 
                }

                if (!Directory.Exists(sRutaExportarInformacionBD))
                {
                    Directory.CreateDirectory(sRutaExportarInformacionBD); 
                }

                return sRutaExportarInformacionBD; 
            }
        }

        public static bool ExportarInformacionBD()
        {
            return ExportarInformacionBD(true); 
        }

        public static bool ExportarInformacionBD(bool MostrarDetalles) 
        {
            bool bRegresa = false;

            FrmExportarBD f = new FrmExportarBD(MostrarDetalles);
            f.ShowDialog();

            bRegresa = f.SeGeneroRespaldo;
            sRutaExportarInformacionBD = f.sRutaExportarInformacionBD; 

            return bRegresa; 
        }
        #endregion Base de Datos

        public static void PrepararConexion()
        {
            cnn = new clsConexionSQL(General.DatosConexion);
            leer = new clsLeer(ref cnn); 
        }

        public static void ObtenerDatosOrigenFarmacia()
        {
            ObtenerDatosOrigen("CFGC_ConfigurarConexion");
        } 

        public static void ObtenerDatosOrigenRegional()
        {
            ObtenerDatosOrigen("CFGSC_ConfigurarConexion"); 
        } 

        private static void ObtenerDatosOrigen(string Tabla)
        {
            //DtGeneral.EstadoConectado, DtGeneral.FarmaciaConectada
            string sSql = string.Format("Select C.Servidor, C.WebService, C.PaginaWeb, C.Status, " + 
                " C.IdEstado, IsNull(E.Estado, '') as Estado, C.IdFarmacia, IsNull(E.Farmacia, '') as Farmacia, " +
                " (IsNull(E.ClaveRenapo, '00') + C.IdFarmacia) as FarmaciaOrigen, IsNull(E.ClaveRenapo, '00') as ClaveRenapo " +
                " From {0} C (NoLock) " +
                " Left Join vw_Farmacias E (NoLock) On ( C.IdEstado = E.IdEstado and C.IdFarmacia = E.IdFarmacia ) " +
                " Where C.Status = 'A' ", Tabla); 

            if (!leer.Exec(sSql))
            {
                // Error.GrabarError(leer, "ObtenerDestino()");
            }
            else
            {
                if (leer.Leer())
                {
                    DtGeneral.EstadoConectado = leer.Campo("IdEstado");
                    DtGeneral.EstadoConectadoNombre = leer.Campo("Estado"); 
                    DtGeneral.ClaveRENAPO = leer.Campo("ClaveRenapo"); 
                    DtGeneral.FarmaciaConectada = leer.Campo("IdFarmacia");
                    DtGeneral.FarmaciaConectadaNombre = leer.Campo("Farmacia");

                    Transferencia.ServidorFTP = leer.Campo("Servidor"); 

                }
            }
        }

        #endregion Funciones y Procedimientos Publicos
    }
}
