using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Proveedores_Web
{
    /// <summary>
    /// Descripción breve de ItemTemplate
    /// </summary>
    public class ItemTemplate : ITemplate
    {
        public ItemTemplate()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public void InstantiateIn(Control container)
        {
            var row = new HtmlGenericControl("tr");

            row.DataBinding += DataBinding;
            container.Controls.Add(row);
        }

        public void DataBinding(object sender, EventArgs e)
        {
            var sContenido = String.Empty;
            var iWidth = 0;
            var container = (HtmlGenericControl)sender;
            var dataItem = ((ListViewDataItem)container.NamingContainer).DataItem;

            var col = new HtmlGenericControl();

            DataRowView Resultado = (DataRowView)dataItem;

            for (int i = 0; i < Resultado.Row.ItemArray.Count(); i++)
            {
                iWidth = Resultado.Row[i].ToString().Length;
                sContenido = "<td>" + Resultado.Row[i].ToString() + "</td>";
                container.Controls.Add(new Literal() { Text = sContenido });
            }
        }
    }
}