$(document).ready(function () {
    var oTable;
    var oMenu;

    //variables de control
    var menuId = "menu";
    var menu = $("#" + menuId);
    var wrapperId = "lstOrdenes_ListViewTable tbody tr";
    var wrapper = $("#" + wrapperId);
    var sFolio;
    var sFarmaciaRecibe;
    var sFarmacia;

    if (top.location == this.location) {
        top.location = this.location;
    }
    $(function () {
        $.datepicker.setDefaults($.datepicker.regional[""]);

        $("#dtpFechaInicial, #dtpFechaFinal").datepicker($.datepicker.regional["es"]);
        $("#dtpFechaInicial, #dtpFechaFinal").datepicker("option", "dateFormat", "yy-mm-dd");
        $("#dtpFechaInicial, #dtpFechaFinal").datepicker("option", "maxDate", "D");
        $("#dtpFechaInicial, #dtpFechaFinal").datepicker("option", "changeMonth", "true");
        $("#dtpFechaInicial, #dtpFechaFinal").datepicker("option", "changeYear", "true");
    });
    function KeyDownHandler() {
        event.returnValue = false;
        event.cancel = true;
    }
    $(document).bind("contextmenu", function (e) {
        return false;
    });

    $(document).click(function (e) {
        menu.css("display", "none");
    });
    oTable = $('#lstOrdenes_ListViewTable').dataTable(
            {
                "bScrollInfinite": true,
                "sScrollY": "232px", //Con sDom 20px menos 274px | 252
                "sScrollX": "200%",
                "bFilter": false,
                "bSearchable": false,
                "bSort": true,
                "sDom": '<"toolbar">frtip',
                "aoColumnDefs": [
                  { "bVisible": false, "aTargets": [8] }
                ],
                "oLanguage": {
                    "sLengthMenu": "Mostrar _MENU_ registros por pagina",
                    "sZeroRecords": "No se encontro información - verifique su busqueda",
                    "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando del 0 al 0 de 0 registros",
                    "sInfoFiltered": "(filtrado de _MAX_ del total de registros)"
                },
                "bDeferRender": true
            });
    $("div.toolbar").html('<div class="mensaje"> ' +
            			            '<b>Click derecho para descargar o marcar orden de compra como leída.</b>' +
            		            '</div>');
    /* Add events */
    $('#lstOrdenes_ListViewTable tbody tr').live('contextmenu', function (e) {
        oTable.$('tr.row_selected').removeClass('row_selected');
        $(this).addClass('row_selected');
        //}
        var sTitle;
        var nTds = $('td', this);
        sFolio = $(nTds[0]).text();
        sFarmaciaRecibe = $(nTds[2]).text();
        var aData = oTable.fnGetData(this);
        sFarmacia = aData[8];
        var menuId = "menu";
        var menu = $("#" + menuId);

        menu.html('<ul> ' +
            			    '<li id="menu_marcarorden">Marcar como leido el folio ' + sFolio + '</li>' +
                            '<li id="menu_download">Descargar la orden</li>' +
            		    '</ul>');
        menu.css({ 'display': 'block', 'left': e.pageX, 'top': e.pageY });

        return false;
    });

    /* Add a click handler to the rows - this could be used as a callback */
    $("#lstOrdenes_ListViewTable tbody tr").click(function (e) {
        menu.css("display", "none");
        if ($(this).hasClass('row_selected')) {
            $(this).removeClass('row_selected');
        }
        else {
            oTable.$('tr.row_selected').removeClass('row_selected');
            $(this).addClass('row_selected');
        }
    });

    menu.click(function (e) {
        //si la opcion esta desactivado, no pasa nada
        if (e.target.className == "disabled") {
            return false;
        }
        //si esta activada, gestionamos cada una y sus acciones
        else {
            switch (e.target.id) {
                case "menu_marcarorden":
                    document.OrdenesColocadas.__IdFolio.value = sFolio;
                    document.OrdenesColocadas.__IdFarmaciaRecibe.value = sFarmaciaRecibe;
                    __doPostBack("MarcarOrdenLeida", "");
                    break;
                case "menu_download":
                    document.OrdenesColocadas.__IdFolio.value = sFolio;
                    document.OrdenesColocadas.__IdFarmaciaRecibe.value = sFarmaciaRecibe;
                    document.OrdenesColocadas.__IdFarmacia.value = sFarmacia;
                    __doPostBack("GenerarReporte", "");
                    break;
            }
            menu.css("display", "none");
        }
    });

    /* Get the rows which are currently selected */
    function fnGetSelected(oTableLocal) {
        return oTableLocal.$('tr.row_selected');
    }
    //Div Modal
    //seleccionamos todos los a tags con el name=modal
    $('a[name=modal]').click(function (e) {

        var Target = $(this).attr('rev');
        var sFechai = document.getElementById("dtpFechaInicial").value;
        var sFechaf = document.getElementById("dtpFechaFinal").value;
        var sEstado = document.getElementById("cboEstados").value;

        if (Target == "Limpiar") {
            __doPostBack(Target, '');
        }
        if (Target == "Ejecutar") {
            if (sFechai == "" && sFechaf == "" && sEstado == "<< Seleccione >>") {
                alert("Selecciona estado y el rango de fechas a analizar.");
            } else if (sFechai == "" && sFechaf == "") {
                alert("Selecciona el rango de fechas a analizar.");
            } else if (sEstado == "<< Seleccione >>") {
                alert("Selecciona el estado a verificar.");
            } else if (sFechai == "") {
                alert("Selecciona fecha inicial.");
            } else if (sFechaf == "") {
                alert("Selecciona fecha final.");
            } else if (sFechai != "" && sFechaf != "" && sEstado != "<< Seleccione >>") {
                //Llamada Post
                __doPostBack(Target, '');

                //Cancel the link behavior
                e.preventDefault();

                //Obtenemos el A tag
                var id = $(this).attr('href');

                //Enviamos el height y width del screen
                var maskHeight = $(document).height();
                var maskWidth = $(window).width();

                //Enviamos el  heigth y width del div mask para cubrir la ventana
                $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

                //Efecto de transicción
                //$('#mask').fadeIn(1000); //Efecto de 
                $('#mask').fadeTo("fast", 0.3);

                //Enviar el height y width de la ventana
                var winH = $(window).height();
                var winW = $(window).width();

                //Establecemos el popup window en centro
                $(id).css('top', winH / 2 - $(id).height() / 2);
                $(id).css('left', winW / 2 - $(id).width() / 2);

                //Efecto de transicción
                $(id).fadeIn(2000);
            }
        }
        //
    });

    //if close button is clicked
    $('.window .close').click(function (e) {
        document.location.href = "frmOrdenesColocadas.aspx";
        //Cancelamos el link
        e.preventDefault();

        $('#mask').hide();
        $('.window').hide();
    });

    $(window).resize(function () {

        var box = $('#boxes .window');

        //Obtenemos  el height y width del screen
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();

        //Enviar height y width al div mask para cubrir la ventana
        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

        //Obtenemos el height y width de la ventana
        var winH = $(window).height();
        var winW = $(window).width();

        //Posicionar el popup en el centro
        box.css('top', winH / 2 - box.height() / 2);
        box.css('left', winW / 2 - box.width() / 2);
    });
    //
    function __doPostBack(eventTarget, eventArgument) {
        document.OrdenesColocadas.__EVENTTARGET.value = eventTarget;
        document.OrdenesColocadas.__EVENTARGUMENT.value = eventArgument;
        if (eventTarget == "Limpiar") {
            document.location.href = "frmOrdenesColocadas.aspx";
        } else if (eventTarget == "Ejecutar") {
            document.OrdenesColocadas.submit();
        } else if (eventTarget == "MarcarOrdenLeida") {
            document.OrdenesColocadas.submit();
        } else if (eventTarget == "GenerarReporte") {
            document.OrdenesColocadas.submit();
        }
    }
});