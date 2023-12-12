// JavaScript Document
var oDataTable = null;

var grid = {
    init: function () {
    },
    initTable: function ($document, $table, bSelect, bDelete, width, height) {
        //oDataTable = null
        oDataTable = $table.DataTable({
            dom: 't',
            language: { 'url': '../assets/DataTables/i18n/Spanish.json' },
            scrollX: width,
            scrollY: height,
            scrollCollapse: false,
            paging: false,
            ordering: false,
            searching: false,
            destroy: true,
            autoWidth: false
        });

        if (bSelect) {
            oDataTable.on('click', 'tbody tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    oDataTable.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });

            //$('#example tbody').on('click', 'tr', function () {
            /*oDataTable.$('tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    oDataTable.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });*/
        }

        if (bDelete) {
            $document.on('keydown', function (event) {
                switch (event.keyCode) {
                    case 46: // Delete
                        if (typeof (oDataTable) !== 'undefined') {
                            if (oDataTable.$('tr.selected') != []) {
                                oDataTable.row('.selected').remove().draw(false);
                            }
                        }
                        break;
                }
            });
        }

        return oDataTable;
    }
};

function initEditable() {
    var heightText = 32;
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

            initEditable();
        }

        searchNextEdit(oNextRow, value);

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
}