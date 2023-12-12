<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmExistenciaPorcClave.aspx.cs" Inherits="Puebla_frmExistenciaPorcClave" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">--%>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Existencias por Clave</title>
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
            width: 918px;
        }
        .Results 
        {
            width: 901px;
            height: 450px;
            margin-top: -4px;
            position: relative;
        }
        .container {
            width: 918px;
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
            margin-top: 4px;
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
            width: 228px;
            margin: 0px;
            padding-top: 27px;
            padding-bottom: 0px;
            margin-bottom: 8px;
            height: 64px;
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
        
        #Claves
        {
            width: 650px;
            padding-top: 16px;
            padding-bottom: 0px;
            margin-bottom: 8px;
            margin-right: 4px;
        }
        
        input[type="text"].m-wrap
        {
            padding: 2px;
        }
        
        #txtClave
        {
            text-align: center;
        }
        
        #txtDescripcion
        {
            width: 516px;
            margin-left: 112px;
            text-overflow: ellipsis;
        }
        #LabelFecha
        {
            margin-top: 8px;
        }
        /*Help*/
        body {
            overflow: hidden; 
        }
        #Mask, .Mask {
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
        #Caja, .caja {
            position: relative;
            margin: 0 auto;
            margin-top: 48px;
            border: 4px solid #41a62a;
            border-radius: 5px;
            background: #fff;
            width: 700px;
            height: 500px;
        }
        #close, .close {
            position: absolute;
            top: -15px;
            right: -15px;
            width: 37px;
            height: 34px;
            background: transparent url('../images/fancybox_sprite.png') -40px 0px;
            cursor: pointer;
            z-index: 1103;
        }
        #Reporte, #MsjRpt, .helpcontent {
            width: 100%;
            height: 100%;
            overflow: auto;
        }
        #nvahelp {
            background: #00632D;
            top: 0px;
            left: 0px;
            position: absolute;
            width: 100%;
            height: 47px;
            z-index: 1102;
        }
        
        #nvahelp #Exportar{
            left: 8px;
        }
        
        .caja #ResumenHelp {
            position: absolute;
            bottom: 8px;
            right: 8px;
            text-align: right;
            height: 12px;
        }
        
        .helpcontent span {
            position: absolute;
            font-size: 2em;
            text-align: center;
            margin-top: 33%;
            width: 100%;
            left: 0px;
        }
        #MsjRpt,
        .helpcontent {
            position: absolute;
            top: 8px;
            margin: 0px;
            padding: 0px;
            height: 500px;
            width: 700px;
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
         });
    </script>
</head>
<body>
    <div id="Mask">
        <div id="Caja">
            <div id="MsjClavesSSA" class="helpcontent" runat="server">Ayuda</div>
            <a id="close" style="display: inline;" title="Cerrar"></a>
            <div id="btnAdd" runat="server"><i class="icon-plus icon-white"></i>Agregar</div>
        </div>
    </div>
    <form id="frmExistenciaPorcClave" runat="server">
    <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <%--<span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>--%>
         <span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>
    </div>
    <div id="container" class="container">
        <div id="Info"class="Controls cont">
            <div id="Claves" class="Groups">
                <span class="labelGpoleft">Datos de consulta</span>
                <div id="InfoClave" class="Combo m-input-prepend" runat="server">
                    <label class="m-wrap inline">
                        <span class="add-on">Clave SSA :</span>
                        <input type="text" class="m-wrap" id="txtClave" value="" placeholder="Clave" maxlength="20" />
                        <%--<span id="msjFarmacia" class="MsjBottom hide" style="display: none;">Presione Enter para obtener la lista de claves.</span>--%>
                    </label>
                    <label class="m-wrap inline">
                        <input type="text" class="m-wrap" id="txtDescripcion" value="" placeholder="Descripcion Clave" disabled="" />
                    </label>
                </div>
            </div>
            <div id="Fechas" class="Groups">
                <span class="labelGpoleft">Periodo</span>
                <div class="Textbox Unique">
                    <label id="labelFecha" class="m-wrap inline">
                        <span class="fechaspan">Fecha :</span>
                        <input type="text" class="m-wrap" id="dtpFechaInicial" value="" placeholder="Seleccione periodo" />
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
