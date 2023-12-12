<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTBC_Surtimiento_Regional.aspx.cs" Inherits="DllClienteRegionalWeb_FrmTBC_Surtimiento_Regional" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Surtimiento de Insumos</title>
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
            width: 984px;
        }
        .Results {
            width: 966px;
            height: 350px;
            margin-top: -4px;
            position: relative;
        }
        .container {
            width: 984px;
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
            width: 80px;
            margin-right: 16px;
        }
        #Insumos {
            height: 48px;
            margin: 0px;
            padding-top: 29px;
            min-width: 80px;
            width: 144px;
        }
        #Insumos .m-radio.inline, .m-checkbox.inline {
            width: 140px;
            margin-left: 40px;
        }
        #Insumos label{
            padding-left: 32px;
            margin-bottom: 4px;
        }
        #Fechas 
        {
            margin: 0px;
            padding-top: 27px;
            padding-bottom: 0px;
            margin-bottom: 8px;
            margin-right: 8px;
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
            $("#dtpFechaInicial").datepicker("option", "hideIfNoPrevNext", "true");
            $("#dtpFechaInicial").datepicker("setDate", "-1m");

            $("#dtpFechaFinal").datepicker($.datepicker.regional["es"]);
            $("#dtpFechaFinal").datepicker("option", "dateFormat", "yy-mm-dd");
            $("#dtpFechaFinal").datepicker("option", "changeMonth", "true");
            $("#dtpFechaFinal").datepicker("option", "changeYear", "true");
            $("#dtpFechaFinal").datepicker("option", "maxDate", "d");
            $("#dtpFechaFinal").datepicker("option", "hideIfNoPrevNext", "true");
            $("#dtpFechaFinal").datepicker("setDate", "m");

            $("#dtpFechaInicial").keydown(function (e) {
                return false;
            });

            $("#dtpFechaFinal").keydown(function (e) {
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="FrmTBC_Surtimiento_Regional" runat="server">
    <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <%--<span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>--%>
         <span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>
    </div>
    <div id="container" class="container">
        <div id="Info"class="Controls cont">
            <div class="Groups">
                <span class="labelGpoleft">Información de jurisdicciones</span>
                <div id="ComboJF" class="Combo m-input-prepend" runat="server"></div>
            </div>
            <div id="Fechas" class="Groups">
                <span class="labelGpoleft">Rango de fechas</span>
                <div class="Textbox Unique">
                    <label class="m-wrap inline">
                        <span class="fechaspan">Inicio :</span>
                        <input type="text" class="m-wrap" id="dtpFechaInicial" value="" placeholder="Seleccione periodo" />
                    </label>
                </div>
                <div class="Textbox Unique">
                    <label class="m-wrap inline">
                        <span class="fechaspan">Fin :</span>
                        <input type="text" class="m-wrap" id="dtpFechaFinal" value="" placeholder="Seleccione periodo" />
                    </label>
                </div>
           </div>
            <div id="Insumos" class="Groups">
                <span class="labelGpoleft">Tipo de reporte</span>
                <label class="m-radio m-wrap">
                    <input type="radio" class="m-wrap" name="tipoReporte" id="rdoCauses" value="1" checked/>
                    Causes
                </label>
                <label class="m-radio m-wrap">
                    <input type="radio" class="m-wrap" name="tipoReporte" id="rdoNoCauses" value="2"/>
                    No causes
                </label>
            </div>
        </div>
        <div class="Results">
            <span class="labelGpoleft">Listado de Claves</span>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>