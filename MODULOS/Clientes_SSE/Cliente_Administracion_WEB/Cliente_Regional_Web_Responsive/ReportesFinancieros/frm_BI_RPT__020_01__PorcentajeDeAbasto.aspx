<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_BI_RPT__020_01__PorcentajeDeAbasto.aspx.cs" Inherits="ReportesFinancieros_frm_BI_RPT__020_01__PorcentajeDeAbasto" %>

<!DOCTYPE html>
<html lang="es">
<head id="Head1" runat="server">
    <title>PorcentajeDeAbasto</title>
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
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.datepicker-es.min.js"></script>
    <script type="text/javascript" src="../js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/Scroller.js"></script>
    <style type="text/css">
        .Controls {
            width: 100%;
        }
        .Results {
            width: 780px;
            height: 334px;
            position: relative;
            padding: 0px;
            margin-top: 8px;
        }
        .container {
            width: 783px;
            height: 552px;
            padding-bottom: 0px;
        }
        select.m-wrap {
            height: 24px;
            padding: 0px;
            margin-top: 6px
        }
        .Groups select.m-wrap {
            width: 655px;
        }
        .m-input-prepend .add-on {
            width: 100px;
        }
        label.m-wrap {
            margin: 0px;
        }
       
        #toolbar table tr:nth-child(2) td:nth-child(1)
        {
            display: none;
        }
        #Combos
        {
            margin-top: 16px;
        }
    </style>
    <script type="text/javascript">
        if (top.location == this.location) top.location = '../Default.aspx';
    </script>
</head>
<body>
    <form id="frm_BI_RPT__001__Existencias_vs_Maximos_y_Minimos" runat="server">
    <%--<div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>
         <span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>
    </div>--%>
    <div id="container" class="container elementCenter">
        <div class="titleFrm">Añadir titulo Aquí</div>
        <div id="Info"class="Controls cont">
            <div id="InfoUnidad" class="Groups">
                <span class="labelGpoleft">Información de Unidad</span>
                <div id="Combos" class="Combo m-input-prepend" runat="server">
                    <label class="m-wrap">
                        <span class="add-on">Tipo de unidad :</span>
                        <select id="cboTipoUnidad" class="m-wrap"></select>
                    </label>
                    <label class="m-wrap">
                        <span class="add-on">Jurisdicción :</span>
                        <select id="cboJurisdiccion" class="m-wrap"></select>
                    </label>
                    <label class="m-wrap">
                        <span class="add-on">Localidad :</span>
                        <select id="cboLocalidad" class="m-wrap"></select>
                    </label>
                    <label class="m-wrap">
                        <span class="add-on">Farmacia :</span>
                        <select id="cboFarmacia" class="m-wrap"></select>
                    </label>
                    <div class="clear"></div>
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
