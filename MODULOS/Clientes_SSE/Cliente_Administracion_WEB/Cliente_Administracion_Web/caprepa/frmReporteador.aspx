<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmReporteador.aspx.cs" Inherits="caprepa_frmReporteador" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporteador caprepa</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" /> 
	<link rel="shortcut icon" href="../images/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="../css/m-styles.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/GeneralForm.css" />
    <%  if (DtGeneral.Empresa == "001" || DtGeneral.Empresa == "003")
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

    <style type="text/css">
        body
        {
            -webkit-user-select: none;
            -moz-user-select: none;
            -khtml-user-select: none;
            -ms-user-select:none;
        }
        
        .container
        {
            width: 1006px;
            position: relative;
            height: 574px;
        }
        .select
        {
            position: relative;
            float: left;
            width: 50%;
            height: 100%;
        }
        
        #columnsAndOrder
        {
            width: 330px;
            overflow: hidden;
        }
        
        #columns
        {
            width: 100%;
            background-color: #eee;
            overflow: hidden;
            height: 100%;
            margin-bottom: 8px;
        }
        
        #top
        {
            width: 100%;
            background-color: #eee;
            overflow: hidden;
            height: 62px;
            margin-bottom: 8px;
        }
        
        #txtTop
        {
            width: 316px;
        }
        
        #Order
        {
            width: 100%;
            background-color: #eee;
            overflow: hidden;
            height: 115px;
        }
        
        #Order ul
        {
            height: 87px;
        }
        
        #Order .m-radio.inline
        {
            margin-top: 0px;
            padding-top: 0px;
        }
        
        #criterions
        {
            margin-left: 8px;
            width: 330px;
            height: 210px;
        }
        
        h2
        {
            position: relative;
            width: 100%;
            line-height: 20px;
            font-size: 20px;
            display: inline-block;
            background: #bbb;
            margin: 0px;
            padding: 4px;
        }
        
        ul
        {
            list-style: none;
            height: 476px;
            padding: 0px;
            margin: 0px;
            overflow: hidden;
            overflow-y: auto;
        }
        
        li
        {
            height: 25px;
            padding: 3px 8px;
            list-style: none;
            cursor: pointer;
        }
        
        li:nth-child(odd) 
        {
            color:#fff;
            /*background-color: #5e8fb7;*/
            background-color: #488538;
        }
        
        li:nth-child(even) 
        {
            /*color: #255482;
            background: #badff7;*/
            color: #205612;
            background-color: #91C284;
        }
        
        #Farmacias
        {
            position: relative;
            width: 100%;
            height: 62px;
            background-color: #eee;
            overflow: hidden;
            margin-bottom: 8px;
        }
        
        #cboFarmacia
        {
            width: 100%;
        }
        
        #columnsSelected
        {
            position: relative;
            background-color: #eee;
            width: 100%;
            height: 140px;
            margin-bottom: 8px;
            overflow: hidden;
        }
        
        #columnsSelected ul
        {
            /*height: 112px;*/
            height: 86%;
        }
        
        #columnsSelected ul span{
            float: left;
            cursor: pointer;
            margin-top: 2px;
            margin-right: 4px;
        }
        
        .btn_del {
            float: right;
            padding-right: 4px;
            padding-top: 4px;
            cursor: pointer;
        }
        
        #Fechas
        {
            position: relative;
            background-color: #eee;
            width: 100%;
            height: 70px;
            overflow: hidden;
        }
        
        #Fechas label.m-wrap
        {
            margin-top: 4px;
        }
        
        #dtpFechaInicial,
        #dtpFechaFinal
        {
            display: inline-block;
            width: 80px;
        }
        
        .fechaspan {
            display: inline-block;
            height: 24px;
            min-width: 16px;
            padding: 4px 0px;
            font-size: 14px;
            font-weight: normal;
            line-height: 24px;
            text-align: right;
            width: 40px;
            margin-left: 16px;
        }
        
        #group,
        #sum
        {
            position: relative;
            width: 330px;
            height: 110px;
            background-color: #eee;
            float: left;
            overflow: hidden;
            margin-bottom: 8px;
        }
        
        #sum
        {
            margin-left: 8px;
        }
        
        #group ul,
        #sum ul
        {
            height: 82px;
        }
        
        #Condiciones
        {
            position: relative;
            background-color: #eee;
            width: 100%;
            height: 115px;
            margin-bottom: 8px;
            overflow: hidden;
        }
        
        #Condiciones ul
        {
            height: 87px;
        }
        
        #Condiciones .m-wrap
        {
            border: none;
            max-height: 14px;
        }
        
        #TipoInformacion,
        #TipoReporte
        {
            position: relative;
            background-color: #eee;
            width: 100%;
            height: 62px;
            margin-bottom: 8px;
            overflow: hidden;
        }
        
        #TipoInformacion .m-radio.inline
        {
            margin-left: 24px;
            margin-top: 2px;
        }
        
        #TipoReporte .m-radio.inline
        {
            margin-left: 22px;
            margin-top: 2px;
        }
        
        #block
        {
            position: absolute;
            width: 100%;
            height: 100%;
            padding: 0px;
            margin: 0px;
            background-color: Red;
        }
        
        #filters
        {
            width: 330px;
            height: 210px;
            margin-left: 8px;
        }
        
        #dinamicos
        {
            margin-top: 8px;
            margin-left: 8px;
            width: 668px;
            height: 286px;
        }
    </style>

     <script type="text/javascript">
         if (top.location == this.location) top.location = '../Default.aspx';
         $(function () {
             /*
             $("#dtpFechaInicial").datepicker($.datepicker.regional["es"]);
             $("#dtpFechaInicial").datepicker("option", "dateFormat", "yy-mm-dd");
             $("#dtpFechaInicial").datepicker("option", "changeMonth", "true");
             $("#dtpFechaInicial").datepicker("option", "changeYear", "true");
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
             */

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


             $("#columns li").draggable({
                 appendTo: "body",
                 helper: "clone",
                 start: function (e, ui) {
                     $(ui.helper).addClass("ui-draggable-helper");
                     $(ui.helper).css({ 'background-color': $(this).css('background-color') });
                 }
             });

             $("#columnsSelected, #group, #sum, #Condiciones, #Order").droppable({
                 activeClass: "a",
                 hoverClass: "h",
                 accept: ":not(.ui-sortable-helper)",
                 drop: function (event, ui) {
                     var $divLista = $(this);
                     var $ulLista = $divLista.find('ul');
                     var oLi = '';
                     var Grupo = 0;
                     var Relacion = 0;
                     var bAdd = false;
                     var sMensaje = '';

                     switch ($(this).attr('id')) {
                         case 'columnsSelected':
                             var oData = busqueda(ui.draggable.attr("value"));
                             Grupo = $(this).attr('id');
                             if (oData.EsTotalizable) {
                                 Grupo = 'sum';
                                 Relacion = ui.draggable.attr("value");

                                 oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                 addItem($('#sum').find('ul'), $('#sum'), ui, oLi, false);
                             }
                             else {

                                 Grupo = 'group';
                                 Relacion = ui.draggable.attr("value");

                                 oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                 addItem($('#group').find('ul'), $('#group'), ui, oLi, false);
                             }

                             //Grupo = $(this).attr('id');
                             Relacion = ui.draggable.attr("value");
                             oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                             addItem($ulLista, $divLista, ui, oLi, true);

                             break;
                         case 'group':
                             var oData = busqueda(ui.draggable.attr("value"));
                             if (oData.EsAgrupable) {
                                 Grupo = $(this).attr('id');
                                 Relacion = ui.draggable.attr("value");
                                 oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                 addItem($ulLista, $divLista, ui, oLi, true);

                                 Grupo = 'group';
                                 Relacion = ui.draggable.attr("value");
                                 oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                 addItem($('#columnsSelected').find('ul'), $('#columnsSelected'), ui, oLi, false);
                             }
                             else {
                                 alert("No se puede agrupar el campo " + ui.draggable.text());
                             }
                             break;
                         case 'sum':
                             var oData = busqueda(ui.draggable.attr("value"));
                             if (oData.EsTotalizable) {
                                 Grupo = $(this).attr('id');
                                 Relacion = ui.draggable.attr("value");
                                 oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                 addItem($ulLista, $divLista, ui, oLi, true);

                                 Grupo = 'sum';
                                 Relacion = ui.draggable.attr("value");
                                 oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                 addItem($('#columnsSelected').find('ul'), $('#columnsSelected'), ui, oLi, false);

                             }
                             else {
                                 alert("No se puede sumar el campo " + ui.draggable.text());
                             }
                             break;
                         case 'Condiciones':
                             var oData = busqueda(ui.draggable.attr("value"));
                             if (oData.EsCondicional) {
                                 if (ui.draggable.attr("value") == 'Farmacia') {
                                     $('#cboFarmacia').focus();
                                     alert('Seleccione una farmacia.');
                                 }
                                 else {
                                     Grupo = $(this).attr('id');
                                     Relacion = ui.draggable.attr("value");
                                     oLi = ui.draggable.text() + ': <input type="text" class="m-wrap" id="txt' + Relacion + '" value="" placeholder="Escriba condición"> <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                     addItem($ulLista, $divLista, ui, oLi, true);

                                     Grupo = 'Condiciones';
                                     Relacion = ui.draggable.attr("value");
                                     oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                     addItem($('#columnsSelected').find('ul'), $('#columnsSelected'), ui, oLi, false);

                                     Grupo = 'Condiciones';
                                     Relacion = ui.draggable.attr("value");
                                     oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                     addItem($('#group').find('ul'), $('#group'), ui, oLi, false);
                                 }
                             }
                             else {
                                 alert("No se puede aplicar una condicional al campo " + ui.draggable.text());
                             }
                             break;
                         case 'Order':
                             var oData = busqueda(ui.draggable.attr("value"));
                             Grupo = $(this).attr('id');
                             if (oData.EsTotalizable) {
                                 Grupo = 'sum';
                                 Relacion = ui.draggable.attr("value");

                                 oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                 addItem($('#sum').find('ul'), $('#sum'), ui, oLi, false);

                                 Grupo = 'sum';
                                 Relacion = ui.draggable.attr("value");
                                 oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                 addItem($('#columnsSelected').find('ul'), $('#columnsSelected'), ui, oLi, false);
                             }
                             else {
                                 Grupo = 'Condiciones';
                                 Relacion = ui.draggable.attr("value");
                                 oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                 addItem($('#group').find('ul'), $('#group'), ui, oLi, false);

                                 Grupo = 'Condiciones';
                                 Relacion = ui.draggable.attr("value");
                                 oLi = ui.draggable.text() + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                                 addItem($('#columnsSelected').find('ul'), $('#columnsSelected'), ui, oLi, false);
                             }

                             //Grupo = $(this).attr('id');
                             Relacion = ui.draggable.attr("value");
                             var oCheck =   '<label class="m-radio m-wrap inline">' +
                                                '<input type="radio" class="m-wrap" name="tipo' + Relacion + '" id="rdo' + Relacion + '" value="Asc" checked="checked" /> ' +
                                                'Ascendente' +
                                            '</label>' +
                                            '<label class="m-radio m-wrap inline">' +
                                                '<input type="radio" class="m-wrap" name="tipo' + Relacion + '" id="rdo' + Relacion + '" value="Desc" /> ' +
                                                'Descendente' +
                                            '</label>';
                             oLi = ui.draggable.text() + ' : ' + oCheck + ' <a class="btn_del" rel="' + Grupo + '" rev="' + Relacion + '"><i class="icon-trash"></i></a>';
                             addItem($ulLista, $divLista, ui, oLi, true);

                             break;
                         default:

                     }
                 }
             });

             function addItem($ulLista, $divLista, ui, oLi, bMostrarMensaje) {

                 if (!$ulLista.length) {
                     $("<ul></ul>").appendTo($divLista);
                     $ulLista = $divLista.find('ul');
                 }

                 if ($ulLista.text().indexOf(ui.draggable.text()) === -1) {
                     $("<li></li>").html(oLi).appendTo($ulLista);
                     initDel();

                     $("#columnsSelected ul").sortable();
                     $("#columnsSelected ul").disableSelection();

                     $("#Order ul").sortable();
                     $("#Order ul").disableSelection();

                 } else {
                     if (bMostrarMensaje) {
                         alert('Ya existe ' + ui.draggable.text() + ' en ' + $divLista.find('h2').text());
                     }
                 }
             }

             function initDel() {
                 $(".btn_del").off();
                 $(".btn_del").on('click', function (e) {
                     var $liPadre = $($(this).parents().get(0));
                     var $ulLista = $($liPadre.parents().get(0));
                     $liPadre.remove();
                     if ($ulLista.find('li').length == 0) { $ulLista.remove(); }

                     //var delItem = $('#columnsSelected ul li').find('a[rev="' + $(this).attr('rev') + '"]');

                     var $liPadre = $($('#columnsSelected ul li').find('a[rev="' + $(this).attr('rev') + '"]').parents().get(0));
                     var $ulLista = $($liPadre.parents().get(0));
                     $liPadre.remove();
                     if ($ulLista.find('li').length == 0) { $ulLista.remove(); }

                     var $liPadre = $($('#group ul li').find('a[rev="' + $(this).attr('rev') + '"]').parents().get(0));
                     var $ulLista = $($liPadre.parents().get(0));
                     $liPadre.remove();
                     if ($ulLista.find('li').length == 0) { $ulLista.remove(); }

                     var $liPadre = $($('#sum ul li').find('a[rev="' + $(this).attr('rev') + '"]').parents().get(0));
                     var $ulLista = $($liPadre.parents().get(0));
                     $liPadre.remove();
                     if ($ulLista.find('li').length == 0) { $ulLista.remove(); }

                     var $liPadre = $($('#Condiciones ul li').find('a[rev="' + $(this).attr('rev') + '"]').parents().get(0));
                     var $ulLista = $($liPadre.parents().get(0));
                     $liPadre.remove();
                     if ($ulLista.find('li').length == 0) { $ulLista.remove(); }

                     var $liPadre = $($('#Order ul li').find('a[rev="' + $(this).attr('rev') + '"]').parents().get(0));
                     var $ulLista = $($liPadre.parents().get(0));
                     $liPadre.remove();
                     if ($ulLista.find('li').length == 0) { $ulLista.remove(); }
                 });
             }

             function busqueda(columna) {
                 var aDataInfoGeneral = $.parseJSON($("#configColumn").val());
                 var aDataReturn = $.grep(aDataInfoGeneral.Columnas, function (element, index) {
                     return element.NombreColumna == columna;
                 });
                 return aDataReturn[0];
             }
         });
     </script>
</head>
<body>
    <form id="frmReporteador" runat="server">
    <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <%--<span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>--%>
         <span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>
    </div>
    <div id="container" class="container">
        <div id="columnsAndOrder" class="select">
            <div id="TipoInformacion">
                <h2>Tipo de información</h2>
                <label id="lblFacturado" class="m-radio m-wrap inline" runat="server">
                    <input type="radio" class="m-wrap" name="tipoDeInformacion" id="rdoFacturado" value="1"/>
                    Facturado
                </label>
                <label id="lblNoFacturado" class="m-radio m-wrap inline" runat="server">
                    <input type="radio" class="m-wrap" name="tipoDeInformacion" id="rdoNoFacturado" value="0"/>
                    No Facturado
                </label>
                <label id="lblrdoAmbosTiposDeReporte" class="m-radio m-wrap inline" runat="server">
                    <input type="radio" class="m-wrap" name="tipoDeInformacion" id="rdoAmbosTiposDeReporte" value="2" checked="checked"/>
                    Ambos
                </label>
            </div>
            <div id="columns" runat="server"></div>
        </div>
        <div id="criterions" class="select">
            <div id="TipoReporte">
                <h2>Tipo de reporte</h2>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDeReporte" id="rdoVenta" value="1"/>
                    Venta
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDeReporte" id="rdoVales" value="2"/>
                    Vales Surtídos
                </label>
                <label class="m-radio m-wrap inline">
                    <input type="radio" class="m-wrap" name="tipoDeReporte" id="rdoAmbos" value="0" checked="checked"/>
                    Ambos
                </label>
            </div>
            <div id="columnsSelected">
                <h2>Columnas seleccionadas</h2>
            </div>
        </div>
        <div id="filters" class="select">
            <div id="top">
                <h2>Top</h2>
                <label class="m-wrap">
                    <input type="text" class="m-wrap" id="txtTop" value="" placeholder="Número de resultados" maxlength="6"/>
                </label>
            </div>
            <div id="Farmacias" runat="server"></div>
            <div id="Fechas">
                <h2>Rango de fechas</h2>
                <label class="m-wrap inline">
                    <span class="fechaspan">Inicio :</span>
                    <input type="text" class="m-wrap" id="dtpFechaInicial" value="" placeholder="Seleccione periodo" />
                    <span class="fechaspan">Fin :</span>
                    <input type="text" class="m-wrap" id="dtpFechaFinal" value="" placeholder="Seleccione periodo" />
                </label>
            </div>
        </div>
        <div id="dinamicos" class="select">
            <div id="group">
                <h2>Agrupar por</h2>
            </div>
            <div id="sum">
                <h2>Sumatoria</h2>
            </div>
            <div id="Condiciones">
                <h2>Filtros</h2>
            </div>
            <div id="Order">
                <h2>Ordenación</h2>
            </div>
        </div>
    </div>
    <input type="hidden" id="configColumn" name="__configColumn" value ="" runat="server"/>
    <input type="hidden" id="EVENTTARGET" name="__EVENTTARGET" value ="" />
    <input type="hidden" id="EVENTARGUMENT" name="__EVENTARGUMENT" value ="" />
    </form>
</body>
</html>