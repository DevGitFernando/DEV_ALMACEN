var frmId;
var title;
var frmActivo = '';
var $iFrameActivo = '';
var oTableProductos = ''; 

var forms = {
    init: init,
    initAyudaAfiliado: initAyudaAfiliado,
    initbtnAyudaAfiliado: initbtnAyudaAfiliado,
    loadInfoAfiliado: loadInfoAfiliado,
    initEditable: initEditable,
    initAyudaProducto: initAyudaProducto,
    initbtnAyudaProducto: initbtnAyudaProducto,
    initAyudaDiagnosticos: initAyudaDiagnosticos,
    initbtnAyudaDiagnosticos: initbtnAyudaDiagnosticos,
    loadInfoDiagnosticos: loadInfoDiagnosticos,
    initAyudaProcedimientos: initAyudaProcedimientos,
    initbtnAyudaProcedimientos: initbtnAyudaProcedimientos,
    loadInfoProcedimientos: loadInfoProcedimientos
}

function init($iFrame, Id, name, bClient) {
    $iFrameActivo = $iFrame.contents();
    frmId = Id;
    title = name;
    
    initEvents(frmId);
}

function initEvents(frmId) {
    switch (frmId) {
        case '2':
            initReceta();
            break;
        default:

    }
}

function initReceta() {
    var $containerBusquedaAfiliado = $iFrameActivo.find('#BusquedaAfiliado');
    var $codAfiliado = $iFrameActivo.find('#txtcodAfiliado');

    $codAfiliado.off();
    $codAfiliado.on('keydown', function (e) {
        switch (e.keyCode) {
            case 13:
                if ($codAfiliado.val() != '' && $codAfiliado.val().length >= 8) {
                    webservices.BuscarAfiliado($iFrameActivo, $codAfiliado.val());
                }
                break;
            case 112: //F1
            case 113: //F2
            case 114: //F3
                e.preventDefault();
                $containerBusquedaAfiliado.fadeIn(500);
                $iFrameActivo.find('#txtSearchCodAfiliado').focus();
                initAyudaAfiliado();
                break;
            default:
        }
    });

    var $containerBusquedaCIE = $iFrameActivo.find('#BusquedaCIE');
    var $ParentClaveDiagnostico = '';
    var $ClaveDiagnostico = $iFrameActivo.find('.clavediag');

    $ClaveDiagnostico.off();
    $ClaveDiagnostico.on('keydown', function (e) {
        switch (e.keyCode) {
            case 13:
                //Implementar
                console.log('Servicio web con Diagnostico, validar lenght');
                break;
            case 112: //F1
            case 113: //F2
            case 114: //F3
                e.preventDefault();
                $containerBusquedaCIE.fadeIn(500);
                $ParentClaveDiagnostico = $(this).parent();
                $iFrameActivo.find('#txtSearchProducto').focus();
                initAyudaDiagnosticos($ParentClaveDiagnostico);
                break;
                break;
            default:

        }
    });

    var $containerBusquedaProcedimiento = $iFrameActivo.find('#BusquedaProcedimientos');
    var $ParentClaveProcedimiento = '';
    var $ClaveProcedimiento = $iFrameActivo.find('.claveProced');

    $ClaveProcedimiento.off();
    $ClaveProcedimiento.on('keydown', function (e) {
        switch (e.keyCode) {
            case 13:
                //Implementar
                console.log('Servicio web con Procedimiento, validar lenght');
                break;
            case 112: //F1
            case 113: //F2
            case 114: //F3
                e.preventDefault();
                $containerBusquedaProcedimiento.fadeIn(500);
                $ParentClaveProcedimiento = $(this).parent();
                $iFrameActivo.find('#txtSearchProcedimiento').focus();
                initAyudaProcedimientos($ParentClaveProcedimiento);
                break;
                break;
            default:

        }
    });

    var $btnGuardar = $iFrameActivo.find('#btnGuardar');

    $btnGuardar.off();
    $btnGuardar.on('click', function (e) {
        //if (validarEnviar()) {
        if (true) {
            saveReceta();
        }
    });

    //oTableProductos = grid.initTable($iFrameActivo, $iFrameActivo.find('#TableProductos'), false, false, '150%', $iFrameActivo.find('#containerTableClave').height() - 40);
    oTableProductos = grid.initTable($iFrameActivo, $iFrameActivo.find('#TableProductos'), false, true, '150%', $iFrameActivo.find('#containerTableClave').height() - 40);
    initEditable(oTableProductos);
    initBtnsTableProductos();

    //page.initNav();
    initNavRecetario();
    $codAfiliado.focus(); 

}

function saveReceta() {
    var parametros = {
        CodEmpresa: $iFrameActivo.find('.npaciente2').val(),
        CodAfiliado: $iFrameActivo.find('#txtcodAfiliado').val(),
        Correlativo: $iFrameActivo.find('.Nafiliado2').val(),
        CodPeriodo: $iFrameActivo.find('.CodPeriodo').val(),
        Comentario: $iFrameActivo.find('#txtNotaMedicas').val(),
        Diagnostico: getDiagnosticosReceta(),
        Procedimientos: getProcedimientosReceta(),
        Medicamentos: getMedicamentosReceta(),
        laboratorio: getLaboratoriosReceta(),
        Gabinete: getGabinetesReceta()
    };

    $.ajax({
        url: "../DllRecetario/ws_General.aspx/Autorizacion",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parametros),
        success: function (res) {
            console.log(res.d);
        },
        error: (page.Client() ? Error : DevError)
    }).done(function (e) { });
}

function getDiagnosticosReceta() {
    var sDiagnostico = '<productos>';
    var $Diagnosticos = $iFrameActivo.find('.clavediag');

    $.each($Diagnosticos, function (index, element) {
        if ($(element).val() != '') {
            sDiagnostico += "<producto Diag='" + $(element).val() + "'></producto>";
        }
    });

    sDiagnostico += '</productos>';

    return sDiagnostico;
}

function getProcedimientosReceta() {
    var sProcedimientos = '<productos>';
    var $Procedimientos = $iFrameActivo.find('.claveProced');

    $.each($Procedimientos, function (index, element) {
        if ($(element).val() != '') {
            $ParentClaveProcedimiento = $(element).parent();
            sMonto = $ParentClaveProcedimiento.find('.monto').val();
            
            sProcedimientos += "<producto proc='" + $(element).val() + "' Monto='" + sMonto + "'></producto>";
        }
    });

    sProcedimientos += '</productos>';

    return sProcedimientos;
}

function getMedicamentosReceta() {
    var sReceta = '<productos>';

    var aDataTableMedicamentos = oTableProductos.data();

    for (var i = 0; i < aDataTableMedicamentos.length; i++) {
        //sReceta += "<producto Id='" + aDataTableMedicamentos[i][0] + "' Cantidad='" + aDataTableMedicamentos[i][3] + "' Monto='" + 0 + "' Descripcion = '" + aDataTableMedicamentos[i][2] + "'></producto>";
        sReceta += "<producto Id='" + aDataTableMedicamentos[i][0] + "' Cantidad='" + aDataTableMedicamentos[i][3] + "' Monto='" + 0 + "' Descripcion='" + aDataTableMedicamentos[i][2] + "' Nombre='" + aDataTableMedicamentos[i][1] + "'></producto>";
    }

    sReceta += '</productos>';
    return sReceta;
}

function getLaboratoriosReceta() {
    var Laboratorios = '<productos>';
    Laboratorios += '</productos>';
    
    return Laboratorios;
}

function getGabinetesReceta() {
    var Gabinetes = '<productos>';
    Gabinetes += '</productos>';
    
    return Gabinetes;
}


function initNavRecetario() {
    var sItems = '<div id="btnAddLabGab" class="btn_general_header" title="Agregar un nuevo Laboratorio">' +
                    '<i class="iconLab">' +
                        '<span class="Notificacion">0</span>' +
                    '</i>' +
                '</div>';

    var $AyudaLabGab = $iFrameActivo.find('#BusquedaLaboratorio');

    //$navContainer.html(sItems);
    $(document).find('#content_iconos').html(sItems);

    var $bntAddLabGab = $navContainer.find('#btnAddLabGab');

    $bntAddLabGab.off();
    $bntAddLabGab.on('click', function (e) {
        if ($AyudaLabGab.is(':visible')) {
            $AyudaLabGab.fadeOut('500');
        }
        else {
            $AyudaLabGab.fadeIn('500');
        }

        //console.log(getMedicamentosReceta());
    });


    var $container = $iFrameActivo.find('#BusquedaLaboratorio');
    var $btnClose = $iFrameActivo.find('#BusquedaLaboratorio .btnClose');
    var $btnSearch = $iFrameActivo.find('#btnBuscarLab');
    var $btnClean = $iFrameActivo.find('#btnLimpiarLab');
    var $tableContainer = $iFrameActivo.find('.TableLab1');

    $btnClose.off();
    $btnClose.click(function (e) {
        $container.fadeOut(500);
        $btnClean.click();
    });

    $btnSearch.off();
    $btnSearch.click(function (e) {
        if (validarAyuda('BusquedaLabGab')) {
            $tableContainer.html('');
            console.log($iFrameActivo.find('[name="typelab"]:checked').val());
            webservices.BusquedaLabGabAyuda($iFrameActivo, $iFrameActivo.find('.plan1').val(), $iFrameActivo.find('#txtSearchLabGab').val(), $iFrameActivo.find('[name="typelab"]:checked').val());
        }
    });

    $btnClean.off();
    $btnClean.click(function (e) {
        var $ctrlInput = $container.find('input');
        $ctrlInput.val('');

        $tableContainer.html('');
        $iFrameActivo.find('#txtSearchCIE').focus();
    });
}

function initBtnsTableProductos() {
    var $btnAdd = $iFrameActivo.find('#btnAgregar');
    var $btnDel = $iFrameActivo.find('#btnBorrar');

    $btnAdd.off();
    $btnAdd.on('click', function (e) {
        $iFrameActivo.find('#BusquedaProducto').fadeIn(500);
        $iFrameActivo.find('#txtSearchProducto').focus();
        initAyudaProducto();
    });

    $btnDel.off();
    $btnDel.on('click', function (e) {
        oTableProductos.row(oTableProductos.$('tbody tr.selected')).remove().draw(false);
    });


    oTableProductos.on('click', 'tbody tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            oTableProductos.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
}

function initEditable(oDataTable) {
    var $container = $iFrameActivo.find('#BusquedaProducto');
    oDataTable.$('td').off();
    oDataTable.$('td.edit').editable(function (value, settings) {
        var cell = oDataTable.cell(this).index();
        var oNextRow = $(this).parent();
        oDataTable.cell(cell.row, cell.column).data(value).draw(false);
        oDataTable.fixedColumns().update();

        var hCols = $(oNextRow).find('td');

        if ($(this).hasClass('clave') && value != '') {
            $(this).removeClass('edit');
            $(this).removeAttr('title')
            $(hCols[2]).addClass('edit');
            $(hCols[3]).addClass('edit');

            //oDataTable.$('td.edit').off();
            initEditable(oDataTable);
        }

        //searchNextEdit(oNextRow, value);

        return value;
    }, {
        type: 'text',
        placeholder: 'Click para editar',
        event: 'click',
        tooltip: 'Click para editar',
        height: 'auto',
        width: 'auto',
        onblur: 'submit'
    });

    oDataTable.$('td.edit').on('keydown', function (e) {
        switch (e.keyCode) {
            case 13:
                //Implementar
                console.log('Servicio web con idproducto, validar lenght');
                break;
            case 112: //F1
            case 113: //F2
            case 114: //F3
                /*e.preventDefault();
                $container.fadeIn(500);
                $iFrameActivo.find('#txtSearchProducto').focus();
                initAyudaProducto();*/
                break;
                break;
            default:

        }
    });
}

function initAyudaDiagnosticos($ParentClaveDiagnostico) {
    var $container = $iFrameActivo.find('#BusquedaCIE');
    var $btnClose = $iFrameActivo.find('#BusquedaCIE .btnClose');
    var $btnSearch = $iFrameActivo.find('#btnBuscarCIE');
    var $btnClean = $iFrameActivo.find('#btnLimpiarCIE');
    var $tableContainer = $iFrameActivo.find('.TableClaveCie');

    $btnClose.off();
    $btnClose.click(function (e) {
        $container.fadeOut(500);
        $btnClean.click();
    });

    $btnSearch.off();
    $btnSearch.click(function (e) {
        if (validarAyuda('BusquedaCIE')) {
            $tableContainer.html('');
            webservices.BusquedaDiagnosticoAyuda($iFrameActivo, $ParentClaveDiagnostico, $iFrameActivo.find('#txtSearchCIE').val());
        }
    });

    $btnClean.off();
    $btnClean.click(function (e) {
        var $ctrlInput = $container.find('input');
        $ctrlInput.val('');

        $tableContainer.html('');
        $iFrameActivo.find('#txtSearchCIE').focus();
    });
}

function initbtnAyudaDiagnosticos(oTableCIE, oParent) {
    var $btnAdd = $iFrameActivo.find('#btnAddCIE');

    oTableCIE.$('tbody tr').dblclick(function (e) {
        if (!$(this).hasClass('selected')) {
            oTableCIE.$('tbody tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        $btnAdd.click();
    });

    $btnAdd.off();
    $btnAdd.click(function (e) {
        var aData = oTableCIE.row(oTableCIE.$('tbody tr.selected')).data()
        loadInfoDiagnosticos(oParent, aData);
        $iFrameActivo.find('#BusquedaCIE .btnClose').click();
    });
}

function loadInfoDiagnosticos(oParent, aData) {
    oParent.find('.clavediag').val(aData[0]);
    oParent.find('.descripcion').val(aData[1]);
    oParent.find('.descripcion').attr('title', aData[1]);
}

function initAyudaProcedimientos($ParentClaveProcedimientos) {
    var $container = $iFrameActivo.find('#BusquedaProcedimientos');
    var $btnClose = $iFrameActivo.find('#BusquedaProcedimientos .btnClose');
    var $btnSearch = $iFrameActivo.find('#btnBuscarProcedimiento');
    var $btnClean = $iFrameActivo.find('#btnLimpiarProcedimiento');
    var $tableContainer = $iFrameActivo.find('.TableProcedimientos1');

    $btnClose.off();
    $btnClose.click(function (e) {
        $container.fadeOut(500);
        $btnClean.click();
    });

    $btnSearch.off();
    $btnSearch.click(function (e) {
        if (validarAyuda('BusquedaProcedimientos')) {
            $tableContainer.html('');
            webservices.BusquedaProcedimientosAyuda($iFrameActivo, $ParentClaveProcedimientos, $iFrameActivo.find('.plan1').val(), $iFrameActivo.find('#txtSearchProcedimiento').val());
        }
    });

    $btnClean.off();
    $btnClean.click(function (e) {
        var $ctrlInput = $container.find('input');
        $ctrlInput.val('');

        $tableContainer.html('');
        $iFrameActivo.find('#txtSearchProcedimiento').focus();
    });
}

function initbtnAyudaProcedimientos(oTableProcedimientos, oParent) {
    var $btnAdd = $iFrameActivo.find('#btnAddCIE');

    oTableProcedimientos.$('tbody tr').dblclick(function (e) {
        if (!$(this).hasClass('selected')) {
            oTableProcedimientos.$('tbody tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        $btnAdd.click();
    });

    $btnAdd.off();
    $btnAdd.click(function (e) {
        var aData = oTableProcedimientos.row(oTableProcedimientos.$('tbody tr.selected')).data()
        loadInfoProcedimientos(oParent, aData);
        $iFrameActivo.find('#BusquedaProcedimientos .btnClose').click();
    });
}

function loadInfoProcedimientos(oParent, aData) {
    oParent.find('.claveProced').val(aData[0]);
    oParent.find('.descripcionProced').val(aData[1]);
    oParent.find('.descripcionProced').attr('title', aData[1]);
    oParent.find('.monto').val(aData[2]);
}

function initAyudaAfiliado() {
    var $container = $iFrameActivo.find('#BusquedaAfiliado');
    var $btnClose = $iFrameActivo.find('#BusquedaAfiliado .btnClose');
    var $btnSearch = $iFrameActivo.find('#btnBuscarAfiliado');
    var $btnClean = $iFrameActivo.find('#btnLimpiarAfiliado');

    $btnClose.off();
    $btnClose.click(function (e) {
        $container.fadeOut(500);
        $btnClean.click();
    });

    $btnSearch.off();
    $btnSearch.click(function (e) {
        if (validarAyuda('BusquedaAfiliado')) {
            $iFrameActivo.find('.TableAfiliado').html('');
            webservices.BuscarAfiliadoAyuda($iFrameActivo.find('.TableAfiliado'), $iFrameActivo.find('#txtSearchCodAfiliado').val(), $iFrameActivo.find('#txtSearchNombre').val(), $iFrameActivo.find('#txtSearchApPaterno').val(), $iFrameActivo.find('#txtSearchApMaterno').val());
        }
    });

    $btnClean.off();
    $btnClean.click(function (e) {
        var $ctrlInput = $iFrameActivo.find('#BusquedaAfiliado input');
        $ctrlInput.val('');

        $iFrameActivo.find('.TableAfiliado').html('');
        $iFrameActivo.find('#txtSearchCodAfiliado').focus();
    });
}

function initbtnAyudaAfiliado(oTableAfiliado) {
    var $btnAdd = $iFrameActivo.find('#btnAddAfiliado');

    oTableAfiliado.$('tbody tr').dblclick(function (e) {
        if (!$(this).hasClass('selected')) {
            oTableAfiliado.$('tbody tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        $btnAdd.click();
    });

    $btnAdd.off();
    $btnAdd.click(function (e) {
        var aData = oTableAfiliado.row(oTableAfiliado.$('tbody tr.selected')).data()
        loadInfoAfiliado(aData);
        $iFrameActivo.find('#BusquedaAfiliado .btnClose').click();
    });
}

function loadInfoAfiliado(aData) {
    $iFrameActivo.find('#txtcodAfiliado').val(aData[0]);
    $iFrameActivo.find('.Nafiliado2').val(aData[1]);
    $iFrameActivo.find('.npaciente1').val(aData[2]);
    $iFrameActivo.find('.plan1').val(aData[3]);
    $iFrameActivo.find('.plan2').val(aData[4]);
    $iFrameActivo.find('.plan3').val(aData[5]);
    $iFrameActivo.find('.plan4').val(aData[6]);
    $iFrameActivo.find('.npaciente2').val(aData[7]);
    $iFrameActivo.find('.npaciente3').val(aData[8]);
    $iFrameActivo.find('.CodPeriodo').val(aData[9]);
}

function initAyudaProducto() {
    var $container = $iFrameActivo.find('#BusquedaProducto');
    var $btnClose = $iFrameActivo.find('#BusquedaProducto .btnClose');
    var $btnSearch = $iFrameActivo.find('#btnBuscarProducto');
    var $btnClean = $iFrameActivo.find('#btnLimpiarProducto');

    $btnClose.off();
    $btnClose.click(function (e) {
        $container.fadeOut(500);
        $btnClean.click();
    });

    $btnSearch.off();
    $btnSearch.click(function (e) {
        if (validarAyuda('BusquedaProducto')) {
            $iFrameActivo.find('.TableClaveAyuda').html('');
            webservices.BuscarProductoAyuda($iFrameActivo, $iFrameActivo.find('.plan3').val(), $iFrameActivo.find('.plan1').val(), $iFrameActivo.find('#txtSearchProducto').val());
        }
    });

    $btnClean.off();
    $btnClean.click(function (e) {
        var $ctrlInput = $iFrameActivo.find('#BusquedaProducto input');
        $ctrlInput.val('');

        $iFrameActivo.find('.TableClaveAyuda').html('');
        $iFrameActivo.find('#txtSearchProducto').focus();
    });
}

function initbtnAyudaProducto(oTableProducto) {
    var $btnAddProducto = $iFrameActivo.find('#btnAddProducto');
    var $btnDelProducto = $iFrameActivo.find('#btnAddProducto');

    oTableProducto.$('tbody tr').dblclick(function (e) {
        if (!$(this).hasClass('selected')) {
            oTableProducto.$('tbody tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }

        $btnAddProducto.click();
    });

    $btnAddProducto.off();
    $btnAddProducto.click(function (e) {
        var aData = oTableProducto.row(oTableProducto.$('tbody tr.selected')).data()
        loadInfoProducto(aData);
        $iFrameActivo.find('#BusquedaProducto .btnClose').click();
    });
}

function loadInfoProducto(aData) {
    var newTr = $('<tr>' +
                        '<td>' + aData[0] + '</td>' +
                        '<td>' + aData[1] + '</td>' +
                        '<td class="edit indicacion"></td>' +
                        '<td class="edit cantidad"></td>' +
                '</tr>');

    oTableProductos.row.add(newTr).draw(false);
    oTableProductos.fixedColumns().update();

    initEditable(oTableProductos);
}

function validarAyuda(type) {
    var bContinuar = true;
    switch (type) {
        case 'BusquedaAfiliado':
            bContinuar = validarBusquedaAfiliado();
            break;
        case 'BusquedaProducto':
            bContinuar = validarBusquedaProducto();
            break;
        case 'BusquedaCIE':
            bContinuar = validarBusquedaCIE();
            break;
        case 'BusquedaProcedimientos':
            bContinuar = validarBusquedaProcedimientos();
            break;
        case 'BusquedaLabGab':
            bContinuar = validarBusquedaLabGab();
            break;
        default:

    }

    return bContinuar;
}

function validarBusquedaAfiliado() {
    var bContinuar = true;
    var numValues = 0;
    
    if ($iFrameActivo.find('#txtSearchCodAfiliado').val() != '') {
        numValues++;
    }

    if ($iFrameActivo.find('#txtSearchApPaterno').val() != '') {
        numValues++;
    }

    if ($iFrameActivo.find('#txtSearchApMaterno').val() != '') {
        numValues++;
    }

    if ($iFrameActivo.find('#txtSearchNombre').val() != '') {
        numValues++;
    }

    if (numValues == 0) {
        bContinuar = false;
        $iFrameActivo.find('#txtSearchCodAfiliado').focus();
        alert('Debe escribir al menos un valor para poder realizar la búsqueda');
    }

    return bContinuar;
}

function validarBusquedaProducto() {
    var bContinuar = true;
    if ($iFrameActivo.find('#txtSearchProducto').val() == '') {
        bContinuar = false;
        $iFrameActivo.find('#txtSearchProducto').focus();
        alert('Debe escribir la descripción del producto a buscar');
    }

    return bContinuar;
}

function validarBusquedaCIE() {
    var bContinuar = true;
    if ($iFrameActivo.find('#txtSearchCIE').val() == '') {
        bContinuar = false;
        $iFrameActivo.find('#txtSearchCIE').focus();
        alert('Debe escribir la descripción de la clave CIE 10 a buscar');
    }

    return bContinuar;
}

function validarBusquedaProcedimientos() {
    var bContinuar = true;
    if ($iFrameActivo.find('#txtSearchProcedimiento').val() == '') {
        bContinuar = false;
        $iFrameActivo.find('#txtSearchProcedimiento').focus();
        alert('Debe escribir la descripción del procedimiento a buscar');
    }

    return bContinuar;
}

function validarBusquedaLabGab() {
    var bContinuar = true;
    if ($iFrameActivo.find('#txtSearchLabGab').val() == '') {
        bContinuar = false;
        $iFrameActivo.find('#txtSearchLabGab').focus();
        alert('Debe escribir la descripción del procedimiento a buscar');
    }

    return bContinuar;
}

function Error(res) {
    var error = JSON.parse(res.responseText).Message;
    console.log('Solicitud no enviada debido a ' + error);
}

function DevError(XMLHttpRequest, textStatus, errorThrown) {
    console.log(XMLHttpRequest);
    console.log(textStatus);
    console.log(XMLHttpRequest.responseText);
    console.log('Un error ocurrio durante la petición: ' + errorThrown);
}