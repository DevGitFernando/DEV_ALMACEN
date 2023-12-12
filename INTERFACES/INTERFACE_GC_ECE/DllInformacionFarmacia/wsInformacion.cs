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
using System.Reflection;
using System.Security.Cryptography;
using System.Data.Odbc;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;


namespace DllInformacionFarmacia 
{
    [WebService(Description = "Modulo conexión", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsInformacion : System.Web.Services.WebService
    {
        /// <summary>
        /// Proporciona funciones generales que se implementan en la clase
        /// </summary> 
        /// 

        clsDatosConexion datosCnn = new clsDatosConexion();
        basSeguridad funciones; // = new basSeguridad();
        clsCriptografo Cryp = new clsCriptografo();
        string sConfig = "SII-AccesoDatos";

        string sHost = "";
        string sNombreHost = "";

        #region Funciones y Procedimientos Standar
        /// <summary>
        /// Obtiene los datos de conexion con el servidor de BD
        /// </summary>
        /// <returns>Regresa un Dataset con la información completa para la conexion con el servidor.</returns>
        [WebMethod(Description = ".")]
        private void AbrirConexion()
        {
            datosCnn = new clsDatosConexion();
            funciones = new basSeguridad(sConfig);

            datosCnn.Servidor = funciones.Servidor;
            datosCnn.Puerto = funciones.Puerto;
            datosCnn.BaseDeDatos = funciones.BaseDeDatos;
            datosCnn.Usuario = funciones.Usuario;
            datosCnn.Password = funciones.Password;
            datosCnn.TipoDBMS = funciones.TipoDBMS;
            datosCnn.NormalizarDatos();

            sHost = this.Context.Request.UserHostAddress;
            sNombreHost = this.Context.Request.UserHostName;

            // return datosCnn.DatosCnn();
        }

        [WebMethod(Description = "Conectividad.")]
        public string Conectividad()
        {
            string sResultado = "Prueba";
            bool bRegresa = true;

            // clsDatosConexion datosCnn = new clsDatosConexion();
            clsConexionSQL cnn = new clsConexionSQL();
            clsLeer leer = new clsLeer();

            try
            {
                AbrirConexion();
                // datosCnn = new clsDatosConexion(AbrirConexion());
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
                    if (!leer.Exec("Select getdate() as Fecha"))
                    {
                        sResultado = leer.Error.Message;
                    }
                    else
                    {
                        //sResultado = "Fecha servidor :  " + leer.CampoFecha("Fecha").ToLongDateString().ToUpper();
                        sResultado = "Fecha servidor :  " + leer.CampoFecha("Fecha").ToString().ToUpper();
                    }
                }
                catch { }
            }

            return sResultado;

        }
        #endregion Funciones y Procedimientos Standar

        [WebMethod(Description = "Cuadro Básico de la Unidad.")]
        public DataSet CuadroBasico(string IdEstado, string IdFarmacia)
        {
            DataSet dts = new DataSet();

            AbrirConexion();
            clsGetInformacion inf = new clsGetInformacion(datosCnn, IdEstado, IdFarmacia,sHost, sNombreHost);

            dts = inf.CuadroBasico();

            return dts;
        }

        [WebMethod(Description = "Cuadro Básico de la Unidad.")]
        public DataSet Existencia_Clave(string IdEstado, string IdFarmacia, string ClaveSSA)
        {
            DataSet dts = new DataSet();

            AbrirConexion();
            clsGetInformacion inf = new clsGetInformacion(datosCnn, IdEstado, IdFarmacia, sHost, sNombreHost);

            dts = inf.Existencia(ClaveSSA);

            return dts;
        }

        [WebMethod(Description = "Cuadro Básico de la Unidad.")]
        public DataSet Existencia_Clave_Grupo(string IdEstado, string IdFarmacia, string ClaveSSA)
        {
            DataSet dts = new DataSet(); 

            AbrirConexion(); 
            clsGetInformacion inf = new clsGetInformacion(datosCnn, IdEstado, IdFarmacia, sHost, sNombreHost); 

            dts = inf.Existencia(ClaveSSA, true);

            return dts;
        } 
    }
}
