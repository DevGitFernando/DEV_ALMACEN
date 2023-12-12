using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net.Mail;
using System.Text;
using System.Web.Script.Serialization;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;

/// <summary>
/// Funciones generales que ayudaran al intercambio de información entre
/// el servidor y el cliente, todas estas serán basadas en html, json 
/// con funcionalidades especificas.
/// </summary>
public static class ClsToolsHtml
{
    static clsLeer leer;
    static clsConexionSQL cnn;

	static ClsToolsHtml()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
        cnn = DtGeneral.DatosConexion;
        leer = new clsLeer(ref cnn);
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

    /// <summary>
    /// Traduce la primera table de un DataSet a Table HTML, formateada en base a la información
    /// contenida, el nombre de las columnas serán el thead y los registros el tbody
    /// </summary>
    /// <param name="dtsGeneral">Dataset que será traducido a Table HTML</param>
    /// <param name="sIdTabla">Identificador que será agregado como ID a la tabla creada</param>
    /// <returns>Table HTML</returns>
    static public string DtsToTableHtml(DataSet dtsGeneral, string sIdTabla)
    {
        StringBuilder sTabla = new StringBuilder();
        DataSet dtsTabla = new DataSet(sIdTabla);
        leer.DataSetClase = dtsGeneral;

        string[] sColumnasNombre = leer.ColumnasNombre;

        StringBuilder sTHead = new StringBuilder();
        StringBuilder sTBody = new StringBuilder();

        sTHead.Append("<thead>");
        foreach (string value in sColumnasNombre)
        {
            string sTextColum = value;
            //sTHead.Append("<tr>");
            sTHead.AppendFormat("<th>{0}</th>", sTextColum);
            //sTHead.Append("</tr>");
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

    /// <summary>
    /// Traduce la primera table de un DataSet a Table HTML, formateada en base a la información
    /// contenida, el nombre de las columnas serán el thead y los registros el tbody
    /// </summary>
    /// <param name="dtsGeneral">Dataset que será traducido a Table HTML</param>
    /// <param name="sIdTabla">Identificador que será agregado como ID a la tabla creada</param>
    /// <returns>Table HTML</returns>
    static public string DtsToTableHtml(DataSet dtsGeneral, string sIdTabla, string sClass)
    {
        StringBuilder sTabla = new StringBuilder();
        DataSet dtsTabla = new DataSet(sIdTabla);
        leer.DataSetClase = dtsGeneral;

        string[] sColumnasNombre = leer.ColumnasNombre;

        StringBuilder sTHead = new StringBuilder();
        StringBuilder sTBody = new StringBuilder();

        sTHead.Append("<thead>");
        foreach (string value in sColumnasNombre)
        {
            string sTextColum = value;
            //sTHead.Append("<tr>");
            sTHead.AppendFormat("<th>{0}</th>", sTextColum);
            //sTHead.Append("</tr>");
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
        sTabla.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"{3}\" id=\"{2}\">{0}{1}</table>", sTHead, sTBody, sIdTabla, sClass);
        return sTabla.ToString();
    }

    /// <summary>
    /// Traduce la primera table de un DataSet a Table HTML, formateada en base a la información
    /// contenida, el nombre de las columnas serán el thead y los registros el tbody
    /// </summary>
    /// <param name="dtsGeneral">Dataset que será traducido a Table HTML</param>
    /// <param name="sIdTabla">Identificador que será agregado como ID a la tabla creada</param>
    /// <param name="sClass">Cadena que contenga las clases css necesarias</param>
    /// <param name="sColumnasNombres">Una arreglo de tipo cadena que debe contener ['nombreactualcolumna', 'nuevonombre']</param>
    /// <returns>Tabla HMTL</returns>
    static public string DtsToTableHtml(DataSet dtsGeneral, string sIdTabla, string sClass, string[,] sColumnasNombres)
    {
        StringBuilder sTabla = new StringBuilder();
        DataSet dtsTabla = new DataSet(sIdTabla);
        leer.DataSetClase = dtsGeneral;

        string[] sColumnasNombre = leer.ColumnasNombre;

        StringBuilder sTHead = new StringBuilder();
        StringBuilder sTBody = new StringBuilder();

        int iCol = 0;

        sTHead.Append("<thead>");
        foreach (string value in sColumnasNombre)
        {
            string sTextColum = value;
            
            if (sTextColum == sColumnasNombres[iCol, 0])
            {
                sTextColum = sColumnasNombres[iCol, 1];
            }
            iCol++;

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
        sTabla.AppendFormat("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"{3}\" id=\"{2}\">{0}{1}</table>", sTHead, sTBody, sIdTabla, sClass);
        return sTabla.ToString();
    }

    /// <summary>
    /// Ejectuta una consulta SQl y traduce primera table de un DataSet a Table HTML, formateada en base a la información
    /// contenida, el nombre de las columnas serán el thead y los registros el tbody
    /// </summary>
    /// <param name="sSql">Query que será ejecutada, esta debe retornar como resultado una tabla</param>
    /// <param name="sIdTabla">Identificador que será agregado como ID a la tabla creada</param>
    /// <returns>Table HTML</returns>
    static public string DtsToTableHtml(string sSql, string sIdTabla)
    {
        string sTabla = "";
        DataSet dtsTabla = new DataSet(sIdTabla);

        if (leer.Exec(sSql))
        {
            dtsTabla = leer.DataSetClase;
            sTabla = DtsToTableHtml(dtsTabla, sIdTabla);
        }

        return sTabla;
    }

    /// <summary>
    /// Ejectuta una consulta SQl y traduce primera table de un DataSet a Table HTML, formateada en base a la información
    /// contenida, el nombre de las columnas es personalizable en base a sColumnasNombre y estás serán el thead y los registros el tbody
    /// </summary>
    /// <param name="sSql">Query que será ejecutada, esta debe retornar como resultado una tabla</param>
    /// <param name="sIdTabla">Identificador que será agregado como ID a la tabla creada</param>
    /// <param name="sColumnasNombres">Una arreglo de tipo cadena que debe contener ['nombreactualcolumna', 'nuevonombre']</param>
    /// <returns>Table HTML</returns>
    static public string DtsToTableHtml(string sSql, string sIdTabla, string[,] sColumnasNombres)
    {
        string sTabla = "";
        int iCol = 0;
        DataSet dtsTabla = new DataSet(sIdTabla);

        if (leer.Exec(sSql))
        {
            string[] sColumnasNombre = leer.ColumnasNombre;

            string sTHead = "";
            string sTBody = "";

            sTHead += "<thead>";
            foreach (string value in sColumnasNombre)
            {
                //sTHead += "<tr>";
                string sTextColum = value;

                if (sTextColum == sColumnasNombres[iCol, 0])
                {
                    sTextColum = sColumnasNombres[iCol, 1];
                }
                iCol++;

                sTHead += string.Format("<th>{0}</th>", sTextColum);
                //sTHead += "</tr>";
            }
            sTHead += "</thead>";
            if (leer.Leer())
            {

                leer.RenombrarTabla(1, "Detalle");

                dtsTabla = leer.DataSetClase;
                sTBody = "<tbody>";
                foreach (DataRow Row in dtsTabla.Tables["Detalle"].Rows)
                {
                    object oType = "";
                    sTBody += "<tr>";
                    foreach (object item in Row.ItemArray)
                    {
                        oType = item.GetType();
                        if (item is DateTime)
                        {
                            //sTBody += string.Format("<td>{0}</td>", General.FechaYMD((DateTime)item));
                            sTBody += string.Format("<td>{0}</td>", ((DateTime)item).ToString("yyyy-MM-dd hh:mm tt"));
                        }
                        else if (item is Decimal)
                        {
                            double sMoney = Convert.ToDouble(item);
                            sTBody += string.Format("<td>{0}</td>", sMoney.ToString("$ ##,###,##0.00"));
                        }
                        else
                        {
                            sTBody += string.Format("<td>{0}</td>", item);
                        }
                    }
                    sTBody += "</tr>";
                }
                sTBody += "</tbody>";
            }
            sTabla = string.Format("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"display\" id=\"{2}\">{0}{1}</table>", sTHead, sTBody, sIdTabla);
        }

        return sTabla;
    }

    /// <summary>
    /// Ejectuta una consulta SQl y traduce primera table de un DataSet a Table HTML, formateada en base a la información
    /// contenida, el nombre de las columnas es personalizable en base a sColumnasNombre y estás serán el thead y los registros el tbody
    /// </summary>
    /// <param name="sSql">Query que será ejecutada, esta debe retornar como resultado una tabla</param>
    /// <param name="sIdTabla">Identificador que será agregado como ID a la tabla creada</param>
    /// <param name="sColumnasNombres">Una arreglo de tipo cadena que debe contener ['nombreactualcolumna', 'nuevonombre']</param>
    /// <param name="sColumnasStyles">Una arreglo de tipo cadena que debe contener ['nombreactualcolumna', 'style']</param>
    /// <returns>Table HTML</returns>
    static public string DtsToTableHtml(string sSql, string sIdTabla, string[,] sColumnasNombres, string[,] sColumnasStyles)
    {
        string sTabla = "";
        int iCol = 0;
        DataSet dtsTabla = new DataSet(sIdTabla);

        if (leer.Exec(sSql))
        {
            string[] sColumnasNombre = leer.ColumnasNombre;

            string sTHead = "";
            string sTBody = "";

            sTHead += "<thead>";
            foreach (string value in sColumnasNombre)
            {
                //sTHead += "<tr>";
                string sTextColum = value;
                string sClass = "";

                if (sTextColum == sColumnasNombres[iCol, 0])
                {
                    sTextColum = sColumnasNombres[iCol, 1];
                }

                if (sTextColum == sColumnasStyles[iCol, 0])
                {
                    sClass = sColumnasStyles[iCol, 1];
                }
                
                if(sClass == "")
                {
                    sTHead += string.Format("<th>{0}</th>", sTextColum);
                }
                else
                {
                    sTHead += string.Format("<th class=\"{0}\">{1}</th>", sClass, sTextColum);
                }

                iCol++;
                //sTHead += "</tr>";
            }
            sTHead += "</thead>";
            if (leer.Leer())
            {

                leer.RenombrarTabla(1, "Detalle");

                dtsTabla = leer.DataSetClase;

                sTBody = "<tbody>";
                foreach (DataRow Row in dtsTabla.Tables["Detalle"].Rows)
                {
                    sTBody += "<tr>";
                    foreach (object item in Row.ItemArray)
                    {
                        if (item is DateTime)
                        {
                            sTBody += string.Format("<td>{0}</td>", General.FechaYMD((DateTime)item));
                        }
                        else
                        {
                            sTBody += string.Format("<td>{0}</td>", item);
                        }
                    }
                    sTBody += "</tr>";
                }
                sTBody += "</tbody>";
            }
            sTabla = string.Format("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"display\" id=\"{2}\">{0}{1}</table>", sTHead, sTBody, sIdTabla);
        }

        return sTabla;
    }

    static public string DtsToTableHtml(DataSet dtsGeneral, string sIdTabla, string[,] sColumnasNombres, string[,] sColumnasStyles)
    {
        DataSet dtsTabla = new DataSet(sIdTabla);
        leer.DataSetClase = dtsGeneral;
        string sTabla = "";
        int iCol = 0;

        //if (leer.Exec(sSql))
        {
            string[] sColumnasNombre = leer.ColumnasNombre;

            string sTHead = "";
            string sTBody = "";

            sTHead += "<thead>";
            foreach (string value in sColumnasNombre)
            {
                //sTHead += "<tr>";
                string sTextColum = value;
                string sClass = "";

                if (sTextColum == sColumnasNombres[iCol, 0])
                {
                    sTextColum = sColumnasNombres[iCol, 1];
                    sClass = sColumnasStyles[iCol, 1];
                }

                if (sClass == "")
                {
                    sTHead += string.Format("<th>{0}</th>", sTextColum);
                }
                else
                {
                    sTHead += string.Format("<th class=\"{0}\">{1}</th>", sClass, sTextColum);
                }

                iCol++;
                //sTHead += "</tr>";
            }
            sTHead += "</thead>";
            if (leer.Leer())
            {

                leer.RenombrarTabla(1, "Detalle");

                dtsTabla = leer.DataSetClase;

                sTBody = "<tbody>";
                foreach (DataRow Row in dtsTabla.Tables["Detalle"].Rows)
                {
                    object oType = "";
                    sTBody += "<tr>";
                    foreach (object item in Row.ItemArray)
                    {
                        oType = item.GetType();
                        if (item is DateTime)
                        {
                            sTBody += string.Format("<td>{0}</td>", General.FechaYMD((DateTime)item));
                        }
                        else if (item is Decimal)
                        {
                            double sMoney = Convert.ToDouble(item);
                            sTBody += string.Format("<td>{0}</td>", sMoney.ToString("$ ##,###,##0.00"));
                        }
                        else
                        {
                            sTBody += string.Format("<td>{0}</td>", item);
                        }
                    }
                    sTBody += "</tr>";
                }
                sTBody += "</tbody>";
            }
            sTabla = string.Format("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"display\" id=\"{2}\">{0}{1}</table>", sTHead, sTBody, sIdTabla);
        }

        return sTabla;
    }

    static public string DtsToTableHtml(DataSet dtsGeneral, string sIdTabla, string[,] sColumnasNombres)
    {
        DataSet dtsTabla = new DataSet(sIdTabla);
        leer.DataSetClase = dtsGeneral;
        string sTabla = "";
        int iCol = 0;

        //if (leer.Exec(sSql))
        {
            string[] sColumnasNombre = leer.ColumnasNombre;

            string sTHead = "";
            string sTBody = "";

            sTHead += "<thead>";
            foreach (string value in sColumnasNombre)
            {
                //sTHead += "<tr>";
                string sTextColum = value;
                if (sTextColum == sColumnasNombres[iCol, 0])
                {
                    sTextColum = sColumnasNombres[iCol, 1];
                }


                sTHead += string.Format("<th>{0}</th>", sTextColum);
                
                iCol++;
                //sTHead += "</tr>";
            }
            sTHead += "</thead>";
            if (leer.Leer())
            {

                leer.RenombrarTabla(1, "Detalle");

                dtsTabla = leer.DataSetClase;

                sTBody = "<tbody>";
                foreach (DataRow Row in dtsTabla.Tables["Detalle"].Rows)
                {
                    object oType = "";
                    sTBody += "<tr>";
                    foreach (object item in Row.ItemArray)
                    {
                        oType = item.GetType();
                        if (item is DateTime)
                        {
                            sTBody += string.Format("<td>{0}</td>", General.FechaYMD((DateTime)item));
                        }
                        else if (item is Decimal)
                        {
                            double sMoney = Convert.ToDouble(item);
                            sTBody += string.Format("<td>{0}</td>", sMoney.ToString("$ ##,###,##0.00"));
                        }
                        else
                        {
                            sTBody += string.Format("<td>{0}</td>", item);
                        }
                    }
                    sTBody += "</tr>";
                }
                sTBody += "</tbody>";
            }
            sTabla = string.Format("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"display\" id=\"{2}\">{0}{1}</table>", sTHead, sTBody, sIdTabla);
        }

        return sTabla;
    }

    public static string CreateDropList(string sId, string sClass, DataSet dtsData, string sValue, string sText, bool AddSelect)
    {
        string sEstructura = string.Empty;

        sEstructura = "<select";

        if (sId != "")
        {
            sEstructura += string.Format(" id=\"{0}\"", sId);
        }

        if (sClass != "")
        {
            sEstructura += string.Format(" class=\"{0}\"", sClass);
        }

        sEstructura += ">";

        sEstructura += OptionDropList(dtsData, sValue, sText, AddSelect);

        sEstructura += "</select>";

        return sEstructura;
    }

    public static string CreateDropList(string sId, string sClass, DataTable dtData, string sValue, string sText, bool AddSelect)
    {
        string sEstructura = string.Empty;

        sEstructura = "<select";

        if (sId != "")
        {
            sEstructura += string.Format(" id=\"{0}\"", sId);
        }

        if (sClass != "")
        {
            sEstructura += string.Format(" class=\"{0}\"", sClass);
        }

        sEstructura += ">";

        sEstructura += OptionDropList(dtData, sValue, sText, AddSelect);

        sEstructura += "</select>";

        return sEstructura;
    }

    public static string CreateDropList(string sId, string sClass, DataSet dtsData, string sValue, string sText, bool AddSelect, string sValueSelected, bool bEnabled)
    {
        string sEstructura = string.Empty;

        sEstructura = "<select";

        if (sId != "")
        {
            sEstructura += string.Format(" id=\"{0}\"", sId);
        }

        if (sClass != "")
        {
            sEstructura += string.Format(" class=\"{0}\"", sClass);
        }

        if (!bEnabled)
        {
            sEstructura += "disabled";
        }
        
        sEstructura += ">";

        sEstructura += OptionDropList(dtsData, sValue, sText, AddSelect, sValueSelected);

        sEstructura += "</select>";

        return sEstructura;
    }

    public static string CreateDropList(string sId, string sClass)
    {
        string sEstructura = string.Empty;

        sEstructura = "<select";

        if (sId != "")
        {
            sEstructura += string.Format(" id=\"{0}\"", sId);
        }

        if (sClass != "")
        {
            sEstructura += string.Format(" class=\"{0}\"", sClass);
        }

        sEstructura += ">";

        sEstructura += "<option value=\"0\">&lt;&lt;Seleccione&gt;&gt;</option>";

        sEstructura += "</select>";

        return sEstructura;
    }

    public static string OptionDropList(DataSet dtsData, string sValue, string sText, bool AddSelect)
    {
        string sEstructura = string.Empty;

        if (AddSelect)
        {
            sEstructura = "<option value=\"0\">&lt;&lt;Seleccione&gt;&gt;</option>";
        }

        leer.DataSetClase = dtsData;

        while (leer.Leer())
        {
            sEstructura += string.Format("<option value=\"{0}\">{1}</option>", leer.Campo(sValue), leer.Campo(sText));
        }

        return sEstructura;
    }

    public static string OptionDropList(DataTable dtData, string sValue, string sText, bool AddSelect)
    {
        string sEstructura = string.Empty;
        string sValues = string.Empty;

        if (AddSelect)
        {
            sEstructura = "<option value=\"0\">&lt;&lt;Seleccione&gt;&gt;</option>";
        }

        leer.DataTableClase = dtData;

        while (leer.Leer())
        {
            //sEstructura += string.Format("<option value=\"{0}\">{0} -- {1}</option>", leer.Campo(sValue), leer.Campo(sText));
            sEstructura += string.Format("<option value=\"{0}\">{1}</option>", leer.Campo(sValue), leer.Campo(sText));
        }

        return sEstructura;
    }

    public static string OptionDropList(DataSet dtsData, string sValue, string sText, bool AddSelect, string sValueSelected)
    {
        string sEstructura = string.Empty;

        if (AddSelect)
        {
            sEstructura = "<option value=\"0\">&lt;&lt;Seleccione&gt;&gt;</option>";
        }

        leer.DataSetClase = dtsData;

        while (leer.Leer())
        {
            if (leer.Campo(sValue) == sValueSelected)
            {
                sEstructura += string.Format("<option value=\"{0}\" selected>{1}</option>", leer.Campo(sValue), leer.Campo(sText));
            }
            else
            {
                sEstructura += string.Format("<option value=\"{0}\">{1}</option>", leer.Campo(sValue), leer.Campo(sText));
            }
        }

        return sEstructura;
    }

    public static string OptionDropList(DataSet dtsData, string sValue, string sText, bool AddSelect, bool SelectePersonalizado,  string sValueSelect)
    {
        string sEstructura = string.Empty;

        if (AddSelect)
        {
            if (SelectePersonalizado)
            {
                sEstructura = string.Format("<option value=\"0\">&lt;&lt;{0}&gt;&gt;</option>", sValueSelect);
            }
            else
            {
                sEstructura = "<option value=\"0\">&lt;&lt;Seleccione&gt;&gt;</option>";
            }
            
        }

        leer.DataSetClase = dtsData;

        while (leer.Leer())
        {
            sEstructura += string.Format("<option value=\"{0}\">{0} -- {1}</option>", leer.Campo(sValue), leer.Campo(sText));
        }

        return sEstructura;
    }

    public static string OptionDropList(DataSet dtsData, string sValue, string sText, string sValueFirstOption, string sTextFirstOption)
    {
        string sEstructura = string.Empty;

        if (sValueFirstOption != "" && sTextFirstOption != "")
        {
            sEstructura = string.Format("<option value=\"{0}\">{1}</option>", sValueFirstOption, sTextFirstOption);
        }

        leer.DataSetClase = dtsData;

        while (leer.Leer())
        {
            sEstructura += string.Format("<option value=\"{0}\">{1}</option>", leer.Campo(sValue), leer.Campo(sText));
        }

        return sEstructura;
    }

    public static string OptionDropList(DataSet dtsData, string sValue, string sText, string sValueFirstOption, string sTextFirstOption, string sValueSelected)
    {
        string sEstructura = string.Empty;

        if (sValueFirstOption != "" && sTextFirstOption != "")
        {
            sEstructura = string.Format("<option value=\"{0}\">{1}</option>", sValueFirstOption, sTextFirstOption);
        }

        leer.DataSetClase = dtsData;

        while (leer.Leer())
        {
            if (leer.Campo(sValue) == sValueSelected)
            {
                sEstructura += string.Format("<option value=\"{0}\" selected>{1}</option>", leer.Campo(sValue), leer.Campo(sText));
            }
            else
            {
                sEstructura += string.Format("<option value=\"{0}\">{1}</option>", leer.Campo(sValue), leer.Campo(sText));
            }
        }

        return sEstructura;
    }

}