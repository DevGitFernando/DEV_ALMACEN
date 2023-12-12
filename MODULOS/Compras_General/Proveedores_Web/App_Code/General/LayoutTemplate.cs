using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace Proveedores_Web
{
    /// <summary>
    /// Descripción breve de LayoutTemplate
    /// </summary>
    public class LayoutTemplate : ITemplate
    {
        static string[] sColumnas;
        public LayoutTemplate()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public void InstantiateIn(Control container)
        {
            var table = new HtmlGenericControl("table") { ID = "ListViewTable" };
            var tbody = new HtmlGenericControl();

            table.Attributes.Add("class", "display");
            table.Controls.Add(tbody);

            table.Controls.Add(new Literal() { Text = "<thead>" });
            for (int i = 0; i < sColumnas.Length; i++)
            {
                var sContenido = "<th>" + sColumnas[i] + "</th>";
                table.Controls.Add(new Literal() { Text = sContenido });
            }
            table.Controls.Add(new Literal() { Text = "</thead>" });

            var placeHolder = new HtmlGenericControl("PlaceHolder") { ID = "itemPlaceHolder" };

            table.Controls.Add(new Literal() { Text = "<tbody>" });
            table.Controls.Add(placeHolder);
            table.Controls.Add(new Literal() { Text = "</tbody>" });
            container.Controls.Add(table);
        }

        public void Columunas(string[] sCols)
        {
            sColumnas = sCols;
        }
    }
}