<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUsuario.aspx.cs" Inherits="UsuariosyPermisos_frmUsuario" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">--%>
<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <title>Dar de alta usuarios</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" /> 
	<link rel="stylesheet" type="text/css" href="../css/m-styles.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/GeneralForm.css" />
    <link rel="stylesheet" type="text/css" href="../css/default.css" />
    <%  if (DtGeneral.IdEmpresa == "001")
        {%>
    
            <link rel="stylesheet" href="../css/south-street/jquery-ui-1.10.1.custom.css" />
    <%
        }
        else
        {
    %>
            <link rel="stylesheet" href="../css/redmond/jquery-ui-1.10.3.custom.css" />
    <%
        }
    %>
    <% { Response.Write("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/GeneralForm_" + DtGeneral.IdEmpresa + ".css\" />"); } %>
    <% { Response.Write("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/" + DtGeneral.IdEmpresa + ".css\" />"); } %>
    <link rel="stylesheet" type="text/css" href="../css/components.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.datepicker-es.min.js"></script>
    <script type="text/javascript" src="../js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/Scroller.js"></script>
    <style type="text/css">
        
        body
        {
            background-image:none;
            background-color:#e8e6fd;
        }
                
        .Controls {
            width: 894px;
        }
        .container {
            width: 400px;
            height: auto;
        }
        #Info 
        {
            width: 400px;
            margin-bottom: 0px;
        }
        #InfoUser
        {
            position: relative;
            width: 382px;
            padding-top: 16px;
        }
        #InfoUser .m-input-prepend .add-on {
            width: 150px;
            background: #00632d;
            height: auto;
            text-align: right;
            text-shadow: none;
            margin: -1px;
        }
        #InfoUser input {
            height: 30px;
            margin-top: -1px;
            width: 222px;
        }
        input[type="text"].m-wrap,
        input[type="password"].m-wrap
        {
            padding: 0px;
            margin: 0px;
        }
        .first
        {
            margin-top: 8px;
        }
        #navBarraFrame
        {
            padding-left: 16px;
            width: 382px;
            position: relative;
        }
        #txtIdUsuario
        {
            width: 56px !important;
            text-align: center;
        }
        #Exec {
            left: 56px;
        }
        #Print
        {
            left: 96px;
        }
        
        /*Help*/
        body {
            overflow: hidden; 
        }
        #Mask, .Mask {
            position: absolute;
            width: 100%;
            height: 100%;
            overflow: hidden;
            background: url('../images/fancybox_overlay.png');
            margin: 0px;
            padding: 0px;
            top: 47px;
            left: 0px;
            z-index: 999999999999;
            text-align: center;
            display: none;
        }
        #Caja, .caja {
            position: relative;
            margin: 0 auto;
            margin-top: 48px;
            border: 4px solid #41a62a;
            border-radius: 5px;
            background: #fff;
            width: 400px;
            height: 300px;
        }
        #close, .close {
            position: absolute;
            top: -15px;
            right: -15px;
            width: 37px;
            height: 34px;
            background: transparent url('../images/fancybox_sprite.png') -40px 0px;
            cursor: pointer;
            z-index: 1103;
        }
        #Reporte, #MsjRpt, .helpcontent {
            width: 100%;
            height: 100%;
            overflow: auto;
        }
        #nvahelp {
            background: #00632D;
            top: 0px;
            left: 0px;
            position: absolute;
            width: 100%;
            height: 47px;
            z-index: 1102;
        }
        
        #nvahelp #Exportar{
            left: 8px;
        }
        
        .caja #ResumenHelp {
            position: absolute;
            bottom: 8px;
            right: 8px;
            text-align: right;
            height: 12px;
        }
        
        .helpcontent span {
            position: absolute;
            font-size: 2em;
            text-align: center;
            margin-top: 33%;
            width: 100%;
            left: 0px;
        }
        #MsjRpt {
            position: absolute;
            margin: 0px;
            padding: 0px;
        }  
        
        #btnAdd {
            position: absolute;
            width: 125px;
            height: 12px;
            bottom: 4px;
            right: 4px;
        }
        
        .dataTables_filter {
            float: left;
            text-align: left;
            margin-left: 8px;
        }
        /*Help*/
        
        #statusUser
        {
            float: right;
            width: 100px;
            height: 32px;
            color: #fff;
            line-height: 32px;
            margin-top: -1px;
            font-size: 14px;
            font-weight: bolder;
            text-align: center;
            margin-left: 66px;
        }
        .cancelado
        {
            background-color: #bd362f;
        }
        .activo
        {
            background-color: #51a351;
        }
        .loadHelp
        {
            position: relative;
            width: 100%;
            height: 100%;
            background-image: url(../images/user_003.gif);
            background-position: center center;
            background-size: 64px 64px;
            background-repeat: no-repeat
        }
        #cboFarmacia
        {
            width: 224px !important;
            margin-top: -1px;
            height: 32px;
        }
    </style>
    <script type="text/javascript">
        if (top.location == this.location) top.location = '../Default.aspx';
    </script>
</head>
<body>
    <div id="Mask">
        <div id="Caja">
            <div id="MsjRpt" runat="server">
                <div id="loadHelp" class="loadHelp"></div>
            </div>
            <a id="close" style="display: inline;" title="Cerrar"></a>
            <div id="btnAdd" runat="server"><i class="icon-plus icon-white"></i>Agregar</div>
        </div>
    </div>
    <form id="frmUsuario" runat="server">
    
    <div id="container" class="container elementCenter">
        <div id="navBarraFrame">
             <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
             <span id="Exec" class="menu-button" title="Guardar"><i class="icon-edit icon-white"></i></span>
             <span id="Print" class="menu-button" title="Cancelar"><i class="icon-remove icon-white"></i></span>
             <%--<span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>--%>
        </div>
        <div id="Info"class="Controls cont">
            <div id="InfoUser" class="Groups">
                <span class="labelGpoleft">Información de usuario</span>
                <div class="m-input-prepend first">
                    <span class="add-on">Id. Usuario :</span>
                    <input class="m-wrap" type="text" id="txtIdUsuario" value="" placeholder="ID" maxlength="4">
                    <span id="statusUser"></span>
                </div>
                <div id="contFarmacia" class="m-input-prepend" runat="server"></div>
                <div class="m-input-prepend">
                    <span class="add-on">Nombre :</span>
                    <input type="text" class="m-wrap" id="txtNombre" value="" placeholder="Nombre del usuario">
                </div>
                <div class="m-input-prepend">
                    <span class="add-on">Login :</span>
                    <input class="m-wrap" type="text" id="txtLogin" value="" placeholder="Usuario" maxlength="20">
                </div>
                <div class="m-input-prepend">
                    <span class="add-on">Password :</span>
                    <input class="m-wrap" type="password" id="txtPassword" value="" placeholder="Contraseña">
                </div>
                <div class="m-input-prepend">
                    <span class="add-on">Confirmar Password :</span>
                    <input class="m-wrap" type="password" id="txtPasswordCon" value="" placeholder="Confirme su contraseña">
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>