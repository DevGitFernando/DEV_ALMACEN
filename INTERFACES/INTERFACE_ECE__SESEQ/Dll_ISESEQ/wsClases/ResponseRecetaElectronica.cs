using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualBasic;
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

namespace Dll_ISESEQ.wsClases
{


    public class ResponseRecetaElectronicaItem
    {
        string sCampo = "";
        string sValor = "";

        public ResponseRecetaElectronicaItem(string Campo, string Valor)
        {
            sCampo = Campo;
            sValor = Valor;
        }

        public string Campo
        {
            get { return sCampo; }
            set { sCampo = value; }
        }

        public string Valor
        {
            get { return sValor; }
            set { sValor = value; }
        }
    }

    public class ResponseRecetaElectronica
    {
        int iEstatus = 100;
        string sError = "";
        List<ResponseRecetaElectronicaItem> itemsRespuesta = new List<ResponseRecetaElectronicaItem>();

        DataSet dtsRespuesta_General = new DataSet();
        DataSet dtsRespuestas = new DataSet();
        DataTable myDataTable = new DataTable();
        basGenerales Fg; 

        public ResponseRecetaElectronica()
        {
            Fg = new basGenerales(); 
        }

        #region Propiedades Publicas
        public int Estatus
        {
            get { return iEstatus; }
            set { iEstatus = value; }
        }

        public string Error
        {
            get { return sError; }
            set { sError = value; }
        }

        public DataSet Respuesta
        {
            get { return dtsRespuestas; }
            set { dtsRespuestas = value; }
        }
        #endregion Propiedades Publicas

        #region Funciones y Procedimientos Publicos
        public void AddItemRespuesta(string Campo, string Valor)
        {
            itemsRespuesta.Add(new ResponseRecetaElectronicaItem(Campo, Valor));
        }

        public void ResetItemsRespuesta()
        {
            itemsRespuesta = new List<ResponseRecetaElectronicaItem>();
        }

        public string GetStringList()
        {
            string sRegresa = "";

            //if (itemsRespuesta != null)
            //{
            //    foreach (ResponseRecetaElectronicaItem item in itemsRespuesta)
            //    {
            //        sRegresa += string.Format("<{0}>{1}</{0}>", item.Campo, item.Valor);
            //    }
            //}

            //sRegresa += string.Format("<{0}>{1}</{0}>", "Estatus", iEstatus);
            //sRegresa += string.Format("<{0}>{1}</{0}>", "Mensaje", sError);


            sRegresa = string.Format("{0}|{1}", iEstatus, sError);

            return sRegresa;
        }

        public string GetString()
        {
            string sRegresa = "";

            if (itemsRespuesta != null)
            {
                foreach (ResponseRecetaElectronicaItem item in itemsRespuesta)
                {
                    sRegresa += string.Format("<{0}>{1}</{0}>", item.Campo, item.Valor);
                }
            }

            sRegresa += string.Format("<{0}>{1}</{0}>", "Estatus", iEstatus);
            sRegresa += string.Format("<{0}>{1}</{0}>", "Mensaje", sError);

            return sRegresa;
        }


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
            myDataTable.Columns.Add("Estatus", System.Type.GetType("System.String"));
            myDataTable.Columns.Add("Mensaje", System.Type.GetType("System.String"));

            object[] obj = { iEstatus, sError };
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
            sRegresa = sRegresa.Replace("\n", "");

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
