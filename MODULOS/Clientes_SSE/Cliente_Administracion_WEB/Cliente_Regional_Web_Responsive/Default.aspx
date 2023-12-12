<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!doctype html>
<html lang="es">
<head runat="server">
    <title>Cliente Regional Web</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, minimum-scale=1.0, maximum-scale=1.0">
    <link type="text/css" rel="stylesheet" href="css/normalize.css">
    <link type="text/css" rel="stylesheet" href="css/toastr.css" />
    <link type="text/css" rel="stylesheet" href="css/TimeCircles.css" />
    <link type="text/css" rel="stylesheet" href="css/components.css">
    <link type="text/css" rel="stylesheet" href="css/jquery-ui.min.css">
    <link type="text/css" rel="stylesheet" href="css/001/main.css">
    <link type="text/css" rel="stylesheet" href="css/mediasqueriesUI.css">
</head>
<body>
    <form id="frmMain" runat="server" class="fullSize">
    <div id="loader" class="maskloader">
        <div class="caja">
            <div class="msjInfoLoader">
                Cargando información, espere por favor.</div>
        </div>
    </div>
    <div id="MaskPassword" class="mask">
        <div class="CajaHelp">
           
            <div class="titlePass">Cambiar Contraseña</div>
            <div class="Help">
                <div id="MsjM" class="">
                    <br />
                    <div class="m-input-prepend">                
                        <span class="add-on labelPass">Password anterior :</span>      
                        <input id="txtPasswordAnterior" class="m-wrap inputPass" size="16" type="password" placeholder="Password anterior" />
                    </div>
                    <div class="m-input-prepend">                
                        <span class="add-on labelPass">Password :</span>      
                        <input id="txtPassword" class="m-wrap inputPass" size="16" type="password" placeholder="Password Nuevo" />
                    </div> 
                    <div class="m-input-prepend">                
                        <span class="add-on labelPass">Confirmar password :</span>      
                        <input id="txtPasswordCon" class="m-wrap inputPass" size="16" type="password" placeholder="Password Nuevo" />
                    </div>
                    <span id="MsjResp" class="add-on"></span>
                    <div class="clear"></div>
                    <div id="btnPass" runat="server" class="btnPass">Guardar</div>
                    <div class="clear"></div>
                </div>
                <a id="closePassword" class="closePass" title="Cerrar"></a>
            </div>
        </div>
    </div>
    <div id="Config" class="mask">
        <div class="box">
            <div id="Options">
            <div class="titleConfig">Configuración de Usuarios</div>
                <%--<ul id="listOption" class="menu-toc">
                    <li><a href="Opcion1">Opcion 1</a></li>
                    <li><a href="Opcion2">Opcion 2</a></li>
                    <li><a href="Opcion3">Opcion 3</a></li>
                    <li><a href="Opcion4">Opcion 4</a></li>
                </ul>--%>
                <asp:TreeView ID="twOpciones" runat="server" ShowLines="True" 
                    ForeColor="Black">
                    <NodeStyle HorizontalPadding="5px" />
                </asp:TreeView>
            </div>
            <div id="ContenedorOpciones">
                <iframe id="iOptions" width="100%" height="100%" src="" frameborder="0"></iframe>
            </div>
            <a id="closeConfig" class="closePass" title="Cerrar"></a>
        </div>
    </div>
    <div id="wrapper" class="fullSize">
        <header>
            <div id="TopL" class="top-left-header">
                <div id="tgtMenu" class="btnMenu">
                    <div class="logo-title">
                        Menu
                    </div>
                    <div id="logoIcon" class="logo-icon">
                    </div>
                </div>
            </div>

            <div class="contMenu">
                <span id="btnHome" class="btnMain btnHome" title="Home"></span>
                <div class="contDinamic">
                    <span id="New" class="btnMain btnNuevo" title="Nuevo"></span>
                    <span id="Exec" class="btnMain btnEjecutar" title="Ejecutar"></span>
                    <span id="Print" class="btnMain btnImprimir" title="Imprimir"></span>
                </div>
            </div>

            <div id="cookieCountdown" class="top-right-header" data-date="2016-11-18 20:00:00" style="width: 100%;"></div>

            <div class="top-right-header">
                <div class="userProfile imgCircle">
                </div>
                <div id="nameUser" class="nameUser" title="Juan Carlos Fernández González" runat="server">
                    Juan Carlos Fernández González
                </div>
            </div>
        </header>
        <div id="nav" class="leftside">
            <div class="profile">
                <div class="leftsidePhoto imgCircle">
                    <span class="photo imgCircle"></span>
                </div>
                <div id="nameUserPanel" class="title" runat="server">
                    Juan.Fernandez
                </div>

                <div class="contMenuOption">
                    <div id="logout" class="icoCloseSesion" title="Cerrar Sesión" runat="server">
                    </div>
                    <div id="ChangePass" class="icoChangePass" title="Cambiar contraseña" runat="server">
                    </div>
                    <div id="btnConfig" class="icoConfigUser" title="Configuración de Usuario" runat="server">
                    </div>
                </div>

                
            </div>
            <div id="navPermisos" class="nav-profile">
                Menu
                <asp:TreeView ID="twNavegador" runat="server" ShowLines="True" ForeColor="Black">
                    <NodeStyle HorizontalPadding="5px" />
                </asp:TreeView>
                <div class="clear">
                </div>
            </div>
        </div>
        <div id="wrapperPage" class="wrapperPage">
            <iframe id="iContent" src=""></iframe>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="js/toastr.js"></script>
    <script type="text/javascript" src="js/general.js"></script>
    <script type="text/javascript" src="js/reports.js"></script>
    <script type="text/javascript" src="js/forms.js"></script>
    <script type="text/javascript" src="js/main.js"></script>
    <script type="text/javascript" src="js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="js/TimeCircles.js"></script>
    <script type="text/javascript">
        $(function () {
            main.init();
        });
	</script>
</body>
</html>
