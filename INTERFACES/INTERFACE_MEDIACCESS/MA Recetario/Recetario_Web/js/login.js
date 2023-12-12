// JavaScript Document
var oCatEstadosFarmacias = {};
var $cboEstado = $('#cboEstado');
var $cboFarmacia = $('#cboFarmacia');
var $usuario = $('#txtUser');
var $pass = $('#txtPassword');

var login = {
    init: init,
    CatEstadosFarmacias: function () {
        return oCatEstadosFarmacias;
    }
}

function init() {
    reSize();
    //GetCatEstadosFarmacias();
    initBtnLogin();
    initUsabilidad();
    $cboEstado.focus();
}

function reSize() {
    $(window).resize(function () {
        reSize();
    });

    $('#content_general').height($('body').outerHeight() - $('header').outerHeight());
}

function GetCatEstadosFarmacias() {
    $.ajax({
        url: '../DllRecetario/ws_General.aspx/getCatEstadosFarmacias',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: {},
        success: function Ready(data) {
            if (data.d != '') {
                oCatEstadosFarmacias = $.parseJSON(data.d);
                oCatEstadosFarmacias = oCatEstadosFarmacias['catEstadosFarmacias'];
            }
        }
    }).done(function () { });
}

function initUsabilidad() {
    //initCboEstado();

    /*$cboFarmacia.off();
    $cboFarmacia.change(function (e) {
        $usuario.focus();
    });*/

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
            $('#btn_sign').focus();
            $('#btn_sign').click();
        }
    });
}

function initCboEstado() {
    $cboEstado.off();
    $cboEstado.change(function () {
        $("option:selected", $cboEstado).each(function () {
            var IdEstado = $(this).val();
            var aDataReturn = $.grep(oCatEstadosFarmacias, function (element, index) {
                return element.IdEstado == IdEstado;
            });

            var sFirstOpt = $cboFarmacia.children(0);
            $cboFarmacia.empty();
            $cboFarmacia.append(sFirstOpt[0].outerHTML);

            //delete items
            var filter = [];
            var sColumn = new Array('IdFarmacia', 'Farmacia');

            $.each(aDataReturn, function (index, element) {
                if ($.inArray(element[sColumn[0]], filter) == -1) {
                    filter.push(element[sColumn[0]], element[sColumn[1]]);
                    $cboFarmacia.append('<option value="' + element[sColumn[0]] + '">' + element[sColumn[0]] + ' -- ' + element[sColumn[1]] + '</option>');
                }
            });

            $cboFarmacia.removeAttr("disabled");
            $cboFarmacia.focus();
        });
    });
}

function initBtnLogin() {
    $('#btn_sign').off();
    $('#btn_sign').click(function (e) {
        if (validarEnviar()) {
            $('#loader').fadeIn();
            if (validarEnviar()) {
                //var parametros = { sEstado: $cboEstado.val(), sUnit: $cboFarmacia.val(), sUser: $usuario.val(), sPass: $pass.val() };
                var parametros = { sUser: $usuario.val(), sPass: $pass.val() };
                $.ajax({
                    url: "../DllRecetario/ws_General.aspx/Autenticar",
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(parametros),
                    success: Ready,
                    error: Error
                }).done(function (e) {
                    $('#loader').fadeOut();
                });
                $usuario.focus();
                $pass.val('');
            }
        }
    });
}

function validarEnviar() {
    var bContinuar = true;

    if ($cboEstado.val() == 0) {
        bContinuar = false;
        alert('No ha seleccionado un Estado, verifique.');
        $cboEstado.focus();
    }
    else if ($cboFarmacia.val() == 0) {
        bContinuar = false;
        alert('No ha seleccionado una clínica, verifique.');
        $cboFarmacia.focus();
    }
    else if ($usuario.val() == '') {
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
        alert(res.d);
    }
}

function Error(res) {
    $('#loader').fadeOut();
    //var error = JSON.parse(res.responseText).Message;
    //console.log('Solicitud no enviada debido a ' + error);
    alert('No se pudo autenticar, intentelo de nuevo');
}