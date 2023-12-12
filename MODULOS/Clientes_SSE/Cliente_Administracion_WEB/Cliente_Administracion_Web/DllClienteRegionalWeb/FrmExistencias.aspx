<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmExistencias.aspx.cs" Inherits="DllClienteRegionalWeb_FrmExistencias" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Existencias</title>
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
        /*@import "../css/default.css";*/
        .Controls {
            width: 894px;
        }
        .Results {
            width: 876px;
            height: 370px;
            margin-top: -4px;
            position: relative;
        }
        .container {
            width: 894px;
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
            padding: 4px 0px;
            font-size: 14px;
            font-weight: normal;
            line-height: 24px;
            text-align: center;
            text-shadow: 0 1px 0 #fff;
        }
        label.m-wrap {
            margin: 0px;
        }
        #dtpFechaInicial,
        #dtpFechaFinal {
            width: 60px;
            margin-right: 16px;
        }
        #Dispensacion, #Insumos,
        #Existencias, #Clave {
            /*width: 400px;*/
            /*margin: 0px;*/
            height: 24px;
            margin-bottom: 4px;
            padding-top: 12px;
            margin-right: 0px;
        }
        #Insumos, #Existencias {
            margin-right: 4px;
        }
        
        #Dispensacion, #Insumos,
        #Existencias, #Clave {
            width: 427px;
        }
        #Dispensacion .m-radio.inline,
        #Insumos .m-radio.inline,
        #Existencias .m-radio.inline,
        #Clave .m-radio.inline {
            width: 120px;
            /*margin-left: 32px;*/
        }
        .m-checkbox.inline {
            width: 90px;
            margin-left: 16px;
        }
        #Fechas 
        {
            /*width: 400px;*/
            width: 284px;
            margin: 0px;
            padding-top: 20px;
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
        #InfoUnidad 
        {
            width: 876px;
            margin-right: 0px;
            padding-top: 16px;
            padding-bottom: 0px;
        }
        #ComboLF {
            margin-left: 8px;
        }
        .m-radio.inline+.m-radio.inline, .m-checkbox.inline+.m-checkbox.inline {
            margin-left: 0px;
        }
        
        #Dispensacion,
        #Insumos
        {
            margin-bottom: 0px;
        }
    </style>
    <script type="text/javascript">
        if (top.location == this.location) top.location = '../Default.aspx';
    </script>
</head>
<body>
    <form id="frmExistencias" runat="server">
    <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <%--<span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>--%>
         <span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>
    </div>
    <div id="container" class="container">
        <div class="Controls cont">
            <div id="InfoUnidad" class="Groups">
                <span class="labelGpoleft">Información Unidad</span>
                <div id="ComboJ" class="checkbox m-wrap inline" runat="server">
                </div>
                <div id="ComboF" class="checkbox m-wrap inline" runat="server">
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
            <div id="Dispensacion" class="Groups">
                <span class="labelGpoleft">Tipo de Dispensacion</span>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDispensacion" id="rdoTpDispAmbos" value="0" checked/>
                    Ambos
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDispensacion" id="rdoTpDispVenta" value="2"/>
                    Venta
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDispensacion" id="rdoTpDispConsignacion" value="1"/>
                    Consignación
                </label>
            </div>
        </div>
        <div class="Controls cont">
            <div id="Existencias" class="Groups">
                <span class="labelGpoleft">Tipo de Existencias</span>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoExistencias" id="rdoRptTodos" value="0" checked/>
                    Todos
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoExistencias" id="rdoRptConExist" value="1"/>
                    Con Existencia
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoExistencias" id="rdoRptSinExist" value="2"/>
                    Sin Existencia
                </label>
            </div>
            <div id="Clave" class="Groups">
                <span class="labelGpoleft">Tipo de Clave</span>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoClave" id="rdoClaveAmbos" value="0" checked/>
                    Todos
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoClave" id="rdoClaveCauses" value="1"/>
                    Causes
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoClave" id="rdoClaveNoCauses" value="2"/>
                    No Causes
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