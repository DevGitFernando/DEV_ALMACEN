<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="UsuariosyPermisos_Default" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">--%>
<!doctype html>
<html lang="es">
<head runat="server">
	<meta charset="utf-8">
    <link rel="stylesheet" href="../css/componentes.css">
    <link rel="stylesheet" href="../css/home.css">
    <link rel="stylesheet" href="../css/normalize.css">
    <link rel="stylesheet" href="../css/ayuda.css">

    <title>Login</title>
</head>

<body>
  	<form id="Main" runat="server"> 
    <!--Ventana Buscar clave-->
        
    <div class="mask">
    <div id="claveCie" class="contentCie elementCenter">
            <div class="headerCie">Busque una Clave</div>
            <div class="datosCie">
                            
                <div class="labelGeneral">Clave CIE</div>
                <input  class="search" type="text" name="search" placeholder="Escriba la descripción de la clave CIE">
                <div class="btnclick search">Buscar</div>
                <div class="btnclick clean">Limpiar</div>
                                    
            </div>
            <div class="TableClaveCie"></div>
        </div>
    </div>
    <header> <!--Header-->
        <div id="menu_content">
        	<div  id="btnMenu" class="btn_general_header" title="Menu de opciones"><!--Menu-->
            	<i class="iconmenu"></i>
            </div>
            
            <div id="btnHome"class="btn_general_header" title="Regresar a la pantalla de inicio"><!--Home-->
                <i class="iconhome"></i>
            </div>

            
           <!--Iconos aleatorios--> 
            <div id="content_iconos" class="content_iconos_aleatorios">
                <%--
                <div id="btnLab"class="btn_general_header" >
            	    <i class="iconLab">title="Agregar un nuevo Laboratorio"
                	    <span class="Notificacion">90</span>
                    </i>
                </div>
                <div class="btn_general_header" title="Crear nuevo">
                <i class="iconnuevo"></i>
                </div>
                        
                 <div class="btn_general_header" title="Guardar Receta Médica">
                    <i class="iconguardar"></i>
                </div>
            
                <div class="btn_general_header" title="Editar Receta Médica"><!--Editar-->
                    <i class="iconeditar"></i>
                </div>
            
                <div class="btn_general_header" title="Consultar información"><!--Editar-->
                    <i class="iconconsultar"></i>
                </div>--%>
            </div>   
            
            <div id="btnMenuUser" class="btn_close_all" title="Cerrar sesión"></div>
            <div id="nombre_usuario_cont">
        	    <h3 id="nameUser" class="nombre_usuario" runat="server">Carlos Alfredo Hernández Marcelino</h3>
            </div>
            <div id="menuUser" class="menu_usuario">
        	    <div class="img_usuario"></div>
        	    <div id="infoUser" class="txt_close" runat="server">Carlos Alfredo Hernández Marcelino <br> <strong>Usuario:</strong> CarlosH</div>
                <div id="btnLogout" class="btn_close">Cerrar sesion</div>
            </div>
		</div>
    </header>
    <!--HeaderEND-->
	
    <!--Iframe-->
	<div class="icontent">
    	<iframe id="iContent" frameborder="0" src=""></iframe>
    </div>
    
    <div id="menupanel" class="menupanel">
    	<div class="header_menu_opciones"></div>
        <div class="contenido_menu_opciones">
        	<%--<div class="btnMenu_aside">Recetario</div>--%>
            <asp:TreeView ID="twNavegador" runat="server" ShowLines="True" 
                ForeColor="Black">
                <NodeStyle HorizontalPadding="5px" />
            </asp:TreeView>
        </div>
    </div>
     
     <footer></footer>
    
        <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
        <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
	</form>
    <%--<script type="text/javascript" src="../js/jquery-3.0.0.min.js"></script>--%>
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.9.1.js" charset="utf-8"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.10.1/jquery-ui.js" charset="utf-8"></script>
    <script type="text/javascript" src="../assets/DataTables/datatables.min.js"></script>
	<script type="text/javascript" src="../js/jquery.jeditable.js"></script>
    
    <script type="text/javascript" src="../js/grid.js"></script>
    <script type="text/javascript" src="../js/webservices.js"></script>
    <script type="text/javascript" src="../js/forms.js"></script>
    <script type="text/javascript" src="../js/page.js"></script>
    <script type="text/javascript">
        $(function () {
            page.init();
        });
	</script>
    </body>
</html>