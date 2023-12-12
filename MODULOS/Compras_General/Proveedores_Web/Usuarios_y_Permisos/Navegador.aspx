<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Navegador.aspx.cs" Inherits="Usuarios_y_Permisos_Navegador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Proveedores</title>
    <link rel="shortcut icon" href="../images/favicon.ico" />
    <link rel="stylesheet" href="../css/dhtmlwindow.css" type="text/css" />
    <link rel="stylesheet" href="../css/themes/base/jquery.ui.all.css" type="text/css" />
    
    <script type="text/javascript" language="javascript" src="../scripts/jquery-1.8.2.js"></script>

	<script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.datepicker.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/ui/i18n/jquery.ui.datepicker-es.js"></script>

	<script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.core.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.widget.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.mouse.js"></script>
	<script type="text/javascript" language="javascript" src="../scripts/ui/jquery.ui.resizable.js"></script>

    <script type="text/javascript" language="javascript" src="../scripts/dhtmlwindow.js"></script>
    <script type="text/javascript" language="javascript" src="../scripts/JScriptDOM.js"></script>
    <script type="text/javascript" language="javascript">
        $(window).load(function() {
            var links = document.getElementById("<%=twNavegador.ClientID %>").getElementsByTagName("a");
            for (var i = 0; i < links.length; i++) {
            var att = links[i].getAttribute("href");
                if (att != null)
                {
                    if (att.indexOf('javascript') != 0) {
                        links[i].setAttribute("href", "javascript:NodeClick(\"" + links[i].id + "\", \"" + links[i].getAttribute("href") + "\")");
                    }
                }
            }
        });
        function NodeClick(id, attribute) {
            var nodeLink = document.getElementById(id);
            if (attribute.indexOf('javascript') != 0) {
                openmypage(attribute, nodeLink.innerHTML);
            }
        }
        function openmypage(sUrl, sTitulo) { //Define arbitrary function to run desired DHTML Window widget codes
            var divid = sTitulo.replace(/\s/g, "_");
            var sEstilos = sStyleWindow[divid];
            if (sEstilos == null)
                sEstilos = 'width=600px,height=500px,center=1';
            var ajaxwin = dhtmlwindow.open(divid, "iframe", sUrl, sTitulo, sEstilos); //ajax
            //var ajaxwin = dhtmlwindow.open(divid, "ajax", sUrl, sTitulo, sEstilos); //ajax
            ajaxwin.isScrolling(false);
            <% if ((Boolean)Session["MsjCerrarDivVentanas"])
                {%>
                ajaxwin.onclose = function () { return window.confirm("¿Seguro que desea cerrar " + sTitulo + "?") }
            <%  }%>
        }
        $(document).ready(function() {
            $("#ArbolNavegacion").resizable({
                handles: 'e, w',
			    maxWidth: 300,
			    minWidth: 260
                });
            $(document).bind("contextmenu", function (e) {
                return false;
            });
		});
    </script>

    <style type="text/css">
        body 
        {
            height: 100%;
            width: 100%;
            margin: 0px;
            padding: 0px;
            overflow: hidden;
            background-color: transparent;
            position: absolute;
        }
	    #ArbolNavegacion 
	    {
	        position:absolute;
	        width: 260px; 
	        height: 90%;
	        overflow:hidden;
	        margin: 8px;
	    }
	    #twNavegador {
	        overflow: hidden;
	        z-index: 0;
	    }
	</style>
</head>
<body>
    <form id="Navegador" runat="server">
    <div id="ArbolNavegacion" class="ui-widget-content">
        <asp:TreeView ID="twNavegador" runat="server" ShowLines="True" 
            ForeColor="Black">
            <NodeStyle HorizontalPadding="5px" />
        </asp:TreeView>
    </div>
    <div id="bottom"></div>
    <div id="statusbar"><p id="status"><% HttpContext.Current.Response.Write(Session["User"].ToString()); %></p></div>
    </form>
</body>
</html>
