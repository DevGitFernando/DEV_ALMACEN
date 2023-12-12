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

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

namespace SC_SolutionsSystem.FP
{
    [WebService(Description = "Modulo conexión", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsFingerPrint
    {
        #region Declaracion de Variables 
        basSeguridad funciones; // = new basSeguridad();
        clsCriptografo Cryp = new clsCriptografo();
        string sFileConexion = "SII_INT_ISIADISSEP";
        #endregion Declaracion de Variables

        #region Funciones y Procedimientos Privados
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
            datosCnn.ForzarImplementarPuerto = funciones.ForzarPuerto == "1"; 

            return datosCnn.DatosCnn();
        }
        #endregion Funciones y Procedimientos Privados

        #region Funciones y Procedimientos Publicos 
        [WebMethod(Description = "Verificar huella.")]
        public virtual RespuestaFP Registrar(string Parametro_01, string Parametro_02, string Parametro_03, string Parametro_04, int Parametro_05, byte[] Parametro_06)
        {
            RespuestaFP response = new RespuestaFP();
            string sRegresa = "";

            try
            {
                DPFP.FeatureSet features = new DPFP.FeatureSet();
                features.DeSerialize(Parametro_06);

                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(Parametro_01));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);

                FP_General.ValidarEn_SQLite = false;
                FP_General.Conexion = datosCnn;
                FP_General.TablaHuellas = Parametro_02;
                FP_General.StoreRegistroHuellas = Parametro_03;

                FP_General.CompararHuella(features);

                if (!FP_General.ExisteHuella)
                {
                    FP_General.Referencia_Huella = Parametro_04;
                    response.HuellaLeida = FP_General.RegistrarHuella(Parametro_06, (Dedos)Parametro_05);  //FP_General.Dedo);
                    response.Codigo = response.HuellaLeida ? 1 : 0;
                    response.Mensaje = response.HuellaLeida ? "Huella registrada correctamente." : "No fue posible registrar la huella.";
                }
            }
            catch 
            { 
            }

            return response;
        }
        #endregion Funciones y Procedimientos Publicos
        
        [WebMethod(Description = "Verificar huella.")]
        public virtual RespuestaFP Verificar(string Parametro_01, string Parametro_02, string Parametro_03, string Parametro_04, byte []Parametro_05)
        {
            RespuestaFP response = new RespuestaFP();
            string sRegresa = ""; 
            
            try
            {
                DPFP.FeatureSet features = new DPFP.FeatureSet();
                features.DeSerialize(Parametro_05);

                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(Parametro_01));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);

                FP_General.ValidarEn_SQLite = true; 
                FP_General.Conexion = datosCnn;
                FP_General.TablaHuellas = Parametro_02;
                FP_General.StoreRegistroHuellas = "";

                FP_General.CompararHuella(features);

                response.Codigo = FP_General.ExisteHuella ? 1 : 0;
                if (FP_General.ExisteHuella)
                {                    
                    response.HuellaLeida = FP_General.ExisteHuella;
                    
                    response.IdHuellaReferencia = FP_General.Referencia_Huella;
                    response.Identificador_Huella = FP_General.Identificador_Huella;
                    response.HuellaLeida = FP_General.RegistrarGUID(Parametro_04, response.IdHuellaReferencia);
                    response.Mensaje = response.Identificador_Huella > 0 ? "Huella encontrada." : "Huella no registrada.";
                }
                else
                {
                    response.HuellaLeida = false;
                    FP_General.RegistrarGUID(Parametro_04, "");
                    response.Mensaje = response.Identificador_Huella > 0 ? "Huella encontrada." : "Huella no registrada.";
                }

                //// tomar el dato final 
                response.Codigo = FP_General.ExisteHuella ? 1 : 0;
            }
            catch (Exception ex)
            {
                response.Codigo = 0;
                response.Mensaje = ex.Message;  
            }

            return response;
        }
    }
}
