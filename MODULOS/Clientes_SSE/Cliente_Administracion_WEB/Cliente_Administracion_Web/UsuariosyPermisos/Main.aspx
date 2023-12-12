<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="UsuariosyPermisos_Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HeadNavegador" runat="server">
    <title>Administrador Regional</title>
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.0/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.0/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/main.js"></script>
</head>
<body>
    <form id="Navegador" runat="server">
        <div id="top">
            <h1>Módulo web de consulta</h1>
            <span id="Logout" title="Cerrar sesión"></span>
        </div>
        
        <div id="lateral">
            <h2>Opciones</h2>
            <asp:TreeView ID="twNavegador" runat="server" ShowLines="True" 
            ForeColor="Black" BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                Font-Size="Small">
                <NodeStyle HorizontalPadding="8px" />
            </asp:TreeView>
            <div class="clear"></div>
        </div>
        
        <div id="content">
            <h2>Inicio</h2>
            <a id="toggler"></a>
            <div id="render">
                <i id="tip">Selecciona una opción a cargar en el listado del panel lateral izquierdo.</i>
                <div id="Mask">
                    <div id="Caja">
                        <img id="imgUser" src="../images/loader_001.gif" width="64" height="64" alt="Procesando" align="middle" runat="server"/> 
                        <br /><br /><span>Cargando información,<br />espere un momento por favor.</span>
                    </div>
                </div>
                <iframe id="iframe" width="100%" height="100%" src="" frameborder="0"></iframe>
            </div>
        </div>
        
        <div id="footer">
            <span>Interfaz beta módulo secretaría</span>
        </div>
        
        <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
        <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>