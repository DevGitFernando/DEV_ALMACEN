<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="UsuariosyPermisos_Default" UICulture="es-MX" Culture="es-MX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "httpss://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="httpss://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" /> 
	<title>Interfaz beta</title>
	<link rel="shortcut icon" href="../images/favicon.ico" /> 
	<link rel="stylesheet" type="text/css" href="../css/jquery.jscrollpane.custom.css" />
	<link rel="stylesheet" type="text/css" href="../css/custom_001.css" />
    <link rel="stylesheet" type="text/css" href="../css/m-styles.min.css" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="../js/modernizr.custom.79639.js"></script>
</head>
<body>
    <form id="Main" runat="server">
    <div class="submenu"></div>
    <div id="MaskM">
        <div id="CajaM">
            <div id="MsjM">
                <br />
                <div class="m-input-prepend">                
                    <span class="add-on">Password anterior :</span>      
                    <input id="txtPasswordAnterior" class="m-wrap" size="16" type="password" placeholder="Password anterior" />
                </div>
                <div class="m-input-prepend">                
                    <span class="add-on">Password :</span>      
                    <input id="txtPassword" class="m-wrap" size="16" type="password" placeholder="Password Nuevo" />
                </div> 
                <div class="m-input-prepend">                
                    <span class="add-on">Confirmar password :</span>      
                    <input id="txtPasswordCon" class="m-wrap" size="16" type="password" placeholder="Password Nuevo" />
                </div>
                <span id="MsjResp" class="add-on"></span>
                <div class="clear"></div>
                <%--<div id="btnModal" class="m-btn green" runat="server"><i class="icon-ok icon-white"></i> Guardar</div>--%>
                <div id="btnModal" runat="server"><i class="icon-ok icon-white"></i> Guardar</div>
                <div class="clear"></div>
            </div>
            <a id="closeM" style="display: inline;" title="Cerrar"></a>
        </div>
    </div>
    <div class="MaskM">
        <div class="CajaM">
            <img id="iLoad" src="../images/iLoad.gif" width="32" height="32" alt="Cargando" runat="server"/>
            <div class="MsjM">Cargando información, espere por favor.</div>
        </div>
    </div>
    <div id="Config" class="MaskM">
        <div class="box">
            <div id="Options">
                <%--<ul id="listOption" class="menu-toc">
                    <li><a href="Opcion1">Opcion 1</a></li>
                    <li><a href="Opcion2">Opcion 2</a></li>
                    <li><a href="Opcion3">Opcion 3</a></li>
                    <li><a href="Opcion4">Opcion 4</a></li>
                </ul>--%>
                <asp:TreeView ID="twNavegador" runat="server" ShowLines="True" 
                    ForeColor="Black">
                    <NodeStyle HorizontalPadding="5px" />
                </asp:TreeView>
            </div>
            <div id="ContenedorOpciones">
                <iframe id="iOptions" width="100%" height="100%" src="" frameborder="0"></iframe>
            </div>
            <a id="closeConfig" class="closeM" style="display: inline;" title="Cerrar"></a>
        </div>
    </div>
    
	<div id="container" class="container">	

		<div id="menupanel" class="menu-panel" runat="server">
            <h3>Menú de opciones</h3>
            <asp:TreeView ID="twOpciones" runat="server" ShowLines="True" ForeColor="Black">
                <NodeStyle HorizontalPadding="5px" />
            </asp:TreeView>
        </div>
        
        <div class="bb-custom-wrapper">
			<div id="bb-bookblock" class="bb-bookblock">
                <div id="Mask">
                    <div id="Caja">
                        <img id="imgUser" src="../images/loader_001.gif" width="96" height="96" alt="Procesando" runat="server"/> 
                        <br /><br /><span>Cargando información,<br />espere un momento por favor.</span>
                    </div>
                </div>
				<iframe id="iContent" width="100%" height="100%" src="" frameborder="0"></iframe>
			</div>
            <nav id="navBarra">
                <span id="tblcontents" class="menu-button" title="Mostrar menú"><i class="icon-list icon-white"></i></span>
                <span id="tblHome" class="menu-button" title="Inicio"><i class="icon-home icon-white"></i></span>
                <span id="Info" class="menu-button"><i id="iconInfo" class="icon-user icon-white"></i></span>
                <div id="Title"><div id="Loading"></div><h3 id="TextTitle" runat="server">Bienvenido</h3></div>
                <div id="menuInfo" class="menu-info" runat="server"></div>
            </nav>
        </div>
        <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
        <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
	</div><!-- /container -->
    </form>
	<%--<script type="text/javascript" src="httpss://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <%--<script type="text/javascript" src="httpss://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="httpss://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>--%>
    <%--Jquery--%>
    <%--<script src="httpss://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/toastr.js"></script>
	<script type="text/javascript" src="../js/jquery.mousewheel.js"></script>
	<script type="text/javascript" src="../js/jquery.jscrollpane.min.js"></script>
    <script type="text/javascript" src="../js/jquery.nicescroll.js"></script>
    <script type="text/javascript" src="../js/reports.js"></script>
    <script type="text/javascript" src="../js/page.js"></script>
    <script type="text/javascript" src="../js/JScript.Init.js"></script>
    <script type="text/javascript" src="../js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/jquery.jeditable.js"></script>
    <script type="text/javascript" src="../js/AjaxPrepare.js"></script>
    <script type="text/javascript">
		$(function () {
            Page.init();
        });
	</script>
</body>
</html>