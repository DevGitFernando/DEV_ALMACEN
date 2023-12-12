<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmAbastoGeneral.aspx.cs" Inherits="DllClienteRegionalWeb_FrmAbastoGeneral" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<%--<html xmlns="http://www.w3.org/1999/xhtml">--%>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Reporte de Porcentaje de Abasto General</title>
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
    <style type="text/css">
        .Controls {
            width: 800px;
        }
        /*.Controls .Groups {
            padding-bottom: 0px;
        }*/
        .Results {
            width: auto;
            height: 275px;
            position: relative;
            margin-bottom: 8px;
        }
        .container {
            width: 800px;
            padding-bottom: 0px;
        }
        .Controls .Groups {
            width: 782px;
            text-align: center;
        }
        select.m-wrap {
            height: 24px;
            padding: 0px;
            margin-top: 6px
        }
        .Groups select.m-wrap {
            width: 662px;
        }
        .m-input-prepend .add-on {
            width: 100px;
        }
        label.m-wrap {
            margin: 0px;
        }
        .Textbox  {
            margin-left: 16px;
        }
        #InfoUnidad {
            /*margin-right: 0px;
            margin-left: auto;*/
        }
        #ComboEstados {
            width: 501px;
            padding-top: 8px;
        }
        #txtFarmacia, #lblFarmacia {
            width: 62px;
            height: 20px;
            padding: 0px;
            margin-top: 6px;
            text-align: center;
        }
        #lblFarmacia {
            margin-left: 8px;
            text-align: left;
            width: 327px;
        }
        #Reportes
        {
            width: 360px;
            margin-right: 0px;
            text-align: center;
        }
        
        #Insumos
        {
            width: 400px;
            margin-right: 4px;
        }
        
        .m-radio.inline, 
        .m-checkbox.inline
        {
            margin-top: 8px;
            margin-left: 26px !important;
        }
        #navBarraFrame span
        {
            height: 14px;
        }
        
        label .first
        {
            margin-left: 0px !important;
        }
        #Detallado
        {
            margin-left: 64px !important;
        }
        /*Help*/
        body {
            overflow: hidden; 
        }
        #Mask {
            position: absolute;
            width: 100%;
            height: 100%;
            overflow: hidden;
            background: url('../images/fancybox_overlay.png');
            margin: 0px;
            padding: 0px;
            top: 47px;
            left: 0px;
            z-index: 999999999999;
            text-align: center;
            display: none;
        }
        #Caja {
            position: relative;
            margin: 0 auto;
            margin-top: 48px;
            border: 4px solid #41a62a;
            border-radius: 5px;
            background: #fff;
            width: 700px;
            height: 500px;
        }
        #close {
            position: absolute;
            top: -15px;
            right: -15px;
            width: 37px;
            height: 34px;
            background: transparent url('../images/fancybox_sprite.png') -40px 0px;
            cursor: pointer;
            z-index: 1103;
        }
        #Reporte, #MsjRpt {
            width: 100%;
            height: 100%;
            overflow: auto;
        }
        
        #MsjRpt {
            position: absolute;
            top: 8px;
            margin: 0px;
            padding: 0px;
        }  
        
        #btnAdd {
            position: absolute;
            width: 125px;
            height: 12px;
            bottom: 4px;
            right: 4px;
        }
        
        .dataTables_filter {
            float: left;
            text-align: left;
            margin-left: 8px;
        }
        /*Help*/
    </style>
     <script type="text/javascript">
         if (top.location == this.location) top.location = '../Default.aspx';
    </script>
</head>
<body>
    <div id="Mask">
        <div id="Caja">
            <div id="MsjRpt" runat="server">Ayuda</div>
            <a id="close" style="display: inline;" title="Cerrar"></a>
            <div id="btnAdd" runat="server"><i class="icon-plus icon-white"></i>Agregar</div>
        </div>
    </div>
    <form id="FrmAbastoGeneral" runat="server">
    <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <%--<span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>--%>
         <span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>
    </div>
    <div id="container" class="container">
        <div id="InfoUnidad" class="Controls cont">
            <div class="Groups">
                <span class="labelGpoleft">Información de Unidad</span>
                <div id="ComboEstados" class="Combo m-input-prepend" runat="server"></div>
            </div>
        </div>
        <div class="Controls cont">
            <div id="Insumos" class="Groups">
                <span class="labelGpoleft">Tipo de Insumo</span>
                <label class="m-radio m-wrap inline first">
                    <input type="radio" class="m-wrap" name="tipoInsumo" id="rdoInsumosAmbos" value="0" checked="checked"/>
                    Ambos
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoInsumo" id="rdoInsumosMedicamento" value="2"/>
                    Medicamento
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoInsumo" id="rdoInsumoMatCuracion" value="1"/>
                    Material de curación
                </label>
            </div>
            <div id="Reportes" class="Groups">
                <span class="labelGpoleft">Tipo de Reporte</span>
                <label class="m-radio m-wrap inline first">
                    <input type="radio" class="m-wrap" name="tipoReporte" id="rdoConcentrado" value="0" checked="checked"/>
                    Concentrado
                </label>
                <label id="Detallado" class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoReporte" id="rdoDetallado" value="1"/>
                    Detallado
                </label>
            </div>
        </div>

        <div class="Results">
            <span class="labelGpoleft">Detalle de Abasto</span>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>
