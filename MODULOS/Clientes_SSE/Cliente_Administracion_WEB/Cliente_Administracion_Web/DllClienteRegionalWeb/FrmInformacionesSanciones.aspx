<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmInformacionesSanciones.aspx.cs" Inherits="DllClienteRegionalWeb_FrmInformacionesSanciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sanciones</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" /> 
	<link rel="shortcut icon" href="../images/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="../css/m-styles.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/GeneralForm.css" />
    <link rel="stylesheet" href="../css/south-street/jquery-ui-1.10.1.custom.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.datepicker-es.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional[""]);
            $("#dtpPeriodo").datepicker($.datepicker.regional["es"]);
            $("#dtpPeriodo").datepicker("option", "dateFormat", "yy-mm-dd");
            $("#dtpPeriodo").datepicker("option", "changeMonth", "true");
            $("#dtpPeriodo").datepicker("option", "changeYear", "true");
            $("#dtpPeriodo").datepicker("option", "maxDate", "d");
            $("#dtpPeriodo").datepicker("option", "hideIfNoPrevNext", "true");

            $("#dtpPeriodo").keydown(function (e) {
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="FrmInformacionesSanciones" runat="server">
    <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>
         <%--<span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>--%>
    </div>
    <div id="container" class="container">
        <div class="Controls">
            <div id="ComboJ" class="Combo Unique m-input-prepend" runat="server">
            </div>
            <div class="Textbox Unique m-input-prepend">
                <label class="m-wrap">
                    <span class="add-on">Periodo :</span>
                    <input type="text" class="m-wrap" id="dtpPeriodo" value="" placeholder="Seleccione periodo" />
                </label>
            </div>
            
        </div>
        <div class="Results">
        </div>
    </div>
    </form>
</body>
</html>