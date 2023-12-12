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

using Dll_SII_IMediaccess.Clases; 
using Dll_SII_IMediaccess.wsClases; 

namespace Dll_SII_IMediaccess
{
    [WebService(Description = "Módulo Interface de Comunicación EPharma", Namespace = "http://SC-Solutions/ServiciosWeb/")]
    public class wsEPharma
    {
        /// <summary>
        /// Proporciona funciones generales que se implementan en la clase
        /// </summary>
        basSeguridad funciones; // = new basSeguridad();
        clsCriptografo Cryp = new clsCriptografo();
        string sFileConexion = "SII Mediaccess";
        string sFileConexion_Consulta = "SII Mediaccess Query";

        #region Funciones y Procedimientos Estandard
        public string SetServidor(string Servidor)
        {
            string sRegresa = "";
            wsTestConexion test = new wsTestConexion();

            sRegresa = test.RevisarServidor(Servidor);

            return sRegresa;
        }

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

            return datosCnn.DatosCnn();
        }

        private DataSet PrepararError(Exception ErrorDetectado)
        {
            clsLeer leerError = new clsLeer();
            DataSet dtsRetorno = leerError.ListaDeErrores();
            string sMensaje = "", sNumError = "", sSqlEstado = "";

            try
            {
                sMensaje = ErrorDetectado.Message.Replace("'", "" + Strings.Chr(34) + "");
                sNumError = "0";
                sSqlEstado = "";

                object[] obj = { sMensaje, sNumError, sSqlEstado };
                dtsRetorno.Tables["Errores"].Rows.Add(obj);
            }
            catch
            {
            }

            return dtsRetorno;
        }
        #endregion Funciones y Procedimientos Estandard

        #region Funciones y Procedimientos Publicos 
        [WebMethod(Description = "Obtener información de Traspasos")]
        public DataSet GetInformacionTraspasos(string IdEstado, string IdFarmacia, string Folio)
        {
            DataSet dtsRetorno = new DataSet();
            string sSql = ""; 

            try
            {
                clsDatosConexion datosCnn = new clsDatosConexion(AbrirConexionEx(sFileConexion_Consulta));
                clsConexionSQL cnn = new clsConexionSQL(datosCnn);
                clsLeer leer = new clsLeer(ref cnn);
                clsGrabarError Error = new clsGrabarError(datosCnn, GnDll_SII_IMediaccess.DatosApp, "wsEPharma");

                IdEstado = General.Fg.PonCeros(IdEstado, 2);
                IdFarmacia = General.Fg.PonCeros(IdFarmacia, 4);
                Folio = "STG" + General.Fg.PonCeros(Folio, 8);

                sSql = string.Format(
                    "Select E.IdEstado, E.IdFarmacia, E.FolioMovtoInv, E.FechaRegistro as FechaTraspaso, D.CodigoEAN, P.Descripcion, cast(D.Cantidad as int) as Cantidad  \n " + 
	                " From MovtosInv_Enc E (NoLock)  \n " + 
	                " Inner Join MovtosInv_Det_CodigosEAN D (NoLock)  \n " +  
	                "	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  \n " + 
	                "Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )  \n " +
                    "Where E.IdTipoMovto_Inv = 'STG' and E.IdEstado = '{0}' and E.IdFarmacia = '{1}' and E.FolioMovtoInv = '{2}'   \n " + 
	                "Order By E.IdEstado, E.IdFarmacia, E.FolioMovtoInv, P.Descripcion   \n ", IdEstado, IdFarmacia, Folio);


                if (!leer.Exec(sSql))
                {
                    dtsRetorno = leer.ListaDeErrores();
                    Error.GrabarError(leer, "GetInformacionTraspasos"); 
                }
                else
                {
                    dtsRetorno = leer.DataSetClase; 
                }
            }
            catch (Exception ex)
            {
                dtsRetorno = PrepararError(ex); 
            }

            return dtsRetorno;
        }
        #endregion Funciones y Procedimientos Publicos 
    }
}
