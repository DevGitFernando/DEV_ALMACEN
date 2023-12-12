<%@ Page Language="C#" AutoEventWireup="true" Inherits="Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
</head>
<body>
    <form id="Inicio" runat="server">
    <div>
        <% if ((Session["Autenticado"] == null) || (!(bool)Session["Autenticado"]))
           {%>
            <iframe id="ifrmLogin" src="Usuarios_y_Permisos/Login.aspx" scrolling="no"></iframe>
            <%}
           else if ((bool)Session["Autenticado"])
           {%>
           <iframe id="ifrmContenedor" src="Usuarios_y_Permisos/Navegador.aspx" scrolling="no" ></iframe>
            <%} %>
    </div>
    </form>
</body>
</html>
