<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDispensacionClaves.aspx.cs" Inherits="DllClienteRegionalWeb_FrmProximosACaducar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Salidas por Clave</title>
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
    <%--<script type="text/javascript" src="../js/JScript.Init.js"></script>
    <script type="text/javascript" src="../js/page.js"></script>--%>
    <style type="text/css">
        .Controls {
            width: 894px;
        }
        .Results {
            width: 876px;
            height: 350px;
            margin-top: -8px;
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
            width: 272px;
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
            font-size: 12px;
        }
        #Insumos,
        #Dispensacion {
            height: 43px;
            margin-bottom: 8px;
            padding-top: 20px;
        }
        #Insumos {
            width: 312px;
        }
        #Dispensacion {
            width: 228px;
        }
        #Insumos .m-radio.inline {
            margin-left: 0px;
        }
        .m-checkbox.inline {
            width: 88px;
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
        #InfoUnidad {
            margin-right: 0px;
        }
        #ComboLF {
            margin-left: 8px;
        }
        .m-radio.inline+.m-radio.inline, .m-checkbox.inline+.m-checkbox.inline {
            margin-left: 0px;
        }
    </style>
    <script type="text/javascript">
        if (top.location == this.location) top.location = '../Default.aspx';
        $(function () {
            //var opcDate = General.bFechaCompleta();
            var opcDate = false;
            
            <% if (DtGeneral.FechaCompleta) { %>
                opcDate = true;
            <% }%>
            
            var dtFormat = opcDate ? "yy-mm-dd" : "yy-mm-01";

            $("#dtpFechaInicial").datepicker({
                dateFormat: dtFormat,
                changeMonth: true,
                changeYear: true,
                hideIfNoPrevNext: true,
                numberOfMonths: 1,
                maxDate: "-1d",
                onClose: function (selectedDate) {
                    var aDate = selectedDate.split('-');

                    if (aDate[1] == 12) {
                        aDate[0] = parseInt(aDate[0]) + 1;
                        aDate[1] = "01";
                        //aDate[1] = 1;
                    }
                    else {
                        var imonth = parseInt(aDate[1]) + 1;
                        if (imonth < 9) {
                            aDate[1] = '0' + imonth;
                            //aDate[1] = imonth;
                        }
                        else {
                            aDate[1] = imonth;
                        }

                    }
                    var nextDate = aDate[0] + '-' + aDate[1] + '-' + aDate[2];
                    //$("#dtpFechaFinal").datepicker("option", "minDate", new Date(selectedDate));
                    //$("#dtpFechaFinal").datepicker("option", "maxDate", new Date(nextDate) + "+1d");
                    //$('#dtpFechaFinal').datepicker('setDate', new Date(nextDate));
                    $('#dtpFechaFinal').val(nextDate);
                }
            });
            $("#dtpFechaInicial").datepicker("setDate", "-1m");
            
            $("#dtpFechaFinal").datepicker({
                dateFormat: dtFormat,
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                hideIfNoPrevNext: true,
                minDate: "-1m+1d",
                //maxDate: "d"
            });

            $("#dtpFechaFinal").datepicker("setDate", "m");

            $("#dtpFechaInicial").keydown(function (e) {
                return false;
            });

            $("#dtpFechaFinal").keydown(function (e) {
                return false;
            });

            //General.ProcesoPorDia(opcDate);
        });
    </script>
</head>
<body>
    <form id="FrmDispensancionClaves" runat="server">
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
                <div id="ComboUJ" class="checkbox m-wrap inline" runat="server">
                </div>
                <div id="ComboLF" class="checkbox m-wrap inline" runat="server">
                </div>
                 <div id="TipoReporte" class="checkbox m-wrap inline">
                    <label class="m-checkbox m-wrap inline">
                        <input id="chkTipoTodasJuris" type="checkbox" class="m-wrap" value=""/>
                            Concentrado
                    </label>
                </div>
            </div>
        </div>
         <div class="Controls cont">
            <div id="Dispensacion" class="Groups">
                <span class="labelGpoleft">Tipo de Dispensación</span>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDispensacion" id="rdoTpDispAmbos" value="0" checked="checked"/>
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
            <div id="Insumos" class="Groups">
                <span class="labelGpoleft">Tipo de Medicamentos</span>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoInsumo" id="rdoInsumosAmbos" value="0" checked="checked"/>
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
        <div class="Results">
            <span class="labelGpoleft">Listado de Claves</span>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>