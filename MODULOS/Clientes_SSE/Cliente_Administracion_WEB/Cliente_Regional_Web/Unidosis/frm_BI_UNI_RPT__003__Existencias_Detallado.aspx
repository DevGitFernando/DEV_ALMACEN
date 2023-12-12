<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_BI_UNI_RPT__003__Existencias_Detallado.aspx.cs" Inherits="Unidosis_frm_BI_UNI_RPT__003__Existencias_Detallado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Existencias_Detallado</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" /> 
	<link rel="stylesheet" type="text/css" href="../css/m-styles.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/GeneralForm.css" />
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

    <link rel="Stylesheet" type="text/css" href="../css/Existencias_Detallado.css"/>
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.datepicker-es.min.js"></script>
    <script type="text/javascript" src="../js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/Scroller.js"></script>
    <script type="text/javascript">
        if (top.location == this.location) top.location = '../Default.aspx';
        $(function () {

            $("#dtpFechaInicial").datepicker({
                dateFormat: "yy-mm-dd",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                maxDate: "d",
                hideIfNoPrevNext: true
            });

            $("#dtpFechaInicial").datepicker("setDate", "-1m");
            $("#dtpFechaInicial").keydown(function (e) {
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="frm_BI_UNI_RPT__003__Existencias_Detallado" runat="server">
   <%-- <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>
         <span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>
    </div>--%>
    <div id="container" class="container elementCenter">
        <div class="titleFrm">Añadir titulo Aquí</div>
        <div id="Info"class="Controls cont contIUnidad">
            <div id="InfoUnidad" class="Groups">
                <span class="labelGpoleft">Información de Unidad</span>
                <div id="Combos" class="Combo m-input-prepend" runat="server"></div>
            </div>
            <div id="Fechas" class="Groups">
                <span class="labelGpoleft">Periodo de revisión</span>
                <div class="Textbox Unique">
                    <label class="m-wrap inline">
                        <span class="fechaspan">Fecha:</span>
                        <input type="text" class="m-wrap" id="dtpFechaInicial" value="" placeholder="Seleccione periodo" />
                    </label>
                </div>
           </div>
        </div>
        <div class="Results">
            <span class="titleResult">Resultado</span>
            <iframe id="iResult" width="100%" height="100%" src="" frameborder="0"></iframe>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>
