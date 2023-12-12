<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdoLogin.aspx.cs" Inherits="UsuariosyPermisos_EdoLogin" %>
<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <title>Login</title>
    <link rel="stylesheet" type="text/css" href="../css/normalize.css"/>
	<link rel="stylesheet" type="text/css" href="../css/components.css"/>
    <link rel="stylesheet" type="text/css" href="../css/login.css"/>
    <link rel="stylesheet" type="text/css" href="../css/001/login.css"/>
</head>

<body>
    <div id="loader" class="mask">
        <div class="elementCenter loader shadowAll">
            <span class="msjLoader">Iniciando sesión, espere por favor.</span>
        </div>
    </div>
	<div class="wrappFrm elementCenter">
    	<div class="head">
        	<div class="logo"></div>
        </div>
        <input id="txtUser" type="text" class="User" placeholder="Usuario" name="txtUser"/>
        <input id="txtPass" type="password" class="Password mtop" placeholder="Contraseña" name="txtPass"/>
        <div id="message" class="MsjLogin">Datos incorrectos</div>
        <div class="footerUp">
        	<div id="btnSign" class="btnIngresar mtop">Ingresar →</div>
        </div>
    </div>
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../js/login.js"></script>
    <script type="text/javascript">
        if (top.location !== this.location) top.location = '../Default.aspx';
        $(function () {
            login.init();
        });
    </script>
</body>
</html>
