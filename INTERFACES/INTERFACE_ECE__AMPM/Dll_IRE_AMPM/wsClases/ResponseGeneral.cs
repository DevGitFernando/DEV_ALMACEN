using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Data; 

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.FuncionesGenerales; 

using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Linq.JsonPath;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;


namespace Dll_IRE_AMPM.wsClases
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
            string sRegresa = "";
            string sXML = "";
            DataSet dtsRetorno = new DataSet();

            dtsRespuesta_General = new DataSet("Response");
            myDataTable = new DataTable("ResponseGeneral"); // dtsRespuesta_General.Tables["ResponseGeneral"];           
            myDataTable.Columns.Add("CodigoEstatus", System.Type.GetType("System.String"));
            myDataTable.Columns.Add("Mensaje", System.Type.GetType("System.String"));

            object []obj = { iCodigoEstatus, sMensaje };
            myDataTable.Rows.Add(obj);

            dtsRespuesta_General.Tables.Add(myDataTable.Copy());
            foreach (DataTable table in dtsRespuestas.Tables)
            {
                dtsRespuesta_General.Tables.Add(table.Copy());
            }

            object objXML = dtsRespuesta_General.GetXml(); 
            sXML = dtsRespuesta_General.GetXml();
            sRegresa = Newtonsoft.Json.JsonConvert.SerializeObject(dtsRespuesta_General, Newtonsoft.Json.Formatting.Indented);


            XmlDocument xDoc = new XmlDocument();
            xDoc = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(sRegresa, "data");

            System.Data.DataSet dts = new System.Data.DataSet();
            dts.ReadXml(new XmlTextReader(new StringReader(xDoc.InnerXml)));

            dtsRetorno = dts.Copy();

            sRegresa = toUTF8(Fg.QuitarSaltoDeLinea(sRegresa.Replace("\t", ""))); 

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
        #endregion Funciones y Procedimientos Privados  
    }
}
