<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Usuarios_y_Permisos_Login" %>

<%@ Register assembly="SC_Controls_CSW" namespace="SC_Controls_CSW" tagprefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <script type="text/javascript" language="javascript" src="../scripts/jquery-1.8.2.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.core.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.datepicker.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.tabs.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/ui/i18n/jquery.ui.datepicker-es.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.button.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/jquery.dataTables.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/JScript_Login.js"></script>
    <link rel="stylesheet" href="../css/themes/base/jquery.ui.all.css"/>
	<link rel="stylesheet" href="../css/demos.css"/>
	<link rel="shortcut icon" href="../images/favicon.ico"/>
    <link href="../css/windowslogin.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="EdoLogin" runat="server">
    <% if ((string)Session["Error"] != "00")
       {%>
        <div class="alerta mensajes"><% HttpContext.Current.Response.Write(Session["Error"].ToString()); %></div>
    <% }%>
    <div id="boxes">
	    <div id="dialog" class="window">
            <div class="d-login"><a href="#" class="close">
                <asp:Image ID="imgCancelar" runat="server" ImageUrl="~/images/servicestopped.ico" />&nbsp;Cancelar</a></div>
            <br />
		    <div id="imageLoad">
        	    <img src="../images/user.gif" width="64" height="64" alt="Procesando" align="middle"/>
                <h2>Inciando Sesión</h2>
		    </div>    
	    </div>
	    <!-- Mask que cubrira el screen -->
	    <div id="mask"></div>
    </div>
    <div id="Contenedor">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td id="tis"></td>
                <td id="tms"><div id="TituloLogin">Autenticación de usuarios</div></td>
                <td id="tds"></td>
            </tr>
            <tr style="height:96px;">
                <td id="tim"></td>
                <td id="tdContenedor">
                <fieldset style="width:310px; height:80px; margin:5px 5px 5px 5px; padding:5px;">
                    <table id="tableContenido" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td style="width:91px;" class="Labels">
                                <asp:Label ID="lblJurisdiccion" runat="server" Text="Usuario :"></asp:Label></td>
                            <td>
                                <cc2:scwTextBoxExt ID="txtUsuario" runat="server" Width="204px" Height="21px" style="text-transform:uppercase;"></cc2:scwTextBoxExt>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:91px;" class="Labels">
                                <asp:Label ID="Label1" runat="server" Text="Password :"></asp:Label></td>
                            <td>
                                <cc2:scwTextBoxExt ID="txtPassword" runat="server" TextMode="Password" Height="21px" 
                                    Width="204px" style="text-transform:uppercase;"></cc2:scwTextBoxExt>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Button ID="btnLogin" runat="server" Text=" Autenticar " 
                                    onclick="btnLogin_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                </td>
                <td id="tdm"></td>
            </tr>
            <tr>
                <td id="tii"></td>
                <td id="tmi"></td>
                <td id="tdi"></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>