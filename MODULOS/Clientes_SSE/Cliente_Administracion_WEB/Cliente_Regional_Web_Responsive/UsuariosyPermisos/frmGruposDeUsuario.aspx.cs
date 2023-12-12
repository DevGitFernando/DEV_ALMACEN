using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UsuariosyPermisos_frmGruposDeUsuario : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Groups.InnerHtml = "<span class=\"labelGpoleft\">Perfil</span>" +
                                DtGeneral.GruposUsuariosPerfiles("%A%--%");
            Users.InnerHtml = "<span class=\"labelGpoleft\">Usuarios</span>" +
                            "<ul id=\"ulUsuarios\" >" +
                                DtGeneral.GetUsers() +
                            "</ul>";
        }
    }
}