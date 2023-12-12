var Page = (function () {

    var $container = $('#container'),
		$bookBlock = $('#bb-bookblock'),
        $home = $('#tblHome'),
        $info = $('#Info'),
        $menuinfo = $('#menuInfo'),
        $items = $bookBlock.children(),
		itemsCount = $items.length,
		current = 0,
        titleMenu = $container.find('#menupanel h3'),
    //$menuItems = $container.find(' ul.menu-toc > li'),
        $menuItems = $('#menupanel').find(' ul.menu-toc > li'),
        $tblcontents = $('#tblcontents'),
		transEndEventNames = {
		    'WebkitTransition': 'webkitTransitionEnd',
		    'MozTransition': 'transitionend',
		    'OTransition': 'oTransitionEnd',
		    'msTransition': 'MSTransitionEnd',
		    'transition': 'transitionend'
		},
		transEndEventName = transEndEventNames[Modernizr.prefixed('transition')],
		supportTransitions = Modernizr.csstransitions,
        aDataInfoGeneral = '',
        aDataInfoGeneralPedidosEspeciales = '',
        bOption = false,
        oTable = '';

    var omenuItemActive = new Object();
    omenuItemActive.src = '#tblHome';
    omenuItemActive.id = '0';
    omenuItemActive.name = 'Bienvenido';


    function init() {
        // initialize jScrollPane on the content div of the first item
        setJSP('init');
        initEvents();
        initFg(bOption);
        window.onload = function () {
            // Carga completa
            initCatalogo();
        }
    }

    function initCatalogo() {
        $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/GetInfoGeneral",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: {},
            onreadystatechange: function (xhr) {
                switch (xhr.readyState) {
                    case 1:
                        Page.AjaxLoadingText('25%');
                        break;
                    case 2:
                        Page.AjaxLoadingText('50%');
                        break;
                    case 3:
                        Page.AjaxLoadingText('75%');
                        break;
                    case 4:
                        Page.AjaxLoadingText('100%');
                        break;
                    default:
                }
            },
            success: function Ready(data) {
                if (data.d != '') {
                    aDataInfoGeneral = $.parseJSON(data.d);
                }
            }
        }).done(function () {
            General.ProcesoPorDia(aDataInfoGeneral.FechaCompleta);
            if (aDataInfoGeneral.UsaPedidos) {
                initCatalogoPedidos();
            }
            else {
                $('.MaskM').fadeOut(500);
            }
        });
    }

    function initCatalogoPedidos() {
        $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/GetInfoGeneralPedidosEspeciales",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: {},
            onreadystatechange: function (xhr) {
                switch (xhr.readyState) {
                    case 1:
                        Page.AjaxLoadingText('25%');
                        break;
                    case 2:
                        Page.AjaxLoadingText('50%');
                        break;
                    case 3:
                        Page.AjaxLoadingText('75%');
                        break;
                    case 4:
                        Page.AjaxLoadingText('100%');
                        break;
                    default:
                }
            },
            success: function Ready(data) {
                if (data.d != '') {
                    aDataInfoGeneralPedidosEspeciales = $.parseJSON(data.d);
                }
            }
        }).done(function () {
            $('.MaskM').fadeOut(500);
        });
    }

    function GetaDataInfoGeneral() {
        var oInfo = $.parseJSON(aDataInfoGeneral.AyudaFarmacias);
        return oInfo.Info;
    }

    function GetPro_SubPro() {
        return $.parseJSON(aDataInfoGeneral.Programas);
    }

    function GetaDataInfoGeneralPedidosEspeciales() {
        return aDataInfoGeneralPedidosEspeciales;
    }

    function filterInfoPedidos(type) {
        var aDataReturn = '';
        var sColumn = '';
        var $cbo = '';
        var aDataFind = '';

        var $cboPerfil = $('#iContent').contents().find('#cboPerfil');
        var $cboBeneficiario = $('#iContent').contents().find('#cboBeneficiario');
        switch (type) {
            case 'Perfil':
            case 'Cuadro':
                sColumn = new Array('IdPerfilAtencion', 'PerfilDeAtencion');
                $cbo = $cboPerfil;
                aDataFind = aDataInfoGeneralPedidosEspeciales.Catalago;
                break;
            case 'Beneficiario':
                sColumn = new Array('IdBeneficiario', 'NombreCompleto');
                $cbo = $cboBeneficiario;
                aDataFind = aDataInfoGeneralPedidosEspeciales.Beneficiarios;
                break;
            default:
        }
        aDataReturn = $.grep(aDataFind, function (element, index) {
            switch (type) {
                case "Beneficiario":
                case 'Cuadro':
                    return eval('element.IdPerfilAtencion == {0}'.format($cboPerfil.val()));
                    break;
                default:
                    break;
            }
        });

        if (type != 'Cuadro') {
            var sFirstOpt = $cbo.children(0);
            $cbo.empty();
            $cbo.append(sFirstOpt[0].outerHTML);

            //delete items
            var filter = [];

            $.each(aDataReturn, function (index, element) {
                if ($.inArray(element[sColumn[0]], filter) == -1) {
                    filter.push(element[sColumn[0]], element[sColumn[1]]);
                    $cbo.append('<option value="' + element[sColumn[0]] + '">' + element[sColumn[0]] + ' -- ' + element[sColumn[1]] + '</option>');
                }
            });

            $cbo.removeAttr("disabled");
            $cbo.focus();
        }

        return aDataReturn;
    }


    function filterInfo(type) {
        var aDataReturn = '';
        var sColumn = '';
        var $cbo = '';
        var sFirstOpt = '';

        var $TipoUnidad = $('#iContent').contents().find('#cboTipoUnidad');
        var $Jurisdiccion = $('#iContent').contents().find('#cboJurisdiccion');
        var $Municipio = $('#iContent').contents().find('#cboLocalidad');
        var $Farmacia = $('#iContent').contents().find('#cboFarmacia');

        var sUnidad = ($("option:selected", $TipoUnidad).val() != undefined ? $("option:selected", $TipoUnidad).val() : '*');
        var sJurisdiccion = ($("option:selected", $Jurisdiccion).val() != undefined ? $("option:selected", $Jurisdiccion).val() : '*');
        var sMunicipio = ($("option:selected", $Municipio).val() != undefined ? $("option:selected", $Municipio).val() : '*');
        var sFarmacia = ($("option:selected", $Farmacia).val() != undefined ? $("option:selected", $Farmacia).val() : '*');

        var sOpUnidad = sUnidad == "*" ? "!=" : "==";
        var sOpJurisdiccion = sJurisdiccion == "*" ? "!=" : "==";
        var sOpMunicipio = sMunicipio == "*" ? "!=" : "==";
        var sOpFarmacia = sFarmacia == "*" ? "!=" : "==";

        switch (type) {
            case 'Jurisdiccion':
                sColumn = new Array('IdJurisdiccion', 'Jurisdiccion');
                $cbo = $('#iContent').contents().find('#cboJurisdiccion');
                break;
            case 'Municipio':
                sColumn = new Array('IdMunicipio', 'Municipio');
                $cbo = $('#iContent').contents().find('#cboLocalidad');
                break;
            case 'Farmacia':
                sColumn = new Array('IdFarmacia', 'Farmacia');
                $cbo = $('#iContent').contents().find('#cboFarmacia');
                break;
            default:
        }

        if (initUser) { sFirstOpt = $cbo.children(0); }
        aDataReturn = $.grep(GetaDataInfoGeneral(), function (element, index) {
            switch (type) {
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
        if (initUser && $(sFirstOpt[0]).val() == '*') { $cbo.append(sFirstOpt[0].outerHTML); }
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

    function initEvents() {

        // tooltip Menu
        //$menuItems.tooltip();

        //init ajax
        $.ajaxSetup({
            timeout: 3600000
        });

        //// Tooltip Panel
        //$tblcontents.tooltip();
        //$home.tooltip();

        // show table of contents
        $tblcontents.on('click', toggleTOC);

        // show home
        $home.on('click', function (e) {
            if (General.checkAjax()) {
                AjaxLoading('Mostrar');
                AjaxLoadingText('');
                $('#iContent').attr("src", "");
                $('#navBarra').width('100%');
                $('#TextTitle').html("Bienvenido");
                closeTOC();
                delTOC();
                AjaxLoading('Ocultar');
                General.clearToastMsj();

                //New 
                omenuItemActive.src = '#tblHome';
                omenuItemActive.id = '0';
                omenuItemActive.name = 'Bienvenido';
            }
        });

        // show menu for info
        $info.on('click', function () {
            if ($menuinfo.is(':visible')) {
                $menuinfo.slideUp();

            } else {
                $menuinfo.slideDown();
            }
        });

        // click frame
        $('#iContent').contents().on('click', function () {
            if ($menuinfo.is(':visible')) {
                $menuinfo.slideUp();
            }
        });

        //Hide menuinfo
        $(document).on('click', function (event) {
            if ($menuinfo.is(':visible') && event.target != $info[0] && jQuery.inArray($info[0], $(event.target).parents().map(function () { return this }).get()) == -1
                && event.target != $menuinfo[0]) {
                $menuinfo.slideUp();
            }
        });

        // click a menu item
        $menuItems.on('click', function (e) {
            e.preventDefault();
            if (General.checkAjax()) {
                var $el = $(this),
				    idx = $el.index(),
				    $aItem = $el.find('a'),
				    paddingCaja = ($container.height() / 2) - 100; // El -100 por el tamaño de la Caja 200/2 = 100

                //if ($aItem.attr('href') != 'Sub-Menu' && !$el.hasClass('menu-toc-current')) {
                if ($aItem.attr('href') != 'Sub-Menu') {
                    current = idx;
                    //mostramos iframe, ocultamos consejo (#Title) y mostrarmos efecto loading
                    $('#navBarra').width('100%');
                    $('#Loading').fadeIn(100);
                    AjaxLoadingText('');
                    $('#TextTitle').html($el.text());
                    $('#Mask').fadeIn(100);
                    $('#Caja').css({
                        display: 'block',
                        'padding-top': paddingCaja
                    });
                    $('#iContent').attr("src", $aItem.attr('href'));
                    $('#iContent').load(function mostrarCarga() {

                        $('#Caja').fadeOut(100);
                        $('#Mask').fadeOut(100);
                        $('#Loading').fadeOut(100);
                        $('#navBarra').width('0px');

                        //General.init($('#iContent'), $el.text(), bOption);
                        General.init($('#iContent'), $aItem.attr('rel'), bOption);
                    });

                    closeTOC();
                    updateTOC();

                    $el.addClass('menu-toc-current');

                    if ($menuinfo.is(':visible')) {
                        $menuinfo.slideUp();
                    }

                } else if ($aItem.attr('href') == 'Sub-Menu') {
                    var submenu = $(this).find('ul').clone(true);
                    $el.addClass('menu-toc-current');
                    if ($('body').height() > ($(this).position().top + $(this).find('ul').height())) {
                        $('.submenu').css({
                            top: $(this).position().top + 47,
                            height: $(this).find('ul').height()
                        });
                    }
                    else {
                        $('.submenu').css({
                            top: $(this).position().top - $(this).find('ul').height() - 1 + 36 + 47,
                            height: $(this).find('ul').height()
                        });
                    }

                    $('.submenu').html(submenu).show().click(function (e) {
                        $(this).hide();
                        $el.removeClass('menu-toc-current');
                    }).on('mouseleave', function () {
                        $(this).hide();
                    });
                }
            }
        });

        $('#btnConfig').off();
        $('#btnConfig').on('click', function (e) {
            if ($('#Config').is(':hidden')) {
                $('#Config').fadeIn(500);

                $('#twNavegador').off();
                $('#twNavegador').on('click', 'a', function (e) {
                    var itemCfg = $(this);
                    var itemVal = itemCfg.attr('href');
                    if (itemVal.indexOf('javascript') != 0) {
                        var aVal = itemVal.split('%7C')
                        $('#iOptions').attr('src', aVal[0]);
                        $('#iOptions').load(function mostrarCarga() {
                            initPermisos(aVal[1], $('#iOptions'));
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

        // Logout
        $('#Logout').off();
        $('#Logout').on('click', function () {
            if (General.checkAjax()) {
                $("#EVENTTARGET").val('Logout');
                $("#EVENTARGUMENT").val('');
                $('#Main').submit();
            }
        });

        //Change pass
        $('#ChangePass').off();
        $('#ChangePass').on('click', function () {
            if (General.checkAjax()) {
                var $ctrlInput = $('#MaskM').find('input');

                $ctrlInput.each(function () {
                    $(this).removeAttr('disabled', '').val('');
                });

                $('#MsjResp').removeClass().fadeOut();

                $('#MaskM').fadeIn(100);
                $('#txtPasswordAnterior').focus();

                initBtnModal();
            }
        });

        //keypress
        $("#txtPasswordAnterior").on('keypress', function (event) {
            if (event.which == 13) {
                event.preventDefault();
                $('#txtPassword').focus();
            }
        });

        $("#txtPassword").on('keypress', function (event) {
            if (event.which == 13) {
                event.preventDefault();
                $('#txtPasswordCon').focus();
            }
        });

        $("#txtPasswordCon").on('keypress', function (event) {
            if (event.which == 13) {
                event.preventDefault();
                $('#btnModal').focus();
                $('#btnModal').click();
            }
        });

        // reinit jScrollPane on window resize
        $(window).on('debouncedresize', function () {
            // reinitialise jScrollPane on the content div
            setJSP('reinit');
            $('.menu-panel').jScrollPane({ showArrows: false, verticalGutter: 0, hideFocus: true });
        });

        $('#twOpciones').on('click', 'a', function (e) {
            var itemCfg = $(this);
            var itemVal = itemCfg.attr('href');
            if (itemVal.indexOf('javascript') != 0) {
                var aVal = itemVal.split('%7C');
                var paddingCaja = ($container.height() / 2) - 100; // El -100 por el tamaño de la Caja 200/2 = 100

                //mostramos iframe, ocultamos consejo (#Title) y mostrarmos efecto loading
                $('#navBarra').width('100%');
                $('#Loading').fadeIn(100);
                AjaxLoadingText('');
                $('#TextTitle').html(itemCfg.text());
                $('#Mask').fadeIn(100);
                $('#Caja').css({
                    display: 'block',
                    'padding-top': paddingCaja
                });

                $('#iContent').attr("src", aVal[0]);
                $('#iContent').load(function mostrarCarga() {

                    $('#Caja').fadeOut(100);
                    $('#Mask').fadeOut(100);
                    $('#Loading').fadeOut(100);
                    $('#navBarra').width('0px');

                    General.init($('#iContent'), aVal[1], bOption);

                    //New 
                    omenuItemActive.src = aVal[0];
                    omenuItemActive.id = aVal[1];
                    omenuItemActive.name = itemCfg.text();
                });

                closeTOC();
                updateTOC();
                return false;
            }
        });
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
            url: '../DllClienteRegionalWeb/ws_General.aspx/GetMovtosAuditor',
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
            url: "../DllClienteRegionalWeb/ws_General.aspx/GetUsers",
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
            url: "../DllClienteRegionalWeb/ws_General.aspx/ManageUser",
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
            url: "../DllClienteRegionalWeb/ws_General.aspx/GetUser",
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

    function initFg($bInit) {
        if ($bInit) {
            //Disabled contextmenu
            $(document).on("contextmenu", function (e) {
                return false;
            });

            $('#iContent').on("contextmenu", function (e) {
                return false;
            });

            $('#iContent').contents().on("contextmenu", function (e) {
                return false;
            });

            $('iframe, frame').on("contextmenu", function (e) {
                return false;
            });

            $('iframe, frame').contents().on("contextmenu", function (e) {
                return false;
            });
        }
    }

    function ValidarInfoPassword() {
        var bRegresa = true,
            sMsj = '',
            sTypeMsj = '';

        if ($('#txtPasswordAnterior').val() == '') {
            sMsj = 'Escriba su password anterior.';
            $('#txtPasswordAnterior').focus();
            sTypeMsj = 'error';
            bRegresa = false;
        } else if ($('#txtPassword').val() == '') {
            sMsj = 'Escriba su nuevo password.';
            $('#txtPassword').focus();
            sTypeMsj = 'error';
            bRegresa = false;
        } else if ($('#txtPasswordCon').val() == '') {
            sMsj = 'Confirme su nuevo password.';
            $('#txtPasswordCon').focus();
            sTypeMsj = 'error';
            bRegresa = false;
        } else if ($('#txtPassword').val() != $('#txtPasswordCon').val()) {
            sMsj = 'Los passwords no son iguales, verifique.';
            $('#txtPassword').focus();
            sTypeMsj = 'error';
            bRegresa = false;
        }

        $('#MsjResp').removeClass().html(sMsj).addClass('add-on alert-' + sTypeMsj).fadeIn(100);

        return bRegresa;
    }

    //Init btnSetPass
    function initBtnModal() {
        var $ctrlInput = $('#MaskM').find('input');

        $('#closeM').off();
        $('#closeM').on('click', function () {
            $('#MaskM').fadeOut(500);
        });

        $('#btnModal').off();
        $('#btnModal').removeClass('disabled');
        $('#btnModal').on('click', function () {
            if (ValidarInfoPassword()) {
                $ctrlInput.each(function () {
                    $(this).attr('disabled', 'disabled');
                });
                $('#btnModal').off().addClass('disabled');
                $('#closeM').off();
                $('#MsjResp').removeClass().html('Realizando los cambios, espere por favor.').addClass('add-on alert-info');
                setPass();
            }
        });
    }

    //Call Change pass
    function setPass() {
        var $ctrlInput = $('#MaskM').find('input'),
            parametros = {
                sPasswordAnterior: $('#txtPasswordAnterior').val(),
                sPasswordNuevo: $('#txtPassword').val()
            };
        $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/ChangePassword",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
            success: function Ready(res) {
                if (res.d == "true") {
                    $('#MsjResp').removeClass().html('Password cambiado correctamente').addClass('add-on alert-success');
                }
                else if (res.d == "false") {
                    $('#MsjResp').removeClass().html('Ocurri&oacute; un error al realizar el cambio de password.').addClass('add-on alert-error');
                }
                else {
                    $('#MsjResp').removeClass().html(res.d).addClass('add-on alert-error');
                }
            },
            error: function errorcall(res) {
                $('#MsjResp').removeClass().html('Ocurri&oacute; un error al realizar el cambio de password.').addClass('add-on alert-error');
            }
        }).done(function () {
            //Clean input's
            $ctrlInput.each(function () {
                $(this).removeAttr('disabled', '').val('');
            });

            $('#txtPasswordAnterior').focus();
            initBtnModal();
        });

    }

    //get url
    function getUrl(tam) {
        var text = "";
        var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        for (var i = 0; i < tam; i++)
            text += possible.charAt(Math.floor(Math.random() * possible.length));

        return text;
    }

    function setJSP(action, idx) {

        idx = idx === undefined ? current : idx,
			$content = $items.eq(idx).children('div.content'),
			apiJSP = $content.data('jsp');

        if (action === 'reinit' && apiJSP !== undefined) {
            apiJSP.reinitialise();
        }
        else if (action === 'destroy' && apiJSP !== undefined) {
            apiJSP.destroy();
        }

        $('.menu-panel').jScrollPane({ showArrows: false, verticalGutter: 0, hideFocus: true });
        $container.find('#menupanel h3').remove();
        $('.menu-panel').prepend(titleMenu);
    }

    function updateTOC() {
        $menuItems.removeClass('menu-toc-current');
    }

    function delTOC() {
        $menuItems.removeClass('menu-toc-current');
    }

    function toggleTOC() {
        var opened = $container.data('opened');
        opened ? closeTOC() : openTOC();
    }

    function openTOC() {
        var mleft = ($('#menupanel').width() / 2) - 8; //Margin iFrame
        var oSize = $('#iContent').contents().find('.container').offset();

        if (oSize != undefined) {
            mleft = oSize.left - ($('#menupanel').width() / 2) - 8;
        }
        $container.addClass('slideRight').data('opened', true);

        $('#iContent').contents().find('.container').css('margin-left', mleft + 'px');
    }

    function closeTOC(callback) {
        $container.removeClass('slideRight').data('opened', false);
        $('#iContent').contents().find('.container').css('margin-left', 'auto');
        $('.submenu').hide();
        if (callback) {
            if (supportTransitions) {
                $container.on(transEndEventName, function () {
                    $(this).off(transEndEventName);
                    callback.call();
                });
            }
            else {
                callback.call();
            }
        }

    }

    function AjaxLoad(Opcion) {
        if (Opcion == 'Mostrar') {
            var paddingCaja = ($container.height() / 2) - 100;

            $('#navBarra').width('100%');
            AjaxLoading('Mostrar');
            AjaxLoadingText('');
            $('#Mask').fadeIn(100);
            $('#Caja').css({
                display: 'block',
                'padding-top': paddingCaja
            });
        } else {
            $('#navBarra').width('0px');
            $('#Caja').fadeOut(100);
            $('#Mask').fadeOut(100);
            AjaxLoading('Ocultar');
        }
    }

    function AjaxLoading(Opcion) {
        if (Opcion == 'Mostrar') {
            $('#Loading').fadeIn(100);
        } else {
            $('#Loading').fadeOut(100);
        }
    }

    function AjaxLoading(Opcion, sText) {
        if (Opcion == 'Mostrar') {
            $('#Loading').fadeIn(100);
            $('#Loading').html(sText);
        } else {
            $('#Loading').fadeOut(100);
        }
    }

    function AjaxLoadingText(sText) {
        $('#Loading').html(sText);
    }

    function relodFrame() {
        //$('.menu-toc-current').click();
        if (omenuItemActive.src == '' || omenuItemActive.src == '#tblHome') {
            $home.click();
        }
        else {
            $('#navBarra').width('100%');
            $('#TextTitle').html(omenuItemActive.name);
            $('#iContent').attr("src", omenuItemActive.src);
            $('#iContent').load(function mostrarCarga() {

                $('#Caja').fadeOut(100);
                $('#Mask').fadeOut(100);
                $('#Loading').fadeOut(100);
                $('#navBarra').width('0px');

                General.init($('#iContent'), omenuItemActive.id, bOption);
            });
        }
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

    function Usapedidos() {
        return aDataInfoGeneral.UsaPedidos;
    }

    function UsaFechaCompleta() {
        return aDataInfoGeneral.FechaCompleta;
    }

    function ProcesoPorMes() {
        return aDataInfoGeneral.ProcesoPorMes;
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

    return {
        init: init,
        AjaxLoad: AjaxLoad,
        AjaxLoading: AjaxLoading,
        AjaxLoadingText: AjaxLoadingText,
        relodFrame: relodFrame,
        filterInfo: filterInfo,
        GetaDataInfoGeneral: GetaDataInfoGeneral,
        filterInfoPedidos: filterInfoPedidos,
        GetaDataInfoGeneralPedidosEspeciales: GetaDataInfoGeneralPedidosEspeciales,
        GetPro_SubPro: GetPro_SubPro,
        Usapedidos: Usapedidos,
        UsaFechaCompleta: UsaFechaCompleta,
        ProcesoPorMes: ProcesoPorMes,
        Empresa: Empresa,
        Estado: Estado,
        UsaBI: UsaBI,
        ServidorBI: ServidorBI,
        FiltroUnidad: FiltroUnidad,
        FiltroJuris: FiltroJuris
    }

})();