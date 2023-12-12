var $FormMain = $('#frmMain');
var $loader = $('#loader');
var $iContent = $('#iContent');
var $___iContent = '';
var $btnMenuOpciones = $('#TopL');
var $MenuOpciones = $('#nav');
var $btnLogout = $('#logout');
var $home = $('#btnHome');
var $btnChangePass = $('#ChangePass');
var $btnConfig = $('#btnConfig');
var $navContainer = $('#content_iconos');
var $cookieCountdown = $('#cookieCountdown');
var aDataInfoGeneral = {};
var bClient = false;
var initUser = false;

var oInfoItem = new Object();

oInfoItem.Id = '0';
oInfoItem.Name = 'Home';
oInfoItem.src = 'Home';

var main = {
    //Alias: Función
    init: init,
    Empresa: Empresa,
    Estado: Estado,
    filterInfo: filterInfo,
    FiltroUnidad: FiltroUnidad,
    FiltroJuris: FiltroJuris,
    ServidorBI: ServidorBI,
    reloadFrame: reloadFrame
}

function init(){
    window.onload = function () {
        // Carga completa
        initCatalogo();

        $(window).resize(function () {
            reSize();
        });

        $('#nav').resizable({
            handles: 'e, w',
            //animate: true,
            //animateDuration: 'fast',
            //animateEasing: 'easeOutBounce',
            maxWidth: 756,
            minWidth: 252,
            helper: 'ui-resizable-helper',
            stop: function (event, ui) {
                $("#TopL").width(ui.size.width);
            }
        });
    }
}

function initEvents() {
    $btnMenuOpciones.off();
    $btnMenuOpciones.click(function (e) {
        if ($MenuOpciones.is(':visible')) {
            $MenuOpciones.css({ 'display': 'none' });
            $("#wrapperPage").css({ 'margin': 0 });

            $('#tgtMenu').width('80px');
            $btnMenuOpciones.width('80px');
            $("#logoIcon").width('30px');
        }
        else {
            $MenuOpciones.css({ 'display': 'block' });
            $("#wrapperPage").css({ 'marginLeft': $MenuOpciones.width() });

            $("#TopL").width($MenuOpciones.width());
        }
    });

    $home.off();
    $home.on('click', function (e) {
        $iContent.attr('src', '');
        $iContent.off();
        oInfoItem.Id = '0';
        oInfoItem.Name = 'Home';
        oInfoItem.src = 'Home';
        $('.contDinamic').fadeOut();
    });

    $btnChangePass.off();
    $btnChangePass.on('click', function (e) {
        $('#password').fadeIn();
        initChangePass();

        $('#closeM').off();
        $('#closeM').on('click', function (e) {
            $('#password').fadeOut();
        });
    });

    //btnConfig
    $btnConfig.off();
    $btnConfig.on('click', function (e) {
        if ($('#Config').is(':hidden')) {
            $('#Config').fadeIn(500);

            $('#twOpciones').off();
            $('#twOpciones').on('click', 'a', function (e) {
                var item = $(this);
                var itemUrl = item.attr('href');
                if (itemUrl.indexOf('javascript') != 0) {
                    var itemTarget = item.attr('target');

                    $('#iOptions').attr("src", itemUrl);
                    $('#iOptions').off();
                    $('#iOptions').on('load', function (e) {

                        initPermisos(itemTarget, $('#iOptions'));
                    });

                    return false;
                }
            });

            $('#closeConfig').off();
            $('#closeConfig').on('click', function () {
                $('#Config').fadeOut(500);
                $('#iOptions').attr('src', '');
            });
        }
    });

    $('#twNavegador').off();
    $('#twNavegador').on('click', 'a', function (e) {
        var item = $(this);
        var itemUrl = item.attr('href');
        if (itemUrl.indexOf('javascript') != 0) {
            var itemTarget = item.attr('target');

            $iContent.attr("src", itemUrl);
            $iContent.off();
            $iContent.on('load', function (e) {
                $___iContent = $iContent.contents();

                oInfoItem.Id = itemTarget;
                oInfoItem.Name = item.html();
                oInfoItem.src = itemUrl;

                loadSrc();
            });

            return false;
        }
    });

    //cookie CountDown
    //$cookieCountdown.attr('data-date', '2016-11-24 10:29:16');
    $cookieCountdown.attr('data-date', aDataInfoGeneral.ExpiracionCookie);
    
    $cookieCountdown.off();
    $cookieCountdown.TimeCircles({
        total_duration: 'Auto',
        count_past_zero: false,
        time: {
            Days: {
                text: 'Días',
                color: '#FFCC66',
                show: false
            },
            Hours: {
                text: 'Horas',
                color: '#99CCFF',
                show: true
            },
            Minutes: {
                text: 'Minutos',
                color: '#BBFFBB',
                show: true
            },
            Seconds: {
                text: 'Segundos',
                color: '#FF9999',
                show: true
            }
        }
    }).addListener(cookieCountdownComplete);

    Logout();
    reSize();
}

function cookieCountdownComplete(unit, value, total) {
    if (total <= 0) {
        //$(this).fadeOut('slow').replaceWith("Sesión expirada");
        $loader.find('.msjInfoLoader').html('Sesión expirada, inicie nuevamente. <a id="btnCookieExpired" href="#">Dar clic aquí para continuar</a>');
        $loader.fadeIn();

        $('#btnCookieExpired').off();
        $('#btnCookieExpired').on('click', function (e) {
            $btnLogout.click();
        });
    }
}

function initPermisos(id, view) {
    switch (id) {
        case '2':
            addUser(view);
            break;
        case '4':
            initAuditor(view);
            break;
        default:

    }
}

function initAuditor(view) {
    var Nuevo = view.contents().find('#New');
    var Ejecutar = view.contents().find('#Exec');
    var Exportar = view.contents().find('#Print');

    Nuevo.off();
    Nuevo.on('click', function () {
        General.clearToastMsj();
        view.attr('src', view.attr('src'));
    });

    Ejecutar.off();
    Ejecutar.on('click', function () {
        General.clearToastMsj();
        General.showToastMsj('Procesando informaci&oacute;n, espere un momento', true, 'info', 10000, 'bottom-full-width');
        view.contents().find('.container input').attr('disabled', 'disabled');
        GetMovtosAuditor();
        $(this).off();
    });

    Exportar.off();
    Exportar.on('click', function (e) {
        if (view.contents().find('#tableAuditorMovimientos').length > 0 && oTable.fnGetData(0) != null) {
            view.contents().find("#EVENTTARGET").val('Download');
            view.contents().find("#EVENTARGUMENT").val(view.contents().find('#dtpFechaInicial').val() + ',' + view.contents().find('#dtpFechaFinal').val());
            view.contents().find('#frmAuditor_Movimientos').submit();
        } else {
            General.showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', false, 'info', 10000, 'bottom-full-width');
        }
    });
}

function GetMovtosAuditor() {
    var parametros = {
        dtpFechaInicial: $('#iOptions').contents().find('#dtpFechaInicial').val(),
        dtpFechaFinal: $('#iOptions').contents().find('#dtpFechaFinal').val()
    };

    $.ajax({
        url: 'DllClienteRegionalWeb/wsGeneral.aspx/GetMovtosAuditor',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parametros),
        success: function Ready(res) {
            General.clearToastMsj();
            var jsonResult = $.parseJSON(res.d);
            $('#iOptions').contents().find('.Results').append(jsonResult['Tabla']);
            oTable = $('#iOptions').contents().find('#tableAuditorMovimientos').dataTable(
                    {
                        'bScrollInfinite': true,
                        'sScrollY': '272px', //76 by f on sDom / 48 btn / 4 border
                        'sScrollX': '100%',
                        'bFilter': true,
                        'bSearchable': false,
                        'bSort': false,
                        'sDom': 't',
                        'iDisplayLength': 999999,
                        'aoColumnDefs': [
                            { 'sClass': 'center', 'aTargets': [0] }
                        ],
                        'oLanguage': {
                            'sLengthMenu': 'Mostrar _MENU_ registros por pagina.',
                            'sZeroRecords': 'No hay movimientos registrados.',
                            'sInfo': 'Mostrando desde _START_ hasta _END_ de _TOTAL_ movimientos.',
                            'sInfoEmpty': 'Mostrando desde 0 hasta 0 de 0 movimientos.',
                            'sInfoFiltered': '(filtrado de _MAX_ movimientos en total).',
                            'sLoadingRecords': 'Cargando...',
                            'sProcessing': 'Procesando...',
                            'sSearch': 'B&uacute;squeda'
                        },
                        'bDeferRender': true
                    });
            if (jsonResult['MostrarResultado']) { }
            else { }
        },
        error: function errorcall(res) {
            General.showToastMsj('El auditor no pudo obtener movimientos, int&eacute;ntelo nuevamente.', false, 'warning', 10000, 'bottom-right');
        }
    }).done(function () { });

}

function addUser(view) {
    var Nuevo = view.contents().find('#New');
    var Guardar = view.contents().find('#Exec');
    var IdUser = view.contents().find('#txtIdUsuario');

    var IdOficinaCentral = '<option value="0001">0001 -- OFICINA CENTRAL</option>';

    $('#iOptions').contents().find("#cboFarmacia option[value='*']").remove();
    if (!$('#iOptions').contents().find("#cboFarmacia option[value='0001']").length) {
        $('#iOptions').contents().find("#cboFarmacia").prepend(IdOficinaCentral);
    }

    Nuevo.off();
    Nuevo.on('click', function () {
        General.clearToastMsj();
        view.attr('src', view.attr('src'));
    });

    Guardar.off();
    Guardar.on('click', function () {
        if (validarAddUser(true)) {
            General.clearToastMsj();
            General.showToastMsj('Procesando informaci&oacute;n, espere un momento', true, 'info', 10000, 'bottom-full-width');
            view.contents().find('.container input').attr('disabled', 'disabled');
            view.contents().find('.container select').attr('disabled', 'disabled');
            ManageUser(1);
            $(this).off();
        }
    });

    view.contents().find('.container input').attr('disabled', 'disabled');
    //view.contents().find('.container select').attr('disabled', 'disabled');
    IdUser.removeAttr('disabled', 'disabled');
    IdUser.focus();
    initInputDigit(IdUser, view);
}

function validarAddUser(bValidarPass) {
    var bReturn = true;
    if (!$('#iOptions').contents().find('#txtIdUsuario').prop('disabled') || $('#iOptions').contents().find('#txtIdUsuario').val() == '') {
        $('#iOptions').contents().find('#txtIdUsuario').focus();
        General.showToastMsj('Escriba el Id del usuario a editar o presionar enter para crear uno nuevo.', false, 'warning', 10000, 'bottom-right');
        bReturn = false;
    }
    else if ($('#iOptions').contents().find('#txtNombre').val() == '') {
        $('#iOptions').contents().find('#txtNombre').focus();
        General.showToastMsj('Escriba el nombre completo del usuario.', false, 'warning', 10000, 'bottom-right');
        bReturn = false;
    }
    else if ($('#iOptions').contents().find('#txtLogin').val() == '') {
        $('#iOptions').contents().find('#txtLogin').focus();
        General.showToastMsj('Escriba el nombre del usuario.', false, 'warning', 10000, 'bottom-right');
        bReturn = false;
    }

    if (bValidarPass) {
        if ($('#iOptions').contents().find('#txtPassword').val() == '') {
            $('#iOptions').contents().find('#txtPassword').focus();
            General.showToastMsj('Escriba el password para el usuario.', false, 'warning', 10000, 'bottom-right');
            bReturn = false;
        }
        else if ($('#iOptions').contents().find('#txtPasswordCon').val() == '') {
            $('#iOptions').contents().find('#txtPasswordCon').focus();
            General.showToastMsj('Confirme el password para el usuario.', false, 'warning', 10000, 'bottom-right');
            bReturn = false;
        }
        else if ($('#iOptions').contents().find('#txtPassword').val() != $('#iOptions').contents().find('#txtPasswordCon').val()) {
            $('#iOptions').contents().find('#txtPassword').focus();
            General.showToastMsj('Los password no concuerdan.', false, 'warning', 10000, 'bottom-right');
            bReturn = false;
        }
    }

    return bReturn;
}

function initInputDigit(ctrl, container) {
    ctrl.off();
    ctrl.on('keydown', function (event) {
        if (event.which == 13) {
            //event.preventDefault();
            if ($(this).val() == '') {
                container.contents().find('.container input').removeAttr('disabled', 'disabled');
                container.contents().find('.container select').removeAttr('disabled', 'disabled');
                container.contents().find('#txtIdUsuario').val('*').attr('disabled', 'disabled');
                container.contents().find('#txtNombre').focus();
            } else if ($(this).val() != '*') {
                General.showToastMsj('Procesando informaci&oacute;n, espere un momento', true, 'info', 10000, 'bottom-full-width');
                container.contents().find('#txtIdUsuario').attr('disabled', 'disabled');
                //getUser($(this).val());
                getUser($(this).val());
            }
            return false;
        }

        switch (event.keyCode) {
            case 8:  // Backspace
            case 37: // Left
            case 39: // Right
            case 46: // Delete
                break;
            case 112: //F1
            case 113: //F2
            case 114: //F3
                getUsers();
                break;
            default:
                var sKey = String.fromCharCode(event.keyCode);
                var iKeyValue = parseInt(sKey);
                if (isNaN(iKeyValue)) {
                    return false;
                }
                break;
        }

    });
}

function getUsers() {
    $('#iOptions').contents().find('#txtIdUsuario');
    $('#iOptions').contents().find('#Mask').fadeIn(100);

    $.ajax({
        url: 'DllClienteRegionalWeb/wsGeneral.aspx/GetUsers',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: {},
        success: function Ready(res) {
            General.clearToastMsj();
            if (res.d != '') {
                $('#iOptions').contents().find('#MsjRpt').html(res.d);

                var oTablePersonal = $('#iOptions').contents().find('#tableUsers').dataTable(
                    {
                        'bScrollInfinite': true,
                        'sScrollY': '172px', //76 by f on sDom / 48 btn / 4 border
                        'sScrollX': '100%',
                        'bFilter': true,
                        'bSearchable': false,
                        'bSort': false,
                        'sDom': 'ft',
                        'iDisplayLength': 999999,
                        'aoColumnDefs': [
                            { 'bVisible': false, 'aTargets': [3] },
                            { 'sClass': 'center', 'aTargets': [0] }
                        ],
                        'oLanguage': {
                            'sLengthMenu': 'Mostrar _MENU_ registros por pagina.',
                            'sZeroRecords': 'No existe usuarios registrados.',
                            'sInfo': 'Mostrando desde _START_ hasta _END_ de _TOTAL_ usuarios.',
                            'sInfoEmpty': 'Mostrando desde 0 hasta 0 de 0 usuarios.',
                            'sInfoFiltered': '(filtrado de _MAX_ usuarios en total).',
                            'sLoadingRecords': 'Cargando...',
                            'sProcessing': 'Procesando...',
                            'sSearch': 'B&uacute;squeda'
                        },
                        'bDeferRender': true
                    });

                oTablePersonal.$('tbody tr').off();
                oTablePersonal.$('tbody tr').click(function (e) {
                    if ($(this).hasClass('row_selected')) {
                        $(this).removeClass('row_selected');
                    }
                    else {
                        oTablePersonal.$('tr.row_selected').removeClass('row_selected');
                        $(this).addClass('row_selected');
                    }
                });

                //oTablePersonal.$('tbody tr').off();
                oTablePersonal.$('tbody tr').dblclick(function (e) {
                    if (!$(this).hasClass('row_selected')) {
                        oTablePersonal.$('tr.row_selected').removeClass('row_selected');
                        $(this).addClass('row_selected');
                    }

                    $('#iOptions').contents().find('#btnAdd').click();
                });

                $('#iOptions').contents().find('#btnAdd').off();
                $('#iOptions').contents().find('#btnAdd').on('click', function (e) {
                    var nTrSelected = oTablePersonal.$('tr.row_selected');
                    var aData = oTablePersonal.fnGetData(nTrSelected.get(0));
                    if (aData != null) {
                        $('#txtIdPersonal').val(aData[0]);
                        $('#iOptions').contents().find('.container input').removeAttr('disabled', 'disabled');
                        $('#iOptions').contents().find("#cboFarmacia option[value='" + aData[0] + "']").attr('selected', 'selected');
                        $('#iOptions').contents().find('#txtIdUsuario').val(aData[1]).attr('disabled', 'disabled');
                        $('#iOptions').contents().find('#txtNombre').val(aData[2]).attr('disabled', 'disabled').attr('title', aData[1]);
                        $('#iOptions').contents().find('#txtLogin').val(aData[3]).attr('disabled', 'disabled');
                        $('#iOptions').contents().find('#txtPassword').focus();
                        if (aData[3] == 'A') {
                            $('#iOptions').contents().find('#statusUser').html('Activo').removeClass().addClass('activo');
                        }
                        else if (aData[3] == 'C') {
                            $('#iOptions').contents().find('#statusUser').html('Cancelado').removeClass().addClass('cancelado');
                        }
                        var cancel = $('#iOptions').contents().find('#Print');

                        cancel.off();
                        cancel.on('click', function (e) {
                            if (jsonResult['Status'] == 'A') {
                                if (validarAddUser(false)) {
                                    General.clearToastMsj();
                                    General.showToastMsj('Procesando informaci&oacute;n, espere un momento', true, 'info', 10000, 'bottom-full-width');
                                    $('#iOptions').contents().find('.container input').attr('disabled', 'disabled');
                                    ManageUser(2);
                                    $(this).off();
                                }
                            }
                        });

                        $('#iOptions').contents().find('#Mask').fadeOut(100);
                    }
                    else {
                        $('#TablePersonal_filter input[type="search"]').focus();
                        showToastMsj('Debe seleccionar un usuario para poder agregar.', false, 'warning', 6000, 'bottom-left');
                    }
                });
            }
            else {
                General.showToastMsj('No existen usuarios registrados.', false, 'warning', 10000, 'bottom-right');
            }
        },
        error: function errorcall(res) {
            General.showToastMsj('No se pudo cargar la ayuda, int&eacute;ntelo nuevamente.', false, 'warning', 10000, 'bottom-right');
        }
    }).done(function () { });

    $('#iOptions').contents().find('#close').off();
    $('#iOptions').contents().find('#close').on('click', function () {
        $('#iOptions').contents().find('#Mask').fadeOut(100);
    });
}

//New User
function ManageUser(Process) {
    var parametros = {
        sIdFarmacia: $('#iOptions').contents().find("#cboFarmacia").val(),
        sIdPersonal: $('#iOptions').contents().find('#txtIdUsuario').val(),
        sNombrePersonal: $('#iOptions').contents().find('#txtNombre').val(),
        sLogin: $('#iOptions').contents().find('#txtLogin').val(),
        sPassword: $('#iOptions').contents().find('#txtPassword').val(),
        iProcess: Process
    };

    $.ajax({
        url: 'DllClienteRegionalWeb/wsGeneral.aspx/ManageUser',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parametros),
        success: function Ready(res) {
            General.clearToastMsj();
            var jsonResult = $.parseJSON(res.d);
            if (jsonResult['bExec']) {
                $('#iOptions').contents().find('#txtIdUsuario').val(jsonResult['Clave']);
                if (jsonResult['Mensaje'].indexOf('cancelada') == -1) {
                    $('#iOptions').contents().find('#statusUser').html('Activo').removeClass().addClass('activo');
                }
                else {
                    $('#iOptions').contents().find('#statusUser').html('Cancelado').removeClass().addClass('cancelado');
                }
                General.showToastMsj(jsonResult['Mensaje'], true, 'success', 10000, 'bottom-right');
                if (Process == 2) {
                    $('#iOptions').contents().find('#txtPassword').removeAttr('disabled', 'disabled');
                    $('#iOptions').contents().find('#txtPasswordCon').removeAttr('disabled', 'disabled');
                }
            }
            else {
                General.showToastMsj('No se pudo registrar el usuario, int&eacute;ntelo nuevamente.', false, 'warning', 10000, 'bottom-right');
            }
        },
        error: function errorcall(res) {
            General.showToastMsj('No se pudo registrar el usuario, int&eacute;ntelo nuevamente.', false, 'warning', 10000, 'bottom-right');
        }
    }).done(function () {
        //Clean input's
        //$('#iOptions').contents().find('.container input').attr('disabled', 'disabled');
        //$('#iOptions').contents().find('#txtIdUsuario').removeAttr('disabled', 'disabled').val('');
        //$('#iOptions').contents().find('#txtIdUsuario').focus();
    });

}

//New User
function getUser() {
    var parametros = {
        sIdFarmacia: $('#iOptions').contents().find("#cboFarmacia").val(),
        sIdPersonal: $('#iOptions').contents().find('#txtIdUsuario').val()
    };

    $.ajax({
        url: 'DllClienteRegionalWeb/wsGeneral.aspx/GetUser',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parametros),
        success: function Ready(res) {
            General.clearToastMsj();
            if (res.d != '') {
                var jsonResultMaster = $.parseJSON(res.d);
                var jsonResult = jsonResultMaster['User'][0];

                $('#iOptions').contents().find('.container input').removeAttr('disabled', 'disabled');
                $('#iOptions').contents().find('#txtIdUsuario').val(jsonResult['IdUsuario']).attr('disabled', 'disabled');
                $('#iOptions').contents().find("#cboFarmacia option[value='" + jsonResult['IdFarmacia'] + "']").attr('selected', 'selected');
                $('#iOptions').contents().find("#cboFarmacia").attr('disabled', 'disabled');
                $('#iOptions').contents().find('#txtNombre').val(jsonResult['Nombre']).attr('disabled', 'disabled').attr('title', jsonResult['Nombre']);
                $('#iOptions').contents().find('#txtLogin').val(jsonResult['Login']).attr('disabled', 'disabled');
                $('#iOptions').contents().find('#txtPassword').focus();
                if (jsonResult['Status'] == 'A') {
                    $('#iOptions').contents().find('#statusUser').html('Activo').removeClass().addClass('activo');
                }
                else if (jsonResult['Status'] == 'C') {
                    $('#iOptions').contents().find('#statusUser').html('Cancelado').removeClass().addClass('cancelado');
                }
                var cancel = $('#iOptions').contents().find('#Print');

                cancel.off();
                cancel.on('click', function (e) {
                    if (jsonResult['Status'] == 'A') {
                        if (validarAddUser(false)) {
                            General.clearToastMsj();
                            General.showToastMsj('Procesando informaci&oacute;n, espere un momento', true, 'info', 10000, 'bottom-full-width');
                            $('#iOptions').contents().find('.container input').attr('disabled', 'disabled');
                            ManageUser(2);
                            $(this).off();
                        }
                    }
                });
            }
            else {
                General.showToastMsj('El id de usuario no existe, int&eacute;ntelo nuevamente.', false, 'warning', 10000, 'bottom-right');
                $('#iOptions').contents().find('#txtIdUsuario').removeAttr('disabled', 'disabled');
                $('#iOptions').contents().find('#txtIdUsuario').focus();
            }
        },
        error: function errorcall(res) {
            General.showToastMsj('No se pudo cargar el usuario, int&eacute;ntelo nuevamente.', false, 'warning', 10000, 'bottom-right');
            $('#iOptions').contents().find('#txtIdUsuario').removeAttr('disabled', 'disabled');
            $('#iOptions').contents().find('#txtIdUsuario').focus();
        }
    }).done(function () {
        //Clean input's
        //$('#iOptions').contents().find('.container input').attr('disabled', 'disabled');
        //$('#iOptions').contents().find('#txtIdUsuario').removeAttr('disabled', 'disabled').val('');
        //$('#iOptions').contents().find('#txtIdUsuario').focus();
    });

}

function initUXPass() {
    //keypress
    $("#txtPasswordAnterior").off();
    $("#txtPasswordAnterior").on('keypress', function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $('#txtPassword').focus();
        }
    });

    $("#txtPassword").off();
    $("#txtPassword").on('keypress', function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $('#txtPasswordCon').focus();
        }
    });

    $("#txtPasswordCon").off();
    $("#txtPasswordCon").on('keypress', function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $('#btnPass').click();
        }
    });

}

function validarPass() {
    var bContinuar = true;
    var sMjsPass = '';
    var ctrlFocus = '';

    General.clearToastMsj();

    if ($('#txtPasswordAnterior').val() == '') {
        ctrlFocus = $('#txtPasswordAnterior');
        sMjsPass = 'Introduzca su contraseña anterior para continuar';
    }
    else if ($('#txtPassword').val() == '') {
        ctrlFocus = $('#txtPassword');
        sMjsPass = 'Introduzca la nueva contraseña anterior para continuar';
    }
    else if ($('#txtPasswordCon').val() == '') {
        ctrlFocus = $('#txtPasswordCon');
        sMjsPass = 'Confirme su nueva contraseña anterior para continuar';
    }
    else if ($('#txtPassword').val() != $('#txtPasswordCon').val()) {
        ctrlFocus = $('#txtPassword');
        sMjsPass = 'Las contraseñas no coiciden, verfiquelas';
    }
    else if ($('#txtPasswordAnterior').val() == $('#txtPassword').val()) {
        ctrlFocus = $('#txtPassword');
        sMjsPass = 'Las contraseñas son identicas, para poder cambiarla deben ser diferentes';
    }

    if (sMjsPass != '') {
        bContinuar = false;
        General.showToastMsj(sMjsPass, true, 'warning', 10000, 'bottom-right');
        ctrlFocus.focus();
    }

    return bContinuar;
}

function initChangePass() {
    $('#MaskPassword').fadeIn(500);

    $('#closePassword').off();
    $('#closePassword').on('click', function () {
        $('#MaskPassword').fadeOut(500);
        $('#txtPasswordAnterior, #txtPassword, #txtPasswordCon').val('');
    });

    $('#txtPasswordAnterior').focus();
    initUXPass();

    $('#btnPass').off();
    $('#btnPass').on('click', function (e) {
        if (validarPass()) {
            changePass();
            $('#closePassword').click();
            General.clearToastMsj();
            General.showToastMsj('Cambiando contraseña, espere por favor.', true, 'info', 10000, 'bottom-full-width');
        }

    });
}

function changePass() {
    var parametros = { sPasswordAnterior: $('#txtPasswordAnterior').val(), sPasswordNuevo: $('#txtPassword').val() };
    $.ajax({
        url: 'DllClienteRegionalWeb/wsGeneral.aspx/ChangePassword',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parametros),
        success: function (result) {
            General.clearToastMsj();
            if (result.d == '') {
                General.showToastMsj('Su contraseña se cambio correctamente', true, 'success', 10000, 'bottom-full-width');
            }
            else {
                General.showToastMsj(result.d, true, 'warning', 10000, 'bottom-full-width');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            General.clearToastMsj();
            General.showToastMsj('Ocurrio un error al intentar cambiar su contraseña, verifique su conexión a internet', true, 'error', 10000, 'bottom-full-width');
        }
    }).done(function () {});
}

function loadSrc() {
    forms.init($iContent, oInfoItem.Id, oInfoItem.Name, bClient);
}

function reloadFrame() {
    if (oInfoItem.src == '' || oInfoItem.src == 'Home') {
        $home.click();
    }
    else {
        $___iContent = $iContent.contents();
        loadSrc();
    }
}

function reSize() {
    var iCntHeight = $('body').height() - 50; // Margin
    $iContent.height(iCntHeight);

    iCntHeight = $MenuOpciones.height() - $('.profile').outerHeight() - 50 - 5; // Margin - Padding navPermisos
    //$('#navPermisos').css({ 'max-height': iCntHeight });
    $('#navPermisos').height(iCntHeight);
}

function Logout() {
    $btnLogout.off();
    $btnLogout.click(function (e) {
        e.preventDefault();
        
        $loader.find('.msjInfoLoader').html('Cerrando sesión');
        $loader.fadeIn();
        
        $("#EVENTTARGET").val('Logout');
        $("#EVENTARGUMENT").val('');
        $FormMain.submit();
    });
}

function initCatalogo() {
    $.ajax({
        url: 'DllClienteRegionalWeb/wsGeneral.aspx/GetInfoGeneral',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: {},
        success: function Ready(data) {
            if (data.d != '') {
                aDataInfoGeneral = $.parseJSON(data.d);
                initUser = aDataInfoGeneral.initUser;
            }
        }
    }).done(function () {
        initEvents();
        $loader.fadeOut();
    });
}

function GetaDataInfoGeneral() {
    var oInfo = $.parseJSON(aDataInfoGeneral.AyudaFarmacias);
    return oInfo.Info;
}

function Empresa() {
    return aDataInfoGeneral.Empresa;
}

function Estado() {
    return aDataInfoGeneral.Estado;
}

function UsaBI() {
    return aDataInfoGeneral.UsaBI;
}

function ServidorBI() {
    return aDataInfoGeneral.ServidorBI;
}

function FiltroJuris() {
    return aDataInfoGeneral.FiltroJuris;
}

function FiltroUnidad() {
    return aDataInfoGeneral.FiltroUnidad;
}

function filterInfo(type) {
    var aDataReturn = '';
    var sColumn = '';
    var $cbo = '';
    var sFirstOpt = '';

    var $TipoUnidad = $___iContent.find('#cboTipoUnidad');
    var $Jurisdiccion = $___iContent.find('#cboJurisdiccion');
    var $Municipio = $___iContent.find('#cboLocalidad');
    var $Farmacia = $___iContent.find('#cboFarmacia');

    var sUnidad = ($("option:selected", $TipoUnidad).val() != undefined ? $("option:selected", $TipoUnidad).val() : '*');
    var sJurisdiccion = ($("option:selected", $Jurisdiccion).val() != undefined ? $("option:selected", $Jurisdiccion).val() : '*');
    var sMunicipio = ($("option:selected", $Municipio).val() != undefined ? $("option:selected", $Municipio).val() : '*');
    var sFarmacia = ($("option:selected", $Farmacia).val() != undefined ? $("option:selected", $Farmacia).val() : '*');

    var sOpUnidad = sUnidad == "*" ? "!=" : "==";
    var sOpJurisdiccion = sJurisdiccion == "*" ? "!=" : "==";
    var sOpMunicipio = sMunicipio == "*" ? "!=" : "==";
    var sOpFarmacia = sFarmacia == "*" ? "!=" : "==";

    switch (type) {
        case 'TipoUnidad':
            sColumn = new Array('IdTipoUnidad', 'TipoDeUnidad');
            $cbo = $___iContent.find('#cboTipoUnidad');
            sFirstOpt = '<option value="*" selected="">Todos los tipo de unidades</option>';
            break;
        case 'Jurisdiccion':
            sColumn = new Array('IdJurisdiccion', 'Jurisdiccion');
            $cbo = $___iContent.find('#cboJurisdiccion');
            sFirstOpt = '<option value="*" selected="">Todas las jurisdicciones</option>';
            break;
        case 'Municipio':
            sColumn = new Array('IdMunicipio', 'Municipio');
            $cbo = $___iContent.find('#cboLocalidad');
            sFirstOpt = '<option value="*" selected="">Todos los Municipios</option>';
            break;
        case 'Farmacia':
            sColumn = new Array('IdFarmacia', 'Farmacia');
            $cbo = $___iContent.find('#cboFarmacia');
            sFirstOpt = '<option value="*" selected="">Todas las Farmacias</option>';
            break;
        default:
    }

    //if (initUser) { sFirstOpt = $cbo.children(0); }
    aDataReturn = $.grep(GetaDataInfoGeneral(), function (element, index) {
        switch (type) {
            case 'TipoUnidad':
                return eval('element.IdTipoUnidad {0} "{1}"'.format(sOpUnidad, sUnidad));
                break;
            case "Jurisdiccion":
                return eval('element.IdTipoUnidad {0} "{1}"'.format(sOpUnidad, sUnidad));
                break;
            case "Municipio":
                return eval('element.IdJurisdiccion {0} "{1}" && element.IdTipoUnidad {2} "{3}"'.format(sOpJurisdiccion, sJurisdiccion, sOpUnidad, sUnidad));
                break;
            case "Farmacia":
                return eval('element.IdJurisdiccion {0} "{1}" && element.IdTipoUnidad {2} "{3}" && element.IdMunicipio {4} "{5}"'.format(sOpJurisdiccion, sJurisdiccion, sOpUnidad, sUnidad, sOpMunicipio, sMunicipio));
                break;
            default:
                break;
        }
    });
    $cbo.empty();
    if (initUser && $(sFirstOpt).val() == '*') { $cbo.append(sFirstOpt); }
    //delete items
    var filter = [];

    $.each(aDataReturn, function (index, element) {
        if ($.inArray(element[sColumn[0]], filter) == -1) {
            filter.push(element[sColumn[0]], element[sColumn[1]]);
            $cbo.append('<option value="' + element[sColumn[0]] + '">' + element[sColumn[0]] + ' -- ' + element[sColumn[1]] + '</option>');
        }
    });

    $cbo.removeAttr("disabled");
    $cbo.trigger("change");
    $cbo.focus();
}

//Add Function
String.prototype.format = function () {
    var content = this;
    for (var i = 0; i < arguments.length; i++) {
        var replacement = '{' + i + '}';
        content = content.replace(replacement, arguments[i]);
    }
    return content;
};