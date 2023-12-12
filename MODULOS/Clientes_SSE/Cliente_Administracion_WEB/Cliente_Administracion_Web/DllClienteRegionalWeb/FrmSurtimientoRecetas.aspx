<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmSurtimientoRecetas.aspx.cs" Inherits="DllClienteRegionalWeb_FrmSurtimientoRecetas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Surtimiento de Recetas y Medicamentos</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" /> 
	<link rel="shortcut icon" href="../images/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="../css/m-styles.min.css" />
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
            width: 527px;
        }
        .Results {
            width: auto;
            height: 100px;
            position: relative;
            margin-bottom: 8px;
        }
        .container {
            width: 527px;
            padding-bottom: 0px;
        }
        select.m-wrap {
            height: 24px;
            padding: 0px;
            margin-top: 6px
        }
        .Groups select.m-wrap {
            width: 402px;
        }
        .m-input-prepend .add-on {
            width: 100px;
        }
        #FechaInicial .m-wrap .add-on,
        #FechaFinal .m-wrap .add-on {
            width: 50px;
        }
        label.m-wrap {
            margin: 0px;
        }
        #dtpFechaInicial,
        #dtpFechaFinal {
            width: 170px;
            margin-right: 16px;
        }
        #Fechas {
            width: 509px;
            margin: 0px;
            padding-top: 20px;
            padding-bottom: 0px;
        }
        #Resumen {
            width: 509px;
            padding: 8px;
        }
        
        #Resumen td {
            text-align: right;
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
        }
        #ComboEstados, #Cliente {
            width: 501px;
            padding-top: 8px;
        }
        #Resumen table {
            padding-top: 8px;
            margin: 0 auto;
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
        #FechaFinal {
            margin-top: -6px;
        }
        #Trans-Range {
            margin-bottom: 0px;
        }
        #Recetas {
            padding-top: 8px;
            position: relative;
            text-align: right;
        }
        
        #Recetas .label {
            position: relative;
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
         $(function () {
             $.datepicker.setDefaults($.datepicker.regional[""]);

             $("#dtpFechaInicial").datepicker($.datepicker.regional["es"]);
             $("#dtpFechaInicial").datepicker("option", "dateFormat", "yy-mm-dd");
             $("#dtpFechaInicial").datepicker("option", "changeMonth", "true");
             $("#dtpFechaInicial").datepicker("option", "changeYear", "true");
             $("#dtpFechaInicial").datepicker("option", "maxDate", "d");
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
    <div id="Mask">
        <div id="Caja">
            <div id="MsjRpt" runat="server">Ayuda</div>
            <a id="close" style="display: inline;" title="Cerrar"></a>
            <div id="btnAdd" runat="server"><i class="icon-plus icon-white"></i>Agregar</div>
        </div>
    </div>
    <div class="Mask">
        <div class="caja">
            <div id="nvahelp">
                <span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>
            </div>
            <div class="helpcontent" runat="server"><span>Cargando información, espere un momento...</span></div>
            <a class="close" style="display: inline;" title="Cerrar"></a>
            <table cellpadding="0" cellspacing="0" border="0" class="display" id="ResumenHelp">
                <tr>
                    <td> Claves : </td>
                    <td id="lblClaves" class="label"></td>
                    <td> Piezas : </td>
                    <td id="lblCantidad" class="label"></td>
                </tr>
            </table>
        </div>
    </div>
    <form id="FrmSurtimientoRecetas" runat="server">
    <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>
         <%--<span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>--%>
    </div>
    <div id="container" class="container">
        <div id="InfoUnidad" class="Controls cont">
            <div class="Groups">
                <span class="labelGpoleft">Información de Unidad</span>
                <div id="ComboEstados" class="Combo m-input-prepend" runat="server"></div>
            </div>
        </div>
        <div class="Controls cont">
            <div class="Groups">
                <span class="labelGpoleft">Parámetros de Cliente</span>
                <div id="Cliente" class="Combo m-input-prepend" runat="server"></div>
            </div>
        </div>
        <div class="Controls cont">
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
        </div>
        <div class="Controls cont">
            <div id="Resumen" class="Groups">
                <span class="labelGpoleft">Resumen de surtimiento recetas</span>
                <label id="Recetas" class="m-wrap inline">
                    <b>Recetas : </b><span id="lblFolios" class="label"></span>
                </label>
                <table>
                    <tr>
                        <td>Recetas surtidas completas : </td>
                        <td id="lblSurtidoPzas" class="label"></td>
                        <td id="lblSurtidos" class="label"></td>
                    </tr>
                    <tr>
                        <td>Rectas surtidas por Vales : </td>
                        <td id="lblValesPzas" class="label"></td>
                        <td id="lblVales" class="label"></td>
                    </tr>
                    <tr>
                        <td>No surtido : </td>
                        <td id="lblNoSurtidoPzas" class="label"></td>
                        <td id="lblNoSurtido" class="label"></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="Results">
            <span class="labelGpoleft">Resumen de Claves y Piezas</span>
            <table cellpadding="0" cellspacing="0" border="0" class="display" id="TablaResumen">
                <thead>
                    <tr>
                        <th>Tipo Dispensación</th>
                        <th>Claves</th>
                        <th>Piezas</th>
                        <th>Porcentaje Piezas</th>
                        <th class="hide">Tipo</th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>