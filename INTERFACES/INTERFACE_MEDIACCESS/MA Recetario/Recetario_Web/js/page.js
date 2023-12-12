// JavaScript Document
var $iContent = $('#iContent');
var $btnMenuOpciones = $('#btnMenu');
var $MenuOpciones = $('#menupanel');
var $btnMenuUsuario = $('#btnMenuUser');
var $MenuUsuario = $('#menuUser');
var $btnLogout = $('#btnLogout');
var $home = $('#btnHome');
var $navContainer = $('#content_iconos');
var bClient = false;

var oItemActive = new Object();

var page = {
    init: init,
    Client: function () { return bClient; }
}

function init() {
    initWindow();
    reSize();
    initMenuOpciones();
    initMenuUsuario();
    initItemActive();
    Logout();

    $('#nameUser').html($('#nameUser').html().toLowerCase());
    //BuscarAfiliado('', 'mario', 'muci', ''); 
}

function initWindow() {
    if (window.history) {
        function noBack() { window.history.forward() }
        noBack();
        window.onload = noBack;
        window.onpageshow = function (evt) { if (evt.persisted) noBack() }
        window.onunload = function () { void (0) }
    }

}

function reSize() {
    $(window).resize(function () {
        reSize();
    });

    $('.icontent').height($('body').outerHeight() - $('header').outerHeight());
}

function initItemActive() {
    oItemActive.src = '#tblHome';
    oItemActive.id = '0';
    oItemActive.name = 'Bienvenido';
    oItemActive.client = bClient;
    oItemActive.container = $iContent;
}

function initMenuOpciones() {
    $btnMenuOpciones.off();
    $btnMenuOpciones.click(function (e) {
        if ($MenuOpciones.hasClass('active')) {
            $MenuOpciones.animate({ left: -$MenuOpciones.outerWidth() }, 300);
            $MenuOpciones.removeClass('active');
        }
        else {
            $MenuOpciones.animate({ left: 0 }, 300);
            $MenuOpciones.addClass('active');
        }
    });

    $('#twNavegador').off();
    $('#twNavegador').on('click', 'a', function (e) {
        e.preventDefault();
        var itemCfg = $(this);
        var itemVal = itemCfg.attr('href').toString();
        if (itemVal.indexOf('javascript') != 0) {
            var aVal = itemVal.split('%7C');
            //var paddingCaja = ($container.height() / 2) - 100; // El -100 por el tamaño de la Caja 200/2 = 100

            //mostramos iframe, ocultamos consejo (#Title) y mostrarmos efecto loading
            //$('#navBarra').width('100%');
            //$('#Loading').fadeIn(100);
            //AjaxLoadingText('');
            //$('#TextTitle').html(itemCfg.text());
            //$('#Mask').fadeIn(100);
            /*$('#Caja').css({
            display: 'block',
            'padding-top': paddingCaja
            });*/

            $iContent.attr("src", aVal[0]);
            $iContent.load(function mostrarCarga(e) {
                //General.init($('#iContent'), aVal[1], bOption);
                forms.init($iContent, aVal[1], itemCfg.text(), bClient);
            });

            //New 
            oItemActive.src = aVal[0];
            oItemActive.id = aVal[1];
            oItemActive.name = itemCfg.text();

            $btnMenuOpciones.click();
            /*closeTOC();
            updateTOC();*/
            return false;
        }
    });

    // show home
    $home.off();
    $home.on('click', function (e) {
        $iContent.attr('src', '');
        $iContent.load(function mostrarCarga(e) {
            $navContainer.html('');
        });
        
    });
}

function initMenuUsuario() {
    $btnMenuUsuario.off();
    $btnMenuUsuario.click(function (e) {
        if ($MenuUsuario.is(':visible')) {
            $MenuUsuario.fadeOut();
        }
        else {
            $MenuUsuario.fadeIn();
        }
    });
}

function Logout() {
    $btnLogout.off();
    $btnLogout.click(function (e) {
        $("#EVENTTARGET").val('Logout');
        $("#EVENTARGUMENT").val('');
        $('#Main').submit();
    });
}

function Error(res) {
    var error = JSON.parse(res.responseText).Message;
    console.log('Solicitud no enviada debido a ' + error);
    alert('No se pudo procesar su solicitud, intentelo de nuevo');
}

function DevError(XMLHttpRequest, textStatus, errorThrown) {
    console.log(XMLHttpRequest);
    console.log(textStatus);
    console.log('Un error ocurrio durante la petición: ' + errorThrown);
}