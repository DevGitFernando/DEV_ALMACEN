var webservices = {
    BuscarAfiliadoAyuda: wsBuscarAfiliadoAyuda,
    BuscarAfiliado: wsBuscarAfiliado,
    BuscarProductoAyuda: wsBuscarProductoAyuda,
    BusquedaDiagnosticoAyuda: wsBusquedaDiagnosticoAyuda,
    BusquedaProcedimientosAyuda: wsBusquedaProcedimientosAyuda,
    BusquedaLabGabAyuda: wsBusquedaLabGabAyuda
}

function wsBuscarAfiliadoAyuda($Container, codAfiliado, nombre, apellidoPaterno, apellidoMaterno) {
    var parametros = {
        Afiliado: codAfiliado,
        Nombre: nombre,
        ApellidoPaterno: apellidoPaterno,
        ApellidoMaterno: apellidoMaterno
    };

    $Container.html('');
    $.ajax({
        url: "../DllRecetario/ws_General.aspx/BuscarAfiliado",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parametros),
        success: function Ready(res) {
            if (res.d != '') {
                $Container.html(res.d);
                //grid.initTable($Container.find('#TableAfiliado'), true, '150%', '100%');
                var oTable = grid.initTable($Container, $Container.find('#TableAfiliado'), true, false, '200%', $Container.height() - 40);
                forms.initAyudaAfiliado();
                forms.initbtnAyudaAfiliado(oTable);
            }
            else {
                //Error
            }

            return res.d;
        },
        error: (page.Client() ? Error : DevError)
    }).done(function () {
    });
}

function wsBuscarAfiliado($Container, codAfiliado) {
    var parametros = {
        Afiliado: codAfiliado,
        Nombre: '',
        ApellidoPaterno: '',
        ApellidoMaterno: ''
    };

    $Container.find('.TableAfiliado').html('');
    $.ajax({
        url: "../DllRecetario/ws_General.aspx/BuscarAfiliado",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parametros),
        success: function Ready(res) {
            if (res.d != '') {
                $Container.find('.TableAfiliado').html(res.d);
                var oTable = grid.initTable($Container, $Container.find('#TableAfiliado'), true, false, '150%', $Container.height() - 40);
                var aData = oTable.row(0).cells().data();
                
                if (aData != '' && aData.length > 1) {
                    forms.loadInfoAfiliado(aData);
                }
                else {
                    $Container.find('#BusquedaAfiliado').fadeIn(500);
                    $Container.find('#txtSearchCodAfiliado').focus();
                    alert('No se encontro beneficiario con el codigo de afilación ' +  codAfiliado + '. Utilice la ayuda para buscar.');
                }

                $Container.find('.TableAfiliado').html('');
            }
            else {
                //Error
            }

            return res.d;
        },
        error: (page.Client() ? Error : DevError)
    }).done(function () {
    });
}


function wsBuscarProductoAyuda($Container, Producto, Plan, Descripcion) {
    var parametros = {
        CodProducto: Producto, 
        CodPlan: Plan, 
        Busqueda: Descripcion
    };

    $Container.find('.TableClaveAyuda').html('');
    $.ajax({
        url: "../DllRecetario/ws_General.aspx/getProducto",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parametros),
        success: function Ready(res) {
            if (res.d != '') {
                $Container.find('.TableClaveAyuda').html(res.d);
                var oTable = grid.initTable($Container, $Container.find('#TableProducto'), true, false, '150%', $Container.find('.TableClaveAyuda').height() - 40);
                //forms.initAyudaAfiliado();
                //forms.initbtnAyudaAfiliado(oTable);
                forms.initAyudaProducto();
                forms.initbtnAyudaProducto(oTable);
            }
            else {
                //Error
            }

            return res.d;
        },
        error: (page.Client() ? Error : DevError)
    }).done(function () {
    });
}

function wsBusquedaDiagnosticoAyuda($Container, $Parent, Descripcion) {
    var parametros = {
        sDescripcion: Descripcion
    };

    $Container.find('.TableClaveCie').html('');
    $.ajax({
        url: "../DllRecetario/ws_General.aspx/getCIE",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parametros),
        success: function Ready(res) {
            if (res.d != '') {
                $Container.find('.TableClaveCie').html(res.d);
                var oTable = grid.initTable($Container, $Container.find('#TableDiagnostico'), true, false, '100%', $Container.find('.TableClaveCie').height() - 40);
                forms.initAyudaDiagnosticos($Parent);
                forms.initbtnAyudaDiagnosticos(oTable, $Parent);
            }
            else {
                //Error
            }

            //return res.d;
        },
        error: (page.Client() ? Error : DevError)
    }).done(function () {
    });
}

function wsBusquedaProcedimientosAyuda($Container, $Parent, Producto, Descripcion) {
    var parametros = {
        CodProducto: Producto,
        Busqueda: Descripcion
    };

    $Container.find('.TableProcedimientos1').html('');
    $.ajax({
        url: "../DllRecetario/ws_General.aspx/getProcedimientos",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parametros),
        success: function Ready(res) {
            if (res.d != '') {
                $Container.find('.TableProcedimientos1').html(res.d);
                //var oTable = grid.initTable($Container, $Container.find('#TableProcedimientos'), true, false, '100%', $Container.find('.TableProcedimientos1').height() - 40);
                var oTable = grid.initTable($Container, $Container.find('#TableProcedimientos'), true, false, '680px', $Container.find('.TableProcedimientos1').height() - 40);
                forms.initAyudaProcedimientos($Parent);
                forms.initbtnAyudaProcedimientos(oTable, $Parent);
            }
            else {
                //Error
            }

            //return res.d;
        },
        error: (page.Client() ? Error : DevError)
    }).done(function () {
    });
}

function wsBusquedaLabGabAyuda($Container, Producto, Descripcion, Tipo) {
    var parametros = {
        CodProducto: Producto,
        Busqueda: Descripcion,
        TipoBusqueda: parseInt(Tipo)
    };
    $Container.find('.TableLab1').html('');

    $.ajax({
        url: "../DllRecetario/ws_General.aspx/getLabGab",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(parametros),
        success: function Ready(res) {
            if (res.d != '') {
                $Container.find('.TableLab1').html(res.d);
                var oTable = grid.initTable($Container, $Container.find('#TableLabGab'), true, false, '100%', $Container.find('.TableLab1').height() - 40);
                //forms.initAyudaLabGab();
                //forms.initbtnAyudaLabGab(oTable);
            }
            else {
                //Error
            }

            //return res.d;
        },
        error: (page.Client() ? Error : DevError)
    }).done(function () {
    });
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