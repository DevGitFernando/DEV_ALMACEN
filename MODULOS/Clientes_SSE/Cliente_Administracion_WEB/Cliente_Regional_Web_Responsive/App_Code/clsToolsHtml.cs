using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Text;
using System.Web.Script.Serialization;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
/// <summary>
/// Descripción breve de clsToolsHtml
/// </summary>
public static class clsToolsHtml
{
    static clsLeer leer;
    //static clsConexionSQL cnn;

	static clsToolsHtml()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
        //cnn = DtGeneral.DatosConexion;
        //leer = new clsLeer(ref cnn);
        leer = new clsLeer();
	}

    static public string DtsToTableHtml(DataSet dtsInfoGeneral, string sIdTabla)
    {
        StringBuilder sTabla = new StringBuilder();
        DataSet dtsTabla = new DataSet(sIdTabla);
        leer.DataSetClase = dtsInfoGeneral;

        string[] sColumnasNombre = leer.ColumnasNombre;

        StringBuilder sTHead = new StringBuilder();
        StringBuilder sTBody = new StringBuilder();

        sTHead.Append("<thead>");
        foreach (string value in sColumnasNombre)
        {
            string sTextColum = value;

            sTHead.AppendFormat("<th>{0}</th>", sTextColum);

        }
        sTHead.Append("</thead>");
        if (leer.Leer())
        {

            leer.RenombrarTabla(1, sIdTabla);

            dtsTabla = leer.DataSetClase;
            sTBody.Append("<tbody>");
            foreach (DataRow Row in dtsTabla.Tables[sIdTabla].Rows)
            {
                sTBody.Append("<tr>");
                foreach (object item in Row.ItemArray)
                {
                    if (item is DateTime)
                    {
                        sTBody.AppendFormat("<td>{0}</td>", General.FechaYMD((DateTime)item));
                    }
                    else
                    {
                        sTBody.AppendFormat("<td>{0}</td>", item);
                    }
                }
                sTBody.Append("</tr>");
            }
            sTBody.Append("</tbody>");
        }
        sTabla.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"display\" id=\"{2}\">{0}{1}</table>", sTHead, sTBody, sIdTabla);
        return sTabla.ToString();
    }



    static public string DtsToTableHtml(string sSql, string sIdTabla)
    {
        string sTabla = "";
        DataSet dtsTabla = new DataSet(sIdTabla);

        dtsTabla = DtGeneral.ExecQuery(sSql, "TableHTML");

        sTabla = DtsToTableHtml(dtsTabla, sIdTabla);
        
        return sTabla;
    }


    /// <summary>
    /// Función que serializa un DataSet, sin importar el número de tablas que contega,
    /// la serialización es realizado a JSON en base a {key:value}.
    /// </summary>
    /// <param name="dtsInfo">Dataset que será convertido en JSON</param>
    /// <returns>Cadena JSON</returns>
    public static string SerializerDataSet(DataSet dtsInfo)
    {

        DataTable dtTable = new DataTable();
        Dictionary<string, object> lstTables = new Dictionary<string, object>();
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        string sReturn = "";

        if (dtsInfo.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dtsInfo.Tables.Count; i++)
            {

                dtTable = dtsInfo.Tables[i];
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = null;

                foreach (DataRow dr in dtTable.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dtTable.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                lstTables.Add(dtTable.TableName.ToString(), rows);
            }

            sReturn = jsSerializer.Serialize(lstTables);
        }

        return sReturn;
    }

    public static string OptionDropList(DataTable dtData, string sValue, string sText, bool AddSelect)
    {
        clsLeer leer = new clsLeer();
        string sEstructura = string.Empty;
        string sValues = string.Empty;

        if (AddSelect)
        {
            sEstructura = "<option value=\"0\">&lt;&lt; Seleccione &gt;&gt;</option>";
        }

        leer.DataTableClase = dtData;

        while (leer.Leer())
        {
            sEstructura += string.Format("<option value=\"{0}\">{0} -- {1}</option>", leer.Campo(sValue), leer.Campo(sText));
        }

        return sEstructura;
    }

    public static string GetAutoFormat(Type type)
    {
        string format = "General";
        if (type == typeof(int))
            format = "0";
        else if (type == typeof(uint))
            format = "0";
        else if (type == typeof(long))
            format = "0";
        else if (type == typeof(ulong))
            format = "0";
        else if (type == typeof(short))
            format = "0";
        else if (type == typeof(ushort))
            format = "0";
        else if (type == typeof(double))
            format = "0.00";
        else if (type == typeof(float))
            format = "0.00";
        else if (type == typeof(decimal))
            //format = NumberFormatInfo.CurrentInfo.CurrencySymbol + " #,##0.00";
            format = "#,##0.00";
        else if (type == typeof(DateTime))
            format = "yyyy-MM-dd";
        else if (type == typeof(string))
            format = "@";
        else if (type == typeof(bool))
            format = "\"" + bool.TrueString + "\";\"" + bool.TrueString + "\";\"" + bool.FalseString + "\"";

        return format;

    }


}