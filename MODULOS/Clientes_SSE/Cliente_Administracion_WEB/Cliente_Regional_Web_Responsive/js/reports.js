var reports = (function () {

    function init() {
    }

    function getNameReport(Frame) {
        var sNameReport = '';

        switch (Frame) {
            case '36':
                sNameReport = 'RPT__001__Existencias_vs_Maximos_y_Minimos';
                break;
            case '37':
                sNameReport = 'RPT__002__Existencias_Detallado';
                break;
            case '38':
                sNameReport = 'RPT__003__Entradas_y_Salidas';
                break;
            case '39':
                sNameReport = 'RPT__004__Caducidades_De_Insumos';
                break;
            case '40':
                sNameReport = 'RPT__005__BeneficiariosAtendidos';
                break;
            case '41':
                sNameReport = 'RPT__007__Claves_Nulo_Movimiento';
                break;
            case '42':
                sNameReport = 'RPT__006__Beneficiario_Atendido__Detalle';
                break;
            case '43':
                //sNameReport = 'RPT__008__Claves_NoSuministradas__Stock';
                sNameReport = 'RPT__008__Claves_Suministradas__Stock';
                break;
            case '44':
                //sNameReport = 'RPT__009__Claves_NoSuministradas__Farmacia';
                sNameReport = 'RPT__009__Claves_Suministradas__Farmacia';
                break;
            case '45':
                sNameReport = 'RPT__010__Vales_Emitidos';
                break;
            case '46':
                sNameReport = 'RPT__012__Vales_Emitidos_y_Surtidos';
                break;
            case '47':
                sNameReport = 'RPT__013__Antibioticos';
                break;
            case '48':
                sNameReport = 'RPT__014__Estupefacientes_Psicotropicos';
                break;
            case '49':
                sNameReport = 'RPT__015__Medicamentos_Prescritos_x_Medico';
                break;
            case '50':
                sNameReport = 'RPT__016__Diagosticos_y_Padecimientos';
                break;
            case '51':
                sNameReport = 'RPT__017__Validacion__Remisiones';
                break;
            case '53':
                sNameReport = 'RPT__018__Graficas';
                break;
            case '54':
                sNameReport = 'UNI_RPT__001__ClavesSSA__Unidosis';
                break;
            case '55':
                sNameReport = 'UNI_RPT__002__Existencias_vs_Maximos_y_Minimos';
                break;
            case '56':
                sNameReport = 'UNI_RPT__003__Existencias_Detallado';
                break;
            case '57':
                sNameReport = 'UNI_RPT__004__Existencias_Consolidado';
                break;
            case '58':
                sNameReport = 'UNI_RPT__005__Entradas_y_Salidas';
                break;
            case '59':
                sNameReport = 'UNI_RPT__006__Consumos_Dispensacion';
                break;
            case '60':
                sNameReport = 'UNI_RPT__007__BeneficiariosAtendidos';
                break;
            case '61':
                sNameReport = 'UNI_RPT__008__Beneficiario_Atendido__Detalle';
                break;
            case '62':
                sNameReport = 'UNI_RPT__009__Claves_NoSuministradas__Farmacia_Unidosis';
                break;
            case '63':
                sNameReport = 'UNI_RPT__010__Antibioticos';
                break;
            case '64':
                sNameReport = 'UNI_RPT__011__Estupefacientes_Psicotropicos';
                break;
            case '65':
                sNameReport = 'UNI_RPT__012__Medicamentos_Prescritos_x_Medico';
                break;
            case '66':
                sNameReport = 'UNI_RPT__013__Diagosticos_y_Padecimientos';
                break;
            case '67':
                sNameReport = 'UNI_RPT__014__Validacion__Remisiones';
                break;
            case '68':
                sNameReport = 'UNI_RPT__016__Graficas';
                break;
            case '74':
                sNameReport = 'BI_RPT__020_01__PorcentajeDeAbasto';
                break;
            case '75':
                sNameReport = 'BI_RPT__020_03__Claves__Costos_y_Consumos';
                break;
            case '76':
                sNameReport = 'BI_RPT__020_05__PolizasSP_AtendidasEnUrgencias';
                break;
            case '77':
                sNameReport = 'BI_RPT__020_06__Dispensacion_GrupoTerapeutico';
                break;
            case '78':
                sNameReport = 'BI_RPT__020_09__Costo_x_Unidad';
                break;
        }

        return sNameReport;
    }

    return {
        init: init,
        getNameReport: getNameReport
    };

})();