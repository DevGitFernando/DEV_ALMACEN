var general = (function () {

    function jsontoDate(dateJson, opc) {
        dateJson = new Date(parseInt(dateJson.substr(6)));
        var sDate = '';
        var months = new Array('Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre');
        var days = new Array('Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado');

        var date = dateJson.getDate();
        var year = dateJson.getFullYear();
        var month = dateJson.getMonth() + 1;
        var day = dateJson.getDay();

        var hour = dateJson.getHours();
        var minute = dateJson.getMinutes();
        var second = dateJson.getSeconds();

        hour = (hour > 12) ? hour - 12 : hour;
        hour = (hour < 10) ? '0' + hour : hour;
        minute = (minute < 10) ? '0' + minute : minute;
        minute += (dateJson.getHours() < 12) ? ' a. m.' : ' p. m.';
        second = (second < 10) ? '0' + second : second;
        //second += (dateJson.getHours() < 12) ? ' am' : ' pm';

        date = (date < 10) ? '0' + date : date;
        month = (month < 10) ? '0' + month : month;


        switch (opc) {
            case 'LargeDate':
                sDate = '' + days[day] + ', ' + date + ' de ' + months[month - 1] + ' de ' + year;
                break;
            case 'DMY':
                sDate = ' ' + date + '-' + month + '-' + year;
                break;
            case 'YMD':
                sDate = ' ' + year + '-' + month + '-' + date ;
                break;
            case 'H:M':
                sDate = ' ' + hour + ':' + minute ;
                break;
            default:

        }

        return sDate;
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

    function init() {
    }

    return {
        init: init,
        jsontoDate: jsontoDate,
        showToastMsj: showToastMsj,
        clearToastMsj: clearToastMsj
    };

})();