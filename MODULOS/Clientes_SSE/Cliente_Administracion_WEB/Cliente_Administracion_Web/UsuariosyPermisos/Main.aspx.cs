using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Web.Services;
using System.Web.Script.Services;

using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;

//public partial class UsuariosyPermisos_Main : System.Web.UI.Page
public partial class UsuariosyPermisos_Main : BasePage
{
    DataSet dtsPermisos = new DataSet("Arbol");
    string sNombreRaiz = "";

    string sNodo = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["__EVENTTARGET"] == "Logout" || Request.Form["__EVENTTARGET"] == ",Logout")
        {
            //Metodo_Click(this, new EventArgs());
            Logout();
        }
        
        if (!IsPostBack)
        {
            HeadNavegador.InnerHtml =   "<link rel=\"shortcut icon\" href=\"../images/favicon.ico\" /> " +
                                        "<link rel=\"stylesheet\" href=\"../css/main_" + DtGeneral.Empresa +".css\" type=\"text/css\" />" +
                                        "<link rel=\"stylesheet\" href=\"../css/main_general.css\" type=\"text/css\" />" +
                                        "<link rel=\"stylesheet\" href=\"http://code.jquery.com/ui/1.10.0/themes/base/jquery-ui.css\" />" +
                                        "<script type=\"text/javascript\" src=\"http://code.jquery.com/jquery-1.8.3.js\"></script>" +
                                        "<script type=\"text/javascript\" src=\"http://code.jquery.com/ui/1.10.0/jquery-ui.js\"></script>" +
                                        "<script type=\"text/javascript\" src=\"../js/main.js\"></script>" +
                                        "<script type=\"text/javascript\" src=\"../js/jquery.nicescroll.js\"></script>";

            HeadNavegador.Title = DtGeneral.NombreModulo;//"Administrador Regional";

            dtsPermisos = DtGeneral.Permisos;
            DtGeneral.GetJurisdicciones(DtGeneral.EstadoConectado);
            
            //if (dtsPermisos != null)
            if(dtsPermisos.Tables.Count >0)
            {
                sNombreRaiz = (string)dtsPermisos.Tables[0].Rows[0]["Nombre"];

                TreeNode nodo = new TreeNode(sNombreRaiz);

                twNavegador.Nodes.Clear();
                nodo.SelectAction = TreeNodeSelectAction.Expand;
                nodo.ImageUrl = "~/images/nodo1.png";
                twNavegador.Nodes.Add(nodo);

                AgregarRamasHijas(nodo, 1, dtsPermisos.Copy());
            }
            else
            {
                //Response.Write("<script>top.location.href=\"../Default.aspx\"</script>");
                Response.Redirect("~/Default.aspx");
            }
        }
        //else
        //{
        //    //Checar valores del post del formulario
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("Form Elements:<br /><br />");
        //    foreach (string key in HttpContext.Current.Request.Form.AllKeys)
        //    {
        //        sb.AppendFormat("{0}={1}<br />", key, HttpContext.Current.Request.Form[key]);
        //    }
        //}
    }

    #region Funciones Generales
    private void Logout()
    {
        Session.Abandon();
        Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        Session["Autenticado"] = false;
        DtGeneral.Autenticado = false;

        if (General.ArchivoIni == "SII-Regional_Web_Dev.ini")
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            Response.Redirect("http://inter-med.com.mx/");
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
                //nuevoNodo.ImageUrl = "~/images/ui-bg_diagonals-thick_18_b81900_40x40.png";
            }
            else
            {
                nuevoNodo.NavigateUrl = "~/" + Ramas.Campo("GrupoOpciones") + "/" + Ramas.Campo("FormaLoad") + ".aspx";
                nuevoNodo.ImageUrl = "~/images/nodo3.png";
            }


            NodoPadre.ChildNodes.Add(nuevoNodo);
            AgregarRamasHijas(nuevoNodo, Ramas.CampoInt("Rama"), dtsPermisos.Copy());

            nuevoNodo.Collapse();
        }
    }
    #endregion Funciones Generales
}