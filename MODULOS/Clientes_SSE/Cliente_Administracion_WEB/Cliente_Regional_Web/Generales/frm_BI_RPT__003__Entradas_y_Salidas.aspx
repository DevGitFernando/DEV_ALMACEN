<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_BI_RPT__003__Entradas_y_Salidas.aspx.cs" Inherits="Generales_frm_BI_RPT__003__Entradas_y_Salidas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="https://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Entradas_y_Salidas</title>
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
            width: 894px;
        }
        .Results {
            width: 780px;
            height: 150px;
            position: relative;
            padding: 0px;
        }
        .container {
            width: 783px;
            height: auto;
            padding-bottom: 8px;
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
            margin-top: 28px;
        }
        #toolbar table tr:nth-child(2) td:nth-child(1)
        {
            display: none;
        }
        #cboMovimiento
        {
            width: 397px;
        }
        
        
        #Parametros {
            padding-top: 16px;
            margin-bottom: 8px;
            padding-bottom: 0px;
            width: 764px;
        }
        
        
        #Procedencia
        {
            float:right;
         }
        .Semaforizacion
        {
            height:25px;
            margin-top:5px !important;
        }
        
        
        .select.m-wrap {
            height: 25px;
            line-height: 25px;
            padding: 0px;
            margin-top: 6px;
            border: none;
            color: #74809d;
        }
        
        input[type="text"].m-wrap {
            width: 254px;
        }
        
        #cboSemaforizacion
        {
            width: 268px;
            height: 33.56px;
            margin-top: 0px;
            padding: 0px 8px;
        }
        
        .Semaforizacion
        {
            height:25px;
            margin-top:5px !important;
        }
        
        #txtProcedencia {
            height: auto;
        }
        
        #Entrada{
        
            padding-top: 40px;
            margin-bottom: 16px;
            padding-bottom: 0px;
            width: 764px;
            min-height:50px;
        }
        .fLeft
        {
            float: left;
            margin-right: 35px;
            width: 100px;
            height: 31px;
            line-height: 31px;
        }
        
        .mTop
        {
            Width: 20px;
            margin: 10px;
            height: 20px;
            margin-left: -20px;
            margin-top: 6px;
        }
        
        .INumber
        {
            width:280px !important;
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
            $("#dtpFechaInicial").datepicker("setDate", "-1m");

            $("#dtpFechaFinal").datepicker({
                dateFormat: "yy-mm-dd",
                changeMonth: true,
                changeYear: true,
                numberOfMonths: 1,
                maxDate: "d",
                hideIfNoPrevNext: true
            });
            $("#dtpFechaFinal").datepicker("setDate", "m");

            $("#txtTop").keydown(function (event) {
                if (event.shiftKey) {
                    event.preventDefault();
                }

                if (event.keyCode == 46 || event.keyCode == 8) {
                }
                else {
                    if (event.keyCode < 95) {
                        if (event.keyCode < 48 || event.keyCode > 57) {
                            event.preventDefault();
                        }
                    }
                    else {
                        if (event.keyCode < 96 || event.keyCode > 105) {
                            event.preventDefault();
                        }
                    }
                }
            });


            $("#dtpFechaInicial").datepicker("setDate", "-1m");
            $("#dtpFechaInicial").keydown(function (e) {
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="frm_BI_RPT__003__Entradas_y_Salidas" runat="server">
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
                <div id="Combos" class="Combo m-input-prepend" runat="server"></div>
            </div>
            <div id="Fechas" class="Groups">
                <span class="labelGpoleft">Periodo de revisión</span>
                <div class="Textbox Unique">
                    <label class="m-wrap inline">
                        <span class="fechaspan">Inicio:</span>
                        <input type="text" class="m-wrap" id="dtpFechaInicial" value="" placeholder="Seleccione periodo" />
                    </label>
                </div>
                <div class="Textbox Unique">
                    <label class="m-wrap inline">
                        <span class="add-on">Fin :</span>
                        <input type="text" class="m-wrap" id="dtpFechaFinal" value="" placeholder="Seleccione periodo" />
                    </label>
                </div>
           </div>
           <div id="Parametros" class="Groups">
                <span class="labelGpoleft">Parámetros</span>

                 <div class="m-input-prepend">
                    <label class="m-wrap">
                        <span class="add-on Semaforizacion">Semaforización :</span>
                        <select id="cboSemaforizacion" class="m-wrap">
                            <option value="0" selected="selected">Todos</option>
                            <option value="1">Próximos a caducar</option>
                            <option value="2">Mediana Caducicad</option>
                            <option value="3">Larga Caducidad</option>
                        </select>
                    </label>
                </div>

                <div class="m-input-prepend">
                    <label class="m-wrap">
                        <span class="add-on">Procedencia:</span>
                        <input type="text" class="m-wrap" id="txtProcedencia" value="" placeholder="Procedencía" maxlength="100" />
                    </label>
                </div>

                <div class="clear"></div>
                <div class="m-input-prepend">
                    <label class="m-wrap">
                        <span class="add-on">Clave SSA :</span>
                        <input type="text" class="m-wrap" id="txtClaveSSA" value="" placeholder="Clave SSA" maxlength="100" />
                    </label>
                </div>
                <div class="m-input-prepend">
                    <label class="m-wrap">
                        <span class="add-on">Fuente :</span>
                        <input type="text" class="m-wrap " id="txtFuenteFinanciamiento" value="" placeholder="Fuente de Financiamiento" maxlength="1000" />
                    </label>
                </div>
                <div class="clear"></div>
          <%-- </div>


           <div id="Entrada" class="Groups">
             <span class="labelGpoleft">Movimientos</span>
             --%>
             <label class="m-checkbox m-wrap">
                <span class="add-on fLeft">Validar Entrada:</span>
                <input id="chkValidar_Entradas" class="mTop" type="checkbox" value="">
                
                <input type="number" class="m-wrap INumber" id="txtEntrada_Menor" value="" placeholder="Valor inicial" maxlength="100" />
                <input type="number" class="m-wrap INumber" id="txtEntrada_Mayor" value="" placeholder="Valor final" maxlength="100" />
            </label>
             
            <label class="m-checkbox m-wrap">
                <span class="add-on fLeft">Validar Salida:</span>
                <input id="chkValidar_Salidas" class="mTop" type="checkbox" value="">
                <input type="number" class="m-wrap INumber" id="txtSalida_Menor" value="" placeholder="Valor inicial" maxlength="100" />
                <input type="number" class="m-wrap INumber" id="txtSalida_Mayor" value="" placeholder="Valor final" maxlength="100" />
            </label>
           

           </div>

        </div><!--Container-->

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
