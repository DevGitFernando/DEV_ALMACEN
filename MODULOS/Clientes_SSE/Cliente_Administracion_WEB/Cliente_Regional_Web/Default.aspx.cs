using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//System extra
using System.Data;

//SC Solutions
using SC_SolutionsSystem.Data;

public partial class _Default : BasePage
{
    DataSet dtsPermisos = new DataSet("dtsPermisos");

    protected void Page_Load(object sender, EventArgs e)
    {
        //Desencriptamos Cookie
        ClsDictionary myDictionary = new ClsDictionary();
        myDictionary.Descifrar(HttpContext.Current.Request.Cookies[DtGeneral.NombreCookie]["value"].ToString());

        if (Request.Form["__EVENTTARGET"] == "Logout" || Request.Form["__EVENTTARGET"] == ",Logout")
        {
            //Metodo_Click(this, new EventArgs());
            Logout();
        }

        if (!IsPostBack)
        {
            Header.Title = DtGeneral.NombreModulo;//"Administrador Regional";

            //nameUser.InnerHtml = HttpContext.Current.Request.Cookies["cteRegional"]["NombrePersonal"].ToString();
            nameUser.InnerHtml = myDictionary.Search("NombrePersonal");
            //nameUserPanel.InnerHtml = HttpContext.Current.Request.Cookies["cteRegional"]["LoginUser"].ToString();
            nameUserPanel.InnerHtml = myDictionary.Search("LoginUser");

            //Permisos
            clsConsultas Consultas = new clsConsultas();
            clsLeer myLeer = new clsLeer();
            bool bAdminPermisos = false;

            //dtsPermisos = Consultas.Permisos();
            dtsPermisos = Consultas.GetArbolNavegacion(DtGeneral.Arbol);
            myLeer.DataSetClase = dtsPermisos;

            if(myLeer.Leer())
            {
                string sNombreRaiz = (string)dtsPermisos.Tables[0].Rows[0]["Nombre"];

                //if (HttpContext.Current.Request.Cookies["cteRegional"]["IdSucursal"] == DtGeneral.IdSucursal)
                if (myDictionary.Search("IdSucursal") == DtGeneral.IdSucursal)
                {
                    bAdminPermisos = true;
                }
                
                TreeNode nodo = new TreeNode(sNombreRaiz);

                twNavegador.Nodes.Clear();
                nodo.SelectAction = TreeNodeSelectAction.Expand;
                nodo.ImageUrl = "~/images/nodo1.png";
                twNavegador.Nodes.Add(nodo);

                AgregarRamasHijas(nodo, 1, dtsPermisos.Copy());
            }

            DataSet dtsPermisosConfig = Consultas.GetArbolNavegacion(DtGeneral.ArbolConfiguracion);
            myLeer.Reset();
            myLeer.DataSetClase = dtsPermisosConfig;

            if (myLeer.Leer())
            {
                if (dtsPermisosConfig.Tables["Arbol"].Rows.Count > 0)
                {

                    string sNombreRaiz = (string)dtsPermisosConfig.Tables[0].Rows[0]["Nombre"];

                    TreeNode nodo = new TreeNode(sNombreRaiz);

                    twOpciones.Nodes.Clear();
                    nodo.SelectAction = TreeNodeSelectAction.Expand;
                    nodo.ImageUrl = "~/images/nodo1.png";
                    twOpciones.Nodes.Add(nodo);

                    AgregarRamasHijas(nodo, 1, dtsPermisosConfig.Copy());
                }
            }
            else
            {
                bAdminPermisos = false;
            }

            string sClass = "optionChild";
            if (!bAdminPermisos)
            {
                sClass += " optionChildMedium";
                btnConfig.Visible = false;
            }

            logout.Attributes.Add("class", string.Format("{0} icoCloseSesion", sClass));
            ChangePass.Attributes.Add("class", string.Format("{0} icoChangePass", sClass));
            btnConfig.Attributes.Add("class", string.Format("{0}D icoConfigUser", sClass));
        }
    }

    #region Funciones Generales
    private void Logout()
    {

        if (HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null)
        {
            HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddYears(-1);
        }

        if (HttpContext.Current.Request.Cookies[DtGeneral.NombreCookie] != null)
        {
            HttpContext.Current.Response.Cookies[DtGeneral.NombreCookie].Expires = DateTime.Now.AddYears(-1);
        }

        HttpContext.Current.Session["Autenticado"] = false;
        HttpContext.Current.Session.Abandon();

        HttpContext.Current.Response.Redirect("~/UsuariosyPermisos/EdoLogin.aspx");
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
                nuevoNodo.NavigateUrl = string.Format("~/{0}/{1}.aspx", Ramas.Campo("GrupoOpciones"), Ramas.Campo("FormaLoad"));
                nuevoNodo.Target = Ramas.Campo("Rama");
                nuevoNodo.ToolTip = Ramas.Campo("Nombre");
                nuevoNodo.ImageUrl = "~/images/nodo3.png";
            }


            NodoPadre.ChildNodes.Add(nuevoNodo);
            AgregarRamasHijas(nuevoNodo, Ramas.CampoInt("Rama"), dtsPermisos.Copy());

            nuevoNodo.Collapse();
        }
    }
    #endregion Funciones Generales
}