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

public partial class Usuarios_y_Permisos_Login : System.Web.UI.Page
{
    string sError = string.Empty;
    DataSet dtsPermisos = new DataSet("Permisos");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Autenticado"] == null)
            {
                Session["Autenticado"] = false;
            }

            if (Session["Error"] == null)
            {
                Session["Error"] = "00";
            }

            if ((bool)Session["Autenticado"])
            {
                Response.Redirect("~/UsuariosyPermisos/Navegador.aspx");
            }

            
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        clsLogin.Empresa = DtGeneral.Empresa;
        clsLogin.Estado = ""; //DtGeneral.EstadoConectado;
        clsLogin.Sucursal = DtGeneral.Sucursal;
        clsLogin.Usuario = txtUsuario.Text;
        clsLogin.Password = txtPassword.Text;
        clsLogin.Arbol = DtGeneral.Arbol;

        if (clsLogin.AutenticarUsuarioLogin())
        {
            Session["Autenticado"] = true;
            Session["User"] =string.Format("Bienvenido {0} , hora de conexión: {1}", DtGeneral.GetProvedor(clsLogin.Personal), DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
            Response.Redirect("~/Usuarios_y_Permisos/Navegador.aspx");
        }
        else
        {
            sError = "Verifique sus datos";
        }
        if (sError != "")
        {
            Session["Error"] = clsLogin.ErrorAutenticacion;
        }
    }
}