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

using System.Text;
using System.IO;
using System.Configuration;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.FuncionesGrid;

using DllFarmaciaSoft;

using Dll_ISESEQ;
using Dll_ISESEQ.wsClases;
using Dll_ISESEQ.wsAcuseProcesos_cnnInterna;

namespace Dll_ISESEQ.wsClases
{
    public class ResponseGeneral
    {
        int iCodigoEstatus = 0;
        string sMensaje = "";

        DataSet dtsRespuesta_General = new DataSet();
        DataSet dtsRespuestas = new DataSet();
        DataTable myDataTable = new DataTable();

        basGenerales Fg; 

        public ResponseGeneral()
        {
            Fg = new basGenerales(); 
        }

        #region Propiedades Publicas 
        public int Estatus
        {
            get { return iCodigoEstatus; }
            set { iCodigoEstatus = value; }
        }

        public string Mensaje
        {
            get { return sMensaje; }
            set { sMensaje = value; }
        }

        public DataSet Respuesta
        {
            get { return dtsRespuestas; }
            set { dtsRespuestas = value; }
        }
        #endregion Propiedades Publicas 

        #region Funciones y Procedimientos Publicos 
        public bool ClearInformacion()
        {
            bool bRegresa = false;

            try
            {
                dtsRespuesta_General = new DataSet();
                dtsRespuestas = new DataSet(); 
                bRegresa = true;
            }
            catch 
            { }

            return bRegresa; 
        }

        public bool Add_Table(DataTable Datos)
        {
            bool bRegresa = false;

            try
            {
                dtsRespuestas.Tables.Add(Datos.Copy());

                bRegresa = true; 
            }
            catch 
            { 
            }

            return bRegresa; 
        }

        public string GetResponse()
        {
            return GetResponse(true);
        }
        public string GetResponse(bool QuitarSaltoDeLinea)
        {
            string sRegresa = "";
            string sXML = "";
            DataSet dtsRetorno = new DataSet();

            dtsRespuesta_General = new DataSet("Response");
            ////myDataTable = new DataTable("ResponseGeneral"); // dtsRespuesta_General.Tables["ResponseGeneral"];           
            ////myDataTable.Columns.Add("CodigoEstatus", System.Type.GetType("System.String"));
            ////myDataTable.Columns.Add("Mensaje", System.Type.GetType("System.String"));

            ////object []obj = { iCodigoEstatus, sMensaje };
            ////myDataTable.Rows.Add(obj);

            ////dtsRespuesta_General.Tables.Add(myDataTable.Copy());
            foreach (DataTable table in dtsRespuestas.Tables)
            {
                dtsRespuesta_General.Tables.Add(table.Copy());
            }

            object objXML = dtsRespuesta_General.GetXml();
            sXML = Normalizar(dtsRespuesta_General.GetXml());


            sRegresa = Newtonsoft.Json.JsonConvert.SerializeObject(dtsRespuesta_General, Newtonsoft.Json.Formatting.Indented);
            sRegresa = Normalizar(sRegresa);


            XmlDocument xDoc = new XmlDocument();
            xDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(sRegresa, "data");

            System.Data.DataSet dts = new System.Data.DataSet();
            dts.ReadXml(new XmlTextReader(new StringReader(xDoc.InnerXml)));

            dtsRetorno = dts.Copy();

            if(QuitarSaltoDeLinea)
            {
                sRegresa = toUTF8(Fg.QuitarSaltoDeLinea(sRegresa.Replace("\t", "")));
            }
            else
            {
                sRegresa = toUTF8(sRegresa.Replace("\t", ""));
            }
            return sRegresa;
        }

        #endregion Funciones y Procedimientos Publicos 

        #region Funciones y Procedimientos Privados
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
        #endregion Funciones y Procedimientos Privados  
    }
}
