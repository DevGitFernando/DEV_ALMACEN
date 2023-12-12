<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmReportesTransferencias.aspx.cs" Inherits="DllClienteRegionalWeb_FrmReportesTransferencias" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reportes Administrativos de Transferencias</title>
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
            width: 527px;
        }
        .Results {
            width: auto;
            height: 150px;
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
            width: 120px;
            /*margin-right: 16px;*/
            height: 20px;
            padding: 0px;
            margin-top: 6px;
        }
        #Dispensacion, #Insumos {
            /*width: 400px;*/
            /*margin: 0px;*/
            height: 24px;
            margin-bottom: 8px;
            padding-top: 12px;
            margin-right: 0px;
        }
        
        #Insumos {
            margin-right: 8px;
        }
        
        #Dispensacion, #Insumos, #Destino {
            width: 485px;
            margin-bottom: 4px;
            padding-left: 32px;
        }
        
        #Dispensacion .m-radio.inline,
        #Insumos .m-radio.inline,
        #Destino .m-radio.inline {
            width: 120px;
            /*margin-left: 4px;*/
        }
        
        .m-checkbox.inline {
            width: 90px;
            margin-left: 16px;
        }
        #Fechas 
        {
            /*width: 400px;*/
            width: 242px;
            margin: 0px;
            padding-top: 12px;
            padding-bottom: 0px;
            margin-bottom: 8px;
            height: 76px;
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
        #Transferencia { 
            width: 217px;
            padding-top: 32px;
            height: 48px;
            padding-left: 32px;
        }   
        #Transferencia .m-radio.inline {
            width: 80px;
        }
        #FechaFinal {
            margin-top: -6px;
        }
        #Trans-Range {
            margin-bottom: 0px;
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
         $(function () {
             $.datepicker.setDefaults($.datepicker.regional[""]);

             $("#dtpFechaInicial").datepicker($.datepicker.regional["es"]);
             $("#dtpFechaInicial").datepicker("option", "dateFormat", "yy-mm-dd");
             $("#dtpFechaInicial").datepicker("option", "changeMonth", "true");
             $("#dtpFechaInicial").datepicker("option", "changeYear", "true");
             //$("#dtpFechaInicial").datepicker("option", "maxDate", "d");
             $("#dtpFechaInicial").datepicker("option", "hideIfNoPrevNext", "true");
             $("#dtpFechaInicial").datepicker("setDate", "-1m");

             $("#dtpFechaFinal").datepicker($.datepicker.regional["es"]);
             $("#dtpFechaFinal").datepicker("option", "dateFormat", "yy-mm-dd");
             $("#dtpFechaFinal").datepicker("option", "changeMonth", "true");
             $("#dtpFechaFinal").datepicker("option", "changeYear", "true");
             //&$("#dtpFechaFinal").datepicker("option", "maxDate", "d");
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
    <form id="FrmReportesTransferencias" runat="server">
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
        <div id="Trans-Range" class="Controls cont">
            <div id="Transferencia" class="Groups">
                <span class="labelGpoleft">Tipo de transferencia</span>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoTransferencia" id="rdoTRE" value="0" checked/>
                    Entrada
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoTransferencia" id="rdoTRS" value="1"/>
                    Salida
                </label>
            </div>
            <div id="Fechas" class="Groups">
                <span class="labelGpoleft">Rango de fechas</span>
                <div id="FechaInicial" class="m-input-prepend">
                    <label  class="m-wrap">
                        <span class="add-on">Inicio :</span>
                        <input type="text" class="m-wrap" id="dtpFechaInicial" value="" placeholder="Seleccione periodo" />
                    </label>
                </div>
                <div id="FechaFinal" class="m-input-prepend">
                    <label class="m-wrap">
                        <span class="add-on">Fin :</span>
                        <input type="text" class="m-wrap" id="dtpFechaFinal" value="" placeholder="Seleccione periodo" />
                    </label>
                </div>
           </div>
        </div>
        <div class="Controls cont">
            <div id="Insumos" class="Groups">
                <span class="labelGpoleft">Tipo de Insumo</span>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoInsumo" id="rdoInsumosAmbos" value="0" checked/>
                    Ambos
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoInsumo" id="rdoInsumosMedicamento" value="1"/>
                    Medicamento
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoInsumo" id="rdoInsumoMatCuracion" value="2"/>
                    Material de curación
                </label>
            </div>
        </div>
        <div class="Controls cont">
            <div id="Dispensacion" class="Groups">
                <span class="labelGpoleft">Tipo de Dispensación</span>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDispensacion" id="rdoTpDispAmbos" value="0" checked/>
                    Ambos
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDispensacion" id="rdoTpDispVenta" value="1"/>
                    Venta
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDispensacion" id="rdoTpDispConsignacion" value="2"/>
                    Consignación
                </label>
            </div>
        </div>
        <div class="Controls cont">
            <div id="Destino" class="Groups">
                <span class="labelGpoleft">Destino</span>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDestino" id="rdoAmbos" value="2" checked/>
                    Ambos
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDestino" id="rdoFarmacia" value="0"/>
                    Farmacia
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDestino" id="rdoAlmacen" value="1"/>
                    Almacén
                </label>
            </div>
        </div>
        <div class="Results">
            <span class="labelGpoleft">Resumen de Claves y Piezas</span>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>
