var General = (function () {
    var $Nuevo, $Ejecutar, $Imprimir, $Exportar; //Botones
    var $TipoUnidad, $Jurisdiccion, $Municipio, $Farmacia; //Combos
    var $title; //Nombre de la opcion ejecutada
    var $frmActivo = '';
    var $iFrameActivo = '';
    var oTableAyuda = '';
    var oTable = '';
    var bRptReportesTransferencias = false;
    var bNuevoRegistro = true;
    var doc = $(document);
    var bBtnImprimir = false;
    var jsonData = '';
    var oTableClaves = '';
    var oAjax = '';
    var oSettingsTable = '';
    var bOptionCte = true;
    var bopcDate = false;
    var bopcProcesoPorMes = false;

    function init($iFrame, $title, bOption) {
        toastr.options = {};
        toastr.clear();
        $iFrameActivo = $iFrame;
        $iFrame.off();

        oAjax = '';
        oTable = '';
        initoSettingsTable();
        initRButton($iFrame, bOption);
        bOptionCte = bOption;

        $Nuevo = $iFrame.contents().find('#New');
        $Ejecutar = $iFrame.contents().find('#Exec');
        $Imprimir = $iFrame.contents().find('#Print');
        $Exportar = $iFrame.contents().find('#Exportar');

        $TipoUnidad = $iFrame.contents().find('#cboTipoUnidad');
        $Jurisdiccion = $iFrame.contents().find('#cboJurisdiccion');
        $Municipio = $iFrame.contents().find('#cboLocalidad');
        $Farmacia = $iFrame.contents().find('#cboFarmacia');

        //$FechaInicial = $iFrame.contents().find('#dtpFechaInicial');
        //$FechaFinal = $iFrame.contents().find('#dtpFechaFinal');
        bBtnImprimir = false;
        initEvents($title);
        bopcDate = Page.UsaFechaCompleta();
        bopcProcesoPorMes = Page.ProcesoPorMes();
    }

    function initRButton($iFrame, bOption) {
        if (bOption) {
            $iFrame.on("contextmenu", function (e) {
                return false;
            });
            $iFrame.contents().on("contextmenu", function (e) {
                return false;
            });
        }
    }

    function initoSettingsTable() {
        oSettingsTable = {
            "bScrollInfinite": true,
            "sScrollY": '200px',
            "sScrollX": '100%',
            "aaSorting": '',
            "bFilter": true,
            "bSearchable": false,
            "bSort": true,
            "sDom": 'lfrtip',
            "iDisplayLength": 999999,
            "aoColumnDefs": '',
            "oLanguage": {
                "sLengthMenu": "Mostrar _MENU_ registros por pagina.",
                "sZeroRecords": "No se encontro información con los criterios especificados",
                "sInfo": "Mostrando desde _START_ hasta _END_ de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando desde 0 hasta 0 de 0 registros.",
                "sInfoFiltered": "(filtrado de _MAX_ registros en total).",
                "sLoadingRecords": "Cargando...",
                "sProcessing": "Procesando...",
                "sSearch": "Búsqueda"
            }
        };
    }

    function initEvents($title) {
        $frmActivo = $title;
        switch ($title) {
            case '2':
                initNav(true, true, false, true);
                if (initUser) { initComboJF(); }
                break;
            case '14':
                initNav(true, true, false, true);
                if (initUser) { initCombosUJMF(); }
                break;
            case '13':
                initNav(true, true, false, true);
                if (initUser) { initComboJF(); }
                break;
            case '15':
                initNav(true, true, false, true);
                if (initUser) { initCombosUJMF(); }
                break;
            case '16':
                initNav(true, true, false, true);
                if (initUser) { initCombosUJMF(); }
                break;
            case '17':
                initNav(true, true, false, true);
                if (initUser) { initCombosUJMF(); }
                break;
            case '18':
                initNav(true, true, false, true);
                if (initUser) { initComboJF(); }
                $Farmacia.find("option[value='*']").remove();
                break;
            case '19':
                initNav(true, true, true, false);
                break;
            case '20':
                initNav(true, true, false, true);
                bBtnImprimir = false;
                initIdFarmacia();
                break;
            case '28':
                initNav(true, false, false, false);
                initPedidos();
                break;
            case '21':
                initNav(true, true, false, true);
                break;
            case '22':
                initNav(true, true, false, false);
                if (initUser) { initComboJF(); }
                break;
            case '23':
                initNav(true, true, false, false);
                bBtnImprimir = false;
                initIdFarmacia();
                break;
            case '24':
                initNav(true, true, false, false);
                bBtnImprimir = false;
                jsonData = '';
                initIdFarmacia();
                break;
            case '25':
                initNav(true, true, true, false);
                bBtnImprimir = false;
                initIdFarmacia();
                break;
            case '26':
            case '27':
                initNav(true, true, false, true);
                if (initUser) { initComboJF(); }
                $Farmacia.find("option[value='*']").remove();
                break;
            case '29':
                if (Page.Usapedidos()) {
                    initNav(true, true, false, true);
                    initRegistroPedidos();
                }
                else {
                    initNav(true, false, false, true);
                    DisabledControls();
                    showToastMsj('La unidad no esta configurada para realizar pedidos especiales.', true, 'info', 10000, 'bottom-full-width');
                }
                break;
            case '30_':
                initNav(true, true, false, false);
                break;
            case '30':
                initNav(true, true, false, false);
                bBtnImprimir = false;
                jsonData = '';
                initIdFarmacia();
                initPro_SubPro();
                //initRangoFechas();
                break;
            case '31':
            case '34':
                initNav(true, true, false, false);
                jsonData = '';
                if (initUser) { initCombosUJMF(); }
                break;
            case '32':
            case '69':
                //if ($iFrameActivo.contents().find('#txtFarmacia ul').length) {
                if ($iFrameActivo.contents().find('#columns ul').length) {
                    initNav(true, true, false, false);
                }
                else {
                    initNav(true, false, false, true);
                    DisabledControls();
                    showToastMsj('La unidad no esta configurada para utilizar el reporteador.', true, 'info', 10000, 'bottom-full-width');
                }
                if (idEstado == '09') {
                    $Farmacia.find("option[value='0020']").remove();
                }

                if (idEstado == '16') {
                    $iFrameActivo.contents().find('#TipoInformacion').css({ 'display': 'none' });
                    var iheight = $iFrameActivo.contents().find('#columns').height() - 28;
                    $iFrameActivo.contents().find('#ulColumnas').css({ 'height': iheight });

                    iheight = $iFrameActivo.contents().find('#columnsSelected').height() + 70;
                    $iFrameActivo.contents().find('#TipoReporte').css({ 'display': 'none' });
                    $iFrameActivo.contents().find('#columnsSelected').css({ 'height': iheight });
                    iheight = $iFrameActivo.contents().find('#columnsSelected').height() - 28;
                    iheight = $iFrameActivo.contents().find('#columnsSelected ul').height() + iheight;
                    $iFrameActivo.contents().find('#columnsSelected ul').css({ 'height': iheight });
                }
                break;
            case '33':
                initNav(true, true, false, false);
                initClaveSSA();
                initTableHelpById([{ "sClass": "center", "aTargets": [0]}], [[0, "asc"]], 'AyudaClaves');
                break;
            case 'test':
                initNav(true, true, false, true);
                //if (initUser) { initComboJF(); }
                initCombosUJMF();
                break;
            case '36':
            case '37':
            case '38':
            case '39':
            case '40':
            case '41':
            case '42':
            case '43':
            case '44':
            case '45':
            case '46':
            case '47':
            case '48':
            case '49':
            case '50':
            case '51':
            case '53':
            case '55':
            case '56':
            case '57':
            case '58':
            case '59':
            case '60':
            case '61':
            case '62':
            case '63':
            case '64':
            case '65':
            case '66':
            case '67':
            case '68':
                initNav(true, true, false, true);
                initCombosUJMF();
                if (Page.FiltroUnidad()) {
                    $TipoUnidad.find("option[value='*']").remove();
                }
                if (Page.FiltroJuris()) {
                    $Jurisdiccion.find("option[value='*']").remove();
                }
                resultAdaptive();
                break;
            case '54':
                initNav(true, true, false, true);
                resultAdaptive();
                break;
            case '70':
                initNav(true, true, false, false);
                break;
            default:
                //Opcion en caso de no conicidir
        }
    }

    function resultAdaptive() {
        $iFrameActivo.contents().find('.Results').append('<span class="return">Maximizar</span>');

        $iFrameActivo.contents().find('.Results .return').off();
        $iFrameActivo.contents().find('.Results .return').on('click', function (e) {
            if ($iFrameActivo.contents().find('.Results').hasClass('Max')) {
                $iFrameActivo.contents().find('.Results').removeClass('Max');
                $iFrameActivo.contents().find('.Results .return').html('Maximizar');
            }
            else {
                $iFrameActivo.contents().find('.Results').addClass('Max');
                $iFrameActivo.contents().find('.Results .return').html('Minimizar');
            }
        });
    }

    function initNav(bNuevo, bEjecutar, bImprimir, bExportar) {
        $Nuevo.off();
        if (bNuevo) {
            $Nuevo.on('click', function () {
                toastr.clear();
                limpiar();
            });
        }

        $Ejecutar.off();
        if (bEjecutar) {
            $Ejecutar.on('click', function () {
                var $bDisbledControld = true;
                toastr.clear();
                switch ($frmActivo) {
                    case '2':
                    case '26':
                        Existencias();
                        break;
                    case '14':
                        DispensancionClaves();
                        break;
                    case '13':
                    case '27':
                        ProximosACaducar();
                        break;
                    case '15':
                        MedicosDiagnostico();
                        break;
                    case '16':
                        AntibioticosControlados();
                        break;
                    case '17':
                        CostoPacienteProgramaDeAtencion();
                        break;
                    case '18':
                        CortesDiarios();
                        break;
                    case '19':
                        ListadosVarios();
                        break;
                    case '20':
                        if ($iFrameActivo.contents().find('#txtFarmacia').val() != '') {
                            ReportesTransferencias();
                        }
                        else {
                            $bDisbledControld = false;
                            $iFrameActivo.contents().find('#txtFarmacia').focus();
                            showToastMsj('Selecciona la farmacia que desea consultar.', false, 'warning', 10000, 'bottom-right');
                        }
                        break;
                    case '28':
                        if ($iFrameActivo.contents().find('#txtFolio').val() == '*') {
                            var bContinua = true;
                            if ($iFrameActivo.contents().find('#txtObservaciones').val() == '') {
                                showToastMsj('No ah capturado observaciones, verifique.', false, 'warning', 10000, 'bottom-right');
                                $iFrameActivo.contents().find('#txtObservaciones').focus();
                                bContinua = false;
                                $bDisbledControld = false;
                            } else {
                                bContinua = bValidarTable();

                                if (bContinua) {
                                    PedidosEspeciales();
                                }
                                else {
                                    showToastMsj('Debe capturar al menos una Clave para el Pedido\n y/o capturar cantidades para al menos una Clave, verifique.', false, 'warning', 10000, 'bottom-right');
                                }
                            }
                        }
                        else {
                            showToastMsj('Este Folio ya ha sido guardado por lo tanto no puede ser modificado.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '29':
                        if ($iFrameActivo.contents().find('#txtFolio').val() == '*') {
                            var bContinua = true;
                            var $cboPerfil = $iFrameActivo.contents().find('#cboPerfil');
                            var $cboBeneficiario = $iFrameActivo.contents().find('#cboBeneficiario');
                            var $txtReferencia = $iFrameActivo.contents().find('#txtReferencia');
                            if ($cboPerfil.val() == 0) {
                                showToastMsj('Seleccione un perfil.', false, 'warning', 10000, 'bottom-right');
                                $cboPerfil.focus();
                                bContinua = false;
                                $bDisbledControld = false;
                            } else if ($cboBeneficiario.val() == 0) {
                                showToastMsj('Seleccione un beneficiario.', false, 'warning', 10000, 'bottom-right');
                                $cboBeneficiario.focus();
                                bContinua = false;
                                $bDisbledControld = false;
                            } else if ($txtReferencia.val() == '') {
                                showToastMsj('No ah capturado folio documento, verifique.', false, 'warning', 10000, 'bottom-right');
                                $txtReferencia.focus();
                                bContinua = false;
                                $bDisbledControld = false;
                            } else if ($iFrameActivo.contents().find('#txtObservaciones').val() == '') {
                                showToastMsj('No ah capturado observaciones, verifique.', false, 'warning', 10000, 'bottom-right');
                                $iFrameActivo.contents().find('#txtObservaciones').focus();
                                bContinua = false;
                                $bDisbledControld = false;
                            } else {
                                bContinua = bValidarTable();

                                if (bContinua) {
                                    RegistroPedidosEspeciales();
                                }
                                else {
                                    showToastMsj('Debe capturar al menos una Clave para el Pedido\n y/o capturar cantidades para al menos una Clave, verifique.', false, 'warning', 10000, 'bottom-right');
                                }
                            }
                        } else if ($iFrameActivo.contents().find('#txtFolio').val() == '') {
                            $iFrameActivo.contents().find('#txtFolio').focus();
                            showToastMsj('Debe crear un folio nuevo antes de poder guardar el pedido.', false, 'warning', 10000, 'bottom-right');
                            $bDisbledControld = false;
                        } else {
                            showToastMsj('Este Folio ya ha sido guardado por lo tanto no puede ser modificado.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '21':
                        ClavesNegadas();
                        break;
                    case '22':
                        SurtimientoInsumos();
                        break;
                    case '23':
                        if ($iFrameActivo.contents().find('#txtFarmacia').val() != '') {
                            EfectividadVales();
                        }
                        else {
                            $bDisbledControld = false;
                            $iFrameActivo.contents().find('#txtFarmacia').focus();
                            showToastMsj('Selecciona la farmacia que desea consultar.', false, 'warning', 10000, 'bottom-right');
                        }
                        break;
                    case '24':
                        if ($iFrameActivo.contents().find('#txtFarmacia').val() != '') {
                            SurtimientoRecetas();
                        }
                        else {
                            $bDisbledControld = false;
                            $iFrameActivo.contents().find('#txtFarmacia').focus();
                            showToastMsj('Selecciona la farmacia que desea consultar.', false, 'warning', 10000, 'bottom-right');
                        }
                        break;
                    case '25':
                        if ($iFrameActivo.contents().find('#txtFarmacia').val() != '') {
                            AbastoFarmacias();
                        }
                        else {
                            $bDisbledControld = false;
                            $iFrameActivo.contents().find('#txtFarmacia').focus();
                            showToastMsj('Selecciona la farmacia que desea consultar.', false, 'warning', 10000, 'bottom-right');
                        }
                        break;
                    case '30_':
                        AbastoFarmaciasGeneral();
                        break;
                    case '30':
                        var bContinuar = true;
                        if ($iFrameActivo.contents().find('#txtFarmacia').val() == '') {
                            bContinuar = false;
                            $iFrameActivo.contents().find('#txtFarmacia').focus();
                            showToastMsj('Selecciona la farmacia que desea consultar.', false, 'warning', 10000, 'bottom-right');
                        } else if ($iFrameActivo.contents().find('#txtPrograma').val() == '') {
                            bContinuar = false;
                            $iFrameActivo.contents().find('#txtPrograma').focus();
                            showToastMsj('Selecciona el programa que desea consultar.', false, 'warning', 10000, 'bottom-right');
                        }
                        else if ($iFrameActivo.contents().find('#txtSubPrograma').val() == '') {
                            bContinuar = false;
                            $iFrameActivo.contents().find('#txtSubPrograma').focus();
                            showToastMsj('Selecciona el sub-programa que desea consultar.', false, 'warning', 10000, 'bottom-right');
                        }

                        if (bContinuar) {
                            ReportesDispensacion();
                        }
                        else {
                            $bDisbledControld = false;
                        }

                        break;
                    case '31':
                        var bContinuar = true;
                        if ($iFrameActivo.contents().find('#txtFarmacia').val() == '') {
                            bContinuar = false;
                            $iFrameActivo.contents().find('#txtFarmacia').focus();
                            showToastMsj('Selecciona la farmacia que desea consultar.', false, 'warning', 10000, 'bottom-right');
                        }

                        if (bContinuar) {
                            ConsumosFacturados();
                        }
                        else {
                            $bDisbledControld = false;
                        }

                        break;
                    case '32':
                    case '69':
                        var bContinuar = true;
                        var mensaje = '';
                        if (!$iFrameActivo.contents().find('#columnsSelected ul').length) {
                            mensaje = 'Selecciona al menos una columna para el reporte.';
                            bContinuar = false;
                        }

                        var $ctrlInput = $iFrameActivo.contents().find('#Condiciones ul li input');
                        if ($ctrlInput.length > 0) {
                            $ctrlInput.each(function (index) {
                                if ($(this).val() == "") {
                                    mensaje = 'Escriba un valor al filtro ' + $(this).parent().text() + ' antes de continuar';
                                    bContinuar = false;
                                }
                            });
                        }

                        if (bContinuar) {
                            MakeReport();
                        }
                        else {
                            $bDisbledControld = false;
                            showToastMsj(mensaje, false, 'warning', 10000, 'bottom-right');
                        }
                        break;
                    case '33':
                        var bContinuar = true;
                        if ($iFrameActivo.contents().find('#txtClave').val() == '') {
                            bContinuar = false;
                            $iFrameActivo.contents().find('#txtClave').focus();
                            showToastMsj('Agregue una clave para generar reporte.', false, 'warning', 10000, 'bottom-right');
                        }

                        if (bContinuar) {
                            ExistenciaPorClave();
                        }
                        else {
                            $bDisbledControld = false;
                        }
                        break;
                    case 'test':
                        //ExistenciaSPAC();
                        break;
                    case '34':
                        initNav(true, false, false, false);
                        DisabledControls();
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $($iFrameActivo.contents().find('#dtpFechaInicial')).val();
                        var FechaFinal = $($iFrameActivo.contents().find('#dtpFechaFinal')).val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal
                        };

                        loadFrameReportBI(parametros, $iFrameActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '36':
                    case '55':
                    case '56':
                    case '57':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            Fecha: FechaInicial
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '37':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var ClaveSSA = $iFrameActivo.contents().find('#txtClaveSSA').val();
                        var FuenteFinancianciamiento = $iFrameActivo.contents().find('#txtFuenteFinanciamiento').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            Fecha: FechaInicial,
                            ClaveSSA: ClaveSSA,
                            FuenteFinancianciamiento: FuenteFinancianciamiento
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '38':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var TipoMovto = 0;
                        var ClaveSSA = $iFrameActivo.contents().find('#txtClaveSSA').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            TipoMovto: TipoMovto,
                            ClaveSSA: ClaveSSA,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '40':
                    case '41':
                    case '53':
                    case '58':
                    case '68':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '39':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var Status_Semaforizacion = $($iFrameActivo.contents().find('#cboSemaforizacion'), 'option:selected').val();
                        var Procedencia = $iFrameActivo.contents().find('#txtProcedencia').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            Fecha: FechaInicial,
                            Status_Semaforizacion: Status_Semaforizacion,
                            Procedencia: Procedencia
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '42':
                    case '61':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
                        var NumeroDePoliza = $iFrameActivo.contents().find('#txtNumeroDePoliza').val();
                        var NombreBeneficiario = $iFrameActivo.contents().find('#txtNombreBeneficiario').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            NumeroDePoliza: NumeroDePoliza,
                            NombreBeneficiario: NombreBeneficiario
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '43':
                    case '44':
                    case '62':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.contents().find('#txtClaveSSA').val();
                        var Unidad_Beneficiario = $iFrameActivo.contents().find('#txtUnidad_Beneficiario').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            Unidad_Beneficiario: Unidad_Beneficiario
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '45':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.contents().find('#txtClaveSSA').val();
                        var Benefeciario = $iFrameActivo.contents().find('#txtBeneficiario').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            Benefeciario: Benefeciario
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '46':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.contents().find('#txtClaveSSA').val();
                        var Benefeciario = $iFrameActivo.contents().find('#txtBeneficiario').val();
                        var Poliza = $iFrameActivo.contents().find('#txtNumeroDePoliza').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            Benefeciario: Benefeciario,
                            Poliza: Poliza
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '47':
                    case '48':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.contents().find('#txtClaveSSA').val();
                        var Benefeciario = $iFrameActivo.contents().find('#txtBeneficiario').val();
                        var Poliza = $iFrameActivo.contents().find('#txtNumeroDePoliza').val();
                        var Medico = $iFrameActivo.contents().find('#txtMedico').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            NombreBeneficiario: Benefeciario,
                            NumeroDePoliza: Poliza,
                            NombreMedico: Medico
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '49':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.contents().find('#txtClaveSSA').val();
                        var Medico = $iFrameActivo.contents().find('#txtMedico').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            NombreMedico: Medico
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '50':
                    case '66':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.contents().find('#txtClaveSSA').val();
                        var CIE10 = $iFrameActivo.contents().find('#txtCIE10').val();
                        var Diagnostico = $iFrameActivo.contents().find('#txtDiagnostico').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            CIE10: CIE10,
                            Diagnostico: Diagnostico
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '51':
                    case '59':
                    case '67':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
                        var Remision = $iFrameActivo.contents().find('#txtRemision').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            Remision: Remision
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '54':
                        loadFrameReportBI('', $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '60':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
                        var Servicio = $iFrameActivo.contents().find('#txtServicio').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            Servicio: Servicio
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '63':
                    case '64':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.contents().find('#txtClaveSSA').val();
                        var Benefeciario = $iFrameActivo.contents().find('#txtBeneficiario').val();
                        var Poliza = $iFrameActivo.contents().find('#txtNumeroDePoliza').val();
                        var Medico = $iFrameActivo.contents().find('#txtMedico').val();
                        var Servicio = $iFrameActivo.contents().find('#txtServicio').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            NombreBeneficiario: Benefeciario,
                            NumeroDePoliza: Poliza,
                            NombreMedico: Medico,
                            Servicio: Servicio
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '65':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.contents().find('#txtClaveSSA').val();
                        var Medico = $iFrameActivo.contents().find('#txtMedico').val();
                        var Servicio = $iFrameActivo.contents().find('#txtServicio').val();

                        var parametros = {
                            IdEmpresa: Page.Empresa(),
                            IdEstado: Page.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            NombreMedico: Medico,
                            Servicio: Servicio
                        };

                        loadFrameReportBI(parametros, $frmActivo, $iFrameActivo.contents().find('#iResult'));
                        break;
                    case '70':
                        Penalizaciones();
                        break;
                    default:
                        //Opcion en caso de no conicidir
                }
                if ($bDisbledControld) {
                    DisabledControls();
                }
            });
        }

        $Imprimir.off();
        if (bImprimir) {
            $Imprimir.on('click', function () {
                toastr.clear();
                switch ($frmActivo) {
                    case '19':
                        if ($iFrameActivo.contents().find('#ListadosVarios').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#cboListados option:selected').val());
                            $iFrameActivo.contents().find('#FrmListadosVarios').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realiace una consulta.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '20':
                        if (bRptReportesTransferencias) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($($iFrameActivo.contents().find('[name="tipoTransferencia"]:checked')).val());
                            $iFrameActivo.contents().find('#FrmReportesTransferencias').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realiace una consulta.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '28':
                    case '29':
                        var value = $iFrameActivo.contents().find('#txtFolio').val();
                        if (value != '' || value != '*') {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val(value);
                            $iFrameActivo.contents().find('#FrmRegistroPedidosEspeciales').submit();
                        } else {
                            showToastMsj('Folio de Pedido Inicial inválido, verifique.', false, 'warning', 10000, 'bottom-right');
                        }
                        break;
                    case '23':
                        if (bBtnImprimir) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find('#FrmEfectividadVales').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realiace una consulta.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '24':
                        if (bBtnImprimir) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find('#FrmSurtimientoRecetas').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realiace una consulta.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '25':
                        if (bBtnImprimir) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find('#FrmAbastoFarmacias').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realiace una consulta.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    default:
                        //Opcion en caso de no conicidir
                }
            });
        }

        $Exportar.off();
        if (bExportar) {
            $Exportar.on('click', function () {
                toastr.clear();
                switch ($frmActivo) {
                    case '2':
                    case '26':
                        if ($iFrameActivo.contents().find('#Existencia').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#cboJurisdiccion option:selected').val() + ',' + $iFrameActivo.contents().find('#cboJurisdiccion option:selected').text() + ',' + $iFrameActivo.contents().find('#cboFarmacia option:selected').val() + ',' + $iFrameActivo.contents().find('#cboFarmacia option:selected').text());
                            var frm = $frmActivo == '2' ? $iFrameActivo.contents().find('#frmExistencias') : $iFrameActivo.contents().find('#frmExistenciasEnLinea');
                            frm.submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '14':
                        if ($iFrameActivo.contents().find('#DispensancionClaves').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($($iFrameActivo.contents().find('#dtpFechaInicial')).val() + ',' + $($iFrameActivo.contents().find('#dtpFechaFinal')).val() + ',' + ($iFrameActivo.contents().find('#chkTipoTodasJuris').is(':checked') ? '1' : '0'));
                            $iFrameActivo.contents().find('#FrmDispensancionClaves').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '13':
                    case '27':
                        if ($iFrameActivo.contents().find('#ProximosACaducar').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($($iFrameActivo.contents().find('#dtpFechaInicial')).val() + ',' + $($iFrameActivo.contents().find('#dtpFechaFinal')).val());
                            var frm = $frmActivo == '13' ? $iFrameActivo.contents().find('#FrmProximosACaducar') : $iFrameActivo.contents().find('#frmProximosACaducarEnLinea');
                            frm.submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '15':
                        if ($iFrameActivo.contents().find('#MedicosDiagnostico').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#cboTipoUnidad option:selected').text() + ',' + $iFrameActivo.contents().find('#cboLocalidad option:selected').text() + ',' + $($iFrameActivo.contents().find('#dtpFechaInicial')).val() + ',' + $($iFrameActivo.contents().find('#dtpFechaFinal')).val());
                            $iFrameActivo.contents().find('#frmMedicosDiagnostico').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '16':
                        if ($iFrameActivo.contents().find('#AntibioticosControlados').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#cboTipoUnidad option:selected').text() + ',' + $iFrameActivo.contents().find('#cboLocalidad option:selected').text() + ',' + $($iFrameActivo.contents().find('#dtpFechaInicial')).val() + ',' + $($iFrameActivo.contents().find('#dtpFechaFinal')).val() + ',' + ($iFrameActivo.contents().find('#rdoAntibiotico').is(':checked') ? '0' : '1'));
                            $iFrameActivo.contents().find('#FrmAntibioticosControlados').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '17':
                        if ($iFrameActivo.contents().find('#CostoPacienteProgramaDeAtencion').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#cboTipoUnidad option:selected').text() + ',' + $iFrameActivo.contents().find('#cboLocalidad option:selected').text() + ',' + $($iFrameActivo.contents().find('#dtpFechaInicial')).val() + ',' + $($iFrameActivo.contents().find('#dtpFechaFinal')).val() + ',' + ($iFrameActivo.contents().find('#rdoConcentradoProgramaDeAtencion').is(':checked') ? '2' : '1'));
                            $iFrameActivo.contents().find('#FrmCostoPacienteProgramaDeAtencion').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '18':
                        if ($iFrameActivo.contents().find('#CortesDiarios').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#cboFarmacia option:selected').text() + ',' + $($iFrameActivo.contents().find('#dtpFechaInicial')).val());
                            $iFrameActivo.contents().find('#FrmCortesDiarios').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '21':
                        if ($iFrameActivo.contents().find('#ClavesNegadas').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#dtpFechaInicial').val());
                            $iFrameActivo.contents().find('#FrmClavesNegadas_Regional').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '22':
                        if ($iFrameActivo.contents().find('#SurtimientoInsumos').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#dtpFechaInicial').val() + ',' + $iFrameActivo.contents().find('#dtpFechaFinal').val() + ',' + ($iFrameActivo.contents().find('#rdoCauses').is(':checked') ? '1' : '2'));
                            $iFrameActivo.contents().find('#FrmTBC_Surtimiento_Regional').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', 'info', 'warning', 10000, 'bottom-right');
                        }
                        break;
                    case '24':
                        if ($iFrameActivo.contents().find('#ClavesSurtimiento').length > 0 && oTable.fnGetData(0) != null && oTableClaves.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('DownloadExcel');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val(jsonData['Empresa'] + ',' + jsonData['Farmacia'] + ',' + jsonData['Periodo'] + ',' + jsonData['FechaReporte']);
                            $iFrameActivo.contents().find('#FrmSurtimientoRecetas').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '20':
                        if ($iFrameActivo.contents().find('#ReportesTransferencias').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('DownloadExcel');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('[name="tipoTransferencia"]:checked').val() + ',' + $iFrameActivo.contents().find('#lblFarmacia').val() + ',' + $iFrameActivo.contents().find('#dtpFechaInicial').val() + ',' + $iFrameActivo.contents().find('#dtpFechaFinal').val());
                            $iFrameActivo.contents().find('#FrmReportesTransferencias').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '30':
                        if (bBtnImprimir && $iFrameActivo.contents().find('#cboReporte option:selected').val() != '0') {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('DownloadExcel');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#cboReporte option:selected').val() + ',' + $iFrameActivo.contents().find('#cboReporte option:selected').text() + ',' + $iFrameActivo.contents().find('#dtpFechaInicial').val() + ',' + $iFrameActivo.contents().find('#dtpFechaFinal').val() + ',' + $iFrameActivo.contents().find('#txtFarmacia').val() + ',' + $iFrameActivo.contents().find('#lblFarmacia').val());
                            $iFrameActivo.contents().find('#FrmReportesFacturacionUnidad').submit();
                        } else {
                            if (!bBtnImprimir) {
                                showToastMsj('No se encontro información con los criterios especificados.', true, 'warning', 10000, 'bottom-full-width');
                            } else if ($iFrameActivo.contents().find('#cboReporte option:selected').val() == '0') {
                                $iFrameActivo.contents().find('#cboReporte').focus();
                                showToastMsj('Debe seleccionar un reporte para poder realizar la exportación.', true, 'warning', 10000, 'bottom-right');
                            }

                        }
                        break;
                    case '31':
                        //if ($iFrameActivo.contents().find('#ConsumosFacturados').length > 0 && oTable.fnGetData(0) != null) {
                        if (bBtnImprimir) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#cboFarmacia option:selected').text() + ',' + $iFrameActivo.contents().find('#dtpFechaInicial').val());
                            $iFrameActivo.contents().find('#FrmConsumosFacturados').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', 'info', 'warning', 10000, 'bottom-right');
                        }
                        break;
                    case '32':
                    case '69':
                        if (bBtnImprimir) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#cboFarmacia option:selected').text() + ',' + $iFrameActivo.contents().find('#dtpFechaInicial').val() + ',' + $iFrameActivo.contents().find('#dtpFechaFinal').val() + ',' + ($iFrameActivo.contents().find('#TipoInformacion').length ? $iFrameActivo.contents().find('#TipoInformacion [name="tipoDeInformacion"]:checked').val() : 2));
                            $iFrameActivo.contents().find('#frmReporteador').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', 'info', 'warning', 10000, 'bottom-right');
                        }
                        break;
                    case '33':
                        if ($iFrameActivo.contents().find('#ExistenciaPorClave').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#txtClave').val() + ',' + $iFrameActivo.contents().find('#dtpFechaInicial').val() + ',' + $iFrameActivo.contents().find('#txtDescripcion').val());
                            $iFrameActivo.contents().find('#frmExistenciaPorcClave').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', 'info', 'warning', 10000, 'bottom-right');
                        }
                        break;
                    case 'test':
                        if ($iFrameActivo.contents().find('#ExistenciaSPAC').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val($iFrameActivo.contents().find('#cboJurisdiccion option:selected').val() + ',' + $iFrameActivo.contents().find('#cboJurisdiccion option:selected').text() + ',' + $iFrameActivo.contents().find('#cboFarmacia option:selected').val() + ',' + $iFrameActivo.contents().find('#cboFarmacia option:selected').text());
                            $iFrameActivo.contents().find('#frmExistenciaSinProximosACaducar').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta o inténte con criterios diferentes.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    case '70':
                        if ($iFrameActivo.contents().find('#Penalizaciones').length > 0 && oTable.fnGetData(0) != null) {
                            $iFrameActivo.contents().find("#EVENTTARGET").val('Download');
                            $iFrameActivo.contents().find("#EVENTARGUMENT").val();
                            $iFrameActivo.contents().find('#frmPenalizacionTapachula').submit();
                        } else {
                            showToastMsj('No existe información para exportar, realice una consulta.', false, 'info', 10000, 'bottom-full-width');
                        }
                        break;
                    default:
                        //Opcion en caso de no conicidir
                }
            });
        }
    }

    function initRegistroPedidos() {
        $iFrameActivo.contents().find('#txtFolio').off();
        $iFrameActivo.contents().find('#txtFolio').focus();
        var $cboPerfil = $iFrameActivo.contents().find('#cboPerfil');
        var $cboBeneficiario = $iFrameActivo.contents().find('#cboBeneficiario');
        var $txtReferencia = $iFrameActivo.contents().find('#txtReferencia');
        $cboPerfil.attr('disabled', 'disabled');
        $cboBeneficiario.attr('disabled', 'disabled');
        $txtReferencia.attr('disabled', 'disabled');

        $iFrameActivo.contents().find('#txtFolio').tooltip({
            position: {
                my: "left+15 center",
                at: "right center",
                using: function (position, feedback) {
                    $(this).css(position);
                    $("<div>")
                        .addClass("arrow")
                        .addClass(feedback.vertical)
                        .addClass(feedback.horizontal)
                        .appendTo(this);
                }
            }
        });

        $iFrameActivo.contents().find('#txtFolio').tooltip('open');

        $iFrameActivo.contents().find('#txtFolio').hover(
            function () {
                $iFrameActivo.contents().find('#txtFolio').tooltip('open');
            }, function () {
                $iFrameActivo.contents().find('.ui-tooltip').fadeOut();
            }
        );


        $iFrameActivo.contents().find('#txtFolio').on('keydown', function (event) {
            if (event.which == 13) {
                event.preventDefault();
                if ($(this).val() == '') {
                    initNav(true, true, false, false);
                    $iFrameActivo.contents().find('.ui-tooltip').fadeOut();
                    $iFrameActivo.contents().find('#txtFolio').val('*');
                    $iFrameActivo.contents().find('#txtFolio').attr('disabled', 'disabled');
                    $iFrameActivo.contents().find('#txtObservaciones').removeAttr('disabled', 'disabled');
                    $cboPerfil.removeAttr('disabled', 'disabled');
                    $txtReferencia.removeAttr('disabled', 'disabled');
                    $cboPerfil.focus();
                }
                else if ($(this).val() != '*') {
                    $iFrameActivo.contents().find('.ui-tooltip').fadeOut();
                    GetRegistroPedidosEspeciales($(this).val());
                    initNav(true, false, true, false);
                }
            }

            switch (event.keyCode) {
                case 8:  // Backspace
                case 37: // Left
                case 39: // Right
                case 46: // Delete
                case 48: // 0
                case 49: // 1
                case 50: // 2
                case 51: // 3
                case 52: // 4
                case 53: // 5
                case 54: // 6
                case 55: // 7
                case 56: // 8
                case 57: // 9
                    break;
                default:

                    return false;
            }

        });

        $cboPerfil.change(function () {
            $("option:selected", $cboPerfil).each(function () {
                $cboPerfil.attr('disabled', 'disabled');
                Page.filterInfoPedidos('Beneficiario');
            });
        });

        $cboBeneficiario.change(function () {
            $("option:selected", $cboBeneficiario).each(function () {
                $cboBeneficiario.attr('disabled', 'disabled');
                Page.AjaxLoading('Mostrar');
                Page.AjaxLoadingText('');
                Page.AjaxLoad('Mostrar');
                var aData = Page.filterInfoPedidos('Cuadro');
                $iFrameActivo.contents().find('#TablaClaves').html(aData[0]["Tabla"]);
                initTablaPedidos();
                aData = oTable.fnSettings().aoData;
                $.each(aData, function (index, element) {
                    var nTr = oTable.fnSettings().aoData[index].nTr;
                    nTr.cells.item(5).className = "edit cantidad";
                });
                fnTableDraw(oTable);
                Page.AjaxLoading('Ocultar');
                Page.AjaxLoad('Ocultar');

            });
        });
    }

    function initPedidos() {
        $iFrameActivo.contents().find('#txtFolio').off();
        $iFrameActivo.contents().find('#txtFolio').focus();
        $iFrameActivo.contents().find('#txtFolio').tooltip({
            position: {
                my: "left+15 center",
                at: "right center",
                using: function (position, feedback) {
                    $(this).css(position);
                    $("<div>")
                        .addClass("arrow")
                        .addClass(feedback.vertical)
                        .addClass(feedback.horizontal)
                        .appendTo(this);
                }
            }
        });

        $iFrameActivo.contents().find('#txtFolio').tooltip('open');

        $iFrameActivo.contents().find('#txtFolio').hover(
            function () {
                $iFrameActivo.contents().find('#txtFolio').tooltip('open');
            }, function () {
                $iFrameActivo.contents().find('.ui-tooltip').fadeOut();
            }
        );


        $iFrameActivo.contents().find('#txtFolio').on('keydown', function (event) {
            if (event.which == 13) {
                event.preventDefault();
                if ($(this).val() == '') {
                    initNav(true, true, false, false);
                    $iFrameActivo.contents().find('.ui-tooltip').fadeOut();
                    $iFrameActivo.contents().find('#txtFolio').val('*');
                    $iFrameActivo.contents().find('#txtFolio').attr('disabled', 'disabled');
                    $iFrameActivo.contents().find('#txtObservaciones').removeAttr('disabled', 'disabled');
                    $iFrameActivo.contents().find('#txtObservaciones').focus();

                    initTablaPedidos();
                    initBtnsTablaPedidos();
                }
                else if ($(this).val() != '*') {
                    $iFrameActivo.contents().find('.ui-tooltip').fadeOut();
                    GetPedidosEspeciales($(this).val());
                    initNav(true, false, true, false);
                }
            }

            switch (event.keyCode) {
                case 8:  // Backspace
                case 37: // Left
                case 39: // Right
                case 46: // Delete
                case 48: // 0
                case 49: // 1
                case 50: // 2
                case 51: // 3
                case 52: // 4
                case 53: // 5
                case 54: // 6
                case 55: // 7
                case 56: // 8
                case 57: // 9
                    break;
                default:

                    return false;
            }

        });
    }

    //-----------------------------------------------//
    function initTablaPedidos() {
        oTable = $iFrameActivo.contents().find('#TablaClaves').dataTable(
                {
                    "bScrollInfinite": true,
                    "sScrollY": $iFrameActivo.contents().find('#Tabla').height() - 40 + 'px',
                    "sScrollX": "100%",
                    "bFilter": true,
                    "bSearchable": false,
                    "bSort": false,
                    "aaSortingFixed": [[1, 'asc']],
                    "sDom": 'rt',
                    "iDisplayLength": 999999,
                    "aoColumnDefs": [
                        { "bVisible": false, "aTargets": [1] },
                        { "bSortable": false, "aTargets": [1] },
                        { "sClass": "center", "aTargets": [0, 2, 4, 5, 6, 7] },
                        { "sWidth": "250px", "aTargets": [3] }
                    ],
                    "oLanguage": {
                        "sLengthMenu": "Mostrar _MENU_ registros por pagina.",
                        "sZeroRecords": "Haga click en el botón (+), para agregar información.",
                        "sInfo": "Mostrando desde _START_ hasta _END_ de _TOTAL_ registros",
                        "sInfoEmpty": "Mostrando desde 0 hasta 0 de 0 registros.",
                        "sInfoFiltered": "(filtrado de _MAX_ registros en total).",
                        "sLoadingRecords": "Cargando...",
                        "sProcessing": "Procesando..."
                    },
                    "bDeferRender": true,
                    "fnDrawCallback": function (oSettings) {
                        /* Need to redo the counters if filtered or sorted */
                        if (oSettings.bSorted || oSettings.bFiltered) {
                            for (var i = 0, iLen = oSettings.aiDisplay.length; i < iLen; i++) {
                                $('td:eq(0)', oSettings.aoData[oSettings.aiDisplay[i]].nTr).html(i + 1);
                            }
                        }
                    }
                });


        oTable.$('tbody tr').click(function (e) {
            if ($(this).hasClass('row_selected')) {
                $(this).removeClass('row_selected');
            }
            else {
                oTable.$('tr.row_selected').removeClass('row_selected');
                $(this).addClass('row_selected');
            }
        });

    }

    function fnTableDraw(oTableLocal) {

        oTableLocal.$('tbody tr').off();
        oTableLocal.$('td').off();
        oTableLocal.fnDraw();

        oTableLocal.$('tbody tr').click(function (e) {
            if ($(this).hasClass('row_selected')) {
                $(this).removeClass('row_selected');
            }
            else {
                oTableLocal.$('tr.row_selected').removeClass('row_selected');
                $(this).addClass('row_selected');
            }
        });

        var bExists = false;
        var aData = '';

        oTableLocal.$('td.edit').editable(function (value, settings) {
            bExists = false;
            if ($(this).hasClass('clave')) {
                oTable.$('tbody tr').each(function (i) {
                    $(this).children("td").each(function (j) {
                        switch (j) {
                            case 0:
                                if (value == $(this).text()) {
                                    bExists = true;
                                    showToastMsj('Esta Clave ya se encuentra capturada en otro renglon.', false, 'warning', 10000, 'bottom-right');
                                }
                                break;
                        }
                    });
                    if (bExists) {
                        oTable.$('tr.row_selected').removeClass('row_selected');
                        $(this).addClass('row_selected');
                        return false;
                    }
                });
            }

            if (value == "") {
                bExists = true;
            }

            if (bExists) {
                return false;
            }

            return parseInt(value);

        }, {
            type: 'text',
            loadtext: 'Cargando...',
            placeholder: '0',
            tooltip: 'Click para editar',
            height: '32px',
            callback: function (value, settings) {
                $(this).find('form input').hide();
                if (!bExists) {
                    var $oTr = oTable.$(this).parent();
                    if ($(this).hasClass('clave')) {
                        var bFound = false;
                        var aDataFound = '';

                        if (aData == '') {
                            initTableHelp('', '');
                            aData = oTableAyuda.fnSettings().aoData;
                            oTableAyuda.fnDestroy();
                        }

                        /* Search the current results */
                        for (i = 0; i < aData.length; i++) {
                            if (aData[i]._aData[1] == value) {
                                oTable.fnUpdate([aData[i]._aData[0], aData[i]._aData[1], aData[i]._aData[2], aData[i]._aData[3], aData[i]._aData[4], 0, 0], $oTr.get(0));
                                var nTr = $oTr.children();
                                $(nTr[0]).removeClass('edit');
                                $(nTr[0]).attr('title', '');
                                $(nTr[5]).addClass('edit');

                                fnTableDraw(oTable);
                                bNuevoRegistro = true;
                                bFound = true;
                            }
                        }
                        if (!bFound) {
                            $(this).text('');
                            fnTableDraw(oTable);
                            showToastMsj('Sal no encontrada ó no esta Asignada a la Farmacia.', false, 'warning', 10000, 'bottom-right');
                        }

                    } else if ($(this).hasClass('cantidad')) {
                        var ContPaq = $oTr.children();
                        var result = parseInt(value * $(ContPaq[4]).text());
                        if (value % $(ContPaq[3]).text() > 0) {
                            result += 1;
                        }
                        /*oTable.fnUpdate(result, $oTr.get(0), 6);
                        oTable.fnUpdate(value, $oTr.get(0), 7);*/
                        oTable.fnUpdate(result, $oTr.get(0), 7);
                        oTable.fnUpdate(value, $oTr.get(0), 6);

                        fnTableDraw(oTable);
                    }
                }
            }
        }).on('keydown', function (event) {
            switch (event.keyCode) {
                case 8:  // Backspace
                case 37: // Left
                case 39: // Right
                case 46: // Delete
                case 48: // 0
                case 49: // 1
                case 50: // 2
                case 51: // 3
                case 52: // 4
                case 53: // 5
                case 54: // 6
                case 55: // 7
                case 56: // 8
                case 57: // 9
                    break;
                case 112: //F1
                case 113: //F2
                case 114: //F3
                    event.preventDefault();
                    $iFrameActivo.contents().find('#Mask').fadeIn(500);
                    initTableHelp([{ "bVisible": false, "aTargets": [0]}], [[1, "asc"]]);
                    $iFrameActivo.contents().find('#MsjRpt input[type="text"]').focus();
                    var nTrs = $(this).parent();

                    $iFrameActivo.contents().find('#btnAdd').off();
                    $iFrameActivo.contents().find('#btnAdd').on('click', function (e) {
                        var nTrSelected = fnGetSelected(oTableAyuda);
                        var aData = oTableAyuda.fnGetData(nTrSelected.get(0));
                        if (aData[0] != '' || aData[0] == undefined) {
                            if (!ExistsRow(aData[1])) {
                                oTable.fnUpdate([aData[0], aData[1], aData[2], aData[3], aData[4], 0, 0], nTrs.get(0));
                                var nTr = nTrs.children();
                                $(nTr[0]).removeClass('edit');
                                $(nTr[0]).attr('title', '');
                                $(nTr[5]).addClass('edit');

                                fnTableDraw(oTable);
                                bNuevoRegistro = true;
                            }

                            $iFrameActivo.contents().find('#close').click();
                        }
                        else {
                            showToastMsj('De click en alguna clave para agregarla.', false, 'info', 10000, 'bottom-full-width');
                        }
                    });

                    $iFrameActivo.contents().find('#close').off();
                    $iFrameActivo.contents().find('#close').on('click', function () {
                        $iFrameActivo.contents().find('#Mask').fadeOut(500);
                        oTableAyuda.fnDestroy();
                    });
                    break;
                case 38: //up
                    $(this).find('form').submit();
                    var iRowActual = oTable.fnGetPosition($(this).parents().get(0))
                    var oNextRow = oTable.fnGetNodes(iRowActual - 1);
                    if (oNextRow != null) {
                        $(oNextRow).find('td.edit').click();
                    }
                    return false;
                    break;
                case 9:  // tab
                case 13: // Enter
                case 40: //down
                    $(this).find('form').submit();
                    var iRowActual = oTable.fnGetPosition($(this).parents().get(0))
                    var oNextRow = oTable.fnGetNodes(iRowActual + 1);
                    if (oNextRow != null) {
                        $(oNextRow).find('td.edit').click();
                    }
                    return false;
                    break;
                default:

                    return false;
            }
        });

    }

    function ExistsRow(value) {
        var bReturn = false;
        oTable.$('tbody tr').each(function (i) {
            $(this).children("td").each(function (j) {
                switch (j) {
                    case 0:
                        if (value == $(this).text()) {
                            bReturn = true;
                            showToastMsj('Esta Clave ya se encuentra capturada en otro renglon.', false, 'warning', 10000, 'bottom-right');
                            oTable.$('tr.row_selected').removeClass('row_selected');
                            $(this).parent().addClass('row_selected');
                        }
                        break;
                }
            });
        });
        return bReturn;
    }

    function bValidarTable() {
        var bContinua = true;
        var iCont = 0;
        if (oTable.fnSettings().fnRecordsTotal() > 0) {
            var aData = oTable.fnSettings().aoData;

            for (i = 0; i < aData.length; i++) {
                if (aData[i]._aData[6] != '0') {
                    iCont++;
                } else if (aData[i]._aData[6] == '') {
                    oTable.fnDeleteRow(i);
                    bNuevoRegistro = true;
                    fnTableDraw(oTable);
                }
            }

            if (iCont == 0) {
                bContinua = false;
            }
        } else {
            bContinua = false;
        }
        return bContinua;
    }

    function initBtnsTablaPedidos() {
        /* Add a click handler for the delete row */
        $($iFrameActivo.contents().find('#delrecord')).off();
        $($iFrameActivo.contents().find('#delrecord')).click(function () {
            var anSelected = fnGetSelected(oTable);
            if (anSelected.length !== 0) {
                oTable.fnDeleteRow(anSelected[0]);
                fnTableDraw(oTable);
            }
        });

        /* Add a click handler for the delete row */
        $($iFrameActivo.contents().find('#newrecord')).off();
        $($iFrameActivo.contents().find('#newrecord')).click(function () {
            if (bNuevoRegistro) {
                var a = oTable.fnAddData(['', '', '', '', '', '', '']);
                var nTr = oTable.fnSettings().aoData[a[0]].nTr;
                nTr.cells.item(0).className = "edit clave";
                nTr.cells.item(5).className = "cantidad";

                fnTableDraw(oTable);
                bNuevoRegistro = false;
            }
        });
    }

    //-----------------------------------------------//
    function initClaveSSA() {
        $iFrameActivo.contents().find('#txtClave').off();

        $iFrameActivo.contents().find('#txtClave').keydown(function (event) {
            if (event.shiftKey) {
                event.preventDefault();
            }


            if (event.ctrlKey && (event.which == 86 || event.which == 118)) {
                event.preventDefault();
            } else if (event.keyCode == 13) {
                toastr.options = {};
                toastr.clear();
                var aData = '';
                var bFound = false;
                var value = $(this).val();

                if (value != '') {
                    //if (aData == '') {
                    //initTableHelpByID('', '', 'AyudaClaves');
                    //initTableHelpById([{ "sClass": "center", "aTargets": [0]}], [[0, "asc"]], 'AyudaClaves');
                    aData = oTableAyuda.fnSettings().aoData;
                    //oTableAyuda.fnDestroy();
                    //}

                    /* Search the current results */
                    for (i = 0; i < aData.length; i++) {
                        if (aData[i]._aData[0] == value) {
                            //var nTr = $oTr.children();
                            $(this).val(aData[i]._aData[0]);
                            $(this).attr('disabled', 'disabled');
                            $iFrameActivo.contents().find('#txtDescripcion').val(aData[i]._aData[1]);
                            bFound = true;
                        }
                    }
                }

                if (!bFound) {
                    $iFrameActivo.contents().find('#Mask').fadeIn(500);
                    //initTableHelpById([{ "sClass": "center", "aTargets": [0]}], [[0, "asc"]], 'AyudaClaves');
                    //oTableAyuda.fnFilter('', 0, true, true, true, true);
                    initbtnAddHelp();
                    //showToastMsj('Sal no encontrada.', false, 'warning', 10000, 'bottom-right');
                    //showToastMsj('No se encontró coincidencia con la clave: ' + $(this).val(), true, 'warning', 10000, 'bottom-full-width');
                    //$(this).val('');
                    //GetDesClaveSSA($(this).val());
                    $iFrameActivo.contents().find('#close').off();
                    $iFrameActivo.contents().find('#close').on('click', function () {
                        $iFrameActivo.contents().find('#Mask').fadeOut(0);
                        $iFrameActivo.contents().find('#txtClave').focus();
                        //oTableAyuda.fnDestroy();
                    });

                    $iFrameActivo.contents().find('#MsjClavesSSA input[type="text"]').focus();
                }

            } else if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 110 || event.keyCode == 190) {
                //
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

        $iFrameActivo.contents().find('#txtClave').focus();

    }

    function GetDesClaveSSA(ClaveSSA) {
        var parametros = {
            sClaveSSA: ClaveSSA
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/GetClaveSSA",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                if (res.d != '') {
                    $iFrameActivo.contents().find('#txtClave').attr('disabled', 'disabled');
                    $iFrameActivo.contents().find('#txtDescripcion').val(res.d);
                }
                else {
                    showToastMsj('No se encontró coincidencia con la clave: ' + ClaveSSA, true, 'warning', 10000, 'bottom-full-width');
                }
            },
            error: Error
        }).done(function () {
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }


    function initIdFarmacia() {
        $iFrameActivo.contents().find('#txtFarmacia').off();
        $iFrameActivo.contents().find('#txtFarmacia').on('keydown', function (event) {
            switch (event.keyCode) {
                case 8:  // Backspace
                case 37: // Left
                case 39: // Right
                case 46: // Delete
                case 48: // 0
                case 49: // 1
                case 50: // 2
                case 51: // 3
                case 52: // 4
                case 53: // 5
                case 54: // 6
                case 55: // 7
                case 56: // 8
                case 57: // 9
                    break;
                case 13: // Enter
                case 112: //F1
                case 113: //F2
                case 114: //F3
                    var bFound = false;
                    event.preventDefault();
                    var aDataInfoGeneral = Page.GetaDataInfoGeneral();
                    var aDataReturn = '';
                    var IdFarmacia = fnPonCeros($iFrameActivo.contents().find('#txtFarmacia').val(), 4);

                    if (IdFarmacia != '') {
                        aDataReturn = $.grep(aDataInfoGeneral, function (element, index) {
                            return element.IdFarmacia == IdFarmacia;
                        });
                    }

                    if (aDataReturn != '') { bFound = true; }

                    if (bFound) {
                        $iFrameActivo.contents().find('#txtFarmacia').attr('disabled', 'disabled');
                        $iFrameActivo.contents().find('#txtFarmacia').val(aDataReturn[0].IdFarmacia);
                        $iFrameActivo.contents().find('#lblFarmacia').val(aDataReturn[0].Farmacia);
                        $iFrameActivo.contents().find('#lblFarmacia').attr('title', aDataReturn[0].Farmacia);

                        $iFrameActivo.contents().find('#txtPrograma').removeAttr('disabled', 'disabled').focus();

                        $iFrameActivo.contents().find('#txtFarmacia').off();
                        $iFrameActivo.contents().find('#msjFarmacia').fadeOut();
                    } else {
                        $iFrameActivo.contents().find('#Mask').fadeIn(500);
                        initTableHelp([{ "sClass": "center", "aTargets": [0, 2]}], [[0, "asc"]]);
                        initbtnAddHelp();
                    }
                    break;
                default:
                    return false;
            }

        });

        $iFrameActivo.contents().find('#txtFarmacia').focus(function () {
            $iFrameActivo.contents().find('#msjFarmacia').fadeIn();
        }).blur(function () {
            $iFrameActivo.contents().find('#msjFarmacia').fadeOut();
        });

        $iFrameActivo.contents().find('#close').on('click', function () {
            $iFrameActivo.contents().find('#Mask').fadeOut(500);
            oTableAyuda.fnDestroy();
            $iFrameActivo.contents().find('#txtFarmacia').focus();
        });


        $iFrameActivo.contents().find('#txtFarmacia').focus();

    }

    function initPro_SubPro() {
        $iFrameActivo.contents().find('#txtPrograma').off();
        $iFrameActivo.contents().find('#txtPrograma').on('keydown', function (event) {
            switch (event.keyCode) {
                case 8:  // Backspace
                case 37: // Left
                case 39: // Right
                case 46: // Delete
                case 48: // 0
                case 49: // 1
                case 50: // 2
                case 51: // 3
                case 52: // 4
                case 53: // 5
                case 54: // 6
                case 55: // 7
                case 56: // 8
                case 57: // 9
                    break;
                case 13: // Enter
                case 112: //F1
                case 113: //F2
                case 114: //F3
                    var bFound = false;
                    event.preventDefault();
                    var aDataInfoGeneral = Page.GetPro_SubPro();
                    var aDataReturn = '';
                    var IdPrograma = fnPonCeros($iFrameActivo.contents().find('#txtPrograma').val(), 4);

                    if (IdPrograma != '') {
                        aDataReturn = $.grep(aDataInfoGeneral.Pro_SubPro, function (element, index) {
                            return element.IdFarmacia == $iFrameActivo.contents().find('#txtFarmacia').val() && element.IdPrograma == IdPrograma;
                        });
                    }

                    if (aDataReturn != '') { bFound = true; }

                    if (bFound) {
                        $iFrameActivo.contents().find('#txtPrograma').attr('disabled', 'disabled');
                        $iFrameActivo.contents().find('#txtPrograma').val(aDataReturn[0].IdPrograma);
                        $iFrameActivo.contents().find('#lblPrograma').val(aDataReturn[0].Programa);
                        $iFrameActivo.contents().find('#lblPrograma').attr('title', aDataReturn[0].Programa);

                        $iFrameActivo.contents().find('#txtSubPrograma').removeAttr('disabled', 'disabled').focus();
                    } else {
                        aDataReturn = $.grep(aDataInfoGeneral.Pro_SubPro, function (element, index) {
                            return element.IdFarmacia == $iFrameActivo.contents().find('#txtFarmacia').val();
                        });
                        var aColumns = ['IdPrograma', 'Programa'];

                        $iFrameActivo.contents().find('.helpcontent').fadeOut(500);
                        $iFrameActivo.contents().find('#MsjPro').html(buildHtmlTable(aDataReturn, aColumns, 'tableProgramas', '')).fadeIn(500);
                        $iFrameActivo.contents().find('#Mask').fadeIn(500);

                        initTableHelpById([{ "sClass": "center", "aTargets": [0]}], [[0, "asc"]], 'tableProgramas');
                        initbtnAddHelp();
                    }
                    break;
                default:
                    return false;
            }

        });

        $iFrameActivo.contents().find('#txtSubPrograma').off();
        $iFrameActivo.contents().find('#txtSubPrograma').on('keydown', function (event) {
            switch (event.keyCode) {
                case 8:  // Backspace
                case 37: // Left
                case 39: // Right
                case 46: // Delete
                case 48: // 0
                case 49: // 1
                case 50: // 2
                case 51: // 3
                case 52: // 4
                case 53: // 5
                case 54: // 6
                case 55: // 7
                case 56: // 8
                case 57: // 9
                    break;
                case 13: // Enter
                case 112: //F1
                case 113: //F2
                case 114: //F3
                    var bFound = false;
                    event.preventDefault();
                    var aDataInfoGeneral = Page.GetPro_SubPro();
                    var aDataReturn = '';
                    var IdSubPrograma = fnPonCeros($iFrameActivo.contents().find('#txtSubPrograma').val(), 4);

                    if (IdSubPrograma != '') {
                        aDataReturn = $.grep(aDataInfoGeneral.Pro_SubPro, function (element, index) {
                            return element.IdFarmacia == $iFrameActivo.contents().find('#txtFarmacia').val() && element.IdPrograma == $iFrameActivo.contents().find('#txtPrograma').val() && element.IdSubPrograma == IdSubPrograma;
                        });
                    }

                    if (aDataReturn != '') { bFound = true; }

                    if (bFound) {
                        $iFrameActivo.contents().find('#txtSubPrograma').attr('disabled', 'disabled');
                        $iFrameActivo.contents().find('#txtSubPrograma').val(aDataReturn[0].IdSubPrograma);
                        $iFrameActivo.contents().find('#lblSubPrograma').val(aDataReturn[0].SubPrograma);
                        $iFrameActivo.contents().find('#lblSubPrograma').attr('title', aDataReturn[0].SubPrograma);

                    } else {

                        aDataReturn = $.grep(aDataInfoGeneral.Pro_SubPro, function (element, index) {
                            return element.IdFarmacia == $iFrameActivo.contents().find('#txtFarmacia').val() && element.IdPrograma == $iFrameActivo.contents().find('#txtPrograma').val();
                        });

                        var aColumns = ['IdSubPrograma', 'SubPrograma'];

                        $iFrameActivo.contents().find('.helpcontent').fadeOut(500);
                        $iFrameActivo.contents().find('#MsjSubPro').html(buildHtmlTable(aDataReturn, aColumns, 'tableSubProgramas', '')).fadeIn(500);
                        $iFrameActivo.contents().find('#Mask').fadeIn(500);

                        initTableHelpById([{ "sClass": "center", "aTargets": [0]}], [[0, "asc"]], 'tableSubProgramas');
                        initbtnAddHelp();


                        $iFrameActivo.contents().find('#Mask').fadeIn(500);

                    }
                    break;
                default:
                    return false;
            }

        });

    }

    function initbtnAddHelp() {
        $iFrameActivo.contents().find('#btnAdd').off();
        $iFrameActivo.contents().find('#btnAdd').on('click', function (e) {
            var nTds = $('td', oTableAyuda.$('tr.row_selected'));
            if ($(nTds[0]).text() != '' || $(nTds[0]).text() == undefined) {

                /*
                $iFrameActivo.contents().find('.helpcontent').fadeOut(500);
                $iFrameActivo.contents().find('#MsjPro').html(buildHtmlTable(aDataReturn, aColumns, 'tableProgramas', '')).fadeIn(500);
                $iFrameActivo.contents().find('#Mask').fadeIn(500);
                */

                if ($iFrameActivo.contents().find('#MsjRpt').is(':visible')) {

                    $iFrameActivo.contents().find('#txtFarmacia').attr('disabled', 'disabled');
                    $iFrameActivo.contents().find('#txtFarmacia').val($(nTds[0]).text());
                    $iFrameActivo.contents().find('#lblFarmacia').val($(nTds[1]).text());
                    $iFrameActivo.contents().find('#close').click();

                    $iFrameActivo.contents().find('#txtPrograma').removeAttr('disabled', 'disabled').focus();

                    $iFrameActivo.contents().find('#txtFarmacia').off();
                    $iFrameActivo.contents().find('#msjFarmacia').fadeOut();

                } else if ($iFrameActivo.contents().find('#MsjPro').is(':visible')) {

                    $iFrameActivo.contents().find('#txtPrograma').attr('disabled', 'disabled');
                    $iFrameActivo.contents().find('#txtPrograma').val($(nTds[0]).text());
                    $iFrameActivo.contents().find('#lblPrograma').val($(nTds[1]).text());
                    $iFrameActivo.contents().find('#close').click();

                    $iFrameActivo.contents().find('#txtSubPrograma').removeAttr('disabled', 'disabled').focus();

                } else if ($iFrameActivo.contents().find('#MsjSubPro').is(':visible')) {

                    $iFrameActivo.contents().find('#txtSubPrograma').attr('disabled', 'disabled');
                    $iFrameActivo.contents().find('#txtSubPrograma').val($(nTds[0]).text());
                    $iFrameActivo.contents().find('#lblSubPrograma').val($(nTds[1]).text());
                    $iFrameActivo.contents().find('#close').click();

                } else if ($iFrameActivo.contents().find('#MsjClavesSSA').is(':visible')) {
                    $iFrameActivo.contents().find('#txtClave').attr('disabled', 'disabled');
                    $iFrameActivo.contents().find('#txtClave').val($(nTds[0]).text());
                    $iFrameActivo.contents().find('#txtDescripcion').val($(nTds[1]).text());
                    $iFrameActivo.contents().find('#txtDescripcion').attr('title', $(nTds[1]).text());
                    $iFrameActivo.contents().find('#close').click();

                }
            }
            else {
                showToastMsj('De click en algun registro para seleccionarlo y poder agregarlo.', false, 'warning', 10000, 'bottom-right');
            }
        });
    }

    function initTableHelp(options, sort) {
        var $idtable = $iFrameActivo.contents().find('#MsjRpt table');
        $($iFrameActivo.contents().find('#' + $idtable.attr('id'))).dataTable().fnDestroy();
        oTableAyuda = $($iFrameActivo.contents().find('#' + $idtable.attr('id'))).dataTable(
                    {
                        "bScrollInfinite": true,
                        "sScrollY": ($($iFrameActivo.contents().find('#MsjRpt')).height() - 128) + 'px',
                        "sScrollX": "100%",
                        "aaSorting": sort,
                        "bFilter": true,
                        "bSearchable": false,
                        "bSort": true,
                        "sDom": "lfrtip",
                        "iDisplayLength": 999999,
                        "aoColumnDefs": options,
                        "oLanguage": {
                            "sLengthMenu": "Mostrar _MENU_ registros por pagina.",
                            "sZeroRecords": "No existe información para mostrar.",
                            "sInfo": "Mostrando desde _START_ hasta _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando desde 0 hasta 0 de 0 registros.",
                            "sInfoFiltered": "(filtrado de _MAX_ registros en total).",
                            "sLoadingRecords": "Cargando...",
                            "sProcessing": "Procesando...",
                            "sSearch": "Búsqueda"
                        }
                    });

        oTableAyuda.$('tbody tr').off();
        oTableAyuda.$('tbody tr').click(function (e) {
            if ($(this).hasClass('row_selected')) {
                $(this).removeClass('row_selected');
            }
            else {
                oTableAyuda.$('tr.row_selected').removeClass('row_selected');
                $(this).addClass('row_selected');
            }
        });
    }

    function initTableHelpById(options, sort, id) {
        var $idtable = $iFrameActivo.contents().find('#' + id);
        $($iFrameActivo.contents().find('#' + $idtable.attr('id'))).dataTable().fnDestroy();
        var scrollY = 400;
        if ($iFrameActivo.contents().find('#MsjRpt').length) {
            scrollY = $iFrameActivo.contents().find('#MsjRpt').height() - 128;
        }
        else if ($iFrameActivo.contents().find('.helpcontent').length) {
            scrollY = $iFrameActivo.contents().find('.helpcontent').height() - 128;
        }


        oTableAyuda = $($iFrameActivo.contents().find('#' + $idtable.attr('id'))).dataTable(
                    {
                        "bScrollInfinite": true,
                        "sScrollY": scrollY + 'px',
                        "sScrollX": "100%",
                        "aaSorting": sort,
                        "bFilter": true,
                        "bSearchable": false,
                        "bSort": true,
                        "sDom": "lfrtip",
                        "iDisplayLength": 999999,
                        "aoColumnDefs": options,
                        "oLanguage": {
                            "sLengthMenu": "Mostrar _MENU_ registros por pagina.",
                            "sZeroRecords": "No existe información para mostrar.",
                            "sInfo": "Mostrando desde _START_ hasta _END_ de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando desde 0 hasta 0 de 0 registros.",
                            "sInfoFiltered": "(filtrado de _MAX_ registros en total).",
                            "sLoadingRecords": "Cargando...",
                            "sProcessing": "Procesando...",
                            "sSearch": "Búsqueda"
                        }
                    });

        oTableAyuda.$('tbody tr').off();
        oTableAyuda.$('tbody tr').click(function (e) {
            if ($(this).hasClass('row_selected')) {
                $(this).removeClass('row_selected');
            }
            else {
                oTableAyuda.$('tr.row_selected').removeClass('row_selected');
                $(this).addClass('row_selected');
            }
        });
    }

    function initComboJF() {
        $Jurisdiccion.change(function () {
            $("option:selected", $Jurisdiccion).each(function () {
                Page.filterInfo('Farmacia');
            });
        });

    }

    function initCombosUJMF() {
        $TipoUnidad.change(function () {
            $("option:selected", $TipoUnidad).each(function () {
                Page.filterInfo('Jurisdiccion');
            });
        });

        $Jurisdiccion.change(function () {
            $("option:selected", $Jurisdiccion).each(function () {
                Page.filterInfo('Municipio');
            });
        });

        $Municipio.change(function () {
            $("option:selected", $Municipio).each(function () {
                Page.filterInfo('Farmacia');
            });
        });
    }

    function Farmacia(Id, $Farmacia) {
        var parametros = { Identificador: Id };

        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/Farmacia",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
            success: function Ready(res) {
                $Farmacia.removeAttr("disabled");
                $Farmacia.html(res.d);
                $Farmacia.focus();
            },
            error: Error
        });
    }

    function Error(res) {
        Page.AjaxLoading('Ocultar');
        Page.AjaxLoad('Ocultar');
        oAjax = '';
        initNav(true, false, false, false);
        var error = 'No se pudo completar su petición. \n Inténtelo nuevamente.';
        if (res.responseText != undefined) {
            if (!bOptionCte) {
                showToastMsj('No se pudo obtener información debido a: ' + res.responseText, true, 'error', 0, 'bottom-full-width');
                console.log('No se pudo obtener información debido a: ' + res.responseText);
            }
            else {
                showToastMsj(error, false, 'error', 10000, 'bottom-right');
            }
        } else if (res.statusText == 'abort') {
            //No mostrar mensaje
        } else {
            showToastMsj(error, false, 'error', 10000, 'bottom-right');
        }
    }

    function DevError(XMLHttpRequest, textStatus, errorThrown) {
        Page.AjaxLoading('Ocultar');
        Page.AjaxLoad('Ocultar');
        showToastMsj('Un error ocurrio durante la petición: ' + errorThrown, true, 'error', 0, 'bottom-full-width');
        console.log('Un error ocurrio durante la petición: ' + errorThrown);
    }

    function initdatTable(id, bselectable) {
        try {
            $iFrameActivo.contents().find('#' + id).dataTable().fnDestroy();
            oTable = $iFrameActivo.contents().find('#' + id).dataTable(
                    {
                        "bScrollInfinite": oSettingsTable.bScrollInfinite,
                        "sScrollY": oSettingsTable.sScrollY,
                        "sScrollX": oSettingsTable.sScrollX,
                        "bFilter": oSettingsTable.aaSorting,
                        "bSearchable": oSettingsTable.bSearchable,
                        "bSort": oSettingsTable.bSort,
                        "sDom": oSettingsTable.sDom,
                        "iDisplayLength": oSettingsTable.iDisplayLength,
                        "aoColumnDefs": oSettingsTable.aoColumnDefs,
                        "oLanguage": oSettingsTable.oLanguage
                    });

            if (bselectable) {
                oTable.$('tbody tr').off();
                oTable.$('tbody tr').click(function (e) {
                    if ($(this).hasClass('row_selected')) {
                        $(this).removeClass('row_selected');
                    }
                    else {
                        oTable.$('tr.row_selected').removeClass('row_selected');
                        $(this).addClass('row_selected');
                    }
                });
            }

        } catch (e) {
            $iFrameActivo.contents().find('.Results').css({ 'overflow': 'scroll' });
            showToastMsj('La consulta termino con éxito, pero su navegador no pudo procesar toda la información. Está será mostrada sin formato. \n\n Si no visualiza el resultado inténte generar el reporte con menos parámetros o con un rango más reducido.', false, 'info', 10000, 'bottom-full-width');
        }
    }

    function initdatTableiFrame(idFrame, id, bselectable) {
        try {

            $('#' + idFrame).contents().find('#' + id).dataTable().fnDestroy();
            oTable = $('#' + idFrame).contents().find('#' + id).dataTable(
                    {
                        "bScrollInfinite": oSettingsTable.bScrollInfinite,
                        "sScrollY": oSettingsTable.sScrollY,
                        "sScrollX": oSettingsTable.sScrollX,
                        "bFilter": oSettingsTable.aaSorting,
                        "bSearchable": oSettingsTable.bSearchable,
                        "bSort": oSettingsTable.bSort,
                        "sDom": oSettingsTable.sDom,
                        "iDisplayLength": oSettingsTable.iDisplayLength,
                        "aoColumnDefs": oSettingsTable.aoColumnDefs,
                        "oLanguage": oSettingsTable.oLanguage
                    });

            if (bselectable) {
                oTable.$('tbody tr').off();
                oTable.$('tbody tr').click(function (e) {
                    if ($(this).hasClass('row_selected')) {
                        $(this).removeClass('row_selected');
                    }
                    else {
                        oTable.$('tr.row_selected').removeClass('row_selected');
                        $(this).addClass('row_selected');
                    }
                });
            }

        } catch (e) {
            $('#' + idFrame).contents().find('.Results').css({ 'overflow': 'scroll' });
            showToastMsj('La consulta termino con éxito, pero su navegador no pudo procesar toda la información. Está será mostrada sin formato. \n\n Si no visualiza el resultado inténte generar el reporte con menos parámetros o con un rango más reducido.', false, 'info', 10000, 'bottom-full-width');
        }
    }

    //Get info
    function Existencias() {
        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
        var IdFarmacia = $($Farmacia, 'option:selected').val();
        var TipoExistencia = $($iFrameActivo.contents().find('[name="tipoExistencias"]:checked')).val();
        var TipoInsumo = $($iFrameActivo.contents().find('[name="tipoInsumo"]:checked')).val();
        var TipoClave = $($iFrameActivo.contents().find('[name="tipoClave"]:checked')).val();
        var TipoDispensacion = $($iFrameActivo.contents().find('[name="tipoDispensacion"]:checked')).val();
        var Online = $frmActivo == '2' ? false : true;

        var parametros = {
            sIdJurisdiccion: IdJurisdiccion,
            sIdFarmacia: IdFarmacia,
            iTipoExistencia: TipoExistencia,
            iTipoInsumo: TipoInsumo,
            iTipoDispensacion: TipoDispensacion,
            iTipoClave: TipoClave,
            bOnline: Online
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/Existencia",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                var jsonResult = $.parseJSON(res.d);
                if (jsonResult['MostrarResultado'] == 'True') {
                    oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0, 2, 4]}];
                }
                else {
                    oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0]}];
                    showToastMsj('Reporte generado con éxito.', true, 'success', 10000, 'bottom-full-width');
                }

                initNav(true, false, false, true);
                $iFrameActivo.contents().find('.Results').append(jsonResult['Tabla']);

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 72) + 'px';
                oSettingsTable.sScrollX = '100%';
                oSettingsTable.sDom = 'rti';

                initdatTable('Existencia', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    //Get info
    function ExistenciaSPAC() {
        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
        var IdFarmacia = $($Farmacia, 'option:selected').val();
        var TipoExistencia = $($iFrameActivo.contents().find('[name="tipoExistencias"]:checked')).val();
        var TipoInsumo = $($iFrameActivo.contents().find('[name="tipoInsumo"]:checked')).val();
        var TipoClave = $($iFrameActivo.contents().find('[name="tipoClave"]:checked')).val();
        var TipoDispensacion = $($iFrameActivo.contents().find('[name="tipoDispensacion"]:checked')).val();
        var Online = $frmActivo == '2' ? false : true;

        var parametros = {
            sIdJurisdiccion: IdJurisdiccion,
            sIdFarmacia: IdFarmacia,
            iTipoExistencia: TipoExistencia,
            iTipoInsumo: TipoInsumo,
            iTipoDispensacion: TipoDispensacion,
            iTipoClave: TipoClave,
            bOnline: Online
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/ExistenciaSPAC",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                var jsonResult = $.parseJSON(res.d);
                if (jsonResult['MostrarResultado'] == 'True') {
                    oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0, 2, 4]}];
                }
                else {
                    oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0]}];
                    showToastMsj('Reporte generado con éxito.', true, 'success', 10000, 'bottom-full-width');
                }

                initNav(true, false, false, true);
                $iFrameActivo.contents().find('.Results').append(jsonResult['Tabla']);

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 72) + 'px';
                oSettingsTable.sScrollX = '100%';
                oSettingsTable.sDom = 'rti';

                initdatTable('ExistenciaSPAC', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function ProximosACaducar() {
        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
        var FechaInicial = $($iFrameActivo.contents().find('#dtpFechaInicial')).val();
        var FechaFinal = $($iFrameActivo.contents().find('#dtpFechaFinal')).val();
        var IdFarmacia = $($Farmacia, 'option:selected').val();
        var TipoInsumo = $($iFrameActivo.contents().find('[name="tipoInsumo"]:checked')).val();
        var TipoDispensacion = $($iFrameActivo.contents().find('[name="tipoDispensacion"]:checked')).val();
        var Online = $frmActivo == '13' ? false : true;

        var parametros = {
            sIdJurisdiccion: IdJurisdiccion,
            sIdFarmacia: IdFarmacia,
            dtpFechaInicial: FechaInicial,
            dtpFechaFinal: FechaFinal,
            iTipoInsumo: TipoInsumo,
            iTipoDispensacion: TipoDispensacion,
            bOnline: Online
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/ProximosACaducar",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                var jsonResult = $.parseJSON(res.d);
                if (jsonResult['MostrarResultado'] == 'True') {
                    if ($frmActivo == '13') {
                        oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0, 2, 4, 6, 7, 8, 9]}];
                        oSettingsTable.sScrollX = '200%';
                    } else {
                        oSettingsTable.sScrollX = '150%';
                        oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0, 2]}];
                    }
                }
                else {
                    oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0]}];
                    oSettingsTable.sScrollX = '100%';
                    showToastMsj('Reporte generado con éxito.', true, 'success', 10000, 'bottom-full-width');
                }

                initNav(true, false, false, true);
                $iFrameActivo.contents().find('.Results').append(jsonResult['Tabla']);

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 72) + 'px';
                oSettingsTable.sDom = 'rti';

                initdatTable('ProximosACaducar', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function DispensancionClaves() {
        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
        var IdFarmacia = $($Farmacia, 'option:selected').val();
        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
        var FechaInicial = $($iFrameActivo.contents().find('#dtpFechaInicial')).val();
        var FechaFinal = $($iFrameActivo.contents().find('#dtpFechaFinal')).val();
        var TipoInsumo = $($iFrameActivo.contents().find('[name="tipoInsumo"]:checked')).val();
        var TipoDispensacion = $($iFrameActivo.contents().find('[name="tipoDispensacion"]:checked')).val();
        var Concentrado = $iFrameActivo.contents().find('#chkTipoTodasJuris').is(':checked') ? '1' : '0';
        var ProcesoPorDia = !bopcDate ? '0' : '1';
        //var ProcesoPorDia = '0';

        var parametros = {
            sTipoUnidad: TipoUnidad,
            sIdJurisdiccion: IdJurisdiccion,
            sIdFarmacia: IdFarmacia,
            sLocalidad: Localidad,
            dtpFechaInicial: FechaInicial,
            dtpFechaFinal: FechaFinal,
            iTipoInsumo: TipoInsumo,
            iTipoDispensacion: TipoDispensacion,
            iConcentrado: Concentrado,
            iProcesoPorDia: ProcesoPorDia
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/DispensancionClaves",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                var jsonResult = $.parseJSON(res.d);
                if (jsonResult['MostrarResultado'] == 'True') {
                    oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0, 2, 4]}];
                    oSettingsTable.sScrollX = '200%';
                }
                else {
                    oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0]}];
                    showToastMsj('Reporte generado con éxito.', true, 'success', 10000, 'bottom-full-width');
                    oSettingsTable.sScrollX = '100%';
                }

                initNav(true, false, false, true);
                $iFrameActivo.contents().find('.Results').append(jsonResult['Tabla']);

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 72) + 'px';
                oSettingsTable.sDom = 'rti';

                initdatTable('DispensancionClaves', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function MedicosDiagnostico() {
        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
        var IdFarmacia = $($Farmacia, 'option:selected').val();
        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
        var FechaInicial = $($iFrameActivo.contents().find('#dtpFechaInicial')).val();
        var FechaFinal = $($iFrameActivo.contents().find('#dtpFechaFinal')).val();
        var Claves = $iFrameActivo.contents().find('#chkClaves').is(':checked') ? '1' : '0';
        var Diagnosticos = $iFrameActivo.contents().find('#chkDiagnosticos').is(':checked') ? '1' : '0';
        var Medicos = $iFrameActivo.contents().find('#chkMedicos').is(':checked') ? '1' : '0';
        var ProcesoPorDia = !bopcDate ? '0' : '1';

        var parametros = {
            sTipoUnidad: TipoUnidad,
            sIdJurisdiccion: IdJurisdiccion,
            sIdFarmacia: IdFarmacia,
            sLocalidad: Localidad,
            dtpFechaInicial: FechaInicial,
            dtpFechaFinal: FechaFinal,
            iClaves: Claves,
            iDiagnosticos: Diagnosticos,
            iMedicos: Medicos,
            iProcesoPorDia: ProcesoPorDia
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/MedicosDiagnostico",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {

                var jsonResult = $.parseJSON(res.d);

                if (jsonResult['Resultado'] != 0) {
                    //$iFrameActivo.contents().find('.Results').append(res.d);
                    $iFrameActivo.contents().find('.Results').append(jsonResult['Tabla']);

                    initNav(true, false, false, true);

                    oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 72) + 'px';
                    oSettingsTable.sScrollX = '200%';
                    oSettingsTable.sDom = 'rti';
                    //oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0, 2, 4, 6, 8, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24]}];

                    initdatTable('MedicosDiagnostico', false);
                }
                else {
                    initNav(true, true, false, false);
                    showToastMsj(jsonResult['Mensaje'], true, 'warning', 10000, 'bottom-full-width');
                }
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function AntibioticosControlados() {
        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
        var IdFarmacia = $($Farmacia, 'option:selected').val();
        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
        var FechaInicial = $($iFrameActivo.contents().find('#dtpFechaInicial')).val();
        var FechaFinal = $($iFrameActivo.contents().find('#dtpFechaFinal')).val();
        var TipoMedicamento = $iFrameActivo.contents().find('#rdoAntibiotico').is(':checked') ? '0' : '1';

        var parametros = {
            sTipoUnidad: TipoUnidad,
            sIdJurisdiccion: IdJurisdiccion,
            sIdFarmacia: IdFarmacia,
            sLocalidad: Localidad,
            dtpFechaInicial: FechaInicial,
            dtpFechaFinal: FechaFinal,
            iTipoMedicamento: TipoMedicamento
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/AntibioticosControlados",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                initNav(true, false, false, true);
                $iFrameActivo.contents().find('.Results').append(res.d);

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 72) + 'px';
                oSettingsTable.sScrollX = '200%';
                oSettingsTable.sDom = 'rti';
                oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0, 2, 4, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22]}];

                initdatTable('AntibioticosControlados', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }


    function CostoPacienteProgramaDeAtencion() {
        var TipoUnidad = $($iFrameActivo.contents().find('#cboTipoUnidad'), 'option:selected').val();
        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
        var IdFarmacia = $($Farmacia, 'option:selected').val();
        var Localidad = $($iFrameActivo.contents().find('#cboLocalidad'), 'option:selected').val();
        var FechaInicial = $($iFrameActivo.contents().find('#dtpFechaInicial')).val();
        var FechaFinal = $($iFrameActivo.contents().find('#dtpFechaFinal')).val();
        var TipoReporte = $iFrameActivo.contents().find('#rdoConcentradoProgramaDeAtencion').is(':checked') ? '2' : '1';

        var parametros = {
            sTipoUnidad: TipoUnidad,
            sIdJurisdiccion: IdJurisdiccion,
            sIdFarmacia: IdFarmacia,
            sLocalidad: Localidad,
            dtpFechaInicial: FechaInicial,
            dtpFechaFinal: FechaFinal,
            iTipoReporte: TipoReporte
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/CostoPacienteProgramaDeAtencion",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                initNav(true, false, false, true);
                $iFrameActivo.contents().find('.Results').append(res.d);

                var rows_Style = [{ "sClass": "center", "aTargets": [0, 2, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17]}];

                if ($iFrameActivo.contents().find('#CostoPacienteProgramaDeAtencion th').length == 22) {
                    rows_Style = [{ "sClass": "center", "aTargets": [0, 2, 4, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21]}];
                }

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 72) + 'px';
                oSettingsTable.sScrollX = '200%';
                oSettingsTable.sDom = 'rti';
                oSettingsTable.aoColumnDefs = rows_Style;

                initdatTable('CostoPacienteProgramaDeAtencion', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function CortesDiarios() {
        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
        var IdFarmacia = $($Farmacia, 'option:selected').val();
        var FechaInicial = $($iFrameActivo.contents().find('#dtpFechaInicial')).val();

        var parametros = {
            sIdJurisdiccion: IdJurisdiccion,
            sIdFarmacia: IdFarmacia,
            dtpFechaInicial: FechaInicial
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/CortesDiarios",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                initNav(true, false, false, true);
                $iFrameActivo.contents().find('.Results').append(res.d);

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 72) + 'px';
                oSettingsTable.sScrollX = '200%';
                oSettingsTable.sDom = 'rti';
                oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0, 2, 4, 6]}];

                initdatTable('CortesDiarios', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function ListadosVarios() {
        var Reporte = $iFrameActivo.contents().find('#cboListados option:selected').val();

        var parametros = {
            sReporte: Reporte
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        var jqxhr = oAjax = $.ajax({
            async: true,
            url: "../DllClienteRegionalWeb/ws_General.aspx/ListadosVarios",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                initNav(true, false, true, false);
                $iFrameActivo.contents().find('.Results').append(res.d);
                var rows_Style = '';

                if (Reporte == 1) {
                    rows_Style = [{ "sClass": "center", "aTargets": [0, 2, 4]}];
                } else if (Reporte == 2) {
                    rows_Style = [{ "sClass": "center", "aTargets": [0, 1, 2, 4]}];
                } else if (Reporte == 4) {
                    rows_Style = [{ "sClass": "center", "aTargets": [0, 1]}];
                } else if (Reporte == 5) {
                    rows_Style = [{ "sClass": "center", "aTargets": [0, 1]}];
                } else if (Reporte == 6) {
                    rows_Style = [{ "sClass": "center", "aTargets": [0, 1]}];
                }

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 72) + 'px';
                oSettingsTable.sScrollX = '200%';
                oSettingsTable.sDom = 'rti';
                oSettingsTable.aoColumnDefs = rows_Style;

                initdatTable('ListadosVarios', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function ReportesTransferencias() {
        var TipoTransferencia = $($iFrameActivo.contents().find('[name="tipoTransferencia"]:checked')).val();
        var IdFarmacia = $($iFrameActivo.contents().find('#txtFarmacia')).val();
        var FechaInicial = $($iFrameActivo.contents().find('#dtpFechaInicial')).val();
        var FechaFinal = $($iFrameActivo.contents().find('#dtpFechaFinal')).val();
        var TipoInsumo = $($iFrameActivo.contents().find('[name="tipoInsumo"]:checked')).val();
        var TipoDispensacion = $($iFrameActivo.contents().find('[name="tipoDispensacion"]:checked')).val();
        var TipoDestino = $($iFrameActivo.contents().find('[name="tipoDestino"]:checked')).val();

        var parametros = {
            sTipoTransferencia: TipoTransferencia,
            sIdFarmacia: IdFarmacia,
            dtpFechaInicial: FechaInicial,
            dtpFechaFinal: FechaFinal,
            iTipoInsumo: TipoInsumo,
            iTipoDispensacion: TipoDispensacion,
            iTipoDestino: TipoDestino
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/ReportesTransferencias",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                initNav(true, false, false, true);
                $iFrameActivo.contents().find('.Results').append(res.d);

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 24) + 'px';
                oSettingsTable.sScrollX = '300%';
                oSettingsTable.sDom = 'rt';
                oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0, 2, 3, 4, 5, 6, 7, 8, 9]}];

                initdatTable('ReportesTransferencias', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function PedidosEspeciales() {
        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');

        var aData = oTable.fnSettings().aoData;
        var sInfo = '';

        for (i = 0; i < aData.length; i++) {
            sInfo += "{4} '{0}', '{1}', '{2}', '{3}', '" + aData[i]._aData[0] + "', '" + aData[i]._aData[6] + "', '" + aData[i]._aData[5] + "', '" + aData[i]._aData[1] + "', '" + aData[i]._aData[4] + "' ";
        }

        var parametros = {
            sFolioPedido: $iFrameActivo.contents().find('#txtFolio').val(),
            sObservaciones: $iFrameActivo.contents().find('#txtObservaciones').val(),
            sData: sInfo
        };


        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/PedidosEspeciales",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
                var jsonResult = $.parseJSON(data.d);
                $iFrameActivo.contents().find('#txtFolio').val(jsonResult['Folio']);
                showToastMsj(jsonResult['Mensaje'], false, 'info', 10000, 'bottom-full-width');
                DisabledControls();


                oTable.$('td.cantidad').removeClass('edit');

                fnTableDraw(oTable);

                oTable.$('tbody tr').off();
                oTable.$('tr.row_selected').removeClass('row_selected');

                $iFrameActivo.contents().find('#newrecord').off();
                $iFrameActivo.contents().find('#delrecord').off();

                initNav(true, false, true, false);

                var r = confirm("¿Desea imprimir su pedido?");
                if (r == true) {
                    $Imprimir.click();
                }

            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function RegistroPedidosEspeciales() {
        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');

        var aData = oTable.fnSettings().aoData;
        var sInfo = '';
        var $cboPerfil = $iFrameActivo.contents().find('#cboPerfil');
        var $cboBeneficiario = $iFrameActivo.contents().find('#cboBeneficiario');
        var $txtReferencia = $iFrameActivo.contents().find('#txtReferencia');

        for (i = 0; i < aData.length; i++) {
            if (aData[i]._aData[6] != 0) {
                sInfo += "{4} '{0}', '{1}', '{2}', '{3}', '" + aData[i]._aData[1] + "', '" + aData[i]._aData[7] + "', '" + aData[i]._aData[6] + "', '" + aData[i]._aData[2] + "', '" + aData[i]._aData[5] + "' ";
            }
        }

        var parametros = {
            sFolioPedido: $iFrameActivo.contents().find('#txtFolio').val(),
            sObservaciones: $iFrameActivo.contents().find('#txtObservaciones').val(),
            sData: sInfo,
            iIdPerfil: $($cboPerfil, 'option:selected').val(),
            sIdBeneficiario: $($cboBeneficiario, 'option:selected').val(),
            sReferencia: $txtReferencia.val()
        };


        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/RegistroPedidosEspeciales",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
                var jsonResult = $.parseJSON(data.d);
                $iFrameActivo.contents().find('#txtFolio').val(jsonResult['Folio']);
                showToastMsj(jsonResult['Mensaje'], false, 'info', 10000, 'bottom-full-width');
                DisabledControls();


                oTable.$('td.cantidad').removeClass('edit');

                fnTableDraw(oTable);

                oTable.$('tbody tr').off();
                oTable.$('tr.row_selected').removeClass('row_selected');

                $iFrameActivo.contents().find('#newrecord').off();
                $iFrameActivo.contents().find('#delrecord').off();

                initNav(true, false, true, false);

                var r = confirm("¿Desea imprimir su pedido?");
                if (r == true) {
                    $Imprimir.click();
                }

            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function GetRegistroPedidosEspeciales(FolioPedido) {
        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');

        var parametros = {
            sFolio: FolioPedido
        };

        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/GetInfoRegistroPedidosEspeciales",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
                    var jsonResult = $.parseJSON(data.d);
                    //Cargar Enc
                    var value = new Date(parseInt(jsonResult['Enc'][0]['FechaRegistro'].replace(/(^.*\()|([+-].*$)/g, '')));
                    var mes = value.getMonth() < 10 ? '0' + (parseInt(value.getMonth()) + 1) : (parseInt(value.getMonth()) + 1);
                    $iFrameActivo.contents().find('#txtFolio').val(jsonResult['Enc'][0]['Folio']);
                    $iFrameActivo.contents().find('#dtpFechaRegistro').val(value.getFullYear() + '-' + mes + '-' + value.getDate());
                    $iFrameActivo.contents().find('#txtObservaciones').val(jsonResult['Enc'][0]['Observaciones']);

                    //Cargar Información Adicional
                    $iFrameActivo.contents().find('#txtReferencia').val(jsonResult['InformacionAdiccional'][0]['Referencia']);
                    $iFrameActivo.contents().find('#cboPerfil').val(jsonResult['InformacionAdiccional'][0]['IdPerfilAtencion']);
                    $iFrameActivo.contents().find('#cboBeneficiario').empty();
                    $iFrameActivo.contents().find('#cboBeneficiario').val($iFrameActivo.contents().find('#cboBeneficiario').append('<option value="' + jsonResult['InformacionAdiccional'][0]['IdBeneficiario'] + '">' + jsonResult['InformacionAdiccional'][0]['NombreCompleto'] + '</option>'));

                    //Cargar Det
                    initTablaPedidos();
                    $.each(jsonResult['Det'], function (index, element) {
                        var values = [];
                        var keys = [];

                        values.push(index);
                        values.push(element.IdClaveSSA);
                        values.push(element.ClaveSSA);
                        values.push(element.DescripcionClave);
                        values.push(element.Presentacion);
                        values.push(element.ContenidoPaquete);
                        values.push(element.CantidadEnCajas);
                        values.push(element.Cantidad);

                        oTable.fnAddData(values);
                    });
                    DisabledControls();
                } else {
                    $iFrameActivo.contents().find('#txtFolio').val('');
                    $iFrameActivo.contents().find('#txtFolio').focus();
                    showToastMsj('Folio de Pedido no encontrado, verifique.', false, 'info', 10000, 'bottom-full-width');
                }
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function GetPedidosEspeciales(FolioPedido) {
        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');

        var parametros = {
            sFolio: FolioPedido
        };

        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/GetInfoPedidosEspeciales",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
                    var jsonResult = $.parseJSON(data.d);
                    //Cargar Enc
                    var value = new Date(parseInt(jsonResult['Enc'][0]['FechaRegistro'].replace(/(^.*\()|([+-].*$)/g, '')));
                    var mes = value.getMonth() < 10 ? '0' + (parseInt(value.getMonth()) + 1) : (parseInt(value.getMonth()) + 1);
                    $iFrameActivo.contents().find('#txtFolio').val(jsonResult['Enc'][0]['Folio']);
                    $iFrameActivo.contents().find('#dtpFechaRegistro').val(value.getFullYear() + '-' + mes + '-' + value.getDate());
                    $iFrameActivo.contents().find('#txtObservaciones').val(jsonResult['Enc'][0]['Observaciones']);

                    //Cargar Det
                    initTablaPedidos();
                    $.each(jsonResult['Det'], function (index) {
                        var values = [];
                        var keys = [];
                        $.each(jsonResult['Det'][index], function (key, value) {
                            keys.push(key);
                            values.push(value);
                        });
                        oTable.fnAddData(values);
                    });
                    DisabledControls();
                } else {
                    $iFrameActivo.contents().find('#txtFolio').val('');
                    $iFrameActivo.contents().find('#txtFolio').focus();
                    showToastMsj('Folio de Pedido no encontrado, verifique.', false, 'info', 10000, 'bottom-full-width');
                }
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function ClavesNegadas() {
        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
        var FechaInicial = $($iFrameActivo.contents().find('#dtpFechaInicial')).val();

        var parametros = {
            sIdJurisdiccion: IdJurisdiccion,
            dtpFechaInicial: FechaInicial
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/ClavesNegadas",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
                initNav(true, false, false, true);
                var jsonResult = $.parseJSON(data.d);
                $iFrameActivo.contents().find('#lblClaves').html('Claves : ' + jsonResult['Claves']);
                $iFrameActivo.contents().find('#lblPiezas').html('Piezas : ' + jsonResult['TotalPiezas']);

                $iFrameActivo.contents().find('.Results').append(jsonResult['Tabla']);

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 72) + 'px';
                oSettingsTable.sScrollX = '200%';
                oSettingsTable.sDom = 'rti';
                oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": "all"}];

                initdatTable('ClavesNegadas', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function ConsumosFacturados() {
        var IdFarmacia = $($Farmacia, 'option:selected').val();
        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();

        var parametros = {
            sIdFarmacia: IdFarmacia,
            sIdJurisdiccion: IdJurisdiccion,
            sFecha: FechaInicial
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/ConsumosFacturados",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
                initNav(true, false, false, true);
                if (data.d == 'true') {
                    initNav(true, false, false, true);
                    showToastMsj('Reportes generado con éxito.', true, 'success', 10000, 'bottom-full-width');
                    bBtnImprimir = true;
                } else {
                    initNav(true, false, false, false);
                    showToastMsj('No se encontro información con los criterios especificados.', true, 'warning', 10000, 'bottom-full-width');
                }
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function SurtimientoInsumos() {
        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
        var IdFarmacia = $($Farmacia, 'option:selected').val();
        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
        var TipoReporte = $iFrameActivo.contents().find('#rdoCauses').is(':checked') ? '1' : '2';

        var parametros = {
            sIdJurisdiccion: IdJurisdiccion,
            sIdFarmacia: IdFarmacia,
            dtpFechaInicial: FechaInicial,
            dtpFechaFinal: FechaFinal,
            iTipoReporte: TipoReporte
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/SurtimientoInsumos",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                initNav(true, false, false, true);
                $iFrameActivo.contents().find('.Results').append(res.d);

                var rows_Style = '';

                if (TipoReporte == 1) {
                    rows_Style = [{ "sClass": "center", "aTargets": [0, 1, 3, 5, 6, 7, 8, 9, 10, 11]}];
                } else if (TipoReporte == 2) {
                    rows_Style = [{ "sClass": "center", "aTargets": [0, 2, 5, 6, 7, 8, 9]}];
                }
                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 72) + 'px';
                oSettingsTable.sScrollX = '200%';
                oSettingsTable.sDom = 'rti';
                oSettingsTable.aoColumnDefs = rows_Style;

                initdatTable('SurtimientoInsumos', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function EfectividadVales() {
        var IdFarmacia = $($iFrameActivo.contents().find('#txtFarmacia')).val();
        var FechaInicial = $($iFrameActivo.contents().find('#dtpFechaInicial')).val();
        var FechaFinal = $($iFrameActivo.contents().find('#dtpFechaFinal')).val();
        var parametros = {
            sIdFarmacia: IdFarmacia,
            dtpFechaInicial: FechaInicial,
            dtpFechaFinal: FechaFinal
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/EfectividadVales",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
                initNav(true, false, true, false);
                var jsonResultMaster = $.parseJSON(data.d);
                var jsonResult = jsonResultMaster['Info'][0];
                if (jsonResult['ValesEmitidos'] != '') {
                    bBtnImprimir = true;
                    $iFrameActivo.contents().find('#lblVales').html(jsonResult['ValesEmitidos']);
                    $iFrameActivo.contents().find('#lblValesSurtidos').html(jsonResult['ValesRegistrados']);
                    $iFrameActivo.contents().find('#lblValesSurtidosCompletos').html(jsonResult['ValesSurtidosCompletos']);
                    $iFrameActivo.contents().find('#lblValesSurtidosParcialmente').html(jsonResult['ValesSurtidosParcialmente']);
                    $iFrameActivo.contents().find('#lblValesNoSurtidos').html(jsonResult['Vales_No_Registrados']);

                    $iFrameActivo.contents().find('#lblPzasRequeridas').html(jsonResult['CantidadRequerida']);
                    $iFrameActivo.contents().find('#lblPzasSurtidas').html(jsonResult['CantidadSurtida']);
                    $iFrameActivo.contents().find('#lblPzasNoSurtidas').html(jsonResult['Cantidad_No_Surtida']);

                    $iFrameActivo.contents().find('#lblClaves').html(jsonResult['Claves']);
                    $iFrameActivo.contents().find('#lblEfectividad').html(jsonResult['Efectividad'].toFixed(2));
                } else {
                    showToastMsj('No se encontro información con los criterios especificados.', false, 'info', 10000, 'bottom-full-width');
                }
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function SurtimientoRecetas() {
        var IdFarmacia = $($iFrameActivo.contents().find('#txtFarmacia')).val();
        var FechaInicial = $($iFrameActivo.contents().find('#dtpFechaInicial')).val();
        var FechaFinal = $($iFrameActivo.contents().find('#dtpFechaFinal')).val();
        var parametros = {
            sIdFarmacia: IdFarmacia,
            dtpFechaInicial: FechaInicial,
            dtpFechaFinal: FechaFinal
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/SurtimientoRecetas",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
                initNav(true, false, true, false);
                if (data.d != '') {
                    var jsonResultMaster = $.parseJSON(data.d);
                    var jsonResult = jsonResultMaster['Info'][0];
                    jsonData = jsonResult;
                    if (jsonResult['FoliosDeVenta'] != '') {
                        bBtnImprimir = true;

                        oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 24) + 'px';
                        oSettingsTable.sScrollX = '100%';
                        oSettingsTable.sDom = 'rt';
                        oSettingsTable.aoColumnDefs = [
                                { "sClass": "center", "aTargets": [1, 2, 3] },
                                { "bVisible": false, "aTargets": [4] }
                            ];

                        initdatTable('TablaResumen', false);

                        oTable.fnAddData(['Claves solicitadas', jsonResult['ClavesDiferentes'], jsonResult['CantidadTotal'], '100', '0']);
                        oTable.fnAddData(['Claves dispensadas', jsonResult['ClavesSurtidas'], jsonResult['CantidadSurtida'], jsonResult['PorcClavesSurtidas'], '1']);
                        oTable.fnAddData(['Claves vales', jsonResult['ClavesVales'], jsonResult['CantidadVale'], jsonResult['PorcClavesVales'], '2']);
                        oTable.fnAddData(['Claves no surtidas', jsonResult['ClavesNoSurtido'], jsonResult['CantidadNoSurtida'], jsonResult['PorcClavesNoSurtida'], '3']);
                        oTable.fnDraw();

                        oTable.$('tbody tr').off();
                        oTable.$('tbody tr').on('dblclick', function (e) {
                            if ($(this).hasClass('row_selected')) {
                                $(this).removeClass('row_selected');
                            }
                            else {
                                oTable.$('tr.row_selected').removeClass('row_selected');
                                $(this).addClass('row_selected');
                                var nTrSelected = fnGetSelected(oTable);
                                var aData = oTable.fnGetData(nTrSelected.get(0));
                                ClavesSurtimiento(aData[4]);
                                $iFrameActivo.contents().find('.Mask').fadeIn(500);
                                $iFrameActivo.contents().find('.close').off();
                                $iFrameActivo.contents().find('.close').on('click', function () {
                                    $iFrameActivo.contents().find('.Mask').fadeOut(500);
                                    oTableClaves.fnDestroy();
                                    $iFrameActivo.contents().find('.helpcontent').html('<span>Cargando información, espere un momento...</span>');
                                    $iFrameActivo.contents().find('#lblClaves').html('');
                                    $iFrameActivo.contents().find('#lblCantidad').html('');
                                });
                                $(this).removeClass('row_selected');
                            }
                        });

                        $iFrameActivo.contents().find('#lblFolios').html(jsonResult['FoliosDeVenta']);
                        $iFrameActivo.contents().find('#lblSurtidos').html('% ' + jsonResult['PorcSurtido']);
                        $iFrameActivo.contents().find('#lblVales').html('% ' + jsonResult['PorcVales']);
                        $iFrameActivo.contents().find('#lblNoSurtido').html('% ' + jsonResult['PorcNoSurtido']);

                        var iSurtido = jsonResult['FoliosDeVenta'] - (jsonResult['Vales'] + jsonResult['NoSurtido'])
                        $iFrameActivo.contents().find('#lblSurtidoPzas').html(iSurtido);
                        $iFrameActivo.contents().find('#lblValesPzas').html(jsonResult['Vales']);
                        $iFrameActivo.contents().find('#lblNoSurtidoPzas').html(jsonResult['NoSurtido']);
                    } else {
                        showToastMsj('No se encontro información con los criterios especificados.', false, 'info', 10000, 'bottom-full-width');
                    }
                } else {
                    showToastMsj('No se encontro información con los criterios especificados.', false, 'info', 10000, 'bottom-full-width');
                }
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }


    function ClavesSurtimiento(Tipo) {

        var parametros = { iTipo: Tipo };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/ClavesSurtimiento",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                initNav(true, false, true, true);
                var h_height = $iFrameActivo.contents().find('.caja').height() - 8 - 48 - 38 - 8; // Border - nav - tablaResumenHelp - espacio
                var sumCantidad = 0;
                $iFrameActivo.contents().find('.helpcontent').html(res.d).css({ 'height': h_height + 'px', 'margin-top': '48px' });
                oTableClaves = $($iFrameActivo.contents().find('#ClavesSurtimiento')).dataTable(
                {
                    "bScrollInfinite": true,
                    "sScrollY": ($iFrameActivo.contents().find('.helpcontent').height()) + 'px',
                    "sScrollX": "100%",
                    "bFilter": true,
                    "bSearchable": false,
                    "bSort": true,
                    "sDom": '',
                    "iDisplayLength": 999999,
                    "aoColumnDefs": [{ "sClass": "center", "aTargets": [0, 2]}],
                    "oLanguage": {
                        "sLengthMenu": "Mostrar _MENU_ registros por pagina.",
                        "sZeroRecords": "No existe información para mostrar.",
                        "sInfo": "Mostrando desde _START_ hasta _END_ de _TOTAL_ registros",
                        "sInfoEmpty": "Mostrando desde 0 hasta 0 de 0 registros.",
                        "sInfoFiltered": "(filtrado de _MAX_ registros en total).",
                        "sLoadingRecords": "Cargando...",
                        "sProcessing": "Procesando..."
                    },
                    "bDeferRender": true,
                    "fnFooterCallback": function (nRow, aaData, iStart, iEnd, aiDisplay) {
                        for (var i = 0; i < aaData.length; i++) {
                            sumCantidad += parseInt(aaData[i][2]);
                        }
                    }
                });
                $iFrameActivo.contents().find('#lblClaves').html(oTableClaves.fnSettings().fnRecordsTotal());
                $iFrameActivo.contents().find('#lblCantidad').html(sumCantidad);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            oAjax = '';
        });
    }

    function AbastoFarmacias() {
        var IdFarmacia = $($iFrameActivo.contents().find('#txtFarmacia')).val();
        var parametros = {
            sIdFarmacia: IdFarmacia
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/AbastoFarmacias",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
                initNav(true, false, true, false);
                bBtnImprimir = true;
                $iFrameActivo.contents().find('.Results').append(data.d);

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 8) + 'px';
                oSettingsTable.sScrollX = '100%';
                oSettingsTable.sDom = 'rt';
                oSettingsTable.bSort = false;
                oSettingsTable.aoColumnDefs = [
                                { "sClass": "center", "aTargets": [0, 1, 2, 3] },
                                { "bVisible": false, "aTargets": [4] }
                            ];

                initdatTable('AbastoFarmacias', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function AbastoFarmaciasGeneral() {
        var parametros = {
            iTipoDeInsumo: $iFrameActivo.contents().find('[name="tipoInsumo"]:checked').val(),
            iConcentrado: $iFrameActivo.contents().find('[name="tipoReporte"]:checked').val()
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/AbastoFarmaciasGeneral",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
                initNav(true, false, false, true);
                bBtnImprimir = true;
                $iFrameActivo.contents().find('.Results').append(data.d);

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 8) + 'px';
                oSettingsTable.sScrollX = '100%';
                oSettingsTable.bSort = false;

                initdatTable('AbastoFarmaciasGeneral', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function ReportesDispensacion() {
        var IdFarmacia = $iFrameActivo.contents().find('#txtFarmacia').val();
        var Cte = $iFrameActivo.contents().find('#txtCte').val();
        var SubCte = $iFrameActivo.contents().find('#txtSubCte').val();
        var Pro = $iFrameActivo.contents().find('#txtPrograma').val();
        var SubPro = $iFrameActivo.contents().find('#txtSubPrograma').val();
        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
        var TpDispConsignacion = $iFrameActivo.contents().find('#rdoTpDispConsignacion:checked').val() ? true : false;
        var TpDispVenta = $iFrameActivo.contents().find('#rdoTpDispVenta:checked').val() ? true : false;
        var InsumosMedicamento = $iFrameActivo.contents().find('#rdoInsumosMedicamento:checked').val() ? true : false;
        var InsumoMatCuracion = $iFrameActivo.contents().find('#rdoInsumoMatCuracion:checked').val() ? true : false;
        var InsumoMedicamentoSP = $iFrameActivo.contents().find('#rdoInsumoMedicamentoSP:checked').val() ? true : false;
        var InsumoMedicamentoNOSP = $iFrameActivo.contents().find('#rdoInsumoMedicamentoNOSP:checked').val() ? true : false;

        var parametros = {
            sIdFarmacia: IdFarmacia,
            txtCte: Cte,
            txtSubCte: SubCte,
            txtPro: Pro,
            txtSubPro: SubPro,
            dtpFechaInicial: FechaInicial,
            dtpFechaFinal: FechaFinal,
            rdoTpDispConsignacion: TpDispConsignacion,
            rdoTpDispVenta: TpDispVenta,
            rdoInsumosMedicamento: InsumosMedicamento,
            rdoInsumoMatCuracion: InsumoMatCuracion,
            rdoInsumoMedicamentoSP: InsumoMedicamentoSP,
            rdoInsumoMedicamentoNOSP: InsumoMedicamentoNOSP
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/ReportesDispensacion",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                var jsonResult = $.parseJSON(res.d);
                if (jsonResult['Ejecuto'] == 'True' && jsonResult['Resultado'] != 0) {
                    initNav(true, false, false, true);
                    bBtnImprimir = true;
                    $iFrameActivo.contents().find('#cboReporte').removeAttr('disabled', 'disabled').focus();
                    showToastMsj('Reportes generados con éxito.', true, 'success', 10000, 'bottom-full-width');
                } else {
                    initNav(true, true, false, false);
                    var sMensaje = 'No se encontro información con los criterios especificados.';

                    if (jsonResult['Resultado'] == 0) {
                        sMensaje = jsonResult['Mensaje'];
                    }

                    showToastMsj(sMensaje, true, 'warning', 10000, 'bottom-full-width');
                    //$iFrameActivo.contents().find('#txtPrograma').removeAttr('disabled', 'disabled');
                    //$iFrameActivo.contents().find('#txtSubPrograma').removeAttr('disabled', 'disabled');
                    $iFrameActivo.contents().find('#dtpFechaInicial').removeAttr('disabled', 'disabled');
                    $iFrameActivo.contents().find('#dtpFechaFinal').removeAttr('disabled', 'disabled');
                }
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }


    //Funciones General, recargar formulario
    function limpiar() {
        Page.relodFrame();
    }

    function MakeReport() {

        var Columnas = '';
        var Group = '';
        var Sum = '';
        var Filtros = '';
        var Ordenacion = '';
        var NumRegitros = $iFrameActivo.contents().find('#txtTop').val() != '' ? $iFrameActivo.contents().find('#txtTop').val() : 0;
        var IdFarmacia = $($Farmacia, 'option:selected').val()
        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
        var FechaFinal = $iFrameActivo.contents().find('#dtpFechaFinal').val();
        //var TipoReporte = $iFrameActivo.contents().find('#TipoReporte [name="tipoDeReporte"]:checked').val();
        var TipoReporte = $iFrameActivo.contents().find('#TipoReporte').length ? $iFrameActivo.contents().find('#TipoReporte [name="tipoDeReporte"]:checked').val() : 0;
        //var TipoInformacion = $iFrameActivo.contents().find('#TipoInformacion [name="tipoDeInformacion"]:checked').val();
        var TipoInformacion = $iFrameActivo.contents().find('#TipoInformacion').length ? $iFrameActivo.contents().find('#TipoInformacion [name="tipoDeInformacion"]:checked').val() : 2;


        $iFrameActivo.contents().find('#columnsSelected ul li').each(function (indice, elemento) {
            var cols = $(elemento).find('a');
            //Columnas += cols.attr('rev') + ',';
            Columnas += cols.attr('rev') + '|' + cols.attr('rel');
            if ($iFrameActivo.contents().find('#columnsSelected ul li').length - 1 > indice) {
                Columnas += ',';
            }
        });

        $iFrameActivo.contents().find('#group ul li').each(function (indice, elemento) {
            var cols = $(elemento).find('a');
            Group += cols.attr('rev');
            if ($iFrameActivo.contents().find('#group ul li').length - 1 > indice) {
                Group += ',';
            }
        });

        $iFrameActivo.contents().find('#sum ul li').each(function (indice, elemento) {
            var cols = $(elemento).find('a');
            Sum += cols.attr('rev');
            if ($iFrameActivo.contents().find('#sum ul li').length - 1 > indice) {
                Sum += ',';
            }
        });

        $iFrameActivo.contents().find('#Condiciones ul li').each(function (indice, elemento) {
            var cols = $(elemento).find('a');
            var text = $(elemento).find('txt' + cols.attr('rev'));
            Filtros += cols.attr('rev') + '|' + $(elemento).find('input.m-wrap').val();
            if ($iFrameActivo.contents().find('#Condiciones ul li').length - 1 > indice) {
                Filtros += ',';
            }
        });

        $iFrameActivo.contents().find('#Order ul li').each(function (indice, elemento) {
            var cols = $(elemento).find('a');
            var text = $(elemento).find('input[type=radio]:checked');
            Ordenacion += cols.attr('rev') + '|' + text.val() + '|' + cols.attr('rel');
            if ($iFrameActivo.contents().find('#Order ul li').length - 1 > indice) {
                Ordenacion += ',';
            }
        });

        var parametros = {
            sColumnas: Columnas,
            sGroup: Group,
            sSum: Sum,
            sFiltros: Filtros,
            sOrdenacion: Ordenacion,
            iTop: parseInt(NumRegitros),
            sIdFarmacia: IdFarmacia,
            dtpFechaInicial: FechaInicial,
            dtpFechaFinal: FechaFinal,
            sTipoReporte: TipoReporte,
            sTipoInformacion: TipoInformacion
        };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');

        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/Reporteador",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                var jsonResult = $.parseJSON(res.d);
                if (jsonResult['Ejecuto'] == 'True' && jsonResult['Resultado'] == 0) {
                    initNav(true, false, false, true);
                    bBtnImprimir = true;
                    $iFrameActivo.contents().find('#cboReporte').removeAttr('disabled', 'disabled').focus();
                    showToastMsj('Reporte generado con éxito.', true, 'success', 10000, 'bottom-full-width');

                    /*
                    $iFrameActivo.contents().find('#dtpFechaInicial').attr('disabled', 'disabled');
                    $iFrameActivo.contents().find('#dtpFechaFinal').attr('disabled', 'disabled');
                    $iFrameActivo.contents().find('#cboFarmacia').attr('disabled', 'disabled');

                    $iFrameActivo.contents().find(".btn_del").off();
                    $iFrameActivo.contents().find(".btn_del").remove();

                    $iFrameActivo.contents().find("#columns li").off();
                    $iFrameActivo.contents().find("#columnsSelected, #group, #sum, #Condiciones").off();

                    var $ctrlInput = $iFrameActivo.contents().find('#Condiciones ul li input');
                    $ctrlInput.each(function (index) {
                    $(this).attr('disabled', 'disabled');
                    });*/

                    $iFrameActivo.contents().find(".btn_del").off();
                    $iFrameActivo.contents().find(".btn_del").remove();
                } else {
                    initNav(true, false, false, false);
                    var sMensaje = 'No se encontró información con los criterios especificados.';

                    if (jsonResult['Resultado'] == 1) {
                        sMensaje = jsonResult['Mensaje'];
                    }

                    showToastMsj(sMensaje, true, 'warning', 10000, 'bottom-full-width');
                }


            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function ExistenciaPorClave() {

        var ClaveSSA = $iFrameActivo.contents().find('#txtClave').val();
        var FechaInicial = $iFrameActivo.contents().find('#dtpFechaInicial').val();
        var parametros = { sClaveSSA: ClaveSSA, dtpFechaInicial: FechaInicial };

        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        Page.AjaxLoad('Mostrar');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/ExistenciaPorClave",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(parametros),
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
            success: function Ready(res) {
                initNav(true, false, false, true);
                $iFrameActivo.contents().find('.Results').append(res.d);

                oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 24) + 'px';
                oSettingsTable.sScrollX = '100%';
                oSettingsTable.sDom = 'rt';
                oSettingsTable.bSort = false;
                //oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0] }, { "sClass": "right", "aTargets": [2]}];
                oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0, 3] }, { "sClass": "right", "aTargets": [2]}];

                initdatTable('ExistenciaPorClave', false);
            },
            error: Error
        }).done(function () {
            Page.AjaxLoading('Ocultar');
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function Penalizaciones() {
        Page.AjaxLoading('Mostrar');
        Page.AjaxLoadingText('');
        oAjax = $.ajax({
            url: "../DllClienteRegionalWeb/ws_General.aspx/GetPenalizacionesTapachula",
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
            success: function Ready(res) {
                if (res.d != '') {
                    initNav(true, false, false, true);
                    $iFrameActivo.contents().find('.Results').append(res.d);

                    oSettingsTable.sScrollY = ($iFrameActivo.contents().find('.Results').height() - 48) + 'px';
                    oSettingsTable.sScrollX = '150%';
                    oSettingsTable.sDom = 'rt';
                    oSettingsTable.aoColumnDefs = [{ "sClass": "center", "aTargets": [0, 1, 2, 3, 4, 5, 6]}];

                    initdatTable('Penalizaciones', false);
                }
                else {
                    showToastMsj('No existe información para mostrar', true, 'warning', 10000, 'bottom-full-width');
                }
            },
            error: Error
        }).done(function () {
            Page.AjaxLoad('Ocultar');
            oAjax = '';
        });
    }

    function DisabledControls() {
        var $ctrlInput = $iFrameActivo.contents().find('.container input');
        var $ctrlSelect = $iFrameActivo.contents().find('.container select');
        var $ctrlTextArea = $iFrameActivo.contents().find('.container textarea');

        $ctrlInput.each(function (index) {
            $(this).attr('disabled', 'disabled');
        });

        $ctrlSelect.each(function (index) {
            $(this).attr('disabled', 'disabled');
        });

        $ctrlTextArea.each(function (index) {
            $(this).attr('disabled', 'disabled');
        });
    }

    function EnabledControls() {
        var $ctrlInput = $iFrameActivo.contents().find('.container input');
        var $ctrlSelect = $iFrameActivo.contents().find('.container select');
        var $ctrlTextArea = $iFrameActivo.contents().find('.container textarea');

        $ctrlInput.each(function (index) {
            $(this).removeAttr('disabled', 'disabled');
        });

        $ctrlSelect.each(function (index) {
            $(this).removeAttr('disabled', 'disabled');
        });

        $ctrlTextArea.each(function (index) {
            $(this).removeAttr('disabled', 'disabled');
        });
    }

    /* Get the rows which are currently selected */
    function fnGetSelectedAll(oTableLocal) {
        var aReturn = new Array();
        var aTrs = oTableLocal.fnGetNodes();

        for (var i = 0; i < aTrs.length; i++) {
            if ($(aTrs[i]).hasClass('row_selected')) {
                aReturn.push(aTrs[i]);
            }
        }
        return aReturn;
    }

    /* Get the rows which are currently selected */
    function fnGetSelected(oTableLocal) {
        return oTableLocal.$('tr.row_selected');
    }

    /* Pon ceros */
    function fnPonCeros(sCadena, iLongitud) {
        var sReturn = '';
        var sCeros = '';
        var cantCeros = iLongitud - sCadena.length;

        for (var i = 0; i < cantCeros; i++) {
            sCeros += '0';
        }

        sReturn = sCeros + sCadena;

        return sReturn;
    }

    /*Create HTML Table from JSON*/
    // Builds the HTML Table out of myList.
    function buildHtmlTable(myList, columns, idtable, classtable) {
        var table$ = $('<table cellpadding="0" cellspacing="0" border="0" class="display ' + classtable + '" id="' + idtable + '"/>');
        var columns = addAllColumnHeaders(myList, columns, table$);

        for (var i = 0; i < myList.length; i++) {
            var body$ = $('<tbody/>');
            var row$ = $('<tr/>');
            for (var colIndex = 0; colIndex < columns.length; colIndex++) {
                var cellValue = myList[i][columns[colIndex]];
                if (cellValue == null) { cellValue = ""; }
                row$.append($('<td/>').html(cellValue));
            }
            body$.append(row$);
            table$.append(body$);
        }
        return table$;
    }

    // Adds a header row to the table and returns the set of columns.
    // Need to do union of keys from all records as some records may not contain
    // all records
    function addAllColumnHeaders(myList, columns, table$) {
        var columnSet = [];
        var header$ = $('<thead/>');
        var headerTr$ = $('<tr/>');

        for (var i = 0; i < myList.length; i++) {
            var rowHash = myList[i];
            for (var key in rowHash) {
                if ($.inArray(key, columnSet) == -1) {
                    if ($.inArray(key, columns) != -1) {
                        columnSet.push(key);
                        headerTr$.append($('<th/>').html(key));
                    }
                }
            }
        }
        header$.append(headerTr$);
        table$.append(header$);
        return columnSet;
    }


    /*Check function Ajax*/
    function checkAjax() {
        var bReturn = false;
        if (oAjax != '') {
            var r = confirm("Está realizando una consulta, ¿Desea terminarla?");
            if (r == true) {
                oAjax.abort();
                oAjax = '';
                bReturn = true;
            }
        } else {
            bReturn = true;
        }
        return bReturn;
    }

    /*Range for day*/
    function ProcesoPorDia(bOpcion) {
        bopcDate = bOpcion;
    }

    function bFechaCompleta() {
        return bopcDate;
    }

    function loadFrameReportBI(parameters, report, contenedor) {
        var sValuesRpt = '';
        $.each(parameters, function (key, val) {
            sValuesRpt += '&' + key + '=' + val;
        })

        report = reports.getNameReport(report);

        if (report != '') {
            var url = 'https://{0}/frameset?__report={1}.rptdesign{2}'.format(Page.ServidorBI, report, sValuesRpt);
            contenedor.attr('src', url);
        } else {
            showToastMsj('No existe información para mostrar.', true, 'info', 10000, 'bottom-full-width');
        }
    }

    /*Msj toast*/
    function showToastMsj(sText, bSticky, sType, iTime, sPosition) {

        if (bSticky) {
            iTime = 0;
        }
        else {
            iTime = 4000;
        }

        if (sPosition == '') {
            sPosition = 'toast-bottom-left';
        }
        else {
            sPosition = 'toast-' + sPosition;
        }


        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": sPosition,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": iTime,
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        //toastr.type(sTitle, sText);
        switch (sType) {
            case 'warning':
                toastr.warning(sText);
                break;
            case 'info':
                toastr.info(sText);
                break;
            case 'success':
                toastr.success(sText);
                break;
            case 'error':
                toastr.error(sText);
                break;
            default:

        }
    }

    function clearToastMsj() {
        toastr.options = {};
        toastr.clear();
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

    return {
        init: init,
        checkAjax: checkAjax,
        ProcesoPorDia: ProcesoPorDia,
        clearToastMsj: clearToastMsj,
        bFechaCompleta: bFechaCompleta,
        showToastMsj: showToastMsj
    };

})();