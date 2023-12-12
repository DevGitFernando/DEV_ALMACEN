using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;

public partial class UsuariosyPermisos_Default : BasePage
{
    DataSet dtsPermisos = new DataSet("Permisos");
    clsLeer myLeer = new clsLeer();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["__EVENTTARGET"] == "Logout" || Request.Form["__EVENTTARGET"] == ",Logout")
        {
            //Metodo_Click(this, new EventArgs());
            Logout();
        }

        if (!IsPostBack)
        {
            //Obtenere datos de Estado y sucursal
            if (DtGeneral.DatosConexion == null)
            {
                DtGeneral.Init();
            }
            Title = DtGeneral.NombreModulo;
            nameUser.InnerHtml = HttpContext.Current.Request.Cookies["Recetario"]["NombreMedico"];
            infoUser.InnerHtml = string.Format("{0}<br/> <strong>Clave:</strong> {1}", HttpContext.Current.Request.Cookies["Recetario"]["NombreMedico"], HttpContext.Current.Request.Cookies["Recetario"]["ClaveMedico"]);

            //myLeer.DataSetClase = DtGeneral.ArbolNavegacion(HttpContext.Current.Request.Cookies["Recetario"]["IdEstado"], HttpContext.Current.Request.Cookies["Recetario"]["IdSucursal"], HttpContext.Current.Request.Cookies["Recetario"]["LoginUser"]);
            myLeer.DataSetClase = DtGeneral.ArbolNavegacion("09", "0011", "BENIGNO.VAZQUEZ");
            if (myLeer.Leer())
            {
                string sNombreRaiz = (string)myLeer.DataSetClase.Tables[0].Rows[0]["Nombre"];

                TreeNode nodo = new TreeNode(sNombreRaiz);

                twNavegador.Nodes.Clear();
                nodo.SelectAction = TreeNodeSelectAction.Expand;
                nodo.ImageUrl = "~/images/nodo1.png";
                twNavegador.Nodes.Add(nodo);

                AgregarRamasHijasTree(nodo, 1, myLeer.DataSetClase.Copy());
            }

        }
    }

    #region Funciones Generales
    private void Logout()
    {
        HttpContext.Current.Session["Autenticado"] = false;
        HttpContext.Current.Session.Abandon();

        if (HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null)
        {
            HttpCookie _SessionId = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"];
            HttpContext.Current.Response.Cookies.Remove("ASP.NET_SessionId");
            _SessionId.Expires = DateTime.Now.AddDays(-1);
            _SessionId.Value = null;
            HttpContext.Current.Response.SetCookie(_SessionId);
        }

        if (HttpContext.Current.Request.Cookies["Recetario"] != null)
        {
            HttpCookie currentUserCookie = HttpContext.Current.Request.Cookies["Recetario"];
            HttpContext.Current.Response.Cookies.Remove("Recetario");
            currentUserCookie.Expires = DateTime.Now.AddDays(-1);
            currentUserCookie.Value = null;
            HttpContext.Current.Response.SetCookie(currentUserCookie);
        }

        HttpContext.Current.Response.Redirect("~/Default.aspx");
    }
    
    private void AgregarRamasHijasTree(TreeNode NodoPadre, int RamaPadre, DataSet Arbol)
    {
        clsLeer Ramas = new clsLeer();
        Ramas.DataRowsClase = Arbol.Tables[0].Select(string.Format(" Padre = {0} ", RamaPadre));

        while (Ramas.Leer())
        {
            TreeNode nuevoNodo = new TreeNode();

            nuevoNodo.Text = Ramas.Campo("Nombre");
            nuevoNodo.Value = Ramas.Campo("Rama");
            if (Ramas.Campo("FormaLoad") == "")
            {
                nuevoNodo.SelectAction = TreeNodeSelectAction.Expand;
                nuevoNodo.ImageUrl = "~/images/nodo2.png";
            }
            else
            {
                //nuevoNodo.NavigateUrl = "~/" + Ramas.Campo("GrupoOpciones") + "/" + Ramas.Campo("FormaLoad") + ".aspx";
                //nuevoNodo.NavigateUrl = "~/" + Ramas.Campo("GrupoOpciones") + "/" + Ramas.Campo("FormaLoad") + ".aspx";
                nuevoNodo.NavigateUrl = string.Format("~/{0}/{1}.aspx|{2}", Ramas.Campo("GrupoOpciones"), Ramas.Campo("FormaLoad"), Ramas.Campo("Rama"));
                nuevoNodo.ImageUrl = "~/images/nodo3.png";
            }


            NodoPadre.ChildNodes.Add(nuevoNodo);
            AgregarRamasHijasTree(nuevoNodo, Ramas.CampoInt("Rama"), Arbol.Copy());

            nuevoNodo.Collapse();
        }
    }
    #endregion Funciones Generales

}