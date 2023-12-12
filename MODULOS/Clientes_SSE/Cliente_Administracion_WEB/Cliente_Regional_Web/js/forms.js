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
        case '73':
        case '74':
        case '75':
        case '76':
        case '77':
        case '78':
        case '79':
        case '80':
        case '81':
        case '82':
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
        //case '43':
        //case '83':
            initNav(true, true, false, true);
            initCombosUJMF();
            if (main.FiltroUnidad()) {
                $TipoUnidad.find("option[value='*']").remove();
            }
            if (main.FiltroJuris()) {
                $Jurisdiccion.find("option[value='*']").remove();
            }

            $iFrameActivo.find($Jurisdiccion)
                .clone(false)
                .find('option')
                .removeAttr("id")
                .appendTo($iFrameActivo.find('#cboJurisdiccionEntrega'));

            $iFrameActivo.find('#cboJurisdiccionEntrega').val('*');
            //$TipoUnidad.focus();
            //$TipoUnidad.attr('disabled', 'disabled');
            //$Municipio.attr('disabled', 'disabled');
            //$Jurisdiccion.attr('disabled', 'disabled');
            //$Farmacia.attr('disabled', 'disabled');
            
            resultAdaptive();
            break;
        case '43':
        case '83':
            initNav(true, true, false, true);
            //initCombosUJMF();
            if (main.FiltroUnidad()) {
                $TipoUnidad.find("option[value='*']").remove();
            }
            if (main.FiltroJuris()) {
                $Jurisdiccion.find("option[value='*']").remove();
            }
            $iFrameActivo.find('#cboJurisdiccionEntrega').val('*');
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
            main.filterInfo('TipoUnidad');
        });
    }
    else {
        $Nuevo.addClass('disabled');
    }

    $Ejecutar.off();
    if (bEjecutar) {
        $Ejecutar.removeClass('disabled');
        $Ejecutar.on('click', function () {
            var $bDisbledControld = false;
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
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
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
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
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
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
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
                    var FuenteDeFinanciamiento = $iFrameActivo.find('#txtFuenteFinanciamiento').val();
                    var Procedencia = $iFrameActivo.find('#txtProcedencia').val();
                    var Semaforizacion = $($iFrameActivo.find('#cboSemaforizacion'), 'option:selected').val();;
                    var Validar_Entradas = $iFrameActivo.find('#chkValidar_Entradas').prop('checked') ? 1 : 0;
                    var Entrada_Menor = $iFrameActivo.find('#txtEntrada_Menor').val();
                    var Entrada_Mayor = $iFrameActivo.find('#txtEntrada_Mayor').val();
                    var Validar_Salidas = $iFrameActivo.find('#chkValidar_Salidas').prop('checked') ? 1 : 0;
                    var Salida_Menor = $iFrameActivo.find('#txtSalida_Menor').val();
                    var Salida_Mayor = $iFrameActivo.find('#txtSalida_Mayor').val();

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        TipoMovto: TipoMovto,
                        ClaveSSA: ClaveSSA,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        FuenteDeFinanciamiento: FuenteDeFinanciamiento,
                        Semaforizacion: Semaforizacion,
                        Validar_Entradas: Validar_Entradas,
                        Entrada_Menor: Entrada_Menor,
                        Entrada_Mayor: Entrada_Mayor,
                        Validar_Salidas: Validar_Salidas,
                        Salida_Menor: Salida_Menor,
                        Salida_Mayor: Salida_Mayor,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
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

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
                    };

                    loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                    break;
                case '82':
                    $bDisbledControld = false;
                    var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                    var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                    var IdFarmacia = $($Farmacia, 'option:selected').val();
                    var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                    var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val() + '-01';

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        FechaInicial: FechaInicial
                    };

                    loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                    break;
                case '79':
                    $bDisbledControld = false;
                    var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                    var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                    var IdFarmacia = $($Farmacia, 'option:selected').val();
                    var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                    //var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val() + '-01';
                    var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                    //var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val() + '-01';
                    var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                    var TipoMedicamento = $iFrameActivo.find('[name="optTipoDeMedicamentos"]:checked').val();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdJurisdiccion: IdJurisdiccion,
                        IdMunicipio: Localidad,
                        IdFarmacia: IdFarmacia,
                        IdClaveSSA: '*',
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        TipoMedicamento: TipoMedicamento,
                        TipoDispensacion: '0',
                        TipoInsumo: '1',
                        SubFarmacias: '',
                        AgrupaDispensacion: '1',
                        Filtro: '1',
                        IdTipoUnidad: '*',
                        ProcesoPorDia: 0
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
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
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
                    $bDisbledControld = false;
                    var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                    var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                    var IdFarmacia = $($Farmacia, 'option:selected').val();
                    var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                    var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                    var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                    var NumeroDePoliza = $iFrameActivo.find('#txtNumeroDePoliza').val();
                    var NombreBeneficiario = $iFrameActivo.find('#txtNombreBeneficiario').val();
                    var ProgramaDeAtencion = $iFrameActivo.find('#txtProgramaDeAtencion').val();

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        NumeroDePoliza: NumeroDePoliza,
                        NombreBeneficiario: NombreBeneficiario,
                        ProgramaDeAtencion: ProgramaDeAtencion,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
                    };

                    loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                    break;
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
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
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
                case '83':
                    $bDisbledControld = false;
                    var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                    var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                    var IdFarmacia = $($Farmacia, 'option:selected').val();
                    var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                    var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                    var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                    var ClaveSSA = $iFrameActivo.find('#txtClaveSSA').val();
                    var IdUnidad_Beneficiario = $($iFrameActivo.find('#cboBeneficiarios'), 'option:selected').val();
                    var Unidad_Beneficiario = $iFrameActivo.find('#txtUnidad_Beneficiario').val();
                    var IdJurisdiccionEntrega = $($iFrameActivo.find('#cboJurisdiccionEntrega'), 'option:selected').val();
                    var JurisdiccionEntrega = '';

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var JurisEntrega = $iFrameActivo.find('#cboJurisdiccionEntrega option:selected').text();
                    if (IdUnidad_Beneficiario == '*') {
                        IdUnidad_Beneficiario = '';
                    }

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        ClaveSSA: ClaveSSA,
                        ClaveUnidad_Beneficiario: IdUnidad_Beneficiario,
                        Unidad_Beneficiario: Unidad_Beneficiario,
                        IdJurisdiccionEntrega: IdJurisdiccionEntrega,
                        JurisdiccionEntrega: JurisdiccionEntrega,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia,
                        JurisEntrega: JurisEntrega
                    };
                    loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                    break;
                case '44':
                    $bDisbledControld = false;
                    var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                    var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                    var IdFarmacia = $($Farmacia, 'option:selected').val();
                    var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                    var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                    var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                    var ClaveSSA = $iFrameActivo.find('#txtClaveSSA').val();
                    var Unidad_Beneficiario = $iFrameActivo.find('#txtUnidad_Beneficiario').val();
                    var NumeroDeReceta = $iFrameActivo.find('#txtNumeroDeReceta').val();

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        ClaveSSA: ClaveSSA,
                        Unidad_Beneficiario: Unidad_Beneficiario,
                        NumeroDeReceta: NumeroDeReceta,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
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

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        ClaveSSA: ClaveSSA,
                        Benefeciario: Benefeciario,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
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
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
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

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        ClaveSSA: ClaveSSA,
                        NombreBeneficiario: Benefeciario,
                        NumeroDePoliza: Poliza,
                        NombreMedico: Medico,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
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
                    var CIE10 = $iFrameActivo.find('#txtCIE10').val();
                    var Diagnostico = $iFrameActivo.find('#txtDiagnostico').val();

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        ClaveSSA: ClaveSSA,
                        NombreMedico: Medico,
                        CIE10: CIE10,
                        Diagnostico: Diagnostico,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
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

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        ClaveSSA: ClaveSSA,
                        CIE10: CIE10,
                        Diagnostico: Diagnostico,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
                    };

                    loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                    break;
                case '51':
                case '80':
                    bDisbledControld = false;
                    var IdTipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                    var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                    var IdFarmacia = $($Farmacia, 'option:selected').val();
                    var IdLocalidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                    var FechaInicial = $iFrameActivo.find('#dtpFechaInicial').val();
                    var FechaFinal = $iFrameActivo.find('#dtpFechaFinal').val();
                    var Remision = $iFrameActivo.find('#txtRemision').val();
                    var TipoDeDispensacion = $($iFrameActivo.find('#cboTipoDeDispensacion'), 'option:selected').val();
                    var TipoDeDispensacionTexto = $($iFrameActivo.find('#cboTipoDeDispensacion'), 'option:selected').text();

                    var TituloRpt = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Localidad = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: IdLocalidad,
                        Municipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        Jurisdiccion: Jurisdiccion,
                        IdFarmacia: IdFarmacia,
                        Farmacia: Farmacia,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        Remision: Remision,
                        TipoDeDispensacion: TipoDeDispensacion,
                        TipoDeDispensacionTexto: TipoDeDispensacionTexto,
                        Titulo: TituloRpt
                    };
                    loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                    break;
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

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        Remision: Remision,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
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

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        Servicio: Servicio,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
                    };

                    loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                    break;
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
                    var NumeroReceta = $iFrameActivo.find('#txtFolioReceta').val();
                    var Servicio = $iFrameActivo.find('#txtServicio').val();

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        ClaveSSA: ClaveSSA,
                        Unidad_Beneficiario: Unidad_Beneficiario,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        NumeroReceta: NumeroReceta,
                        Servicio: Servicio,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
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
                    var Benefeciario = $iFrameActivo.find('#txtNombreBeneficiario').val();
                    var Poliza = $iFrameActivo.find('#txtNumeroDePoliza').val();
                    var Medico = $iFrameActivo.find('#txtMedico').val();
                    var Servicio = $iFrameActivo.find('#txtServicio').val();

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        ClaveSSA: ClaveSSA,
                        NombreBeneficiario: Benefeciario,
                        NumeroDePoliza: Poliza,
                        NombreMedico: Medico,
                        Servicio: Servicio,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
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
                    var Turno = $iFrameActivo.find('#txtTurno').val();

                    var Titulo = $iFrameActivo.find('.titleFrm').html();
                    var TipoUnidad = $iFrameActivo.find('#cboTipoUnidad option:selected').text();
                    var Jurisdiccion = $iFrameActivo.find('#cboJurisdiccion option:selected').text();
                    var Municipio = $iFrameActivo.find('#cboLocalidad option:selected').text();
                    var Farmacia = $iFrameActivo.find('#cboFarmacia option:selected').text();

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        ClaveSSA: ClaveSSA,
                        NombreMedico: Medico,
                        Servicio: Servicio,
                        FechaInicial: FechaInicial,
                        FechaFinal: FechaFinal,
                        Area: Servicio,
                        Turno: Turno,
                        Titulo: Titulo,
                        Jurisdiccion: Jurisdiccion,
                        Municipio: Municipio,
                        Farmacia: Farmacia
                    };

                    loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                    break;
                case '70':
                    Penalizaciones();
                    break;
                case '73':
                    initNav(true, false, false, false);
                    bDisbledControld = false;
                    var TipoUnidad = $($iFrameActivo.find('#cboTipoUnidad'), 'option:selected').val();
                    var IdJurisdiccion = $($Jurisdiccion, 'option:selected').val();
                    var IdFarmacia = $($Farmacia, 'option:selected').val();
                    var Localidad = $($iFrameActivo.find('#cboLocalidad'), 'option:selected').val();
                    var FechaInicial = $($iFrameActivo.find('#dtpFechaInicial')).val();
                    var Fecha = FechaInicial.split('-');

                    var parametros = {
                        IdEmpresa: main.Empresa(),
                        IdEstado: main.Estado(),
                        IdMunicipio: Localidad,
                        IdJurisdiccion: IdJurisdiccion,
                        IdFarmacia: IdFarmacia,
                        Year: Fecha[0],
                        Mes: Fecha[1]
                    };

                    loadFrameReportBI(parametros, idForm, $iFrameActivo.find('#iResult'));
                    break;
                case '74':
                case '81':
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

    if (parameters != '') {
        $.each(parameters, function (key, val) {
            sValuesRpt += '&' + key + '=' + val;
        });
    }

    report = reports.getNameReport(report);

    if (report != '') {
        var url = '{0}/frameset?__report={1}.rptdesign{2}'.format(main.ServidorBI, report, sValuesRpt);
        contenedor.attr("src", url);
        contenedor.off().on('load', function (e) {});
    } else {
        showToastMsj('No existe información para mostrar.', true, 'info', 10000, 'bottom-full-width');
    }

    var sUrlForm = $__iFrameActivo.attr('src');
    var aUrlForm = sUrlForm.split('/');
    var iEndVal = aUrlForm.length;
    var aForm = (aUrlForm[iEndVal - 1]).split('.');
    var sForm = aForm[0];

    var Token = AuditorBIRT(JSON.stringify(parameters), sForm);
    console.log(Token);
}

function AuditorBIRT(query, form) {
    var Token = '';
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
            var jsonResult = $.parseJSON(res.d);
            console.log(jsonResult);
            if (!jsonResult.Error) {
                Token = jsonResult.Token;
            }
            else {
                Token = 'Error token';
            }
        },
        error: function errorcall(res) {
        }
    }).done(function () { });
    console.log(Token);
    return Token;
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