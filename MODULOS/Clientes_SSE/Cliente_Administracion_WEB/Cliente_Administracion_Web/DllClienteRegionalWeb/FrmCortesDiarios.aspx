﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmCortesDiarios.aspx.cs" Inherits="DllClienteRegionalWeb_FrmCortesDiarios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cortes diarios de unidades</title>
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
            width: 718px;
        }
        .Results {
            height: 450px;
            margin-top: -4px;
            position: relative;
        }
        .container {
            width: 718px;
        }
        select.m-wrap {
            height: 24px;
            padding: 0px;
            margin-top: 6px
        }
        .Groups select.m-wrap {
            width: 333px;
        }
        .m-input-prepend .add-on {
            width: 100px;
        }
        label.m-wrap {
            margin: 0px;
        }
        #dtpFechaInicial,
        #dtpFechaFinal {
            width: 120px;
            margin-right: 16px;
        }
        #Dispensacion, #Insumos {
            height: 24px;
            margin-bottom: 8px;
            padding-top: 12px;
            margin-right: 0px;
        }
        
        #Insumos {
            margin-right: 8px;
        }
        
        #Dispensacion, #Insumos {
            width: 450px;
        }
        
        #Dispensacion .m-radio.inline,
        #Insumos .m-radio.inline {
            margin-left: 32px;
        }
        .m-checkbox.inline {
            width: 90px;
            margin-left: 16px;
        }
        #Dispensacion
        {
            width: 400px;
        }
        #Fechas 
        {
            width: 224px;
            margin: 0px;
            padding-top: 27px;
            padding-bottom: 0px;
            margin-bottom: 8px;
            height: 58px;
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
        #Info 
        {
            margin-bottom: 0px;
        }
    </style>
     <script type="text/javascript">
         if (top.location == this.location) top.location = '../Default.aspx';
         $(function () {
             $.datepicker.setDefaults($.datepicker.regional[""]);

             $("#dtpFechaInicial").datepicker($.datepicker.regional["es"]);
             $("#dtpFechaInicial").datepicker("option", "dateFormat", "yy-mm-dd");
             $("#dtpFechaInicial").datepicker("option", "changeMonth", "true");
             $("#dtpFechaInicial").datepicker("option", "changeYear", "true");
             //$("#dtpFechaInicial").datepicker("option", "maxDate", "-1d");
             $("#dtpFechaInicial").datepicker("option", "maxDate", "d");
             $("#dtpFechaInicial").datepicker("option", "hideIfNoPrevNext", "true");
             //$("#dtpFechaInicial").datepicker("setDate", "m");
             //$("#dtpFechaInicial").datepicker("setDate", "-1d");

            $("#dtpFechaInicial").keydown(function (e) {
                 return false;
             });
         });
    </script>
</head>
<body>
    <form id="FrmCortesDiarios" runat="server">
    <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <%--<span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>--%>
         <span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>
    </div>
    <div id="container" class="container">
        <div id="Info"class="Controls cont">
            <div class="Groups">
                <span class="labelGpoleft">Datos de consulta</span>
                <div id="ComboJF" class="Combo m-input-prepend" runat="server"></div>
            </div>
            <div id="Fechas" class="Groups">
                <span class="labelGpoleft">Periodo</span>
                <div class="Textbox Unique">
                    <label class="m-wrap inline">
                        <span class="fechaspan">Fecha :</span>
                        <input type="text" class="m-wrap" id="dtpFechaInicial" value="" placeholder="Seleccione periodo" />
                    </label>
                </div>
           </div>
        </div>
        <div class="Results">
            <span class="labelGpoleft">Detalles de dispensación</span>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>
