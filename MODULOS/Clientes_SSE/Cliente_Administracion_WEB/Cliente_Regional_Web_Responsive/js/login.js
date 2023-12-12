// JavaScript Document
var $usuario = $('#txtUser');
var $pass = $('#txtPassword');
var $btnSign = $('#btn_sign');
var bClient = false;

var login = {
    //Alias: Funcion
    init: init
}

function init() {
    initFg();
    reSize();
    initBtnLogin();
    initUsabilidad();
    
    $usuario.focus();
}

function reSize() {
    $(window).resize(function () {
        reSize();
    });
}

function initFg() {
    if (bClient) {
        $(document).on("contextmenu", function (e) {
            return false;
        });
    }
}

function initUsabilidad() {
    //keypress
    $usuario.off();
    $usuario.on('keypress', function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $pass.focus();
        }
    });

    $pass.off();
    $pass.on('keypress', function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $btnSign.focus();
            $btnSign.click();
        }
    });
}

function initBtnLogin() {
    $btnSign.off();
    $btnSign.click(function (e) {
        if (validarEnviar()) {
            $('#loader').fadeIn();
            if (validarEnviar()) {
                var parametros = { sUsuario: $usuario.val(), sPassword: $pass.val() };
                $.ajax({
                    url: "../DllClienteRegionalWeb/wsGeneral.aspx/Autenticar",
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(parametros),
                    success: Ready,
                    error: Error
                }).done(function (e) {
                    //$('#loader').fadeOut();
                });
                $usuario.focus();
                $pass.val('');
            }
        }
    });
}

function validarEnviar() {
    var bContinuar = true;

    if ($usuario.val() == '') {
        bContinuar = false;
        alert('No ha capturado un usuario para el sistema.');
        $usuario.focus();
    }
    else if ($pass.val() == '') {
        bContinuar = false;
        alert('No ha especificado el password de usuario.');
        $pass.focus();
    }

    return bContinuar
}

function Ready(res) {
    if (res.d == '') {
        $(location).attr('href', '../Default.aspx');
    }
    else {
        $('#loader').fadeOut();
        alert(res.d);
    }
}

function Error(res) {
    $('#loader').fadeOut();
    //var error = JSON.parse(res.responseText).Message;
    //console.log('Solicitud no enviada debido a ' + error);
    alert('No se pudo autenticar, intentelo de nuevo');
}