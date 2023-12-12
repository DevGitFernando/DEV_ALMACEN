<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAuditor_Movimientos.aspx.cs" Inherits="UsuariosyPermisos_frmAuditor_Movimientos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Auditor_Movimientos</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" /> 
	<link rel="shortcut icon" href="../images/favicon.ico" />
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
            width: 558px;
        }
        .container {
            width: 558px;
            height: auto;
        }
        #Info 
        {
            width: 566px;
            margin-bottom: 8px;
        }
        .first
        {
            margin-top: 16px;
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
            margin-left: 16px;
        }
        #navBarraFrame
        {
            width:97%;
            padding-left: 16px;
            position:relative;
        }
        #Exec {
            left: 56px;
        }
        #Print
        {
            left: 96px;
        }
        .Results {
            width: 556px;
            padding: 0px;
            padding-top: 16px;
        }
    </style>
    <script type="text/javascript">
        if (top.location == this.location) top.location = '../Default.aspx';
        $(function () {

            $("#dtpFechaInicial").datepicker({
                dateFormat: "yy-mm-dd",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                maxDate: "-1d",
                hideIfNoPrevNext: true
            });
            $("#dtpFechaInicial").datepicker("setDate", "-1m");

            $("#dtpFechaFinal").datepicker({
                dateFormat: "yy-mm-dd",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                maxDate: "d",
                hideIfNoPrevNext: true
            });
            $("#dtpFechaFinal").datepicker("setDate", "m");

            $("#txtTop").keydown(function (event) {
                if (event.shiftKey) {
                    event.preventDefault();
                }

                if (event.keyCode == 46 || event.keyCode == 8) {
                }
                else {
                    if (event.keyCode < 95) {
                        if (event.keyCode < 48 || event.keyCode > 57) {
                            event.preventDefault();
                        }
                    }
                    else {
                        if (event.keyCode < 96 || event.keyCode > 105) {
                            event.preventDefault();
                        }
                    }
                }
            });


            $("#dtpFechaInicial").datepicker("setDate", "-1m");
            $("#dtpFechaInicial").keydown(function (e) {
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="frmAuditor_Movimientos" runat="server">
    
    <div id="container" class="container elementCenter">
        <div class="titleConfig">Periodo de revisión</div>

        <div id="navBarraFrame">
            <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
            <span id="Exec" class="menu-button" title="Ejecutar"><i class="icon-play icon-white"></i></span>
            <span id="Print" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>
            <%--<span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>--%>
        </div>

        <div id="Info"class="Controls cont">
            <div id="Fechas" class="Groups">
             <%--   <span class="labelGpoleft">Periodo de revisión</span>--%>
                <div class="Textbox Unique first">
                    <label class="m-wrap inline">
                        <span class="fechaspan">Inicio:</span>
                        <input type="text" class="m-wrap" id="dtpFechaInicial" value="" placeholder="Seleccione periodo" />
                        <span class="fechaspan">Fin :</span>
                        <input type="text" class="m-wrap" id="dtpFechaFinal" value="" placeholder="Seleccione periodo" />
                    </label>
                </div>
           </div>
        </div>
        <div class="Results">
            <span class="labelGpoleft">Resultado</span>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>
