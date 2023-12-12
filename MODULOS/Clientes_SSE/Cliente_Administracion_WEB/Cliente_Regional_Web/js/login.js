//Variables
var btnLogin = $('#btnSign');
var User = $('#txtUser');
var Pass = $('#txtPass');
var contMsj = $('#message');
var loading = $('#loader');
var urlMain = "../Default.aspx";

var login = {
    //Alias: Función
    init: init
}

function init() {
    btnLogin.off();
    btnLogin.on('click', function (e) {
        e.preventDefault();

        if (validar()) {
            loading.fadeIn();
            contMsj.fadeOut();

            var parametros = {
                sUsuario: User.val(), sPassword: Pass.val()
            };
            $.ajax({
                url: "../DllClienteRegionalWeb/wsGeneral.aspx/Autenticar",
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(parametros),
                success: Ready,
                error: Error
            }).done(function (e) {
                //loading.fadeOut();
            });

            User.focus();
            Pass.val('');
        }
    });

    initUsabilidad();
    User.focus();
}

function initUsabilidad() {
    //keypress
    User.off();
    User.on('keypress', function (event) {
        if (event.which == 13) {
            event.preventDefault();
            Pass.focus();
        }
    });

    Pass.off();
    Pass.on('keypress', function (event) {
        if (event.which == 13) {
            event.preventDefault();
            btnLogin.focus();
            btnLogin.click();
        }
    });
}

function validar() {

    var bContinuar = true;
    if (User.val() == '') {
        bContinuar = false;
        contMsj.html('No ha capturado un usuario para el sistema').fadeIn();
        User.focus();
    }
    else if (Pass.val() == '') {
        bContinuar = false;
        contMsj.html('No ha especificado el password de usuario').fadeIn();
        Pass.focus();
    }

    return bContinuar;
}

function Ready(res) {
    if (res.d == '') {
        $(location).attr('href', urlMain);
    }
    else {
        loading.fadeOut();
        contMsj.html(res.d).fadeIn();
    }
}

function Error(res) {
    loading.fadeOut();
    //var error = JSON.parse(res.responseText).Message;
    //console.log('Solicitud no enviada debido a ' + error);
    contMsj.html('No se pudo autenticar, intentelo de nuevo');
}