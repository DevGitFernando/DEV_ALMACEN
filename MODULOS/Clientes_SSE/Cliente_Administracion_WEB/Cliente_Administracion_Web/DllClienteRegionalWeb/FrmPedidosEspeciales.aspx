﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPedidosEspeciales.aspx.cs" Inherits="DllClienteRegionalWeb_FrmPedidosEspeciales" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pedidos especiales</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /> 
	<meta name="viewport" content="width=device-width, initial-scale=1.0" /> 
	<link rel="shortcut icon" href="../images/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="../css/m-styles.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/GeneralForm.css" />
    <link rel="stylesheet" href="../css/south-street/jquery-ui-1.10.1.custom.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.1/jquery-ui.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.datepicker-es.min.js"></script>
</head>
<body>
    <form id="FrmPedidosEspeciales" runat="server">
    <div id="navBarraFrame">
         <span id="New" class="menu-button" title="Nuevo"><i class="icon-file icon-white"></i></span>
         <span id="Exec" class="menu-button" title="Consultar"><i class="icon-play icon-white"></i></span>
         <span id="Print" class="menu-button" title="Imprimir"><i class="icon-print icon-white"></i></span>
         <%--<span id="Exportar" class="menu-button" title="Exportar"><i class="icon-share icon-white"></i></span>--%>
    </div>
    <div id="container" class="container">
        <div class="Controls">
        </div>  
        <div class="Results">
        </div>
    </div>
    </form>
</body>
</html>