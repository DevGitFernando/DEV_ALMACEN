<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdoLogin.aspx.cs" Inherits="UsuariosyPermisos_EdoLogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autenticación de usuarios</title>
</head>
<body>
    
    <div id="Mensaje" class="Alert"></div>
    <!--WRAPPER-->
    <div id="wrapper">

	    <!--SLIDE-IN ICONS-->
        <div class="user-icon"></div>
        <div class="pass-icon"></div>
        <!--END SLIDE-IN ICONS-->
    
    <!--LOGIN FORM-->
    <form id="EdoLogin" runat="server" class="login-form">    
        <!--HEADER-->
        <div class="header">
        <!--LOGO--><div class="Logo"></div><!--END LOGO-->
        <!--TITLE--><h1 id="NombreEmpresa" runat="server"></h1><!--END TITLE-->
	    <!--DESCRIPTION--><!--<span>INFO</span>--><!--END DESCRIPTION-->
        </div>
        <!--END HEADER-->
	
	    <!--CONTENT-->
        <div class="content">
        <!--USERNAME--><input id="txtUsuario" name="username" type="text" class="input username" value="" placeholder="Usuario" /><!--END USERNAME-->
        <!--PASSWORD--><input id="txtPassword" name="password" type="password" class="input password" value="" placeholder="Password" /><!--END PASSWORD-->
        </div>
        <!--END CONTENT-->
    
        <!--FOOTER-->
        <div class="footer">
        <!--LOGIN BUTTON--><a id="btnLogin" class="button">Login</a><!--END LOGIN BUTTON-->
        </div>
        <!--END FOOTER-->
    
    </form>
    <!--END LOGIN FORM-->

    </div>
    <!--END WRAPPER-->

</body>
</html>