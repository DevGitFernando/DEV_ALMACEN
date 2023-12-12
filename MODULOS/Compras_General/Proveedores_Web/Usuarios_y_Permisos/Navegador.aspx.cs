using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.ExportarDatos;
using SC_SolutionsSystem.FuncionesGrid;
using SC_SolutionsSystem.FuncionesGenerales;
using SC_SolutionsSystem.Reportes;
using Proveedores_Web; 

//public partial class Navegador : System.Web.UI.Page
public partial class Usuarios_y_Permisos_Navegador : BasePage
{
    DataSet dtsPermisos = new DataSet("Arbol");
    string sNombreRaiz = "";

    string sNodo = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtsPermisos = DtGeneral.Permisos;

            if ((dtsPermisos != null) && (dtsPermisos.Tables[0].Rows.Count >0))
            {
                sNombreRaiz = (string)dtsPermisos.Tables[0].Rows[0]["Nombre"];

                TreeNode nodo = new TreeNode(sNombreRaiz);

                twNavegador.Nodes.Clear();
                nodo.SelectAction = TreeNodeSelectAction.Expand;
                nodo.ImageUrl = "~/images/nodo1.png";
                twNavegador.Nodes.Add(nodo);

                AgregarRamasHijas(nodo, 1, dtsPermisos.Copy());

                Session["MsjCerrarDivVentanas"] = DtGeneral.MsjCerrarDivVentanas;
            }
        }
    }
    private void AgregarRamasHijas(TreeNode NodoPadre, int RamaPadre, DataSet Arbol)
    {
        clsLeer Ramas = new clsLeer();
        Ramas.DataRowsClase = Arbol.Tables[0].Select(string.Format(" Padre = {0} ", RamaPadre));

        while (Ramas.Leer())
        {
            TreeNode nuevoNodo = new TreeNode();

            nuevoNodo.Text = Ramas.Campo("Nombre");
            if (Ramas.Campo("FormaLoad") == "")
            {
                nuevoNodo.SelectAction = TreeNodeSelectAction.Expand;
                nuevoNodo.ImageUrl = "~/images/nodo2.png";
            }
            else
            {
                nuevoNodo.NavigateUrl = "~/" + Ramas.Campo("GrupoOpciones") + "/" + Ramas.Campo("FormaLoad") + ".aspx";
                //nuevoNodo.NavigateUrl = "~/" + Ramas.Campo("GrupoOpciones") + "/frmOrdenesColocadas.aspx";
                //nuevoNodo.NavigateUrl = "~/" + Ramas.Campo("GrupoOpciones") + "/frmOrdenesDescargadas.aspx";
                nuevoNodo.ImageUrl = "~/images/nodo3.png";
            }


            NodoPadre.ChildNodes.Add(nuevoNodo);
            AgregarRamasHijas(nuevoNodo, Ramas.CampoInt("Rama"), dtsPermisos.Copy());

            nuevoNodo.Collapse();
        }
    }
}