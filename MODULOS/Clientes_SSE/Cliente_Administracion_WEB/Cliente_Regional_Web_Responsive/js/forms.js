var $Nuevo, $Ejecutar, $Imprimir, $Exportar; //Botones
var $TipoUnidad, $Jurisdiccion, $Municipio, $Farmacia; //Combos
var bOptionCte = true;
var $__iFrameActivo = '';
var $iFrameActivo = '';
var idForm = ''
var nameForm = '';

var bOptionCte = true;

var forms = {
    init: init
}

function init($iFrame, id, name, bClient) {
    $__iFrameActivo = $iFrame;
    $__iFrameActivo.off();
    $iFrameActivo = $__iFrameActivo.contents();
    idForm = id;
    nameForm = name;

    bOptionCte = bClient;
    initRButton();

    $iFrameActivo.find('.titleFrm').html(name);
    $('.contDinamic').fadeIn();

    $Nuevo = $('#New');
    $Ejecutar = $('#Exec');
    $Imprimir = $('#Print');
    $Exportar = $('#Exportar');

    $TipoUnidad = $iFrameActivo.find('#cboTipoUnidad');
    $Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion');
    $Municipio = $iFrameActivo.find('#cboLocalidad');
    $Farmacia = $iFrameActivo.find('#cboFarmacia');

    initForms(id);

    //Ocultar title Resultado
    $iFrameActivo.find('.Results .titleResult').html('');
    
    //Limpiar iFrame contenedor de Birt
    $iFrameActivo.find('#iResult').attr('src', '');
}

function initRButton() {
    if (bOptionCte) {
        $__iFrameActivo.on("contextmenu", function (e) {
            return false;
        });
        $iFrameActivo.on("contextmenu", function (e) {
            return false;
        });
    }
}

function initCombosUJMF() {
    main.filterInfo('TipoUnidad');
    main.filterInfo('Jurisdiccion');
    main.filterInfo('Municipio');
    main.filterInfo('Farmacia');

    $TipoUnidad.change(function () {
        $("option:selected", $(this)).each(function () {
            main.filterInfo('Jurisdiccion');
        });
    });

    $Jurisdiccion.change(function () {
        $("option:selected", $(this)).each(function () {
            main.filterInfo('Municipio');
        });
    });

    $Municipio.change(function () {
        $("option:selected", $(this)).each(function () {
            main.filterInfo('Farmacia');
        });
    });
}

function initForms(id) {
    switch (id) {
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
        case '74':
        case '75':
        case '76':
        case '77':
        case '78':
            initNav(true, true, false, true);
            initCombosUJMF();
            if (main.FiltroUnidad()) {
                $TipoUnidad.find("option[value='*']").remove();
            }
            if (main.FiltroJuris()) {
                $Jurisdiccion.find("option[value='*']").remove();
            }
            $TipoUnidad.focus();
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

function initNav(bNuevo, bEjecutar, bImprimir, bExportar) {
    $Nuevo.off();
    if (bNuevo) {
        $Nuevo.on('click', function () {
            $Nuevo.removeClass('disabled');
            main.reloadFrame();
        });
    }
    else {
        $Nuevo.addClass('disabled');
    }

    $Ejecutar.off();
    if (bEjecutar) {
        $Ejecutar.removeClass('disabled');
        $Ejecutar.on('click', function () {
            var $bDisbledControld = true;
            switch (idForm) {
                case '34':
                        initNav(true, false, false, false);
                        DisabledControls();
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $($iFrameActivo.find('#dtpFechaInicial')).val();
                        var FechaFinal = $($iFrameActivo.find('#dtpFechaFinal')).val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal
                        };

                        loadFrameReportBI(parametros, $iFrameActivo, $iFrameActivo.find('#iResult'));
                        break;
                    case '36':
                    case '55':
                    case '56':
                    case '57':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            Fecha: FechaInicial
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '37':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var ClaveSSA = $iFrameActivo.find('#txtClaveSSA').val();
                        var FuenteFinancianciamiento = $iFrameActivo.find('#txtFuenteFinanciamiento').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            Fecha: FechaInicial,
                            ClaveSSA: ClaveSSA,
                            FuenteFinancianciamiento: FuenteFinancianciamiento
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '38':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var TipoMovto = 0;
                        var ClaveSSA = $iFrameActivo.find('#txtClaveSSA').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            TipoMovto: TipoMovto,
                            ClaveSSA: ClaveSSA,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '40':
                    case '41':
                    case '53':
                    case '58':
                    case '68':
                    case '75':
                    case '76':
                    case '77':
                    case '78':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '39':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var Status_Semaforizacion = $($iFrameActivo.find('#cboSemaforizacion'), 'option:selected').val();
                        var Procedencia = $iFrameActivo.find('#txtProcedencia').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            Fecha: FechaInicial,
                            Status_Semaforizacion: Status_Semaforizacion,
                            Procedencia: Procedencia
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '42':
                    case '61':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                        var NumeroDePoliza = $iFrameActivo.find('#txtNumeroDePoliza').val();
                        var NombreBeneficiario = $iFrameActivo.find('#txtNombreBeneficiario').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            NumeroDePoliza: NumeroDePoliza,
                            NombreBeneficiario: NombreBeneficiario
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '43':
                    case '44':
                    case '62':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.find('#txtClaveSSA').val();
                        var Unidad_Beneficiario = $iFrameActivo.find('#txtUnidad_Beneficiario').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            Unidad_Beneficiario: Unidad_Beneficiario
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '45':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.find('#txtClaveSSA').val();
                        var Benefeciario = $iFrameActivo.find('#txtBeneficiario').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            Benefeciario: Benefeciario
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '46':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.find('#txtClaveSSA').val();
                        var Benefeciario = $iFrameActivo.find('#txtBeneficiario').val();
                        var Poliza = $iFrameActivo.find('#txtNumeroDePoliza').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            Benefeciario: Benefeciario,
                            Poliza: Poliza
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '47':
                    case '48':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.find('#txtClaveSSA').val();
                        var Benefeciario = $iFrameActivo.find('#txtBeneficiario').val();
                        var Poliza = $iFrameActivo.find('#txtNumeroDePoliza').val();
                        var Medico = $iFrameActivo.find('#txtMedico').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
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

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '49':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.find('#txtClaveSSA').val();
                        var Medico = $iFrameActivo.find('#txtMedico').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            NombreMedico: Medico
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '50':
                    case '66':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.find('#txtClaveSSA').val();
                        var CIE10 = $iFrameActivo.find('#txtCIE10').val();
                        var Diagnostico = $iFrameActivo.find('#txtDiagnostico').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            CIE10: CIE10,
                            Diagnostico: Diagnostico
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '51':
                    case '59':
                    case '67':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                        var Remision = $iFrameActivo.find('#txtRemision').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            Remision: Remision
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '54':
                        loadFrameReportBI('', idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '60':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                        var Servicio = $iFrameActivo.find('#txtServicio').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            Servicio: Servicio
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '63':
                    case '64':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.find('#txtClaveSSA').val();
                        var Benefeciario = $iFrameActivo.find('#txtBeneficiario').val();
                        var Poliza = $iFrameActivo.find('#txtNumeroDePoliza').val();
                        var Medico = $iFrameActivo.find('#txtMedico').val();
                        var Servicio = $iFrameActivo.find('#txtServicio').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
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

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '65':
                        bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                        var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                        var ClaveSSA = $iFrameActivo.find('#txtClaveSSA').val();
                        var Medico = $iFrameActivo.find('#txtMedico').val();
                        var Servicio = $iFrameActivo.find('#txtServicio').val();

                        var parametros = {
                            IdEmpresa:main.Empresa(),
                            IdEstado:main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia,
                            FechaInicial: FechaInicial,
                            FechaFinal: FechaFinal,
                            ClaveSSA: ClaveSSA,
                            NombreMedico: Medico,
                            Servicio: Servicio
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    case '70':
                        Penalizaciones();
                        break;
                    case '74':
                        $bDisbledControld = false;
                        var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                        var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                        var IdFarmacia = $($Farmacia, 'option:selected').val();
                        var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                        var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();

                        var parametros = {
                            IdEmpresa: main.Empresa(),
                            IdEstado: main.Estado(),
                            IdMunicipio: Localidad,
                            IdJurisdiccion: IdJurisdiccion,
                            IdFarmacia: IdFarmacia
                        };

                        loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                        break;
                    default:
                    //Opcion en caso de no conicidir
            }
            
            if ($bDisbledControld) {
                DisabledControls();
            }
        });
    }
    else {
        $Ejecutar.addClass('disabled');
    }

    $Imprimir.off();
    if (bImprimir) {
        $Imprimir.removeClass('disabled');
        $Imprimir.on('click', function () {
            toastr.clear();
            switch (idForm) {
                default:
                    //Opcion en caso de no conicidir
            }
        });
    }
    else {
        $Imprimir.addClass('disabled');
    }

    $Exportar.off();
    if (bExportar) {
        $Exportar.removeClass('disabled');
        $Exportar.on('click', function () {
            switch (idFormo) {
                default:
                    //Opcion en caso de no conicidir
            }
        });
    }
    else {
        $Exportar.addClass('disabled');
    }
}

function loadFrameReportBI(parameters, report, contenedor) {
    var sValuesRpt = '';

    $iFrameActivo.find('.Results .return').fadeIn();
    
    $.each(parameters, function (key, val) {
        sValuesRpt += '&' + key + '=' + val;
    })

    report = reports.getNameReport(report);

    if (report != '') {
        var url = '{0}/frameset?__report={1}.rptdesign{2}'.format(main.ServidorBI, report, sValuesRpt);
        contenedor.attr('src', url);
    } else {
        showToastMsj('No existe información para mostrar.', true, 'info', 10000, 'bottom-full-width');
    }

    var sUrlForm = $__iFrameActivo.attr('src');
    var aUrlForm = sUrlForm.split('/');
    var iEndVal = aUrlForm.length;
    var aForm = (aUrlForm[iEndVal - 1]).split('.');
    var sForm = aForm[0];

    AuditorBIRT(JSON.stringify(parameters), sForm);
}

function AuditorBIRT(query, form) {
    var parametros = {
        sQuery: query,
        sForm: form
    };

    $.ajax({
        url: 'DllClienteRegionalWeb/wsGeneral.aspx/AuditorBIRT',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parametros),
        success: function Ready(res) {
            
        },
        error: function errorcall(res) {
        }
    }).done(function () { });

}

function resultAdaptive() {
    $iFrameActivo.find('.Results').append('<span class="return"></span>');

    $iFrameActivo.find('.Results .return').off();
    $iFrameActivo.find('.Results .return').on('click', function (e) {
        if ($iFrameActivo.find('.Results').hasClass('Max')) {
            $iFrameActivo.find('.Results').removeClass('Max');
            $('.contDinamic').fadeIn();
        }
        else {
            $iFrameActivo.find('.Results').addClass('Max');
            $('.contDinamic').fadeOut();
        }
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

//Add Function
String.prototype.format = function () {
    var content = this;
    for (var i = 0; i < arguments.length; i++) {
        var replacement = '{' + i + '}';
        content = content.replace(replacement, arguments[i]);
    }
    return content;
};