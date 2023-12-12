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
    clsLeer myLeer = new clsLeer();
    string sMenuUl = string.Empty;
    DataSet dtsPermisos = new DataSet("Permisos");
    bool bCerrarUl = false;
    string sRamaPadreActual = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Header.Title = DtGeneral.NombreModulo;
        if (Request.Form["__EVENTTARGET"] == "Logout" || Request.Form["__EVENTTARGET"] == ",Logout")
        {
            //Metodo_Click(this, new EventArgs());
            Logout();
        }
        if (!IsPostBack)
        {
            bool bAdminPermisos = false;
            dtsPermisos = (DataSet)HttpContext.Current.Session["Permisos"];
            Header.InnerHtml = "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\" />" +
                                "<meta charset=\"UTF-8\">" +
                                "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />" +
                                "<link rel=\"shortcut icon\" href=\"../images/favicon.ico\" />" +
                                "<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/jquery.jscrollpane.custom.css\" />" +
                                "<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/custom_" + DtGeneral.Empresa + ".css\" />" +
                                "<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/m-styles.min.css\" />" +
                                "<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/components.css\" />" +
                                //"<link rel=\"stylesheet\" href=\"http://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css\" />" +
                                "<link rel=\"stylesheet\" href=\"https://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css\" />" +
                                //"<script type=\"text/javascript\" src=\"http://code.jquery.com/jquery-1.9.1.js\"></script>" +
                                //"<script type=\"text/javascript\" src=\"http://code.jquery.com/ui/1.10.1/jquery-ui.js\"></script>" +
                                //"<script type=\"text/javascript\" src=\"../js/jquery.ui.datepicker-es.min.js\"></script>" +
                                //"<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/" + (DtGeneral.Empresa == "001" ? "south-street/jquery-ui-1.10.1.custom" : "redmond/jquery-ui-1.10.3.custom") + ".css\" />" +

                                "<script type=\"text/javascript\" src=\"../js/modernizr.custom.79639.js\"></script>" +
                                "<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/toastr.css\"/>" +
                                string.Format("<script type=\"text/javascript\">var initUser={0}; var idEstado='{1}';</script>", HttpContext.Current.Session["IdSucursal"].ToString() == "0001" ? "true" : "false", DtGeneral.EstadoConectado);
                
            Header.Title = DtGeneral.NombreModulo;

            imgUser.Src = "../images/loader_" + DtGeneral.Empresa + ".gif";

            string sLogo = string.Empty;
            int iwidth = 96;
            int iheight = 96;
            switch (DtGeneral.Empresa)
            {
                case "001":
                    sLogo = "intermed";
                    break;
                case "002":
                    sLogo = "phoenix";
                    break;
                case "003":
                    sLogo = "nadro";
                    iwidth = 200;
                    iheight = 73;
                    break;
                default:
                    break;
            }

            //sMenuUl = "<h3>Menú de opciones</h3>" +
            //            "<ul id=\"menu-toc\" class=\"menu-toc\">";

            //if (dtsPermisos.Tables.Count > 0)
            //{
                
            //    myLeer.DataSetClase = dtsPermisos;
            //    myLeer.RegistroActual = 2;
            //    AgregarRamasHijas(sMenuUl, 1, myLeer.DataSetClase);
                
            //    //Base opcion menu para pruebas
            //    //sMenuUl += string.Format("<li><a href=\"../{0}/{1}.aspx\" title=\"{2}\" rel=\"{3}\"><i class=\"icon-list-alt icon-white\"></i>{2}</a></li>", "Puebla", "frmExistenciaPorcClave", "Existencia por clave", "test");
            //    //sMenuUl += string.Format("<li><a href=\"../{0}/{1}.aspx\" title=\"{2}\" rel=\"{3}\"><i class=\"icon-list-alt icon-white\"></i>{2}</a></li>", "Unidosis", "frm_BI_UNI_RPT__010__Antibioticos", "Test", "test");
            //    //sMenuUl += string.Format("<li><a href=\"../{0}/{1}.aspx\" title=\"{2}\" rel=\"{3}\"><i class=\"icon-list-alt icon-white\"></i>{2}</a></li>", "BI", "frm_BI_RPT__018_Graficas", "Usuarios", "2");
            //    //sMenuUl += string.Format("<li><a href=\"../{0}/{1}.aspx\" title=\"{2}\" rel=\"{3}\"><i class=\"icon-list-alt icon-white\"></i>{2}</a></li>", "UsuariosyPermisos", "frmGruposDeUsuario", "Grupos de usuarios", "40");
                
            //    sMenuUl += "</ul>";

            //}
            
            ////Agregar menu creado al panel correpondiente
            //menupanel.InnerHtml = sMenuUl;


            if (dtsPermisos != null)
            {
                if (dtsPermisos.Tables["Arbol"].Rows.Count > 0)
                {
                    bAdminPermisos = true;
                    if (HttpContext.Current.Session["IdSucursal"].ToString() != DtGeneral.Sucursal)
                    {
                        bAdminPermisos = false;
                    }
                    string sNombreRaiz = (string)dtsPermisos.Tables[0].Rows[0]["Nombre"];

                    TreeNode nodo = new TreeNode(sNombreRaiz);

                    twOpciones.Nodes.Clear();
                    nodo.SelectAction = TreeNodeSelectAction.Expand;
                    nodo.ImageUrl = "~/images/nodo1.png";
                    twOpciones.Nodes.Add(nodo);

                    AgregarRamasHijasTree(nodo, 1, dtsPermisos.Copy());
                }
            }


            //Arból permisos de configuración
            if (HttpContext.Current.Session["PermisosConfiguracion"] == null)
            //if (HttpContext.Current.Session["PermisosConfiguracion"] != null)
            {
                HttpContext.Current.Session["PermisosConfiguracion"] = DtGeneral.GetArbolNavegacion(DtGeneral.ArbolConfiguracion);
            }

            DataSet dtsPermisosConfig = (DataSet)HttpContext.Current.Session["PermisosConfiguracion"];
            if (dtsPermisosConfig != null)
            {
                if (dtsPermisosConfig.Tables["Arbol"].Rows.Count > 0)
                {
                    bAdminPermisos = true;
                    if (HttpContext.Current.Session["IdSucursal"].ToString() != DtGeneral.Sucursal)
                    {
                        bAdminPermisos = false;
                    }

                    string sNombreRaiz = (string)dtsPermisosConfig.Tables[0].Rows[0]["Nombre"];

                    TreeNode nodo = new TreeNode(sNombreRaiz);

                    twNavegador.Nodes.Clear();
                    nodo.SelectAction = TreeNodeSelectAction.Expand;
                    nodo.ImageUrl = "~/images/nodo1.png";
                    twNavegador.Nodes.Add(nodo);

                    AgregarRamasHijasTree(nodo, 1, dtsPermisosConfig.Copy());
                }
            }


            //menuInfo.InnerHtml = string.Format( "<div><img id=\"imgLogo\" src=\"../images/{0}.png\" width=\"96\" height=\"96\" alt=\"Procesando\" align=\"middle\"/><br /></div>" +
            menuInfo.InnerHtml = string.Format("<div><img id=\"imgLogo\" src=\"../images/{0}.png\" width=\"{3}\" height=\"{4}\" alt=\"Procesando\" align=\"middle\"/><br /></div>" +
                                                "<div>" +
                                                "<strong>BIENVENIDO</strong><br />{2}<br />" +
                                                "</div>" +
                                                //"<a id=\"btnConfig\" class=\"m-btn red icn-only\"><i class=\"icon-tags icon-white\"></i> Configuración</a>" +
                                                (bAdminPermisos ? "<a id=\"btnConfig\" class=\"m-btn red icn-only\"><i class=\"icon-tags icon-white\"></i> Configuración</a>":"") +
                                                "<a id=\"ChangePass\" class=\"m-btn red icn-only\"><i class=\"icon-edit icon-white\"></i> Cambiar password</a>" +
                                                "<a id=\"Logout\" class=\"m-btn red icn-only\"><i class=\"icon-remove icon-white\"></i> Cerrar sesión</a>",
                //DtGeneral.Empresa == "001" ? "intermed" : "phoenix", DtGeneral.NombreModulo, HttpContext.Current.Session["Personal"]);
                //DtGeneral.Empresa == "001" ? "intermed" : "phoenix", DtGeneral.NombreModulo, HttpContext.Current.Session["NombrePersonal"]);
                                                sLogo, DtGeneral.NombreModulo, HttpContext.Current.Session["NombrePersonal"], iwidth, iheight);

            if (DtGeneral.Empresa == "001" || DtGeneral.Empresa == "003")
            {
                btnModal.Attributes.Add("class", "m-btn green");
            }
            else 
            {
                btnModal.Attributes.Add("class", "m-btn blue");
            }
        }
    }

    #region Funciones Generales
    private void Logout()
    {
        Session.Abandon();
        //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        Session["Autenticado"] = false;
        //DtGeneral.Autenticado = false;
        Response.Redirect("~/Default.aspx");
    }

    private void AgregarRamasHijas(string NodoPadre, int RamaPadre, DataSet Arbol)
    {
        clsLeer Ramas = new clsLeer();
        Ramas.DataRowsClase = Arbol.Tables[0].Select(string.Format(" Padre = {0} ", RamaPadre));
        while (Ramas.Leer())
        {
            if (Ramas.Campo("FormaLoad") == "")
            {
                sMenuUl += string.Format("<li><a href=\"{0}\" title=\"{1}\"><i class=\"icon-list-alt icon-white\"></i>{1}</a><ul class=\"menu-toc\">", "Sub-Menu", Ramas.Campo("Nombre"));
                bCerrarUl = true;
            }
            else
            {
                //if (Ramas.Campo("FormaLoad") == "" && bCerrarUl)
                if (bCerrarUl)
                {
                    sMenuUl += string.Format("</ul></li>");
                    bCerrarUl = false;
                }
                sMenuUl += string.Format("<li><a href=\"../{0}/{1}.aspx\" title=\"{2}\" rel=\"{3}\"><i class=\"icon-list-alt icon-white\"></i>{2}</a></li>", Ramas.Campo("GrupoOpciones"), Ramas.Campo("FormaLoad"), Ramas.Campo("Nombre"), Ramas.Campo("Rama"));
            }

            AgregarRamasHijas(NodoPadre, Ramas.CampoInt("Rama"), Arbol);
        }
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
            AgregarRamasHijasTree(nuevoNodo, Ramas.CampoInt("Rama"), dtsPermisos.Copy());

            nuevoNodo.Collapse();
        }
    }
    #endregion Funciones Generales
}