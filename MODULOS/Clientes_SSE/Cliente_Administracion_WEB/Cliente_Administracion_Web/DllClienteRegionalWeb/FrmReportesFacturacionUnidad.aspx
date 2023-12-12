<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmReportesFacturacionUnidad.aspx.cs" Inherits="DllClienteRegionalWeb_FrmReportesFacturacionUnidad" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">--%>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <title>Reportes de Dispensación</title>
    <meta charset="utf-8"/>
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
            width: 800px;
        }
        .Results {
            width: auto;
            height: 28px;
            position: relative;
            margin-bottom: 8px;
        }
        .container {
            width: 800px;
            padding-bottom: 0px;
        }
        select.m-wrap {
            height: 24px;
            padding: 0px;
            margin-top: 6px
        }
        .Groups select.m-wrap {
            width: 661px;
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
        
        #Cliente label.m-wrap {
            height: 28px;
        }
        #dtpFechaInicial,
        #dtpFechaFinal {
            width: 128px;
            margin-right: 16px;
        }
        #Fechas {
            width: 250px;
            margin: 0px;
            padding-top: 20px;
            padding-bottom: 0px;
            height: 106px;
            min-width: 100px;
        }
        #Resumen {
            width: 509px;
            padding: 8px;
        }
        
        #Resumen td {
            text-align: right;
        }
        #Dispensacion
        {
            width: 250px;
            height: 110px;
            margin-right: 4px;
        }
        #Dispensacion label.m-wrap {
            margin: 16px;
            margin-left: 72px;
        }
        #Insumos
        {
            width: 238px;
            height: 110px;
            margin-right: 4px;
        }
        #Insumos label.m-wrap {
            margin-left: 40px;
        }
        #lblNOSP,
        #lblSP
        {
            margin-left: 72px !important;
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
            text-align: right;
            width: 60px;
            text-shadow: 0 1px 0 #fff;
        }
        #InfoUnidad {
            margin-right: 0px;
            margin-left: auto;
        }
        #ComboEstados, #Cliente, #Programas {
            width: 774px;
            padding-top: 8px;
        }
        #Resumen table {
            padding-top: 8px;
            margin: 0 auto;
        }
        #txtFarmacia, #lblFarmacia,
        #txtCte, #lblCte,
        #txtSubCte, #lblSubCte,
        #lblCte, #lblSubCte,
        #lblPrograma, #lblSubPrograma,
        #txtPrograma, #txtSubPrograma {
            width: 62px;
            height: 20px;
            padding: 0px;
            margin-top: 6px;
            text-align: center;
        }
        #lblFarmacia,
        #lblCte, #lblSubCte,
        #lblPrograma, #lblSubPrograma {
            margin-left: 8px;
            text-align: left;
            width: 580px;
            padding-left: 8px;
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
        #cboReporte
        {
            width: 782px;
        }
        
        .first
        {
            margin-top: 8px !important;
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
        #MsjPro,
        #MsjSubPro
        {
            display: none;
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
                 numberOfMonths: 1,
                 maxDate: "d",
                 onClose: function (selectedDate) {
                     var aDate = selectedDate.split('-');
                     var atmpDate = new Date(aDate[2], aDate[1], 0);
                     console.log(atmpDate.getDate());
                     console.log($("#dtpFechaFinal").datepicker("option", "maxDate"));
                     if (aDate[1] == 12) {
                         aDate[0] = parseInt(aDate[0]) + 1;
                         aDate[1] = "01";
                     }
                     else {
                         var imonth = parseInt(aDate[1]) + 1;
                         if (imonth < 9) {
                             aDate[1] = '0' + imonth;
                         }
                         else {
                             aDate[1] = imonth;
                         }

                     }
                     var nextDate = aDate[0] + '-' + aDate[1] + '-' + aDate[2];
                     $("#dtpFechaFinal").datepicker("option", "maxDate", nextDate);
                 }
             });
             $("#dtpFechaInicial").datepicker("setDate", "-1m");

             $("#dtpFechaFinal").datepicker({
                 dateFormat: "yy-mm-dd",
                 changeMonth: true,
                 numberOfMonths: 1,
                 maxDate: "d"
             });
             $("#dtpFechaFinal").datepicker("setDate", "m");
         });

    </script>
</head>
<body>
    <div id="Mask">
        <div id="Caja">
            <div id="MsjRpt" class="helpcontent" runat="server">Ayuda</div>
            <div id="MsjPro" class="helpcontent" runat="server">Ayuda</div>
            <div id="MsjSubPro" class="helpcontent" runat="server">Ayuda</div>
            <a id="close" style="display: inline;" title="Cerrar"></a>
            <div id="btnAdd" runat="server"><i class="icon-plus icon-white"></i>Agregar</div>
        </div>
    </div>
    <div class="Mask">
        <div class="caja">
            <div id="nvahelp">
                <span id="bntExportarCuadro" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>
            </div>
            <div id="Div1" class="helpcontent" runat="server"><span>Cargando información, espere un momento...</span></div>
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
    <form id="FrmReportesFacturacionUnidad" runat="server">
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
            <div class="Groups">
                <span class="labelGpoleft">Parametros de Cliente</span>
                <div id="Cliente" class="Combo m-input-prepend" runat="server"></div>
            </div>
        </div>
        <div class="Controls cont">
            <div id="Insumos" class="Groups">
                <span class="labelGpoleft">Tipo de Insumo</span>
                <label class="m-radio m-wrap first">
                    <input type="radio" class="m-wrap" name="tipoInsumo" id="rdoInsumosAmbos" value="0" checked="checked"/>
                    Ambos
                </label>
                <label class="m-radio m-wrap">
                    <input type="radio" class="m-wrap" name="tipoInsumo" id="rdoInsumosMedicamento" value="1"/>
                    Medicamento
                </label>
                <label id="lblSP" class="m-radio m-wrap">
                    <input type="radio" class="m-wrap" name="Medicamento" id="rdoInsumoMedicamentoSP" value="1" runat="server"/>
                    Seguro Popular
                </label>
                <label id="lblNOSP" class="m-radio m-wrap">
                    <input type="radio" class="m-wrap" name="Medicamento" id="rdoInsumoMedicamentoNOSP" value="2" runat="server"/>
                    No Seguro Popular
                </label>
                <label class="m-radio m-wrap">
                    <input type="radio" class="m-wrap" name="tipoInsumo" id="rdoInsumoMatCuracion" value="2"/>
                    Material de curación
                </label>
            </div>
            <div id="Dispensacion" class="Groups">
                <span class="labelGpoleft">Tipo de Dispensacion</span>
                <label class="m-radio m-wrap">
                    <input type="radio" class="m-wrap" name="tipoDispensacion" id="rdoTpDispAmbos" value="0" checked="checked"/>
                    Ambos
                </label>
                <label class="m-radio m-wrap">
                    <input type="radio" class="m-wrap" name="tipoDispensacion" id="rdoTpDispVenta" value="1"/>
                    Venta
                </label>
                <label class="m-radio m-wrap">
                    <input type="radio" class="m-wrap" name="tipoDispensacion" id="rdoTpDispConsignacion" value="2"/>
                    Consignación
                </label>
            </div>
            <div id="Fechas" class="Groups">
                <span class="labelGpoleft">Rango de fechas</span>
                <div class="Textbox Unique">
                    <label class="m-wrap inline first">
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
            <span class="labelGpoleft">Reporte para Impresión</span>
            <select id="cboReporte" class="m-wrap" runat="server" disabled="disabled">
                <option value="0">&lt;&lt;Seleccione un reporte para exportar&gt;&gt;</option>
                <option value="CteUni_Admon_Validacion">Detallado de Dispensación (Validación)</option>
                <option value="CteUni_Admon_DiagnosticosDetallado">Incidencias Epidemiológicas</option>
                <option value="CteUni_Admon_MedicosDetallado">Detallado Por Medico</option>
                <option value="CteUni_Admon_CostoPorMedico">Costo Por Medico</option>
                <option value="CteUni_Admon_PacientesDet">Detallado Por Paciente</option>
                <option value="CteUni_Admon_CostoPorPaciente">Costo Por Paciente</option>
            </select>
        </div>
    </div>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>
