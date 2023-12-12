<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="UsuariosyPermisos_Login" %>

<!doctype html>
<html>
    <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,  initial-scale=1, minimum-scale=1.0, maximum-scale=1.0" />
   
    <link rel="stylesheet" href="../css/normalize.css">
    <link rel="stylesheet" href="../css/component.css">
    <link rel="stylesheet" href="../css/login.css">
    <title>Autenticación de usuarios</title>
   
</head>

<body>
	<div id="loader" class="mask">
        <div class="elementCenter loader">
            <span class="msjLoader">Iniciando sesión, espere por favor.</span>
        </div>
    </div>

	<div id="wrapper">
    	<div class="wrapForm">
            <div class="logo">
        	    <div class="logoimg"></div>
            </div>
            <%--<h4>Inicia sesión</h4>--%>
            <input id="txtUser" type="text" class="controlInput icouser" placeholder="Usuario" maxlength="20"/>
            <input id="txtPassword" type="password" class="controlInput icopassword" name="Contrasena" value="" placeholder="Contraseña" maxlength="20"/>
            <div class="footerUp">
                <div id="btn_sign" class="btn-primary btn">Ingresar</div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.9.1.js" charset="utf-8"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.10.1/jquery-ui.js" charset="utf-8"></script>
    <script type="text/javascript" src="../js/login.js"></script>
    <script type="text/javascript">
        $(function () {
            login.init();
        });
	</script>
</body>
</html>
