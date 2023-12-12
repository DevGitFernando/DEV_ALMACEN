$(document).ready(function () {
    /*
    * VARIABLES GLOBALES
    */
    //status de panel lateral: 1 ON (default), 0 OFF
    var status = 1;
    //selectores
    var iframe = $("#iframe");
    var tip = $("#tip");
    var title = $("#content h2");
    var toggler = $("#toggler");
    var lateral = $("#lateral");
    var content = $("#content");
    var navegate = $("#twNavegador");
    var lateralWidth = lateral.width() + "px";

    //dimensiones disponibles para elementos del panel
    var windowHeight = 0;
    var renderHeight = 0;
    var lateralHeight = 0;
    var togglerHeight = 0;
    var tmplateralWidth = 0;

    /*
    * AL CARGAR EL DOCUMENTO
    */
    calculateDimensions();
    applyDimensions();

    /*
    * SCROLL
    */
    lateral.niceScroll({ touchbehavior: false, cursorcolor: "#009800", cursoropacitymax: 0.6, cursorwidth: 8 });
    iframe.niceScroll({ touchbehavior: false, cursorcolor: "#009800", cursoropacitymax: 0.6, cursorwidth: 8 });

    /*
    * AL CAMBIAR DE TAMAÑO LA VENTANA DEL NAVEGADOR
    */
    $(window).resize(function () {
        calculateDimensions();
        applyDimensions();
    });

    /*
    * AL HACER CLICK EN TOGGLER (PANEL LATERAL)
    */
    toggler.click(clickToggler);

    /*
    * AL SELECCIONAR UNO DE LOS ITEMS DEL ARBOL
    */
    $("#twNavegador a").click(loadItem);

    /*
    * AL SELECCIONAR LOGOUT PARA CERRAR SESIÓN
    */
    $("#Logout").on('click', function () {
        $("#EVENTTARGET").val('Logout');
        $("#EVENTARGUMENT").val('');
        $('#Navegador').submit();
    });

    /*
    * FUNCIONES DE CONTROL DE ELEMENTOS DE INTERFAZ
    */
    // calculo de dimensiones disponibles
    function calculateDimensions() {
        windowHeight = document.documentElement.clientHeight; //alto disponible en ventana del explorador
        renderHeight = (windowHeight - 51 - 40 - 31) + "px";
        togglerHeight = (windowHeight - 51 - 40 - 31) + "px";
        lateralHeight = (windowHeight - 51 - 31) + "px";
        /* ¿De donde salen esos valores a restar? Pues de:
        * 51: #top: 40px de height, 10px de padding-top, y 1px de border-bottom
        * 40: #content h2: 40px de height
        * 31: #footer: 30px de height y 1px de border-top
        */
    }
    // aplicado de dimensiones disponibles
    function applyDimensions() {
        content.css("height", renderHeight);
        lateral.css("height", lateralHeight);
        toggler.css("height", togglerHeight);
        //Efecto resize
        lateral.resizable({
            //maxHeight: lateralHeight,
            maxHeight: lateral.height(),
            maxWidth: 600,
            minHeight: lateral.height(),
            minWidth: 400,
            handles: "e",
            resize: function (event, ui) { content.css("margin-left", lateral.width() + "px"); }
        });

        //Margin auto
        if (status == 1) {
            content.css("margin-left", lateral.width() + "px");
        }
        else {
            content.css("margin-left", "0px");
        }

        //Scroll
        lateral.getNiceScroll().resize().show();
    }
    // control
    function clickToggler() {
        //ocultamos panel si esta mostrandose
        if (status == 1) {
            tmplateralWidth = lateral.width();

            lateral.hide(100);
            content.css("margin-left", "0px");
            toggler.addClass("off");

            status = 0;
            lateral.getNiceScroll().hide();
        }
        //mostramos panel si esta oculto
        else {
            lateral.show(100);
            alert(tmplateralWidth);
            content.css("margin-left", lateralWidth);
            //content.css("margin-left", tmplateralWidth + "px");

            toggler.removeClass("off");

            status = 1;
            lateral.getNiceScroll().onResize();
            lateral.getNiceScroll().show();
        }
    }

    //control de items a cargar en listado lateral
    function loadItem(e) {
        var src = $(this).attr("href");
        var titleMsj = $(this).text();
        var paddingCaja = (content.height() / 2) - 51;
        if (src.indexOf('javascript') != 0) {
            //mostramos iframe, ocultamos consejo (tip) y mostrarmos efecto loading
            $('#Mask').fadeIn(100);
            $('#Caja').css({
                display: 'block',
                'padding-top': paddingCaja
            });
            iframe.css("display", "block");
            tip.css("display", "none");
            //cargamos en iframe el nuevo sitio seleccionado
            iframe.attr("src", src);

            //Mostramos mensaje de cargando
            title.html("Cargando  &raquo; <i>" + $(this).text() + "</i>");
            //Efecto loading en el iframe
            iframe.load(function mostrarCarga() {
                $('#Caja').fadeOut(100);
                $('#Mask').fadeOut(100);
                //Sustituimos el mensaje de cargando
                title.html("Visualizando &raquo; <i>" + titleMsj + "</i>");
                iframe.getNiceScroll().resize();
            });
            //cancelamos el comportamiento normal, que nos llevaria a la web seleccionada, solo queremos cargarle en el iframe
            return false;
        }
    }

});