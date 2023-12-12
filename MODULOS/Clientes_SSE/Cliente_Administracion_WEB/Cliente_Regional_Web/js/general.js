var General = {
    init: init,
    showToastMsj: showToastMsj,
    clearToastMsj: clearToastMsj,
    jsontoDate: jsontoDate,
    buildHtmlTable: buildHtmlTable,
    fnPonCeros: fnPonCeros,
    fnmakeString: fnmakeString
}

function init()
{
}

/*Msj toast*/
function showToastMsj(sText, bSticky, sType, iTime, sPosition) {
    //clearToastMsj();
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

function jsontoDate(dateJson, opc) {
    console.log(dateJson);
    var sDate = '';
    if (dateJson != null) {
        dateJson = new Date(parseInt(dateJson.substr(6)));
        console.log(dateJson);
        var months = new Array('Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre');
        var days = new Array('Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado');

        var date = dateJson.getDate();
        var year = dateJson.getFullYear();
        var month = dateJson.getMonth() + 1;
        var day = dateJson.getDay();

        var hour = dateJson.getHours();
        var minute = dateJson.getMinutes();
        var second = dateJson.getSeconds();

        if (opc != 'YMDHM24') {
            hour = (hour > 12) ? hour - 12 : hour;
            hour = (hour < 10) ? '0' + hour : hour;
            minute = (minute < 10) ? '0' + minute : minute;
            //minute += (dateJson.getHours() < 12) ? ' AM' : ' PM';
            minute += (dateJson.getHours() < 12) ? ' a. m.' : ' p. m.';
            second = (second < 10) ? '0' + second : second;
            //second += (dateJson.getHours() < 12) ? ' am' : ' pm';

            date = (date < 10) ? '0' + date : date;
            month = (month < 10) ? '0' + month : month;
        }
        else {
            hour = (hour < 10) ? '0' + hour : hour;
            minute = (minute < 10) ? '0' + minute : minute;
            second = (second < 10) ? '0' + second : second;

            date = (date < 10) ? '0' + date : date;
            month = (month < 10) ? '0' + month : month;
        }

        switch (opc) {
            case 'LargeDate':
                sDate = days[day] + ', ' + date + ' de ' + months[month - 1] + ' de ' + year;
                break;
            case 'DMY':
                sDate = date + '-' + month + '-' + year;
                break;
            case 'YMD':
                sDate = year + '-' + month + '-' + date;
                break;
            case 'HM':
                sDate = hour + ':' + minute;
                break;
            case 'YMDHM':
                sDate = year + '-' + month + '-' + date + ' ' + hour + ':' + minute;
                break;
            case 'YMDHM24':
                sDate = year + '-' + month + '-' + date + ' ' + hour + ':' + minute;
                break;
            default:

        }
    }

    return sDate;
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

/* Generar cadenas aleatorias*/
function fnmakeString(iTam) {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (var i = 0; i < iTam; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));

    return text;
}

function clearToastMsj() {
    toastr.options = {};
    toastr.clear();
}