<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_BI_RPT__002__Existencias_Detallado.aspx.cs" Inherits="Generales_frm_BI_RPT__002__Existencias_Detallado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Existencias_Detallado</title>
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
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.datepicker-es.min.js"></script>
    <script type="text/javascript" src="../js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/Scroller.js"></script>
    <style type="text/css">
        .Controls {
            width: 894px;
        }
        .Results {
            width: 780px;
            height: 300px;
            position: relative;
            padding: 0px;
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
            width: 401px;
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
        
        #Fechas,
        #Resumen
        {
            margin: 0px;
            padding: 0px;
            margin-bottom: 8px;
            margin-right: 8px;
            height: 157px;
            width: 236px;
        }
        #Resumen {
            width: 220px;
            min-width: 0px;
            height: 60px;
            margin-right: 0px;
        }
        #Resumen label span {
            display: inline-block;
            width: auto;
            height: 24px;
            min-width: 16px;
            font-size: 14px;
            font-weight: normal;
            line-height: 24px;
            text-align: center;
            text-shadow: 0 1px 0 #fff;
        }
        #txtFarmacia, #lblFarmacia,
        #txtCte, #lblCte,
        #txtSubCte, #lblSubCte {
            width: 62px;
            height: 20px;
            padding: 0px;
            margin-top: 6px;
            text-align: center;
        }
        #lblFarmacia,
        #lblCte, #lblSubCte {
            margin-left: 8px;
            text-align: left;
            width: 327px;
        }
        #txtClaves,
        #txtPiezas {
            padding-top: 0px;
            width: 50px;
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
        #InfoUnidad
        {
            padding-top: 16px;
        }
        .Textbox.Unique {
            width: 220px;
            margin: 0px;
            padding-right: 16px;
            text-align: right;
            margin-top: 56px;
        }
        #toolbar table tr:nth-child(2) td:nth-child(1)
        {
            display: none;
        }
        #Parametros {
            padding-top: 16px;
            margin-bottom: 8px;
            padding-bottom: 0px;
        }
        input[type="text"].m-wrap {
            width: 233px;
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
                maxDate: "d",
                hideIfNoPrevNext: true
            });

            $("#dtpFechaInicial").datepicker("setDate", "d");
            $("#dtpFechaInicial").keydown(function (e) {
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="frm_BI_RPT__002__Existencias_Detallado" runat="server">
    <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <%--<span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>--%>
         <%--<span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>--%>
    </div>
    <div id="container" class="container">
        <div id="Info"class="Controls cont">
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
           <div id="Parametros" class="Groups">
                <span class="labelGpoleft">Parámetros</span>
                <div class="m-input-prepend">
                    <label class="m-wrap">
                        <span class="add-on">Clave SSA :</span>
                        <input type="text" class="m-wrap" id="txtClaveSSA" value="" placeholder="Clave SSA" maxlength="100" />
                    </label>
                </div>
                <div id="Procedencia" class="m-input-prepend">
                    <label class="m-wrap inline">
                        <span class="fechaspan">Fuente de Financiamiento :</span>
                        <input type="text" class="m-wrap" id="txtFuenteFinanciamiento" value="" placeholder="Fuente de Financiamiento" maxlength="1000" />
                    </label>
                </div>
           </div>
        </div>
        <div class="Results">
            <span class="labelGpoleft">Resultado</span>
            <iframe id="iResult" width="100%" height="100%" src="" frameborder="0"></iframe>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>
