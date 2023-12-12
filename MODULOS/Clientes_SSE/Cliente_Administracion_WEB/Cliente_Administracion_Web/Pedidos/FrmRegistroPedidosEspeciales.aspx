<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmRegistroPedidosEspeciales.aspx.cs" Inherits="Pedidos_FrmRegistroPedidosEspeciales" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Pedidos Espaciales</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" /> 
	<link rel="shortcut icon" href="../images/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="../css/m-styles.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/GeneralForm.css" />
    <link rel="stylesheet" href="../css/south-street/jquery-ui-1.10.1.custom.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.datepicker-es.min.js"></script>
    <script type="text/javascript" src="../js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/jquery.jeditable.js"></script>
    <script type="text/javascript" src="../js/Scroller.js"></script>
    <style type="text/css">
        @import "../css/default.css";
        @import "../css/001.css";
        .Controls {
            width: 894px;
        }
        .Results {
            width: 876px;
            height: 370px;
            position: relative;
        }
        .container {
            width: 894px;
        }
        .edit,
        .clave,
        .cantidad {
            text-align: center;
        }
        .edit {
            cursor: pointer;
        }
        select.m-wrap {
            height: 24px;
            padding: 0px;
            margin-top: 6px
        }
        .Groups select.m-wrap {
            width: 333px;
        }
        .m-input-prepend .add-on,
        .m-wrap .add-on {
            width: 100px;
            display: inline-block;
            height: 24px;
            min-width: 16px;
            padding: 4px 8px;
            font-size: 14px;
            font-weight: normal;
            line-height: 24px;
            text-align: right;
            text-shadow: 0 1px 0 #fff;
        }
        label.m-wrap {
            margin: 0px;
        }
        #dtpFechaRegistro {
            width: 87px;
            margin-right: 16px;
        }
        #txtFolio {
            width: 100px;
            text-align: center;
        }
        #txtObservaciones {
            position: relative;
            width: 720px;
            max-width: 720px;
            height: 43px;
            max-height: 43px;
        }
        .HaveLegend {
            margin-top: 16px;
        }
        .fullWidth {
            width: 100%;
            position: relative;
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
        #FechaRegistro {
            position: absolute;
            right:0px;
        }
        #FrameEncabezado 
        {
            position: relative;
            width: 876px;
        }
        .m-radio.inline+.m-radio.inline, .m-checkbox.inline+.m-checkbox.inline {
            margin-left: 0px;
        }
        #MenuTabla {
            width: 24px;
            height: 100px;
            position: relative;
            margin: 0px;
            padding: 0px;
            float: left;
        }
        #newrecord,
        #delrecord {
            border: 1px solid #242424;
            background: #00632d;
            padding: 4px;
        }        
        #newrecord {
            top: 0px;
        }
        
        #delrecord {
            top: 27px;
        }
        #Tabla {
            width: 844px;
            height: 250px;
            margin-left: 8px;
            position: relative;
            float: left;
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
        
        /*Tooltip*/
        .ui-tooltip, .arrow:after {
            background: #242424;
            border: 2px solid #008A18;
          }
          .ui-tooltip {
            padding: 10px 20px;
            color: white;
            border-radius: 20px;
            font: bold 14px "Helvetica Neue", Sans-Serif;
            text-transform: uppercase;
            box-shadow: 0 0 7px black;
          }
          .arrow {
            width: 22px;
            height: 46px;
            overflow: hidden;
            position: absolute;
            top: 10px;
          }
          .arrow.top {
            top: -16px;
            bottom: auto;
          }
          .arrow.left {
            left: -22px;
          }
          .arrow:after {
            content: "";
            position: absolute;
            left: 20px;
            width: 27px;
            height: 36px;
            box-shadow: 6px 5px 9px -9px black;
            -webkit-transform: rotate(45deg);
            -moz-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            -o-transform: rotate(45deg);
            tranform: rotate(45deg);
          }
          .arrow.top:after {
            bottom: -20px;
            top: auto;
          }
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
            <div id="btnAdd" class="m-btn green"><i class="icon-plus icon-white"></i>Agregar</div>
        </div>
    </div>
    <form id="FrmRegistroPedidosEspeciales" runat="server">
    <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Guardar"><i class="icon-check icon-white"></i></span>
         <span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>
         <%--<span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>--%>
    </div>
    <div id="container" class="container">
        <div class="Controls cont">
            <div id="FrameEncabezado" class="Groups">
                <span class="labelGpoleft">Datos generales de Pedido</span>
                <div class="Textbox Unique m-input-prepend HaveLegend">
                    <label class="m-wrap">
                        <span class="add-on">Folio :</span>
                        <input type="text" class="m-wrap numeric" id="txtFolio" value="" 
                         placeholder="Folio" maxlength="8" tabindex="1" title="Teclea el numero de folio o presiona enter para crear uno nuevo."/>
                    </label>
                </div>
                <div id="FechaRegistro" class="Textbox Unique m-input-prepend HaveLegend">
                    <label class="m-wrap">
                        <span class="add-on">Fecha de registro :</span>
                        <input type="text" class="m-wrap" id="dtpFechaRegistro" value="" 
                         placeholder="2013-08-14" maxlength="10" tabindex="2" runat="server"/>
                    </label>
                </div>
                <div class="Textbox Unique m-input-prepend fullWidth">
                    <label class="m-wrap">
                        <span class="add-on">Observaciones :</span>
                        <textarea  id="txtObservaciones" class="m-wrap" rows="" tabindex="3" maxlength="200" disabled></textarea>
                    </label>
                </div>
            </div>
        </div>
        <div class="Results HaveLegend">
            <span class="labelGpoleft">Detalles de pedido</span>
            <div id="MenuTabla">
                <span id="newrecord" class="menu-button" title="Agregar Clave"><i class="icon-plus icon-white"></i></span>
                <br />
                <br />
                <span id="delrecord" class="menu-button" title="Eliminar Clave"><i class="icon-minus icon-white"></i></span>
            </div>
            <div id="Tabla">
                <table cellpadding="0" cellspacing="0" border="0" class="display" id="TablaClaves">
	                <thead>
		                <tr>
                            <th style="display:none;">IdClaveSSA</th>
                            <th>Clave SSA</th>
                            <th>Descripción</th>
                            <th>Presentación</th>
                            <th>Contenido paquete</th>
                            <th>Contenido cajas</th>
                            <th>Cantidad piezas</th>
                        </tr>
	                </thead>
	                <tbody >
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>
