using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Cliente_Regional.Model;
using DevExpress.Web;

using System.Data;
using Cliente_Regional.Code;

//SC Solutions
using SC_SolutionsSystem;
using SC_SolutionsSystem.Data;
using SC_SolutionsSystem.Errores;
using SC_SolutionsSystem.Reportes;
using SC_SolutionsSystem.FuncionesGenerales;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web;

public partial class Root : MasterPage {
    public bool EnableBackButton { get; set; }

    public bool MenuCargado = false;

    protected void Page_Load(object sender, EventArgs e) {
        if (!string.IsNullOrEmpty(Page.Header.Title))
        {
            //Page.Header.Title = string.Format("{0} | {1}", Page.Header.Title, DtGeneral.NombreModulo);
            //Page.Header.Title += string.Format(" {0}", DtGeneral.NombreModulo);
            Page.Header.Title = string.Format(" {0}", DtGeneral.NombreModulo);
        }

        Page.Header.DataBind();
        //UpdateUserMenuItemsVisible();
        HideUnusedContent();
        //UpdateUserInfo();
    }

    protected void HideUnusedContent() {
        bool bAutenticado = AuthHelper.IsAuthenticated();
        if (bAutenticado)
        {
            LeftAreaMenu.Items[1].Visible = EnableBackButton;

            bool hasLeftPanelContent = HasContent(LeftPanelContent);
            LeftAreaMenu.Items.FindByName("ToggleLeftPanel").Visible = hasLeftPanelContent;
            LeftPanel.Visible = hasLeftPanelContent;

            bool hasRightPanelContent = HasContent(RightPanelContent);
            RightAreaMenu.Items.FindByName("ToggleRightPanel").Visible = hasRightPanelContent;
            RightPanel.Visible = hasRightPanelContent;
            RightAreaMenu.Items.FindByName("SignInItem").Visible = !bAutenticado;
                
            bool hasPageToolbar = HasContent(PageToolbar);
            PageToolbarPanel.Visible = hasPageToolbar;

            UpdateUserInfo();

            if (!MenuCargado)
            {
                MenuCargado = true; 
                ArbolNavegacion();
            }
            //Cargar información del usuario y unidad
            //DtGeneral.GetInfoGeneral();
        } 
        else
        {
            bool bVisible = false;

            LeftAreaMenu.Items[1].Visible = bVisible;
            LeftAreaMenu.Items.FindByName("ToggleLeftPanel").Visible = bVisible;
            LeftPanel.Visible = bVisible;
            RightAreaMenu.Items.FindByName("ToggleRightPanel").Visible = bVisible;
            RightPanel.Visible = bVisible;
            PageToolbarPanel.Visible = bVisible;

            RightAreaMenu.Visible = bVisible;
        }
    }

    protected bool HasContent(Control contentPlaceHolder) {
        if(contentPlaceHolder == null) return false;

        ControlCollection childControls = contentPlaceHolder.Controls;
        if(childControls.Count == 0) return false;

        return true;
    }   

    // SignIn/Register
    protected void UpdateUserMenuItemsVisible() {
        var isAuthenticated = AuthHelper.IsAuthenticated();
        RightAreaMenu.Items.FindByName("SignInItem").Visible = !isAuthenticated;
        RightAreaMenu.Items.FindByName("RegisterItem").Visible = !isAuthenticated;
        RightAreaMenu.Items.FindByName("MyAccountItem").Visible = isAuthenticated;
        RightAreaMenu.Items.FindByName("SignOutItem").Visible = isAuthenticated;
    }

    protected void UpdateUserInfo() {
        if (AuthHelper.IsAuthenticated())
        {
            var user = AuthHelper.GetLoggedInUserInfo();
            var myAccountItem = RightAreaMenu.Items.FindByName("MyAccountItem");
            var userName = (ASPxLabel)myAccountItem.FindControl("UserNameLabel");
            var email = (ASPxLabel)myAccountItem.FindControl("EmailLabel");
            var accountImage = (HtmlGenericControl)RightAreaMenu.Items[0].FindControl("AccountImage");
            //userName.Text = string.Format("{0}", user.NombrePersonal);
            userName.Text = string.Format("{0}", DtGeneral.ObtenerValorCookie("NombrePersonal"));
            //email.Text = user.LoginUser;
            email.Text = DtGeneral.ObtenerValorCookie("LoginUser");
            accountImage.Attributes["class"] = "account-image";

            //if(string.IsNullOrEmpty(user.AvatarUrl)) {
            //    accountImage.InnerHtml = string.Format("{0}{1}", user.FirstName[0], user.LastName[0]).ToUpper();
            //} else {
            //    var avatarUrl = (HtmlImage)myAccountItem.FindControl("AvatarUrl");
            //    avatarUrl.Attributes["src"] = ResolveUrl(user.AvatarUrl);
            //    accountImage.Style["background-image"] = ResolveUrl(user.AvatarUrl);                    
            //}

            //accountImage.InnerHtml = string.Format("{0}", user.NombrePersonal[0]);
            accountImage.InnerHtml = string.Format("{0}", DtGeneral.ObtenerValorCookie("NombrePersonal")[0]);
        }
        else
        {
            bool bVisible = false;

            LeftAreaMenu.Items[1].Visible = bVisible;
            LeftAreaMenu.Items.FindByName("ToggleLeftPanel").Visible = bVisible;
            LeftPanel.Visible = bVisible;
            RightAreaMenu.Items.FindByName("ToggleRightPanel").Visible = bVisible;
            RightPanel.Visible = bVisible;
            PageToolbarPanel.Visible = bVisible;

            RightAreaMenu.Visible = bVisible;
        }
    }

    protected void RightAreaMenu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e) {
        if(e.Item.Name == "SignOutItem") {
            AuthHelper.SignOut(); // DXCOMMENT: Your Signing out logic
            Response.Redirect("~/");
        }
    }

    protected void ApplicationMenu_ItemDataBound(object source, MenuItemEventArgs e) {
        e.Item.Image.Url = string.Format("Content/Images/{0}.svg", e.Item.Text);
        e.Item.Image.UrlSelected = string.Format("Content/Images/{0}-white.svg", e.Item.Text);
    }

    protected void ArbolNavegacion()
    {
        //Permisos
        clsLeer myLeer = new clsLeer();
        ClsConsultas mycl = new ClsConsultas();
        DataSet dtsPermisos = mycl.GetArbolNavegacion(DtGeneral.Arbol);
        myLeer.Reset();
        myLeer.DataSetClase = dtsPermisos;

        if (myLeer.Leer())
        {
            if (dtsPermisos.Tables["Arbol"].Rows.Count > 0)
            {
                string sNombreRaiz = myLeer.Campo("Nombre");
                ASPxTreeView myTreeView = new ASPxTreeView
                {
                    ClientInstanceName = "twOpciones",
                    ID = "twOpciones",
                    EnableNodeTextWrapping = true,
                    AllowSelectNode = true,
                    SyncSelectionMode = SyncSelectionMode.None,
                    CssClass = "noselect"
                };
                myTreeView.Nodes.Clear();

                myTreeView.Width = Unit.Percentage(100);
                myTreeView.Styles.Elbow.CssClass = "tree-view-elbow";
                myTreeView.Styles.Node.CssClass = "tree-view-node";
                myTreeView.Styles.Node.HoverStyle.CssClass = "hovered";

                TreeViewNode twNode = new TreeViewNode();
                twNode.Image.Url = "~/Content/Images/nodo1.png";
                twNode.Text = sNombreRaiz;
                twNode.Expanded = true;

                myTreeView.Nodes.Add(twNode);

                AgregarRamasHijas(twNode, 1, dtsPermisos.Copy());

                LeftPanelContent.Controls.Add(myTreeView);
            }
        }
    }

    protected void AgregarRamasHijas(TreeViewNode NodoPadre, int RamaPadre, DataSet Arbol)
    {
        clsLeer Ramas = new clsLeer
        {
            DataRowsClase = Arbol.Tables[0].Select(string.Format(" Padre = {0} ", RamaPadre))
        };

        while (Ramas.Leer())
        {
            TreeViewNode nuevoNodo = new TreeViewNode
            {
                Text = Ramas.Campo("Nombre")
            };

            if (Ramas.Campo("FormaLoad") == "")
            {
                nuevoNodo.Image.Url = "~/Content/Images/nodo2.png";
                //nuevoNodo.Expanded = true;
            }
            else
            {
                nuevoNodo.NavigateUrl = string.Format("~/{0}/{1}.aspx", Ramas.Campo("GrupoOpciones"), Ramas.Campo("FormaLoad"));
                nuevoNodo.Target = Ramas.Campo("Rama");
                nuevoNodo.ToolTip = Ramas.Campo("Nombre");
                nuevoNodo.Image.Url = "~/Content/Images/nodo3.png";
                nuevoNodo.Target = "_self";
            }

            NodoPadre.Nodes.Add(nuevoNodo);

            AgregarRamasHijas(nuevoNodo, Ramas.CampoInt("Rama"), Arbol);
        }
    }
}