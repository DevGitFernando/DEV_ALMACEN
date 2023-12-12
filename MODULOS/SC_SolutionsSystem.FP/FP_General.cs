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

using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.Usuarios_y_Permisos;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.SistemaOperativo;

namespace SC_SolutionsSystem.FP
{
    delegate void Function();	// a simple delegate for marshalling calls from event handlers to the GUI thread

    public enum Dedos
    {
        Ninguno = 0,
        MI_Menique = 1, MI_Anular = 2, MI_Medio = 3, MI_Indice = 4, MI_Pulgar = 5,
        MD_Menique = 6, MD_Anular = 7, MD_Medio = 8, MD_Indice = 9, MD_Pulgar = 10,

        PI_Menique = 11, PI_Anular = 12, PI_Medio = 13, PI_Indice = 14, PI_Pulgar = 15,
        PD_Menique = 16, PD_Anular = 17, PD_Medio = 18, PD_Indice = 19, PD_Pulgar = 20
    }

    public static class FP_General
    {
        #region Declaración de variables
        public static basGenerales Fg = new basGenerales();
        private static clsGrabarError Error;

        // Datos de identificacion del Dll
        private static string sModulo = "SC_SolutionsSystem.FP";
        private static string sVersion = "1.0.0.0";
        private static clsDatosApp dpDatosApp = new clsDatosApp("SC_SolutionsSystem.FP", "1.0.0.0");


        private static clsDatosConexion conexionHuellas = new clsDatosConexion();
        private static string sCampo_IdHuellas = " IdHuella ";
        private static string sTabla_Huellas = " FP_Huellas ";
        private static string sSp_Registro_Huellas = " spp_Registrar_Huellas ";

        private static int iIdHuella = 0;
        private static string sReferencia_Huella = "";

        private static int iIdHuella_Inicial = 0;
        private static int iIdHuella_Final = 0; 

        private static bool bHuellaCapturada = false;
        private static bool bHuellaRegistrada = false;
        private static bool bExisteHuella = false;
        private static int iBloqueHuellas = 500;
        private static byte[] btHuella = null;
        private static Dedos tpDedo = Dedos.Ninguno;
        private static int iIntentosVerificacionBase = 4;
        private static int iIntentosVerificacion = 4;

        private static clsConexionSQL cnn;
        private static clsLeer leerHuella;
        private static clsLeer guardarHuella;
        private static bool bEsEquipoDeDesarrollo = File.Exists(General.UnidadSO + @":\\Dev.xml");

        private static bool bValidarEn_SQLite = false; 
        //private static clsConexionSQLite cnnSQLite;
        //private static clsLeerSQLite leerHuellaSQLite; 

        private static string sRutaRegistrosSanitarios = "";
        private static string sRuta_DB_Checador = "";
        private static string sDB_Checador = ""; 

        #endregion Declaración de variables

        #region Constructor
        static FP_General()
        {
            ObtenerDatosDll();

            //InicializarConexion(); 

            sDB_Checador = "SII_ChecadorHuellas.s3db";
            sRuta_DB_Checador = Application.StartupPath + @"\BD_Checador\"; // Application.StartupPath + @"\RegistrosSanitarios\";

            //sRuta_DB_Checador = @"D:\BASES_DE_DATOS\SQLite\";


            ////CrearDirectorios();
            ////CrearBaseDeDatos();

            ////cnnSQLite = new clsConexionSQLite(FP_General.DaseDeDatos_SQLite); 
            ////leerHuellaSQLite = new clsLeerSQLite(ref cnnSQLite);


            //EliminarHuellasViejas(); 

        }
        #endregion Constructor

        #region Propieades Dll
        public static clsDatosApp DatosApp
        {
            get { return dpDatosApp; }
            set { dpDatosApp = value; }
        }

        public static string Modulo
        {
            get { return sModulo; }
        }

        public static string Version
        {
            get { return sVersion; }
        }
        #endregion Propieades Dll

        #region Propiedades Desarrollo
        public static bool EsEquipoDeDesarrollo
        {
            get { return bEsEquipoDeDesarrollo; }
            set { bEsEquipoDeDesarrollo = value; }
        }
        #endregion Propiedades Desarrollo

        #region Propiedades Lector
        private static bool bLectorConectado = false;
        private static Color cFondoDefaultLectorConectado = Color.White;
        private static Color cFondoDefaultLectorDesconectado = Color.LightGray;

        private static Color cFondoLectorConectado = Color.White;
        private static Color cFondoLectorDesconectado = Color.LightGray;

        public static bool LectorConectado
        {
            get { return bLectorConectado; }
            set { bLectorConectado = value; }
        }

        public static Color FondoLectorConectado
        {
            get { return cFondoLectorConectado; }
            set { cFondoLectorConectado = value; }
        }

        public static Color FondoLectorDesconectado
        {
            get { return cFondoLectorDesconectado; }
            set { cFondoLectorDesconectado = value; }
        }

        public static void SetFondoDefault()
        {
            cFondoLectorConectado = cFondoDefaultLectorConectado;
            cFondoLectorDesconectado = cFondoDefaultLectorDesconectado;
        }
        #endregion Propiedades Lector

        #region Propiedades
        private static string DaseDeDatos_SQLite
        {
            get { return Path.Combine(sRuta_DB_Checador, sDB_Checador); }
        }

        public static clsDatosConexion Conexion
        {
            get { return conexionHuellas; }
            set
            {
                conexionHuellas = value;
                Error = new clsGrabarError(conexionHuellas, FP_General.DatosApp, "FP_General");
            }
        }
        
        /// <summary>
        /// Validar huella primero en base de datos local de huellas 
        /// </summary>
        public static bool ValidarEn_SQLite
        {
            get { return bValidarEn_SQLite; }
            set { bValidarEn_SQLite = value; }
        }

        public static int Identificador_Huella
        {
            get { return iIdHuella; }
            set { iIdHuella = value; }
        }

        public static string Referencia_Huella
        {
            get { return sReferencia_Huella; }
            set { sReferencia_Huella = value; }
        }

        public static Dedos Dedo
        {
            get { return tpDedo; }
            set { tpDedo = value; }
        }

        public static bool ExisteHuella
        {
            get { return bExisteHuella; }
            set { bExisteHuella = value; }
        }

        public static bool HuellaRegistrada
        {
            get { return bHuellaRegistrada; }
            set { bHuellaRegistrada = value; }
        }

        public static bool HuellaCapturada
        {
            get { return bHuellaCapturada; }
            set { bHuellaCapturada = value; }
        }

        public static int IntentosVerificacion
        {
            get { return iIntentosVerificacion; }
            set
            {
                iIntentosVerificacion = value;
                if (value <= 0)
                {
                    iIntentosVerificacion = iIntentosVerificacionBase;
                }
            }
        }

        public static string CampoIdHuella
        {
            get { return sCampo_IdHuellas; }
            set { sCampo_IdHuellas = value; }
        }

        public static string TablaHuellas
        {
            get { return sTabla_Huellas; }
            set { sTabla_Huellas = value; }
        }

        public static string StoreRegistroHuellas
        {
            get { return sSp_Registro_Huellas; }
            set { sSp_Registro_Huellas = value; }
        }
        #endregion Propiedades

        #region Manejo de Huellas
        public static void Reset()
        {
            bHuellaCapturada = false;
            bHuellaRegistrada = false;
            bExisteHuella = false;
        }

        public static bool RegistrarHuella()
        {
            bool bRegresa = RegistrarHuella(btHuella, Dedos.Ninguno);
            return bRegresa;
        }

        public static bool RegistrarHuella(byte[] Huella, Dedos Dedo)
        {
            bool bRegresa = false;
            string sBase64 = Convert.ToBase64String(Huella);

            string sSql = string.Format("Exec {0}   '{1}', '{2}', '{3}' ",
                sSp_Registro_Huellas, sReferencia_Huella, (int)Dedo, sBase64);


            InicializarConexion();
            guardarHuella = new clsLeer(ref cnn);
            Error = new clsGrabarError(conexionHuellas, FP_General.DatosApp, "FP_General");

            bHuellaRegistrada = false;
            if (!guardarHuella.Exec(sSql))
            {
                Error.GrabarError(guardarHuella, "RegistrarHuella");
            }
            else
            {
                if (guardarHuella.Leer())
                {
                    bHuellaRegistrada = true;
                    bRegresa = true;
                }
            }

            return bRegresa;
        }

        public static int NumTotalHuellas()
        {
            int iRegresa = 0;
            string sSql = string.Format("Select Count(*) as NumHuellas, min({1}) as Minimo, max({1}) as Maximo From {0} (NoLock) ", sTabla_Huellas, sCampo_IdHuellas);

            InicializarConexion();
            leerHuella = new clsLeer(ref cnn);
            Error = new clsGrabarError(conexionHuellas, FP_General.DatosApp, "FP_General");

            if (!leerHuella.Exec(sSql))
            {
                Error.GrabarError(leerHuella, "RegistrarHuella");
            }
            else
            {
                leerHuella.Leer();
                iRegresa = leerHuella.CampoInt("NumHuellas");
                iIdHuella_Inicial = leerHuella.CampoInt("Minimo");
                iIdHuella_Final = leerHuella.CampoInt("Maximo");
            }

            return iRegresa;
        }

        private static int NumTotalHuellas_SQLite()
        {
            int iRegresa = 0;
            string sSql = string.Format("Select Count(*) as NumHuellas, min(IdHuella) as Minimo, max(IdHuella) as Maximo From FP_Huellas__Recientes  ");

            InicializarConexion();
            leerHuella = new clsLeer(ref cnn);
            //leerHuellaSQLite = new clsLeerSQLite(ref cnnSQLite); 
            Error = new clsGrabarError(conexionHuellas, FP_General.DatosApp, "FP_General");

            if (!leerHuella.Exec(sSql))
            {
                Error.GrabarError(leerHuella.DatosConexion, leerHuella.Error, "NumTotalHuellas_SQLite");
            }
            else
            {
                leerHuella.Leer();
                iRegresa = leerHuella.CampoInt("NumHuellas");
                iIdHuella_Inicial = leerHuella.CampoInt("Minimo");
                iIdHuella_Final = leerHuella.CampoInt("Maximo");
            }

            return iRegresa;
        }

        public static bool RegistrarGUID(string GUID, string sIdPersonal)
        {
            bool bRegresa = false;
            string sSql = string.Format( "Insert Into FP_Huellas_Verificado ( GUID, RegistroHuella ,FechaRegistro, Status ) select '{0}', '{1}', getdate(), 'A' ", GUID, sIdPersonal);

            leerHuella = new clsLeer(ref cnn);
            Error = new clsGrabarError(conexionHuellas, FP_General.DatosApp, "FP_General");

            if (!leerHuella.Exec(sSql))
            {
                bRegresa = false;
            }
            else
            {
                bRegresa = true;
            }

            return bRegresa; 
        }

        public static int CompararHuella(DPFP.FeatureSet Features)
        {
            return CompararHuella(Features, false);
        }

        public static int CompararHuella(DPFP.FeatureSet Features, bool RegistroActivo)
        {
            int iFARAchieved = 0;

            if (bValidarEn_SQLite)
            {
                iFARAchieved = CompararHuella_SQLite(Features, RegistroActivo);
                iFARAchieved = Convert.ToInt32(bExisteHuella); 
            }


            if (iFARAchieved == 0)
            {
                iFARAchieved = CompararHuella_SQL(Features, RegistroActivo);
            }

            return iFARAchieved; 
        }

        private static int CompararHuella_SQL(DPFP.FeatureSet Features, bool RegistroActivo)
        {           
            //// Marcar la captura de huella 
            bHuellaCapturada = true;

            InicializarConexion();
            leerHuella = new clsLeer(ref cnn);
            Error = new clsGrabarError(conexionHuellas, FP_General.DatosApp, "FP_General");

            string sSql = ""; 
            int iFARAchieved = 0;
            string sMensaje = "";
            int iNumHuellas = FP_General.NumTotalHuellas();
            int iInicio = iIdHuella_Inicial;
            int iFinal = iIdHuella_Inicial + iBloqueHuellas;
            int iHuellasComparadas = 0; 

            string sBase64 = ""; 
            byte[] tx = null;
            int cont = 0;
            DPFP.Verification.Verification.Result resultado = new DPFP.Verification.Verification.Result();
            DPFP.Verification.Verification verify = new DPFP.Verification.Verification();

            basGenerales Fg = new basGenerales();

            iBloqueHuellas = 500;
            bExisteHuella = false;

            iInicio = iIdHuella_Inicial;
            iFinal = iIdHuella_Inicial + iBloqueHuellas;

            try
            {
                sSql = string.Format("Select IdHuella, FechaRegistro, ReferenciaHuella, NumDedo, Huella, Status " +
                    " From {0} (NoLock) Where IdHuella between {1} and {2} ", sTabla_Huellas, iInicio, iFinal);

                
                sBase64 = Convert.ToBase64String(Features.Bytes);
                for (int iInicioFP = iIdHuella_Inicial; iInicioFP <= iIdHuella_Final; iInicioFP += iBloqueHuellas)
                {
                    sSql = string.Format("Select IdHuella, FechaRegistro, ReferenciaHuella, NumDedo, Huella, Status " +
                                        " From {0} (NoLock) Where IdHuella between {1} and {2} ", sTabla_Huellas, iInicio, iFinal);

                    //CAST(N'' as XML).value('xs:base64Binary(sql:column("F.Logo"))', 'varbinary(max)') as Logo_Imagen, 

                    sSql = string.Format(
                        "Select \n" +
                        "\tIdHuella, FechaRegistro, ReferenciaHuella, NumDedo, Status, \n" +
                        "\tCAST(N'' as XML).value('xs:base64Binary(sql:column({3}Huella{3}))', 'varbinary(max)') as Huella \n" +
                        "From {0} (NoLock) \n" +
                        "Where IdHuella between {1} and {2} and Status = 'A' \n" +
                        "Order by ReferenciaHuella ", sTabla_Huellas, iInicio, iFinal, Fg.Comillas());


                    ////sSql = string.Format(
                    ////    "Declare \n" + 
                    ////    "   @sBase64 varchar(max)\n " +
                    ////    "\n " +
                    ////    "   Set @sBase64 = '{2}' \n " +
                    ////    "\n " +
                    ////    "Select IdHuella, FechaRegistro, ReferenciaHuella, NumDedo, Status, " +
                    ////    "   CAST(N'' as XML).value('xs:base64Binary(sql:column({1}Huella{1}))', 'varbinary(max)') as Huella " +
                    ////    "From {0} (NoLock) " +
                    ////    "Where CAST(N'' as XML).value('xs:base64Binary(sql:column({1}Huella{1}))', 'varbinary(max)') = " +
                    ////        " CAST(N'' as XML).value('xs:base64Binary(sql:variable({1}@sBase64){1})', 'varbinary(max)') ", 
                    ////    sTabla_Huellas, Fg.Comillas(), sBase64);


                    iInicio += iBloqueHuellas;
                    iFinal += iBloqueHuellas;

                    if (!leerHuella.Exec(sSql))
                    {
                        Error.GrabarError(leerHuella, "RegistrarHuella");
                        break;
                    }
                    else
                    {
                        while (leerHuella.Leer())
                        {
                            tx = leerHuella.CampoByte("Huella");
                            tx = leerHuella.CampoImagen("Huella");
                            DPFP.Template templates = new DPFP.Template();

                            try
                            {
                                templates.DeSerialize((byte[])tx);
                                verify.Verify(Features, templates, ref resultado);
                            }
                            catch (Exception ex)
                            {
                                ex = null; 
                            }
                            
                            iFARAchieved = resultado.FARAchieved;

                            //// Datos para Interface 
                            if (!RegistroActivo)
                            {
                                sReferencia_Huella = "";
                                tpDedo = Dedos.Ninguno;
                            }

                            iHuellasComparadas++;
                            if (iHuellasComparadas >= 8456)
                            {
                                iNumHuellas++;
                                iNumHuellas--;
                            }

                            if (resultado.Verified)
                            {
                                // Datos para Interface 
                                sReferencia_Huella = leerHuella.Campo("ReferenciaHuella");
                                tpDedo = (Dedos)leerHuella.CampoInt("NumDedo");
                                bExisteHuella = true;

                                RegistrarHuella_SQLite(tx, leerHuella.RowActivo); 

                                break;
                            }
                        }
                    }

                    if (bExisteHuella)
                    {
                        break;
                    }

                }//  while (iFinal <= iNumHuellas); 
            }
            catch (Exception er)
            {
                Error.GrabarError(er, "CompararHuella_SQL()");
                ////MessageBox.Show(er.Message + "...");
            }

            return iFARAchieved;

        }

        private static void RegistrarHuella_SQLite(byte[] Huella, DataRow Informacion)
        {
            bool bError = false;
            string sBase64 = Convert.ToBase64String(Huella);

            //cnnSQLite = new clsConexionSQLite(FP_General.DaseDeDatos_SQLite);
            //leerHuellaSQLite = new clsLeerSQLite(ref cnnSQLite);
            clsLeer leerDatos = new clsLeer();
            string sSql =""; 

            leerDatos.DataRowClase = Informacion;
            leerDatos.Leer(); 

            sSql = string.Format(
                "Insert into FP_Huellas__Recientes ( IdHuella, FechaRegistro, ReferenciaHuella, NumDedo, Huella, Status, FechaDescarga, FechaUltimaActualizacion ) " +
                "Select '{0}', getdate(), '{1}', '{2}', '{3}', '{4}', getdate(), getdate() ",
                leerDatos.CampoInt("IdHuella"), ////leerDatos.CampoFecha("FechaRegistro"), 
                leerDatos.Campo("ReferenciaHuella"),
                leerDatos.CampoInt("NumDedo"), sBase64, leerDatos.Campo("Status"));

            if (!leerHuella.Exec(sSql))
            {
                bError = true;
                Error.GrabarError(leerHuella, "RegistrarHuella_SQLite()"); 
            }

        }

        private static int CompararHuella_SQLite(DPFP.FeatureSet Features, bool RegistroActivo)
        {
            //// Marcar la captura de huella 
            bHuellaCapturada = true;

            InicializarConexion();
            leerHuella = new clsLeer(ref cnn);
            Error = new clsGrabarError(conexionHuellas, FP_General.DatosApp, "FP_General");

            string sSql = ""; 
            int iFARAchieved = 0;
            string sMensaje = "";
            int iNumHuellas = FP_General.NumTotalHuellas_SQLite();
            int iInicio = iIdHuella_Inicial;
            int iFinal = iIdHuella_Inicial + iBloqueHuellas;
            int iHuellasComparadas = 0;

            byte[] tx = null;
            int cont = 0;
            DPFP.Verification.Verification.Result resultado = new DPFP.Verification.Verification.Result();
            DPFP.Verification.Verification verify = new DPFP.Verification.Verification();

            basGenerales Fg = new basGenerales();

            iBloqueHuellas = 500;
            bExisteHuella = false;

            iInicio = iIdHuella_Inicial;
            iFinal = iIdHuella_Inicial + iBloqueHuellas;

            try
            {
                for (int iInicioFP = iIdHuella_Inicial; iInicioFP <= iIdHuella_Final; iInicioFP += iBloqueHuellas)
                {
                    sSql = string.Format("Select IdHuella, '' as FechaRegistro, ReferenciaHuella, 0 as NumDedo,  " +
                                        " From FP_Huellas__Recientes (NoLock) Where IdHuella between {1} and {2} ", sTabla_Huellas, iInicio, iFinal);

                    sSql = string.Format(
                        "Select \n" +
                        "\tIdHuella, '' as FechaRegistro, ReferenciaHuella, 0 as NumDedo, \n" +
                        "\tCAST(N'' as XML).value('xs:base64Binary(sql:column({3}Huella{3}))', 'varbinary(max)') as Huella \n" +
                        "From FP_Huellas__Recientes (NoLock) \n" +
                        "Where IdHuella between {1} and {2} and Status = 'A' \n" +
                        "Order by ReferenciaHuella ", sTabla_Huellas, iInicio, iFinal, Fg.Comillas());

                    iInicio += iBloqueHuellas;
                    iFinal += iBloqueHuellas;

                    if (!leerHuella.Exec(sSql))
                    {
                        Error.GrabarError(leerHuella.DatosConexion, leerHuella.Error, "RegistrarHuella");
                        break;
                    }
                    else
                    {
                        while (leerHuella.Leer())
                        {
                            tx = leerHuella.CampoByte("Huella");
                            tx = leerHuella.CampoImagen("Huella");
                            DPFP.Template templates = new DPFP.Template();
                            templates.DeSerialize((byte[])tx);
                            verify.Verify(Features, templates, ref resultado);
                            iFARAchieved = resultado.FARAchieved;

                            //// Datos para Interface 
                            if (!RegistroActivo)
                            {
                                sReferencia_Huella = "";
                                tpDedo = Dedos.Ninguno;
                            }

                            iHuellasComparadas++; 
                            if (resultado.Verified)
                            {
                                //// Datos para Interface 
                                sReferencia_Huella = leerHuella.Campo("ReferenciaHuella");
                                tpDedo = (Dedos)leerHuella.CampoInt("NumDedo");
                                bExisteHuella = true;
                                ActualizarInfoHuellas_SQLite(); 
                                break;
                            }
                        }
                    }

                    if (bExisteHuella)
                    {
                        break;
                    }

                }//  while (iFinal <= iNumHuellas); 
            }
            catch (Exception er)
            {
                ////MessageBox.Show(er.Message + "...");
            }

            return iFARAchieved;

        }        
        #endregion Manejo de Huellas

        #region Funciones y Procedimientos Publicos

        public static bool Abrir()
        {
            InicializarConexion();
            return cnn.Abrir();
        }

        public static bool Cerrar()
        {
            InicializarConexion();
            return cnn.Cerrar();
        }

        public static void IniciarTransaccion()
        {
            InicializarConexion();
            cnn.IniciarTransaccion();
        }

        public static void CompletarTransaccion()
        {
            InicializarConexion();
            cnn.CompletarTransaccion();
        }

        public static void DeshacerTransaccion()
        {
            InicializarConexion();
            cnn.DeshacerTransaccion();
        }

        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Privados
        private static void ActualizarInfoHuellas_SQLite()
        {
            string sSql = string.Format("Update FP_Huellas__Recientes Set FechaUltimaActualizacion = getdate() Where ReferenciaHuella = '{0}' ", sReferencia_Huella);

            if (!leerHuella.Exec(sSql))
            {
                Error.GrabarError(leerHuella, "ActualizarInfoHuellas_SQLite()"); 
            }
        }

        /// <summary>
        /// Elimina las huellas no utilizadas en los ultimos 30 dias 
        /// </summary>
        private static void EliminarHuellasViejas()
        {
            string sSql = string.Format(
                "Delete From FP_Huellas__Recientes " +
                "Where datediff(dd, FechaUltimaActualizacion, getdate()) >= 30 ");

            if (!leerHuella.Exec(sSql))
            {
                Error.GrabarError(leerHuella, "EliminarHuellasViejas()");
            }
        }

        private static void ObtenerDatosDll()
        {
            ////Assembly AssemblyCargado = Assembly.Load(sModulo);
            //// AssemblyName x = new AssemblyName(AssemblyCargado.FullName);
            AssemblyName x = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            dpDatosApp = new clsDatosApp(x.Name, x.Version.ToString());
            // dpDatosApp = new clsDatosApp("SC_SolutionsSystem", "3.0.0.0");
        }

        private static void InicializarConexion()
        {
            if (cnn == null)
            {
                cnn = new clsConexionSQL(FP_General.conexionHuellas);
                leerHuella = new clsLeer(ref cnn); 
                //cnnSQLite = new clsConexionSQLite(FP_General.DaseDeDatos_SQLite); 

                EliminarHuellasViejas(); 
            }
        }
        #endregion Funciones y Procedimientos Privados

        #region Funciones y Procedimientos Privados
        private static void CrearDirectorios()
        {
            ////if (!Directory.Exists(sRuta_DB_Checador))
            ////{
            ////    Directory.CreateDirectory(sRuta_DB_Checador);
            ////}
        }

        private static void CrearBaseDeDatos()
        {
            ////if (!File.Exists(Path.Combine(sRuta_DB_Checador, sDB_Checador)))
            ////{
            ////    General.Fg.ConvertirBytesEnArchivo(sDB_Checador, sRuta_DB_Checador, Properties.Resources.SII_ChecadorHuellas, true);
            ////}
        }
        #endregion Funciones y Procedimientos Privados
    }
}
