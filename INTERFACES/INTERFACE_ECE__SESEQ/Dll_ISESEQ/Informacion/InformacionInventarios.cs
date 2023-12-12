using System;
using System.Collections;
using System.Collections.Generic;
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

using System.Text;
using System.IO;
using System.Configuration;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using Dll_ISESEQ;
using Dll_ISESEQ.wsClases;

namespace Dll_ISESEQ.Informacion
{
    public class InformacionInventarios
    {
        clsDatosConexion datosDeConexion;
        clsConexionSQL cnn;
        clsLeer leer;
        clsLeer leerXML;
        clsGrabarError Error;

        string sIdEmpresa = "";
        string sIdEstado = "";
        string sIdFarmacia = "";

        string sIdUMedica = "";
        string sInformacion_XML = "";
        string sInformacion_XML_Converted = "";
        string sFolio_SESEQ = "";
        string sExpediente_SESEQ = "";
        string sFolioReceta_SESEQ = "";
        string sFolioRegistro = "";
        string sFolioRegistro_XML = "";
        string sMensajesError = "";
        TipoProcesoReceta tpProceso = TipoProcesoReceta.Ninguno;
        string sTipoDeProceso = "";
        string sCLUES = "";
        int iRecepcionPrevia = 0;


        DataSet dtsInformacion = new DataSet();

        basGenerales Fg = new basGenerales();
        string sEncabezado = "";
        Dictionary<TipoProcesoReceta, TipoProcesoReceta> listaProcesos = new Dictionary<TipoProcesoReceta, TipoProcesoReceta>();
        TipoProcesoReceta proceso = TipoProcesoReceta.Ninguno;

        #region Constructor de Clase 
        public InformacionInventarios(clsDatosConexion DatosConexion)
        {
            datosDeConexion = DatosConexion;
            cnn = new clsConexionSQL(datosDeConexion);
            leer = new clsLeer(ref cnn);
            leerXML = new clsLeer();

            Error = new clsGrabarError(datosDeConexion, GnDll_SII_SESEQ.DatosApp, "RecetaElectronica");
            Error.NombreLogErorres = "INT_SESEQ__CtlErrores";

            sEncabezado = string.Format("<?xml version={0}1.0{0} encoding={0}UTF-8{0}?>", Fg.Comillas());
        }
        #endregion Constructor de Clase

        #region Funciones y Procedimientos Publicos
        public ResponseGeneral DescargarInventario(string Informacion_XML, int TipoUnidad)
        {
            ResponseRecetaElectronica respuesta = new ResponseRecetaElectronica();
            ResponseGeneral response = new ResponseGeneral();
            Error = new clsGrabarError(General.DatosApp, "Guardar()");


            clsLeer datos = new clsLeer();
            string sSql = string.Format("Exec spp_INT_SESEQ__DescargarInventarios @ListaDeCLUES = '{0}', @TipoUnidad = '{1}' ", Informacion_XML, TipoUnidad); 

            ////XmlDocument xDoc = new XmlDocument();
            ////xDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(Informacion_XML, "DtsInformacion");
            ////sInformacion_XML_Converted = xDoc.InnerXml;

            ////Informacion_XML = toUTF8(Fg.QuitarSaltoDeLinea(Informacion_XML.Replace("\t", "")));
            ////dtsInformacion = new DataSet();
            //////dtsInformacion.ReadXml(new XmlTextReader(new StringReader(Informacion_XML)));
            ////dtsInformacion.ReadXml(new XmlTextReader(new StringReader(xDoc.InnerXml)));
            ////sInformacion_XML_Converted = dtsInformacion.GetXml();


            ////dtsInformacion = PrepararInformacion(dtsInformacion);

            if (!leer.Exec(sSql))
            {
                response.Estatus = 1;
                response.Mensaje = "Error de conexion con el servidor de datos.";
            }
            else
            {
                if (!leer.Leer())
                {
                    response.Estatus = 2;
                    response.Mensaje = "No se encontro informacion de inventarios.";
                }
                else
                {
                    leer.RenombrarTabla(1, "Inventario");

                    response.Estatus = 100;
                    response.Mensaje = "Informacion de inventarios encontrada.";
                    response.Respuesta = leer.DataSetClase;
                }
            }


            return response;
        }
        #endregion Funciones y Procedimientos Publicos

        #region Funciones y Procedimientos Publicos
        public string toUTF8(string Cadena)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(Cadena);
            return encoding.GetString(bytes);
        }

        public string Normalizar(string Cadena)
        {
            string sRegresa = Cadena;

            string consignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜÑçÇ";
            string sinsignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUNcC";

            if (Cadena != "")
            {
                for (int i = 0; i <= consignos.Length - 1; i++)
                {
                    sRegresa = sRegresa.Replace(consignos[i], sinsignos[i]);
                }
            }

            return sRegresa;
        }

        public DataSet PrepararInformacion(DataSet Informacion)
        {
            DataSet dtsRetorno = new DataSet();
            clsLeer leer = new clsLeer();

            leer.DataSetClase = Informacion;

            if (leer.ExisteTabla("Generales"))
            {
                leer.RenombrarTabla("Generales", "General");
            }

            return leer.DataSetClase.Copy();
        }
        #endregion Funciones y Procedimientos Publicos
    }
}
