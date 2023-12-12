<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmListadosVarios.aspx.cs" Inherits="DllClienteRegionalWeb_FrmListadosVarios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listados generales</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" /> 
	<link rel="shortcut icon" href="../images/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="../css/m-styles.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/GeneralForm.css" />
    <%  if (DtGeneral.Empresa == "001")
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
    <% { Response.Write("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/GeneralForm_" + DtGeneral.Empresa + ".css\" />"); } %>
    <% { Response.Write("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/" + DtGeneral.Empresa + ".css\" />"); } %>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.datepicker-es.min.js"></script>
    <script type="text/javascript" src="../js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/Scroller.js"></script>
    <style type="text/css">
        .Controls {
            width: 894px;
        }
        .Results {
            width: 876px;
            height: 450px;
            /*margin-top: -8px;*/
            position: relative;
        }
        .container {
            width: 894px;
        }
        select.m-wrap {
            height: 24px;
            padding: 0px;
            margin-top: 6px
        }
        .Groups select.m-wrap {
            width: 457px;
        }
        .m-input-prepend .add-on,
        .m-wrap .add-on {
            width: 100px;
            display: inline-block;
            height: 24px;
            min-width: 16px;
            padding: 4px 0px;
            font-size: 14px;
            font-weight: normal;
            line-height: 24px;
            text-align: right;
            text-shadow: 0 1px 0 #fff;
            margin-right: 8px;
        }
        label.m-wrap {
            margin: 0px;
        }
        #dtpFechaInicial,
        #dtpFechaFinal {
            width: 60px;
            margin-right: 16px;
        }
        #Insumos,
        #Dispensacion {
            /*width: 400px;*/
            height: 43px;
            /*margin: 0px;*/
            margin-bottom: 8px;
            padding-top: 20px;
        }
        #Insumos {
            width: 312px;
        }
        #Dispensacion {
            width: 228px;
        }
        #Insumos .m-radio.inline {
            margin-left: 0px;
        }
        .m-checkbox.inline {
            width: 90px;
            margin-left: 16px;
        }
        #Fechas 
        {
            /*width: 400px;*/
            width: 284px;
            margin: 0px;
            padding-top: 20px;
        }
        .Textbox  {
            margin-left: 16px;
        }
        .fechaspan {
            display: inline-block;
            width: auto;
            height: 24px;
            min-width: 16px;
            padding: 4px 0px;
            font-size: 14px;
            font-weight: normal;
            line-height: 24px;
            text-align: center;
            text-shadow: 0 1px 0 #fff;
        }
        #InfoUnidad {
            margin-right: 0px;
            margin-left: auto;
            width: 876px;
        }
        #ComboLF {
            margin-left: 8px;
        }
        .m-radio.inline+.m-radio.inline, .m-checkbox.inline+.m-checkbox.inline {
            margin-left: 0px;
        }
        #ComboListado 
        {
            margin-left: 169px;
        }
    </style>
    <script type="text/javascript">
        if (top.location == this.location) top.location = '../Default.aspx';
    </script>
</head>
<body>
    <form id="FrmListadosVarios" runat="server">
   <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>
         <%--<span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>--%>
    </div>
    <div id="container" class="container">
        <div class="Controls cont">
            <div id="InfoUnidad" class="Groups">
               <span class="labelGpoleft">Listado ó Reporte</span>
                <div id="ComboListado" class="checkbox m-wrap inline" runat="server">
                </div>
            </div>
        </div>
        <div class="Results">
            <span class="labelGpoleft">Detalles</span>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>